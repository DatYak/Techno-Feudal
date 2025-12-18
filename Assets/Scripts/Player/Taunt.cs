using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Taunt : MonoBehaviour
{
    public float tauntRadius = 15;

    public float timeAlive;

    float currentTime;

    public Slider timeBar;

    List<Enemy> taunted;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = timeAlive;
        taunted = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] colls = Physics.OverlapSphere(transform.position, tauntRadius);

        foreach (Collider coll in colls)
        {
            if (coll.tag == "Enemy")
            {
                Enemy e = coll.GetComponent<Enemy>();
                if (!taunted.Contains(e))
                {
                    taunted.Add(e);
                    e.Taunt(this.gameObject);
                }
            }
        }

        currentTime -= Time.deltaTime;
        timeBar.value = currentTime / timeAlive;

        if (currentTime <= 0)
        {
            foreach (Enemy e in taunted)
            {
                e.EndTaunt();
            }
            Destroy (this.gameObject);
        }
    }
}
