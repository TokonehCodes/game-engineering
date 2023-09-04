using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBullet : MonoBehaviour
{
    GameObject human;
    public float speed = 10f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
        human = GameObject.FindGameObjectWithTag("human");
        Vector2 moveDir = (human.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
    }  

    private void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);   
    } 
}
