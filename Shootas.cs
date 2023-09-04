using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootas : Enemies
{

    [Header ("Shooter")]
    public Transform ammo;

    public GameObject AIbullet;
    public GameObject bulletParent;

    public float range = 50f;
    public float distance;
    public bool withinRange;
    public int bulletCount = 5;

    public float nextFireTime =0;

    void Update(){        
        distance = Vector2.Distance(transform.position, target.transform.position);
        if(range >= distance){
            withinRange = true;
        }
        if(range <= distance){
            withinRange = false;
        }

        if(target == ammo){
            PathFollow();
        }
        else if (bulletCount >= 0 && withinRange && target == player){
            Shoot();
        }

        else if(bulletCount >= 0 && !withinRange && target == player){
            PathFollow();
        }
        if(bulletCount == 0 && target == player){
            PathFollow();
        }
        AmmoOrPlayer();
        Die();
    }

    void AmmoOrPlayer(){
        if (ammo.gameObject.activeSelf){
            ChangeTarget(ammo);
        }else {ChangeTarget(player);}
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }  

    void Shoot(){
        if(bulletCount > 0){
            if(nextFireTime < Time.time ){
                Instantiate(AIbullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + 1;
                bulletCount = bulletCount-1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Ammo"))
        {
            bulletCount = bulletCount + 5;
        }
   }


}
