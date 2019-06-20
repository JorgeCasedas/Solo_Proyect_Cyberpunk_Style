using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public float hungerSpeed;
    public float thirstSpeed;

    [HideInInspector]
    public float hunger;
    [HideInInspector]
    public float thirst;
   // [HideInInspector]
    public float money;

    GameObject UI;
    PlayerMovement pMov;
    PlayerInteractionSystem pISys;
    GameObject messageHolder;
    Text textHolder;
    ReferencesScript reference;

    bool[] hungry = new bool[2];
    bool[] thirsty = new bool[2];

    // Start is called before the first frame update
    void Start()
    {
        reference = GameObject.Find("References").GetComponent<ReferencesScript>();
        pMov = GetComponent<PlayerMovement>();
        pISys = GetComponent<PlayerInteractionSystem>();
        messageHolder = reference.dialogueBox;
        textHolder = messageHolder.transform.GetChild(0).GetComponent<Text>();
        UI = reference.UI;

        hunger = 100;
        thirst = 100;
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= hungerSpeed * Time.deltaTime;
        thirst -= thirstSpeed * Time.deltaTime;

        if (!hungry[0] && hunger < 50) {
            ShowMessage("I need to eat something");
            hungry[0] = true;
        }
        else if(!hungry[1] && hunger < 5) {
            ShowMessage("I'm going to die if i dont eat anything soon");
            hungry[1] = true;
        }
        if(!thirsty[0] && thirst < 50) {
            ShowMessage("I'm so thiiiiirstyy");
            thirsty[0] = true;
        }
        else if (!thirsty[1] && thirst < 5) {
            ShowMessage("I'm going to die if i dont drink anything soon");
            thirsty[0] = true;
        }

        if(hunger < 0 || thirst < 0) 
            Die();
        

        if (Input.GetKeyDown(KeyCode.Tab)) {
            OnOffInventory();
        }
    }

    public void Eating() {
        hunger = 100;
        hungry[0] = false;
        hungry[1] = false;
    }

    public void Drinking() {
        thirst = 100;
        thirsty[0] = false;
        thirsty[1] = false;
    }

    void OnOffInventory() {
        if (UI.activeSelf) {
            UI.SetActive(false);
            UnlockCharacter();
        }
        else {
            UI.SetActive(true);
            LockCharacter();
        }
    } 

    void LockCharacter() {
        pMov.Lock();
        pISys.Lock();
    }
    void UnlockCharacter() {
        pMov.Unlock();
        pISys.Unlock();
    }

    public void ShowMessage(string _text) {
        textHolder.text = _text;
        StartCoroutine("PopUp");
    }
    IEnumerator PopUp() {
        pISys.Lock();
        messageHolder.SetActive(true);
        pMov.speed = pMov.speed / 3f;
        yield return new WaitForSeconds(1);
        pMov.speed = pMov.speed * 3f;
        messageHolder.SetActive(false);
        pISys.Unlock();
    }

    public void Die() {
        Debug.Log("U r Dead");
    }
}
