using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] float inverseTime = 10;
    private Vector2 gravity;

    public static bool gravDown;

    void Start()
    {
        gravity = new Vector2(0,-9.8f);
    }

    void FixedUpdate()
    {
        inverseTime -= Time.deltaTime;
        if (inverseTime <= 0){
            gravity.y = -(gravity.y);
            inverseTime = 10;  
            Physics2D.gravity = gravity;          
        }

        if (gravity.y < 0){
            gravDown = true;
        }
        if(gravity.y > 0){
            gravDown = false;
        }

        Debug.Log(gravDown);


    }

}