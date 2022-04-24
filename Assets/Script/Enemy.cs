using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public Player player;
    public int health=100;
    public float jumpthreshold=0f;
    private int direction=0;
    public bool crouch;
    private bool visible;
    public Animator animator;
    public SpriteRenderer E_renderer;
    public Collider2D E_Collider;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        E_renderer = GetComponent<SpriteRenderer>();
        E_Collider=GetComponent<Collider2D>();
        crouch=false;
        visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        movep();
        
        
        
    }
    void Animate(){
        
        animator.SetFloat("Horizontal",body.velocity.x);
        if(body.velocity.y<0.1f){
            animator.SetFloat("Vertical",0);
        }
        else{
            animator.SetFloat("Vertical",body.velocity.y);
        }
        
    }
    void OnBecameVisible()
    {
        visible=true;
    }
    void OnBecameInvisible()
    {
        visible=false;
    }
    public void Jump(){
        
        body.AddForce(new Vector2(0,3f), ForceMode2D.Impulse);
       
 }
    void OnCollisionStay2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Tile" ){
            movep();
        }
    }
    void movep(){
        animator.SetBool("IsIdle",true);
            if(player != null && visible==true){
                float EnemyPlayerDistance= player.transform.position.x-gameObject.transform.position.x;
                if(EnemyPlayerDistance>0){
                    direction=1;
                    E_renderer.flipX=false;
                }else if(EnemyPlayerDistance<0){
                    direction=-1;
                    E_renderer.flipX=true;
                }
                else{
                    direction=0;
                }
                float PlayerElevation=player.transform.position.y-gameObject.transform.position.y-jumpthreshold;
                if(PlayerElevation>0){   
                    Jump();
                    animator.SetBool("IsIdle",false);
                }
                
            }
            if(direction!=0){
                animator.SetBool("IsIdle",false);
                
            }
            body.velocity=new Vector2(direction,0);
            
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag=="P_Weapon"){
            damage();
        }
        if(health<=0){
            death();
        }
    } 
    void death(){
        
        animator.SetBool("Is_Dead",true);
        E_Collider.enabled=false;
        Destroy(gameObject);
    }
    void damage(){
        health=health-player.WEAPONDAMAGE;
        animator.SetTrigger("IsHurt");
        
    }
 }
