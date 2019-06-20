using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject tarjet;
    public Vector2 downLeft;
    public Vector2 upRight;
    float x;
    float y;
    float z;

    void Start() {
        z = transform.position.z;
    }

    void Update() {

        if (tarjet.transform.position.y < downLeft.y) {
            y = downLeft.y;
        }
        else if (tarjet.transform.position.y > upRight.y) {
            y = upRight.y;
        }
        else {
            y = tarjet.transform.position.y;
        }

        if (tarjet.transform.position.x < downLeft.x) {
            x = downLeft.x;
        }
        else if (tarjet.transform.position.x > upRight.x) {
            x = upRight.x;
        }
        else {
            x = tarjet.transform.position.x;
        }

        gameObject.transform.position = new Vector3(x, y, z);
    }
}
