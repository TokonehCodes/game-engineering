using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<RectTransform>();       
    }

    void Update(){
        SetSize(player.humanLife);
    }

    public void SetSize(float size){
        if(size > 0){
            bar.localScale = new Vector3(size * 0.01f, 1f);
        }
        else{
            bar.localScale = new Vector3(0.01f, 1f);
        }
    }
}
