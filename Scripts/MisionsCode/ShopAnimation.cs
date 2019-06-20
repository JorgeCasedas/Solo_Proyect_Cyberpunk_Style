using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopAnimation : MonoBehaviour
{
    public Transform petInstantiatePos;
    public GameObject petPrefab;
    public BoxCollider2D animTrigger;
    public BoxCollider2D choiceTrigger;
    public short textsNumber; //number of dialogues before starting animation
    public string[] stopMessages;

    Animator anim;
    Interaction interaction;
    short counter;
    short stopMessagesCounter;
    GameObject character;
    GameObject dialogueBox; //TEMPORAL
    bool makingChoice;  //TEMPORAL
    Text dialogue;  //TEMPORAL
    GameObject refereces;
    PlayerInteractionSystem pISys;

    void Start()
    {
        anim = GetComponent<Animator>();
        counter = 0;
        character = GameObject.Find("Character");
        dialogueBox = GameObject.Find("References").GetComponent<ReferencesScript>().dialogueBox; //TEMPORAL
        dialogue = dialogueBox.transform.GetChild(0).gameObject.GetComponent<Text>(); //TEMPORAL 
        refereces = GameObject.Find("References");
        pISys =character.GetComponent<PlayerInteractionSystem>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (makingChoice) {
                stopMessagesCounter++;
                if (stopMessagesCounter < stopMessages.Length)
                    OpenDialogue(stopMessages[stopMessagesCounter]);
                else
                    anim.SetBool("good", true);
            }
            else if (interaction) {
                counter++;
                if (counter > textsNumber)
                    StartAnim();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (animTrigger) {
            if (collision.gameObject.name == "Character") {
                interaction = GetComponent<Interaction>();
            }
        }
        else if(choiceTrigger) {
            StopCountDown(); //TEMPORAL
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Character") {
            counter = 0;
            interaction = null;
        }
    }

    public void StartAnim() {
        anim.SetBool("start",true);
        Destroy(animTrigger);
    }
    public void StartCountDown() {
        StartCoroutine(CountingDown());
    }
    public void StopCountDown() {
        character.GetComponent<PlayerMovement>().Lock();
        pISys.Lock();
        StopAllCoroutines();
        makingChoice = true;
        OpenDialogue(stopMessages[0]);
       
    }
    public void GivePet() {
        refereces.GetComponent<ReferencesScript>().pet = Instantiate(petPrefab, petInstantiatePos.position, petInstantiatePos.rotation);
        dialogueBox.SetActive(false);
        character.GetComponent<PlayerMovement>().Unlock();
        pISys.Unlock();
    }
    IEnumerator CountingDown() {
        yield return new WaitForSeconds(5);
        anim.SetBool("bad", true);
    }

    void OpenDialogue(string txt) {
        dialogueBox.SetActive(true);
        if (!dialogue) {
            dialogue = dialogueBox.transform.GetChild(0).gameObject.GetComponent<Text>();
        }
        dialogue.text = txt;
    }
    void EndEvent(){
        character.GetComponent<PlayerMovement>().Unlock();
        Destroy(choiceTrigger);
        Destroy(animTrigger);
        Destroy(this);
    }
}
