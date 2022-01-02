using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DieRoller : MonoBehaviour
{

    // Events called corrosponding to dice rolls:
    public Action OnChaosRolled;
    public Action OnPlaneswalkRolled;
    public Action OnBlankRolled;

    // Values for finetuning
    [SerializeField] private float acceleration;
    [SerializeField] private float slowDown;
    [SerializeField] private float torquePower;
    [SerializeField] private float lift;

    private Rigidbody rb;

    //Flags
    private bool shouldFollowCursor = false;
    private bool shouldLookForResult = false;

    // Die sides
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform right;
    [SerializeField] private Transform left;
    [SerializeField] private Transform forward;
    [SerializeField] private Transform back;

    // tap vs drag detection
    [SerializeField] private float tapDurationThreshold;
    [SerializeField] private float tapThrowForce;
    private float tapStart;

    private void Awake() {
        // Initialize shorthands
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Update() {

        MouseControls();
        TouchControls();

        //Check until the dice is laying still and then determine which side is pointing up.
        if (shouldLookForResult) {
            LookForResult();
        }
    }

    // ----------------------- Touch Controls ----------------------
    private void TouchControls() {

        bool noTochinputDetected = Input.touchCount == 0;
        if (noTochinputDetected) return;

        // Get touch input of the first finger on the screen
        Touch touchInput = Input.GetTouch(0);

        //OnTouchDown
        if(touchInput.phase == TouchPhase.Began) {
            bool dieIsPressed = CheckIfTouched(touchInput.position);
            if (dieIsPressed) OnCursorDown();
        }
        //OnTouchUp
        else if (touchInput.phase == TouchPhase.Ended) {
            OnCursorUp();
        }

        if (shouldFollowCursor) {
            FollowCursor(touchInput.position);
        }
    }

    private bool CheckIfTouched(Vector2 touchPos) {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        bool isDieTouched = hit.collider?.tag == "Die";
        return isDieTouched;
    }

    // ---------------------- Mouse controls ------------------------
    private void OnMouseDown() {
        OnCursorDown();
    }

    private void OnMouseUp() {
        OnCursorUp();
    }

    private void MouseControls() {
        if (shouldFollowCursor) {
            FollowCursor(Input.mousePosition);
        }
    }
    // ----------------------------------

    // When mouse or touch input presses down on die.
    private void OnCursorDown() {
        tapStart = Time.time;

        // Set the die to follow the cursor whenever the the mouse is pressed down on the die.
        shouldFollowCursor = true;

        //Lift die and make it spin
        transform.position = new Vector3(transform.position.x, lift, transform.position.z);
        Vector3 randomTorque = UnityEngine.Random.insideUnitSphere;
        rb.AddTorque(randomTorque * torquePower, ForceMode.Impulse);
    }

    // When mouse or touch input resleases the die.
    private void OnCursorUp() {
        // Don't run if we never picked up the die in the first place
        if (!shouldFollowCursor) return;

        // Stop the die from following the cursor when the cursor is released from the die, and then look for result.
        shouldFollowCursor = false;
        shouldLookForResult = true;

        //Make the die spin another random way to prevent predicting how it will roll.
        Vector3 randomTorque = UnityEngine.Random.insideUnitSphere;
        rb.AddTorque(randomTorque * torquePower, ForceMode.Impulse);

        //if die was only tapped for a short while, throw it hard in a random direction
        float tapDuration = Time.time - tapStart;
        if (tapDuration <= tapDurationThreshold) {
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle;
            Vector3 randomForce = new Vector3(randomDirection.x, 0, randomDirection.y) * tapThrowForce;
            rb.AddForce(randomForce);
        }
    }

    private void FollowCursor(Vector2 screenPos) {
        //Make the die follow the mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, lift));
        Vector3 mouseDirection = mousePos - transform.position;
        rb.velocity = slowDown * (rb.velocity + (mouseDirection * acceleration));

        // make sure the die doesn't fall down beneath the card or fly above the camera.
        rb.velocity = Vector3.Scale(rb.velocity, new Vector3(1f, 0f, 1f));
        transform.position = new Vector3(transform.position.x, lift, transform.position.z);
    }

    private void LookForResult() {
        bool noVelocity = rb.velocity == Vector3.zero;
        bool noRotation = rb.angularVelocity == Vector3.zero;
        if (noRotation && noVelocity) {
            DetermineDieRoll();
            shouldLookForResult = false;
        }
    }

    private void DetermineDieRoll() {
        //The dot product closest to 1 is the side facing up.
        Transform currentHighest = up; //Chaos side

        //Compare all the dot products and find the highest.
        if (down.position.y > currentHighest.position.y) {
            currentHighest = down;
        }
        if (right.position.y > currentHighest.position.y) {
            currentHighest = right;
        }
        if (left.position.y > currentHighest.position.y) {
            currentHighest = left;
        }
        if (forward.position.y > currentHighest.position.y) {
            currentHighest = forward; // Planeswalk side
        }
        if (back.position.y > currentHighest.position.y) {
            currentHighest = back;
        }
        Debug.Log("Landed on " + currentHighest.name + " side!");

        // Invoke all the methods listening for the resulting dice roll.
        // Chaos
        if (currentHighest.name == up.name) {
            //Check if action is null before invoking
            OnChaosRolled?.Invoke(); 
            Debug.Log("Calling Chaos methods");
        }
        // Planeswalk
        else if (currentHighest.name == forward.name) {
            OnPlaneswalkRolled?.Invoke();
            Debug.Log("Calling Planeswalk methods");
        }
        // Blank
        else {
            OnBlankRolled?.Invoke(); ;
            Debug.Log("Calling Blank methods");
        }

    }
}
