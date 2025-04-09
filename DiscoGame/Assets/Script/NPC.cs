using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool firstInteraction = true;
    [SerializeField] int repeatStartPosition;

    public string npcName;
    public static DialogueAsset dialogueAsset_;

    [HideInInspector]
    public int StartPosition
    {
        get
        {
            //counts the conversation number, you can have multiple different conversations with the same NPC in a certain order, the number corisponds to the number of the conversation
            if (firstInteraction)
            {
                firstInteraction = false;
                return 0;
            }
            else
            {
                return repeatStartPosition;
            }
        }
    }
}
