using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
        GameObject player = col.gameObject;
        if (player.tag=="Player"){
            player.transform.position = GameObject.FindWithTag("Start").transform.position;
        }
        
        
    }
}
