using UnityEngine;

public class TEST_EnemySpawner : MonoBehaviour
{

    public GameObject EnemToSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(EnemToSpawn, transform.position, Quaternion.identity);
        }
    }
}
