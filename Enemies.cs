using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemies : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public Transform elevator;
    float pathUpdateRate = 0.1f;
    public float count = 0;

    [Header("Physics")]
    public float speed = 1f;
    public float jumpSpeed = 10f;
    float nextPointDistance = 3f;
    public float jumpNodeHeight = 0.8f;
    public Vector2 direction;

    [Header("Lifespan")]
    public int hitCount = 0;
    public int maxHit = 2;


    private Path path;
    private int currentWaypoint = 0;
    public bool grounded = false;
    bool roofed = false;
    public GameObject[] LiftSet;

    Seeker seeker;
    Rigidbody2D rb;
    CircleCollider2D coll;


    public void Start(){
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }

    void Update(){
        Die();
        PathFollow();
    }

    public void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }    

    public void PathFollow(){
        if (path == null){
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count){
            return;
        }

        //See if colliding with anything
        roofed = Physics2D.Raycast(coll.bounds.max, Vector2.up, 0.1f);
        grounded = Physics2D.Raycast(coll.bounds.min, Vector2.down, 0.1f);

        //Direction Calculation
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * (speed) ;
        force.y = 0;

        //Jump
        if (grounded){
            if(direction.y > jumpNodeHeight){
                rb.velocity = Vector2.up * jumpSpeed;
            }
        }

        //Inverse Jump
        if (roofed){
            if(direction.y < jumpNodeHeight){
                rb.velocity = Vector2.down * jumpSpeed;
            }
        }


        //Movement
        rb.AddForce(force);

        //Next Waypoint
        float pathDistance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(pathDistance < nextPointDistance){
            currentWaypoint++;

        //CantReach
        if(Gravity.gravDown){
            if (player.position.y > transform.position.y){
                count += Time.deltaTime;
                if(count >= 3){
                    ChangeTarget(elevator);
                    count =0;
                }
            } else{
                ChangeTarget(player);
            }
        }
        else if(!Gravity.gravDown){
            ChangeTarget(player);
        }
        
        }
    }

    public void OnCollisionEnter2D(Collision2D other){
        GameObject collObject = other.gameObject;
        if(other.gameObject.CompareTag("Bullet")){
            hitCount += 1;
        }
    } 

    public void Die(){
        if(hitCount >= maxHit){
            Destroy(gameObject);
        }
    }

    public void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    public void ChangeTarget(Transform t){
        target = t;
    }

}
