using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDeliverObject : MonoBehaviour
{
    public enum RewardType {None, Money, Object};
    public RewardType type;
    public float rewardMoney;
    public float rewardObject;
    public string ObjToDeliver;
    ReferencesScript reference;
    bool done;

    // Start is called before the first frame update
    void Start()
    {
        reference = GameObject.Find("References").GetComponent<ReferencesScript>();
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (type != RewardType.None && !done /*&& Mission Completed*/) {
        //    done = true;
        //    GiveReward();
        //}
    }

    void GiveReward() {
        if (type == RewardType.Money) {
            reference.character.GetComponent<PlayerManager>().money += rewardMoney;
        }else if (type == RewardType.Object) {
            //Spawn Object In The Floor
        }
    }

    public bool CheckMission() {
        if(done || reference.handSlot.GetComponent<Image>().sprite && reference.handSlot.GetComponent<Image>().sprite.name == ObjToDeliver) {
            if (!done) {
                done = true;
                GiveReward();
                reference.handSlot.GetComponent<Image>().sprite = null;
                reference.handSlot.GetComponent<SlotController>().ChangeType();
                reference.character.GetComponent<PlayerInteractionSystem>().UseHandObject();
            }
            return true;
        }
        return false;
    }

}
