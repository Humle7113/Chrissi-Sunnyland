using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class enemyai : MonoBehaviour { 

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    //hier
    public Transform spieler;
    //stehengeblieben bei 21:07 wowowoowow

    Path path;
    int currentwaypoint = 0;
    bool reachedendofpath=false;

    Seeker seeker;
    Rigidbody2D rb;
   
    void Start()
    {
        seeker =GetComponent<Seeker>();
        rb= GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        spieler = GameObject.FindWithTag("final-Chrissi-stand").transform;
        

    }
    //hier
    private void Update()
    {
        Vector3 direction = spieler.position - transform.position;
        if (direction.x < 0) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
        }
    }

    void UpdatePath() {

        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);


    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path= p;
            currentwaypoint = 0;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        
            return;
       if(currentwaypoint >=path.vectorPath.Count) { 
        reachedendofpath = true;
            return;

        }
        else
        {
            reachedendofpath= false;

        }
       Vector2 direction = ((Vector2)path.vectorPath[currentwaypoint] - rb.position).normalized;



        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce (force);


        float distance = Vector2.Distance(rb.position, path.vectorPath[currentwaypoint]);


        if ( distance < nextWaypointDistance )
        {
            currentwaypoint++;
        }



    }
}
