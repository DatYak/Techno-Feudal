using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBillboardController : MonoBehaviour
{

    //The main camera, for getting player view angle
    Transform cam;
    [SerializeField]
    private Transform root;

    [SerializeField] [Header("Body Billboarding")]
    private Transform horizontalBillboard;
    [SerializeField]
    //Head of the enemy
    private Transform head;
    [SerializeField]
    //Body of the enemy
    private Transform body;


    [SerializeField] [Tooltip("Character height in unity units")]
    private float characterHeight;

    [SerializeField] [Tooltip("The angle the character start tilting at")] 
    private float tiltThreshold;


    [Header("Legs and Walking")]
    [SerializeField]
    private Transform leg_R;
    [SerializeField]
    private Transform leg_L;    

    [SerializeField] [Tooltip("Distance to take a step")]
    private float fullStepDistance;
    [SerializeField] [Tooltip("How high each leg lifts when walking")]
    private float legLift;

    [SerializeField]
    private BillboardSpriteGroup[] spriteGroups;


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

    private BillboardSpriteGroup GetSpriteGroupByName(string groupName)
    {
        BillboardSpriteGroup result = null;
        for (int i = 0; i < spriteGroups.Length; i++)
        {
            if (spriteGroups[i].groupName == groupName)
            {
                result = spriteGroups[i];
                break;
            }
        }  

        return result;
    }

    public void NextSprite(string groupName)
    {
        BillboardSpriteGroup brg = GetSpriteGroupByName(groupName);
        if (brg)
        {
            brg.NextSprite();
        }
    }
    
    public void SwapSpriteToIndex(string groupName, int index)
    {
        BillboardSpriteGroup brg = GetSpriteGroupByName(groupName);
        if (brg)
        {
            brg.SwapSpriteToIndex(index);
        }
    }
}