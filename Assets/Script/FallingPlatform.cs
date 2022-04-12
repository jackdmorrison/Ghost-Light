using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D body;
    
    void Start(){
        body= GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name.Equals("Player")){
            PlatformManager.Instance.StartCoroutine("SpawnPlatform",new Vector2(transform.position.x, transform.position.y));
            Invoke("Fall",0.4f);
            Destroy(gameObject,2f);
        }
    }
    void Fall(){
        body.isKinematic = false;
    }
}
