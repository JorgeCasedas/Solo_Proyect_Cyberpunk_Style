using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour
{
    public enum InteractionType {Dialogue, Object};
    public InteractionType interactionType;

    public string[] dialogueText;
    public string rewardText;
    GameObject dialogueBox;
    Text dialogue; 
    GameObject character;
    Animator anim;
    GameObject hand;
    MissionDeliverObject mission;
    short dialogueCount;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("References").GetComponent<ReferencesScript>().dialogueBox;
        dialogue = dialogueBox.transform.GetChild(0).gameObject.GetComponent<Text>();
        character = GameObject.Find("References").GetComponent<ReferencesScript>().character;
        anim = character.transform.GetChild(0).GetComponent<Animator>();
        hand = GameObject.Find("HandPos");
        if(GetComponent<MissionDeliverObject>())
            mission = GetComponent<MissionDeliverObject>();
        active = false;
        dialogueCount = 0;
    }

    public void InteractionEvent() {
        if (interactionType == InteractionType.Object) {
            GrabObject();
        }
        else if(interactionType == InteractionType.Dialogue) {
           if( mission && mission.CheckMission()) //Check if there is a reward before checking if the mission is completed
                OpenDialogue(rewardText);
            else if (dialogueCount<dialogueText.Length) {
                OpenDialogue(dialogueText[dialogueCount]);
                dialogueCount++;
            }              
            active = true;
        }
    } 
    void OpenDialogue(string txt) {
        dialogueBox.SetActive(true);
        if (!dialogue) {
            dialogue = dialogueBox.transform.GetChild(0).gameObject.GetComponent<Text>();
        }
        dialogue.text = txt;
    }
    void GrabObject() {
        anim.SetTrigger("Grab");
        anim.SetBool("FullHand", true);
        hand.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        character.GetComponent<PlayerInteractionSystem>().ObjectGrabbed(GetComponent<SpriteRenderer>().sprite);
        Destroy(gameObject);
    }
    public void Deactivate() {
        active = false;
        dialogueCount = 0;
    }
}
