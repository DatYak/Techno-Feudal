using UnityEngine;

public class SimpleTiltToFace : MonoBehaviour
{

    Transform cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newRotation = cam.transform.eulerAngles;

        newRotation.y = 0;
        newRotation.z = 0;

        transform.localRotation = Quaternion.Euler(newRotation);
        
    }
}
