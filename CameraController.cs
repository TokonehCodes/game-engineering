using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 midpoint2 = mousePos + player.transform.position;
        midpoint2 /= 2f;
        midpoint2 = new Vector3(midpoint2.x, midpoint2.y, -10f);

        transform.position = midpoint2;

        
    }


}
