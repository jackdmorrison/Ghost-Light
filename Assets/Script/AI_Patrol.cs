using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Patrol : MonoBehaviour
{
    public bool patroling;
    public Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        patroling=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(patroling){
            body.velocity = new Vector2(body.velocity.x, body.velocity.y);
        }
    }
}
