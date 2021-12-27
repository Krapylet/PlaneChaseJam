using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleDiceTray : MonoBehaviour
{
    // Start is called before the first frame update


    // 1000/1000 = 5.75/5.75

    // Når forholdet mellem højde og brede ændre sig i pixels skal det tilsværende ændres i units.

    // heigh of 660 = scale of 5.77f
    // width of 930 = scale of 8.135f

    // height of 380 = scale of 5.735f
    // width of 930 = scale of 14f
    static float basePixelsToUnitScale = 5.75f / 1000f;

    //static float pixelsToUnitScale = 14f / 930f; // = 0.015f

    public RectTransform canvasRectTransform;

    //Die box transforms
    public Transform back;
    public Transform top;
    public Transform bottom;
    public Transform left;
    public Transform right;

    static float dieBoxThickness = 0.1f;
    static float cameraDist = -5f;

    void Update()
    {
        //Calculate how the die box sides should scale from the resolution.
        // canvasRectTransform.localScale.x
        //Vector3 dieBoxScale = Vector3.Scale(new Vector3(horizontalIn, dieBoxThickness, verticalIn), new Vector3(canvasRectTransform.rect.width / 1200f, canvasRectTransform.rect.height / 600f, 1.0f));

        Vector3 dieBoxScale = Vector3.Scale(new Vector3(canvasRectTransform.rect.width, 0, canvasRectTransform.rect.height), canvasRectTransform.localScale);
        //Apply the scale
        back.localScale = new Vector3(dieBoxScale.x, dieBoxThickness, dieBoxScale.z); ;
        bottom.localScale = top.localScale = new Vector3(dieBoxScale.x, cameraDist, dieBoxThickness);
        left.localScale = right.localScale = new Vector3(dieBoxThickness, cameraDist, dieBoxScale.z);

        //Move the sides to fit the back
        
        float horizontalOffset = dieBoxScale.x / 2 + dieBoxThickness / 2;
        float verticalOffset = dieBoxScale.z / 2 + dieBoxThickness / 2;

        bottom.position = new Vector3(0, cameraDist/2, -verticalOffset);
        top.position = new Vector3(0, cameraDist/2, verticalOffset);
        left.position = new Vector3(-horizontalOffset, cameraDist/2, 0);
        right.position = new Vector3(horizontalOffset, cameraDist/2, 0);
    }

}
