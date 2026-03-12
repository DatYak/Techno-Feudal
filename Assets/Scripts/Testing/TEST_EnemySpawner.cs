using UnityEngine;

public class TEST_EnemySpawner : MonoBehaviour
{

    public GameObject EnemyToSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
