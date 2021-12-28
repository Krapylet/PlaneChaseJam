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

        upDot = Vector3.Dot(Vector3.up, transform.up);
        downDot = Vector3.Dot(Vector3.down, transform.up);
        rightDot = Vector3.Dot(Vector3.right, transform.up);
        leftDot = Vector3.Dot(Vector3.left, transform.up);
        forwardDot = Vector3.Dot(Vector3.forward, transform.up);
        backDot = Vector3.Dot(Vector3.back, transform.up);

    }

    public float upDot;
    public float downDot;
    public float rightDot;
    public float leftDot;
    public float forwardDot;
    public float backDot;

    private void DetermineDieRoll() {
        //Calculate dot products to up directions.
        upDot = Vector3.Dot(Vector3.up, transform.up);
        downDot = Vector3.Dot(Vector3.down, transform.up);
        rightDot = Vector3.Dot(Vector3.right, transform.up);
        leftDot = Vector3.Dot(Vector3.left, transform.up);
        forwardDot = Vector3.Dot(Vector3.forward, transform.up);
        backDot = Vector3.Dot(Vector3.back, transform.up);

        Debug.Log("Up: " + upDot + ", down: " + downDot + ", right: " + rightDot + ", left: " + leftDot + ", forward: " + forwardDot + ", back: " + backDot);

        //The dot product closest to 1 is the side facing up.
        float currentClosestDot = upDot;
        string currentClosest = "Up"; //Chaos side

        //Compare all the dot products and find the highest.
        if (downDot > currentClosestDot) {
            currentClosestDot = downDot;
            currentClosest = "Down"; 
        }
        if (rightDot > currentClosestDot) {
            currentClosestDot = rightDot;
            currentClosest = "Right";
        }
        if (leftDot > currentClosestDot) {
            currentClosestDot = leftDot;
            currentClosest = "Left";
        }
        if (forwardDot > currentClosestDot) {
            currentClosestDot = forwardDot;
            currentClosest = "Forward"; // Planeswalk side
        }
        if (backDot > currentClosestDot) {
            currentClosest = "Back";
        }

        Debug.Log("Landed on " + currentClosest + " side!");
    }
}
