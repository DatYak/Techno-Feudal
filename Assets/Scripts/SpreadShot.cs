using UnityEngine;

public class SpreadShot : MonoBehaviour
{

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float spreadAngle;

    [SerializeField]
    int minShots;

    [SerializeField]
    int maxShots;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int shots = Random.Range(minShots, maxShots + 1);

        for (int i = 0; i < shots; i++)
        {
            Vector3 launchDir = GetRandomInSpread();

            Instantiate(projectile, transform.position, Quaternion.LookRotation(launchDir));
        }

        Destroy(this.gameObject);
    }

    private Vector3 GetRandomInSpread()
    {
        Vector3 randomDirection = Random.insideUnitSphere;

        float maxAngle = spreadAngle / 2f;

        Quaternion rotation = Quaternion.AngleAxis(Random.Range(0f, maxAngle), Random.insideUnitSphere);
        Vector3 rotatedDirection = rotation * transform.forward;

        return rotatedDirection.normalized;
    }
}
