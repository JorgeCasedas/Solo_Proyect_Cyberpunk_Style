using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCommand : MonoBehaviour
{
    public GameObject targetPrefab;

    GameObject lastTarget;
    GameObject cam;
    GameObject refereces;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("OutdoorCamera");
        refereces = GameObject.Find("References");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            if (lastTarget)
                Destroy(lastTarget);
            lastTarget = Instantiate(targetPrefab,cam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition),Quaternion.Euler( Vector3.zero));
            refereces.GetComponent<ReferencesScript>().pet.GetComponent<PetMovement>().ChangeTarget(lastTarget);
            refereces.GetComponent<ReferencesScript>().pet.GetComponent<PetMovement>().commanded = true;
        }
    }
}
