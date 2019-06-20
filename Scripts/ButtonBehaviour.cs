using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonType {Grab,Pack,Use}
    public ButtonType type;
    [HideInInspector]
    public GameObject slot;
    GameObject character;
    GameObject handSlot;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("References").GetComponent<ReferencesScript>().character;
        handSlot = GameObject.Find("References").GetComponent<ReferencesScript>().handSlot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Effect() {
        if(type == ButtonType.Grab) {
            if (slot.GetComponent<Image>().sprite) {
                Sprite tempSp = handSlot.GetComponent<Image>().sprite;
                handSlot.GetComponent<Image>().sprite = slot.GetComponent<Image>().sprite;
                slot.GetComponent<Image>().sprite = tempSp;
                slot.GetComponent<SlotController>().ChangeType();
                handSlot.GetComponent<SlotController>().ChangeType();
            }
            character.GetComponent<PlayerInteractionSystem>().ChangeObject(handSlot.GetComponent<Image>().sprite,handSlot);
        }
        else if (type == ButtonType.Use) {
            if(slot.GetComponent<SlotController>().objectType == SlotController.ObjectType.Potato) 
                character.GetComponent<PlayerManager>().Eating();
            else if (slot.GetComponent<SlotController>().objectType == SlotController.ObjectType.WaterBottle) 
                character.GetComponent<PlayerManager>().Drinking();
            slot.GetComponent<Image>().sprite = null;
            slot.GetComponent<SlotController>().ChangeType();
            if(slot.name== "HandSlot") {
                character.GetComponent<PlayerInteractionSystem>().UseHandObject();
            }
        }
        else if (type == ButtonType.Pack) {
            if (slot.GetComponent<Image>().sprite) {
                character.GetComponent<PlayerInteractionSystem>().MoveItemToBackpack();
                slot.GetComponent<SlotController>().ChangeType();
            }
            character.GetComponent<PlayerInteractionSystem>().ChangeObject(handSlot.GetComponent<Image>().sprite, handSlot);
        }
        
    }
}
