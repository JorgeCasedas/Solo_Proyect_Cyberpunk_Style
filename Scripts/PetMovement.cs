using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour {
    public GameObject target;
    public GameObject pivot;
    public float stopDistance; //StopFollowing
    public float followDistance; //StartFollowing
    public float speed;
    public bool commanded;

    GameObject character;
    Rigidbody2D rb;
    bool follow;
    Vector2 dir;
    Animator anim;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        character = GameObject.Find("Character");
        commanded = false;
        target = character;
        gameObject.name = "Pet";
    }

    // Update is called once per frame
    void Update() {
        dir = target.transform.position - pivot.transform.position;
        if (rb.velocity.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }// I dont want the scale to change when the pet stops
        if (Vector2.Distance(pivot.transform.position, target.transform.position) > followDistance) {
            follow = true;
            anim.SetBool("walk", true);
        }
        else if (Vector2.Distance(pivot.transform.position, target.transform.position) < stopDistance) {   
            follow = false;
            anim.SetBool("walk", false);
            if (commanded) {
                commanded = false;
                PerformAction();
            }
        }

    }
    private void FixedUpdate() {
        if (follow) {
            rb.velocity = (dir).normalized * speed * Time.deltaTime;
        }
    }
    public void ChangeTarget(GameObject gO) {
        target = gO;
    }
    public void PerformAction() {
        anim.SetTrigger("action");
        StartCoroutine(StopAction());
    }
    public IEnumerator StopAction() {
        yield return new WaitForSeconds(5);
        anim.SetTrigger("stopAction");
        target = character;
    }
}
