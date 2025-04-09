using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{
    [TextArea]
    public string[] dialogue;

    //text features
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    //turning off the box
    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        dialogueText.text = dialogue;
        dialoguePanel.SetActive(true);
    }

    //turning off the box
    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null; ;
        dialoguePanel.SetActive(false);
    }

    float charactersPerSecond = 90;

    //type affect to show all the letters individually, like a typewriter
    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }
}


