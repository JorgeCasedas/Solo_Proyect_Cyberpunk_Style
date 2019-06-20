using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInteractionSystem : MonoBehaviour
{
    ReferencesScript reference;
    public GameObject interactionObject;
    GameObject dialogueBox;
    GameObject handSlot;
    GameObject hand;
    Animator anim;
    bool blocked;

    public GameObject[] slots;
    public GameObject dropPrefab;

    // Start is called before the first frame update
    void Start()
    {
        reference = GameObject.Find("References").GetComponent<ReferencesScript>();
        dialogueBox = reference.dialogueBox;
        handSlot = reference.handSlot;
        hand = GameObject.Find("HandPos");
        anim =transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!blocked) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (interactionObject) {
                    Debug.Log("Talk");
                    interactionObject.GetComponent<Interaction>().InteractionEvent();
                }
            }
        }           
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Interact") {
            interactionObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Interact") {
            if (collision.gameObject.GetComponent<Interaction>().interactionType == Interaction.InteractionType.Dialogue)
                dialogueBox.SetActive(false);

            interactionObject.GetComponent<Interaction>().Deactivate();
            interactionObject = null;
        }
    }
    public void ObjectGrabbed(Sprite sp) {
        if (handSlot.GetComponent<Image>().sprite) { //There is already an object in the hand
            int i = CheckSlots();
            if (i >= 0) {
                slots[i].GetComponent<Image>().sprite = handSlot.GetComponent<Image>().sprite;
            }
            else { //Instantiate in DropPos the Object in Hand;
                GameObject drop = Instantiate(dropPrefab, transform.GetChild(0).GetChild(2).transform.position,Quaternion.Euler(Vector3.zero));
                drop.GetComponent<SpriteRenderer>().sprite = handSlot.GetComponent<Image>().sprite;
            }
        }
        handSlot.GetComponent<Image>().sprite = sp;
    }
    public int CheckSlots() {
        int i = 0;
         foreach(GameObject slot in slots) {
            if (!slot.GetComponent<Image>().sprite) {
                return i; //There is an empty slot and returns its index
            }
            i++;
        }
        return -1; //There is no empty slot
    }
    public void MoveItemToBackpack() {
        int i = CheckSlots();
        if (i >= 0) {
            slots[i].GetComponent<Image>().sprite = handSlot.GetComponent<Image>().sprite;
        }
        handSlot.GetComponent<Image>().sprite = null;
    }
    public void Lock() {
        blocked = true;
    }
    public void Unlock() {
        blocked = false;
    }

    public void ChangeObject(Sprite sp, GameObject handSlot) {
        if(handSlot.GetComponent<Image>().sprite)
            anim.SetBool("FullHand", true);
        else
            anim.SetBool("FullHand", false);

        hand.GetComponent<SpriteRenderer>().sprite = sp;

        if (hand.GetComponent<SpriteRenderer>().sprite)
            anim.SetBool("FullHand", true);
        else
            anim.SetBool("FullHand", false);
    }
    public void UseHandObject() {
        anim.SetBool("FullHand", false);
        hand.GetComponent<SpriteRenderer>().sprite = null;
    }

}
