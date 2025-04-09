using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueTree : ScriptableObject
{
    public DialogueSection[] sections;

    //chat branch of dialogue
    [System.Serializable]
    public struct DialogueSection
    {
        [TextArea]
        public string[] dialogue;
        public bool endAfterDialogue;
        public BranchPoint branchPoint;
    }

    //choice branch of dialogue
    [System.Serializable]
    public struct BranchPoint
    {
        [TextArea]
        public string question;
        public Answer[] answers;
    }

    //answer branch of dialogue
    [System.Serializable]
    public struct Answer
    {
        public string answerLabel;
        public int nextElement;
    }

}