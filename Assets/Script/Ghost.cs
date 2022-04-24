using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator Animator;
   
    //this is the renderer of the player
    public SpriteRenderer G_Renderer;
    //this is the differential of the players position to the weapons position it changes relative to the players animation 
    private Vector2 differential;
    Vector2 Target;
    public float FlyHeight=15f;
    public float ballDistance=30f;
    //whether or not to flip the side the weapon is on
    public bool flipx =false;
    public Rigidbody2D body;
    public Collider2D collider_G;
    public float forceToPlayer= 3f;
    private float f=1;
    
    public Transform followTransform;
    // Start is called before the first frame update
    void Start()
    {
        G_Renderer=GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        followTransform = GetComponent<Transform>();
        Animator = GetComponent<Animator>();
        collider_G = GetComponent<Collider2D>();
        differential=new Vector2(0.3f,0.4f);
        Target=new Vector2(followTransform.position.x,followTransform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //the animator is set to idle 
        Animator.SetBool("IsIdle",true);

        //the players position is checked 
        Target = new Vector2(followTransform.position.x,followTransform.position.y);
        //if the player is much higher than the ghost
        //turn to ball and rise to players height without collisions
        if(Target.y>transform.position.y-FlyHeight){
            Animator.SetBool("IsBall",true);
            collider.enabled=false;
            body.AddRelativeForce(new Vector3(0,forceToPlayer,0));
        }else{
            Animator.SetBool("IsBall",false);
            collider_G.enabled=true;
        }
        //if the player is much further infront the ghost 
        //increase force on ghost, turn off collisions and turn into a ball
        if(Target.x>transform.position.x-ballDistance){
            Animator.SetBool("IsBall",true);
            collider_G.enabled=false;
            body.AddRelativeForce(new Vector3(forceToPlayer,0,0) );
        }else{
            Animator.SetBool("IsBall",false);
            collider_G.enabled=true;
        }
        
        //if the player is slightly infront of the ghost force becomes ghost force inverts until its 0 of course
        
        //if the player is behind the ghost do same as infront i guess do deedle doot doot     doot doot






        // if(Target.x>this.transform.position.x){
        //     bodyelocity.x +=1;
        // }
        // if(Target.x<this.transform.position.x){
        //     velocity.x -=1;
        // }
        // if(Target.y>this.transform.position.y){
        //     velocity.y +=1;
        // }
        // if(Target.y<this.transform.position.y){
        //     velocity.y -=1;
        // }
        if(flipx){
            G_Renderer.flipX=false;
            f=-1f;
        }
        else{
            G_Renderer.flipX=true;
            f=1f;
        }  
        Target= new Vector2( followTransform.position.x + ( differential.x*f), followTransform.position.y + differential.y );
    }
    public void FlipX(bool asignable){
        flipx=asignable;
    }
}
