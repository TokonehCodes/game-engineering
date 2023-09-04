using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float respawnTime = 3f;
    private int hitCount = 0;
    public int maxCount = 2;

    void Update(){
        if(!Active()){
            if(respawnTime < 0){
                gameObject.GetComponent<SpriteRenderer>().enabled = true; 
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                respawnTime = 3;}
            respawnTime -= Time.deltaTime;
        }
    

    }

    private bool Active(){
        if(gameObject.GetComponent<SpriteRenderer>().enabled == false &&
            gameObject.GetComponent<BoxCollider2D>().enabled == false){
            return false;
            }
        else{
            return true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        GameObject collObject = other.gameObject;
        if(other.gameObject.CompareTag("AIBullet")){
            hitCount = hitCount+1;
            if(hitCount == maxCount){
                gameObject.GetComponent<SpriteRenderer>().enabled = false; 
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    } 
}
