using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;
using TMPro;

public class DialogueBoxControllerMulti : MonoBehaviour
{
    public static DialogueBoxControllerMulti instance;

    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject answerBox;
    [SerializeField] Button[] answerObjects;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;

    bool skipLineTriggered;
    bool answerTriggered;
    int answerIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartDialogue(DialogueTree dialogueTree, int startSection, string name)
    {

        //turns on the text boxes when the dialogue is started
        Debug.Log("Dialogue started");
        ResetBox();
        nameText.text = name + "...";
        dialogueBox.SetActive(true);
        OnDialogueStarted?.Invoke();
        //runs the chain of dialogue scriptable object
        StartCoroutine(RunDialogue(dialogueTree, startSection));
    }

    IEnumerator RunDialogue(DialogueTree dialogueTree, int section)
    {
        for (int i = 0; i < dialogueTree.sections[section].dialogue.Length; i++)
        {
            //formating the dialogue in the textbox
            dialogueText.text = dialogueTree.sections[section].dialogue[i];
            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
        }

        if (dialogueTree.sections[section].endAfterDialogue)
        {
            //when dialogue is complete, turn off the textbox
            OnDialogueEnded?.Invoke();
            dialogueBox.SetActive(false);
            yield break;
        }

        dialogueText.text = dialogueTree.sections[section].branchPoint.question;
        ShowAnswers(dialogueTree.sections[section].branchPoint);

        //if the choice not picked deactivate the object
        while (answerTriggered == false)
        {
            yield return null;
        }
        answerBox.SetActive(false);
        answerTriggered = false;

        StartCoroutine(RunDialogue(dialogueTree, dialogueTree.sections[section].branchPoint.answers[answerIndex].nextElement));
    }

    void ResetBox()
    {
        //reset the prefabs and text choices
        StopAllCoroutines();
        dialogueBox.SetActive(false);
        answerBox.SetActive(false);
        skipLineTriggered = false;
        answerTriggered = false;
    }

    void ShowAnswers(DialogueTree.BranchPoint branchPoint)
    {
        // Reveals the aselectable answers and sets their text values
        answerBox.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            if (i < branchPoint.answers.Length)
            {
                answerObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = branchPoint.answers[i].answerLabel;
                answerObjects[i].gameObject.SetActive(true);
            }
            else {
                answerObjects[i].gameObject.SetActive(false);
            }
        }
    }

    public void SkipLine()
    {
        //fast fowarding throught the text
        skipLineTriggered = true;
    }

    public void AnswerQuestion(int answer)
    {
        //setting the choosen answer to true and startuing that response
        answerIndex = answer;
        answerTriggered = true;
    }

    internal void StartDialogue(string[] dialogue, int startPosition, string npcName)
    {
        throw new NotImplementedException();
    }
}