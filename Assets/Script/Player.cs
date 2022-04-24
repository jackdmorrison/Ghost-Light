using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
   public Rigidbody2D body;
    public HingeJoint2D hinge;
    float horizontal;
    public Collider2D Pcollider;
    public Ghost ghost;
    public float speed = 3.0f;
    public float jumpForce = 120f;
    public float attachForce=15f;
    public bool attached = false;
    public bool crouch = false;
    public Weapon weapon;
    public Transform attachedTo;
    private GameObject disregard;
    
    Animator animator;
    public int WEAPONDAMAGE=0;
    
    public SpriteRenderer P_renderer;
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnLevelLoad;
    }
            
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoad;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        body = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        Pcollider=GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        P_renderer = GetComponent<SpriteRenderer>();
        WEAPONDAMAGE = weapon.damage;
        
        }
    void Update(){
        if(!crouch){
            horizontal=Input.GetAxisRaw("Horizontal");
        }
        
        animator.SetBool("Is_Idle",true);
        getInputs();
        
        
        if(!attached){
            transform.position += new Vector3(horizontal, 0,0)*Time.deltaTime*speed;
        }
        
        if(horizontal!=0){
            
            if(horizontal>0){
                if(attached){
                    body.AddRelativeForce(new Vector3(1,0,0) * attachForce);
                }
                P_renderer.flipX=false;
                weapon.flipx=false;
                ghost.FlipX(false);
            }
            else if(horizontal<0){
                if(attached){
                    body.AddRelativeForce(new Vector3(-1,0,0) * attachForce);
                }
                weapon.flipx=true;
                P_renderer.flipX=true;
                ghost.FlipX(true);
            }
            animator.SetBool("Is_Idle",false);
            Animate();
            
        }
        
        if(Mathf.Abs(body.velocity.y)!=0f){
            animator.SetBool("Is_Idle",false);
            Animate();
        }
    }
    void getInputs(){
        if(Input.GetKey(KeyCode.LeftControl)){
            
            crouch = true;
            animator.SetBool("Is_Crouch",true);
        }
        else{
            crouch=false;
            animator.SetBool("Is_Crouch",false);
        }
        if(Input.GetKey(KeyCode.E)){
            animator.SetBool("Attack",true);
            weapon.Animator.SetBool("Is_Strike",true);
            Strike();
        }else{
            animator.SetBool("Attack",false); 
            weapon.Animator.SetBool("Is_Strike",false);
            
        }
        if(Input.GetButtonDown("Jump")){
            
            if(attached){
                hinge.connectedBody.gameObject.GetComponent<Rope_SEG>().isPlayerAttached=false;
                attached=false;
                hinge.enabled=false;
                Pcollider.enabled=true;
                hinge.connectedBody=null;
            }
            else{
                attachedTo=null;
            }
            if(Mathf.Abs(body.velocity.y)<0.001f && !crouch){
                body.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    void Animate(){
        animator.SetFloat("Horizontal",body.velocity.x);
        animator.SetFloat("Vertical",body.velocity.y);
    }
    public void Strike(){
       
    }
    public void Attach(Rigidbody2D ropeSegment){
        ropeSegment.gameObject.GetComponent<Rope_SEG>().isPlayerAttached = true;
        hinge.connectedBody=ropeSegment;
        hinge.enabled = true;
        attached = true;
        attachedTo = ropeSegment.gameObject.transform.parent;
        Pcollider.enabled = false;
        GetComponent<Transform>().position=ropeSegment.gameObject.transform.position;

    }
    void OnTriggerEnter2D(Collider2D col){
        if(!attached){
            if(col.gameObject.tag == "Rope"){
                if(attachedTo != col.gameObject.transform.parent){
                    if(disregard==null || col.gameObject.transform.parent.gameObject!= disregard){
                        Attach(col.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }
    
    void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        //transform.position = GameObject.FindWithTag("Start").transform.position;
        
    }
}
