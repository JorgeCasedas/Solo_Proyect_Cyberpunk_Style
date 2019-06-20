using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    public Image img;
    GameObject useButton;
    GameObject changeButton;
    public enum ObjectType {Empty, Potato, WaterBottle}
    public ObjectType objectType;
    string empty = "Empty";
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        objectType = ObjectType.Empty;
        useButton = transform.GetChild(0).gameObject;
        changeButton = transform.GetChild(1).gameObject;
        useButton.GetComponent<ButtonBehaviour>().slot = gameObject;
        changeButton.GetComponent<ButtonBehaviour>().slot = gameObject;
        ChangeType();
    }

    // Update is called once per frame
    void Update()
    {
        if (!img.sprite) {
            img.color = new Color(1, 1, 1, 0);
        }
           
        else 
        {
            img.color = new Color(1, 1, 1, 1);
            if (objectType == ObjectType.Empty)
                ChangeType();
        }
    }
    public void ChangeType() {
        if (!img.sprite)
            objectType = ObjectType.Empty;
        else if (img.sprite.name == "Potato")
            objectType = ObjectType.Potato;
        else if (img.sprite.name == "WaterBottle")
            objectType = ObjectType.WaterBottle;
        if(objectType == ObjectType.Empty) {
            changeButton.SetActive(false);
            useButton.SetActive(false);
        }
        else {
            changeButton.SetActive(true);
            useButton.SetActive(true);
        }
    }
}
