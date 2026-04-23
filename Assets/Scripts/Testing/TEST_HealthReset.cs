using UnityEngine;
using UnityEngine.InputSystem;

public class TEST_HealthReset : MonoBehaviour
{

    InputAction resetAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetAction = InputSystem.actions.FindAction("TEST_ResetHealth");
    }

    // Update is called once per frame
    void Update()
    {
        if (resetAction.WasPerformedThisFrame())
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerHealth>().ResetHealth();
        }
    }
}
