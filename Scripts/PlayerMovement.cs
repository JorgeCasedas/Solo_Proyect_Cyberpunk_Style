using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float resultingSpeed;
    Rigidbody2D rb;
    Animator anim;

    bool a;
    bool d;
    bool w;
    bool s;

    bool blocked;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!blocked) {
            #region "Key pressed track"
            if (Input.GetKeyDown(KeyCode.D)) {
                d = true;
                ProcessSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.D)) {
                d = false;
                ProcessSpeed();
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                a = true;
                ProcessSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.A)) {
                a = false;
                ProcessSpeed();
            }

            if (Input.GetKeyDown(KeyCode.W)) {
                w = true;
                ProcessSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.W)) {
                w = false;
                ProcessSpeed();
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                s = true;
                ProcessSpeed();
            }
            else if (Input.GetKeyUp(KeyCode.S)) {
                s = false;
                ProcessSpeed();
            }
            #endregion
            #region "Facing"
            if (a && d) {
                //Stay the same
            }
            else if (a)
                transform.localScale = new Vector3(-1, 1, 1); //They size of the pixel art Character will never change so I can use numbers instead of variables
            else if (d)
                transform.localScale = new Vector3(1, 1, 1);
            #endregion
        }
    }
    private void FixedUpdate() {
        #region "Speed control"
        if (a || d || w || s) {
            if (a && d)
                rb.velocity = new Vector2(0, rb.velocity.y);
            else if (a)
                rb.velocity = new Vector2(-resultingSpeed, rb.velocity.y);
            else if (d)
                rb.velocity = new Vector2(resultingSpeed, rb.velocity.y);

            if (w && s) 
                rb.velocity = new Vector2(rb.velocity.x, 0 );            
            else if (w)
                rb.velocity = new Vector2(rb.velocity.x, resultingSpeed);
            else if (s)
                rb.velocity = new Vector2(rb.velocity.x, -resultingSpeed);

            anim.SetBool("Walk", true);
        }
        else {
            rb.velocity = Vector2.zero;

            anim.SetBool("Walk", false);
        }
        #endregion
    }
    void ProcessSpeed() //Used so when walking diagonal the speed doesn't increase
    {
        if ((a || d) && (w || s))
            resultingSpeed = speed * Mathf.Sqrt(0.5f);
        else
            resultingSpeed = speed;
    }
    public void Lock() {
        blocked = true;
        a = false;
        d = false;
        w = false;
        s = false;
    }
    public void Unlock() {
        blocked = false;
    }
}
