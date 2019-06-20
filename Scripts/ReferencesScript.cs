using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencesScript : MonoBehaviour
{

    public GameObject dialogueBox;
    public GameObject UI;
    public GameObject character;
    public GameObject handSlot;
    public GameObject pet;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
