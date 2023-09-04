using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    private Rigidbody2D connor;
    private PolygonCollider2D coll;


    [SerializeField] float speed = 5f;
    [SerializeField] float jumpSpeed = 8f;    
    [SerializeField] Transform groundChecker;


    public static float humanLife = 100f;
    [SerializeField] float velocityThreshold = 8f;
    private bool wasGrounded;
    bool grounded = false;


    public Text bulletText;

    [SerializeField] int bulletCount = 20;
    [SerializeField] int addBullet = 20;

    public GameObject bullet;
    public Transform bulletTransform;
    private float timer;
    public float hitDamage = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        connor = GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        bulletText.text = "Bullets: " + bulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        play();
    }

    void FixedUpdate(){
        CheckGround();

        if(!wasGrounded && grounded){
            takeFallDamage();
        }

        wasGrounded = grounded;
    }

    void play(){
        walk();
        jump(); 
        if (Input.GetMouseButtonDown(0)){ 
            shoot();
        }
        Death();
        Victory();
    }

    void walk(){
        float direction = Input.GetAxis("Horizontal");
        connor.velocity = new Vector2(direction * speed, connor.velocity.y);
        
    }

    void jump(){
        if(isGrounded() && Input.GetKeyDown(KeyCode.W)){
            connor.velocity = Vector2.up * jumpSpeed;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            connor.velocity = Vector2.down * jumpSpeed;
        }
    }

    public bool isGrounded(){
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.min, Vector2.down, extraHeight);
        if(hit.collider != null){
            if(hit.collider.CompareTag("Platform")){
                transform.SetParent(hit.transform);
            }
        }
        return hit.collider != null;
    }
    
    private void shoot(){ 
        timer += Time.deltaTime;
        if(bulletCount > 0){
            bulletCount = bulletCount - 1;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            timer = 0;
        }
        if(bulletCount <= 0){
            bulletCount = 0;
        }
        bulletText.text = "Bullets: " + bulletCount;
    }

    void CheckGround(){
        grounded = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.1f);
    }

    void takeFallDamage(){
        float fallDamage = connor.velocity.y ;

        if(connor.velocity.y < -velocityThreshold){
            humanLife += fallDamage;
        }
    }

    //Get hit by enemies
    private void OnCollisionEnter2D(Collision2D other){
        GameObject collObject = other.gameObject;
        if(other.gameObject.CompareTag("Enemy") | other.gameObject.CompareTag("AIBullet")){
            humanLife -= hitDamage;
        }
    }

    void Death(){
        if(humanLife <= 0){
            SceneManager.LoadScene("Dead");
            humanLife = 100f;
            bulletCount = 20;
        }
    }

    void Victory(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0){
            SceneManager.LoadScene("Victory");
            humanLife = 100f;
            bulletCount = 20;
        }
    }
   
   void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Ammo"))
        {
            bulletCount = bulletCount + addBullet;
            bulletText.text = "Bullets: " + bulletCount;
        }
   }
}


