using System.Collections.Generic;
using UnityEngine;

public class GoopBomb : MonoBehaviour
{

    // A goop bomb that pulls enemies in and slows them.
    // Has three stages
    //  - Expand: lines rapidly grow from the bomb to connect to each target.
    //  - Pull:   Each target is rapidly moved towards the bomb.
    //  - Slow:   Targets move much slower for a duration.

    public float radius;
    
    [Range(0, 1)]
    public float speedLimit = 0.1f;
    public LayerMask enemyLayers;

    [Header("Timing")]
    public float expandDuration;
    public float pullDuration;
    public float slowDuration;

    public LineRenderer strandPrefab;

    private Enemy[] targets;
    private LineRenderer[] strands;
    
    private Vector3[] origionalPositions;

    const int MAX_TARGETS = 50;

    Collider[] nearbyEnemyColliders = new Collider[MAX_TARGETS];
    
    int numTargets;

    //The time that has passed since starting the current stage
    private float timeInStage;

    int stage;

    void Start()
    {

        //Find all enmeies in radius
        numTargets = Physics.OverlapSphereNonAlloc(transform.position, radius, nearbyEnemyColliders, enemyLayers);

        targets = new Enemy[numTargets];
        strands = new LineRenderer[numTargets];
        origionalPositions = new Vector3[numTargets];

        for (int i = 0; i < numTargets; i++)
        {
            Enemy e = nearbyEnemyColliders[i].GetComponent<Enemy>();
            if (e)
            {
                targets[i] = e;
                LineRenderer lr = Instantiate(strandPrefab, transform);
                lr.SetPosition(0, transform.position);
                strands[i]= lr;
            }
        }


        if (numTargets == 0)
        Destroy(this.gameObject);

        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeInStage += Time.deltaTime;


        switch (stage)
        {
            case 0:
                Expand();
                break;
            case 1:
                Pull();
                break;
            case 2: 
                Slow();
                break;
        }

    }

    void Expand()
    {
        float t = timeInStage / expandDuration;

        for (int i = 0; i < numTargets; i ++)
        {
            if (targets[i] == null)
                continue;

            Vector3 targetPos = targets[i].transform.position;
            Vector3 strandPos =  Vector3.Lerp(transform.position, targetPos, t);
            strands[i].SetPosition(1, strandPos);   
        }

        if (timeInStage >= expandDuration)
        {
            for (int i = 0; i < numTargets; i ++)
            {
                if (targets[i] == null)
                    continue;
                    
                origionalPositions[i] = targets[i].transform.position; 
            }

            timeInStage = 0;
            stage = 1;
        }
    }

    void Pull()
    {
        float t = timeInStage / pullDuration;

        for (int i = 0; i < numTargets; i ++)
        {
            if (targets[i] == null)
                continue;

            Vector3 origionalPos = origionalPositions[i];
            Vector3 targetPos =  Vector3.Lerp(origionalPos, transform.position, t);
            targets[i].transform.position = targetPos;
            strands[i].SetPosition(1, targetPos);   
        }

        if (timeInStage >= pullDuration)
        {
                for (int i = 0; i < numTargets; i ++)
                {
                    if (targets[i] == null)
                        continue;
                    
                    targets[i].GetComponent<EnemyMovement>().speedMod = speedLimit;
                }       
                
            timeInStage = 0;
            stage = 2;
        }
    }

    void Slow()
    {
        float t = timeInStage / slowDuration;

        for (int i = 0; i < numTargets; i ++)
        {
            if (targets[i] == null)
                continue;

            strands[i].SetPosition(1, targets[i].transform.position); 
        }

        //Add strand snap effect here
        if (timeInStage >= slowDuration)
        {
            for (int i = 0; i < numTargets; i ++)
            {
                if (targets[i] == null)
                    continue;
                
                targets[i].GetComponent<EnemyMovement>().speedMod = 1;
            }

            Destroy(this.gameObject);
        }
    }
}
