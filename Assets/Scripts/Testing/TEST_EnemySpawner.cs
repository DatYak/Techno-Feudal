using UnityEngine;
using UnityEngine.InputSystem;

public class TEST_EnemySpawner : MonoBehaviour
{

    public GameObject EnemyToSpawn;

    InputAction spawnAction;

    void Start()
    {
        spawnAction = InputSystem.actions.FindAction("TEST_SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAction.WasPerformedThisFrame())
        {
            Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
