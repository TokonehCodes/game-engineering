using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth = 5f;
    private float hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = startHealth;
    }

    public void takeBulletDamage(float damage){
        hp -= damage;

        if (hp <= 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hp = startHealth;
        }
    }
}
