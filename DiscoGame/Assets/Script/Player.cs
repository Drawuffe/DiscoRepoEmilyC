using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //distance of raycast for you to start conversation
    [SerializeField] float talkDistance = 2;
    bool inConversation;

    //movement
    private CharacterController controller;
    private float verticalVelocity;
    public float playerSpeed = 2.0f;
    
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    void Update()
    {
        //start the interaction to begin the dialogue
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Interact();
            
        }

        bool groundedPlayer = controller.isGrounded;


        // character movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // scale by speed
        move *= playerSpeed;

       
        controller.Move(move * Time.deltaTime);
    }

    void Interact()
    {
        if (inConversation)
        {
            //skips the dialogue if you are in a conversation already
            DialogueBoxControllerMulti.instance.SkipLine();
        }
        else
        {
            //starts the conversation if you are in the range to begin conversation
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out NPC npc))
                {
                    Debug.Log("begin chat");
                    
                    DialogueBoxControllerMulti.instance.StartDialogue(NPC.dialogueAsset_.dialogue, npc.StartPosition, npc.npcName);
                }
            }
        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        DialogueBoxControllerMulti.OnDialogueStarted += JoinConversation;
        DialogueBoxControllerMulti.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        DialogueBoxControllerMulti.OnDialogueStarted -= JoinConversation;
        DialogueBoxControllerMulti.OnDialogueEnded -= LeaveConversation;
    }
}

