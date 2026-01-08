using UnityEngine;

public class EnemyBillboardController : MonoBehaviour
{

    //The main camera, for getting player view angle
    Transform cam;
    

    //Head of the enemy
    public Transform head;

    //Body of the enemy
    public Transform body;

    [Tooltip("When tilting; what percent of the tilt happens in the body versus the legs")]
    [Range(0, 1)]
    public float bodyTiltPercent;

    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraRot = cam.transform.eulerAngles;

        Vector3 newRotation = cam.transform.eulerAngles;

        float totalRot = newRotation.x;

        head.localRotation = Quaternion.Euler(totalRot * (1- bodyTiltPercent), 0, 0);
        body.localRotation = Quaternion.Euler(totalRot * bodyTiltPercent, 0, 0);
        
    }
}

