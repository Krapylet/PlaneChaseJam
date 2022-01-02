using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRoller : MonoBehaviour
{
    public float acceleration;
    public float slowDown;
    public float torquePower;
    public float lift;

    private Rigidbody rb;

    //Flags
    private bool followMouse = false;
    private bool lookForResult = false;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnMouseDown() {
        followMouse = true;

        //Lift die and make it spin
        transform.position = new Vector3(transform.position.x, lift, transform.position.z);
        Vector3 randomTorque = Random.insideUnitSphere;
        rb.AddTorque(randomTorque * torquePower, ForceMode.Impulse);
    }

    private void OnMouseUp() {
        followMouse = false;
        lookForResult = true;

        Vector3 randomTorque = Random.insideUnitSphere;
        rb.AddTorque(randomTorque * torquePower, ForceMode.Impulse);
    }

    public void Update() {

        if (followMouse) {

            //Make the die follow the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, lift));
            Vector3 mouseDirection = mousePos - transform.position;
            rb.velocity = slowDown * (rb.velocity + (mouseDirection * acceleration));

            // make sure the die doesn't fall down beneath the card or fly above the camera.
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(1f, 0f, 1f));
            transform.position = new Vector3(transform.position.x, lift, transform.position.z);
        }

        if (lookForResult) {
            bool noVelocity = rb.velocity == Vector3.zero;
            bool noRotation = rb.angularVelocity == Vector3.zero;
            if (noRotation && noVelocity) {
                Debug.Log("The Die has FALLEN!");
                DetermineDieRoll();
                lookForResult = false;
            }
        }
    }

    public Transform up;
    public Transform down;
    public Transform right;
    public Transform left;
    public Transform forward;
    public Transform back;

    private void DetermineDieRoll() {
        //The dot product closest to 1 is the side facing up.
        string currentClosest = "Up"; //Chaos side
        float currentHighest = up.position.y;

        //Compare all the dot products and find the highest.
        if (down.position.y > currentHighest) {
            currentHighest = down.position.y;
            currentClosest = "Down"; 
        }
        if (right.position.y > currentHighest) {
            currentHighest = right.position.y;
            currentClosest = "Right";
        }
        if (left.position.y > currentHighest) {
            currentHighest = left.position.y;
            currentClosest = "Left";
        }
        if (forward.position.y > currentHighest) {
            currentHighest = forward.position.y;
            currentClosest = "Forward"; 
        }
        if (back.position.y > currentHighest) {
            currentClosest = "Back";// Planeswalk side
        }

        Debug.Log("Up : " + up.position.y + "Down : " + down.position.y + "right : " + right.position.y + "left : " + left.position.y + "forward : " + forward.position.y + "back : " + back.position.y);
        Debug.Log("Landed on " + currentClosest + " side!");
    }
}
