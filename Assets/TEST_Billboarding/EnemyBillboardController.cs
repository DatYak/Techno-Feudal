using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBillboardController : MonoBehaviour
{

    //The main camera, for getting player view angle
    Transform cam;
    public Transform root;

    [Header("Body Billboarding")]
    public Transform horizontalBillboard;
    //Head of the enemy
    public Transform head;
    //Body of the enemy
    public Transform body;


    [Tooltip("Character height in unity units")]
    public float characterHeight;

    [Tooltip("The angle the character start tilting at")] 
    public float tiltThreshold;


    [Header("Legs and Walking")]
    public Transform leg_R;
    public Transform leg_L;    

    [Tooltip("Distance to take a step")]
    public float fullStepDistance;
    [Tooltip("How high each leg lifts when walking")]
    public float legLift;
    private float legDownPos;
    private float legLiftPos;

    private Vector3 lastPosition;
    bool leadingRightLeg;

    void Start()
    {
        cam = Camera.main.transform;
        legDownPos = leg_R.localPosition.y;
        legLiftPos = legDownPos + legLift;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HorizontalBillboard();

        Vector3 camPos = cam.position;
        Vector3 headPos = root.position + new Vector3 (0, characterHeight, 0);

        float heightDelta = cam.position.y - headPos.y;

        float distance2D = Vector3.Distance(camPos, headPos) - heightDelta;

        float totalRot = Mathf.Atan2(heightDelta, distance2D) * Mathf.Rad2Deg;

        if (totalRot <= tiltThreshold) totalRot = 0;
        else
        {
            totalRot -= tiltThreshold;
        }

        head.localRotation = Quaternion.Euler(totalRot/2, 0, 0);
        body.localRotation = Quaternion.Euler(totalRot/2, 0, 0);

        Walk(Vector3.Distance(root.position, lastPosition));
        lastPosition = root.position;
    }

    public void HorizontalBillboard()
    {
        Vector3 newRotation = cam.transform.eulerAngles;

        newRotation.x = 0;
        newRotation.z = 0;

        horizontalBillboard.eulerAngles = newRotation;

    }

    float stepDistance;
    public void Walk(float distance)
    {
        stepDistance += distance;
    
        if (stepDistance > fullStepDistance)
        {
            stepDistance = 0;
            leadingRightLeg = !leadingRightLeg;
        }

        float f = Mathf.Lerp (0, Mathf.PI * 2, stepDistance / fullStepDistance);

        float pos = Mathf.Cos(1 + f);

        //Convert -1 to 1 of Cos into 0 to 1
        pos = pos * 0.5f + 0.5f;

        float legLift = Mathf.Lerp(legDownPos, legLiftPos, pos);


        if (leadingRightLeg)
        {
            Vector3 legPos = new Vector3(leg_R.localPosition.x, legLift, leg_R.localPosition.z);
            leg_R.localPosition = legPos;
        }
        else
        {
            Vector3 legPos = new Vector3(leg_L.localPosition.x, legLift, leg_L.localPosition.z);
            leg_L.localPosition = legPos;
        }

    }
}