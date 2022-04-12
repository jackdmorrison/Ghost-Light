using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


         

//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public HingeJoint2D hinge;
    float horizontal;
    public Collider2D Pcollider;
    public float speed = 3.0f;
    public float jumpForce = 120f;
    public float attachForce=15f;
    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;
    Animator animator;
    SpriteRenderer P_renderer;
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
        animator.SetBool("Is_Jump",false);
        animator.SetBool("Is_Crouch",false);
        }
    void Update(){

        horizontal=Input.GetAxisRaw("Horizontal");
        animator.SetBool("Is_Idle",true);
        animator.SetBool("Is_Walk",false);
        
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            animator.SetBool("Is_Crouch",true);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            animator.SetBool("Is_Crouch",false);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            animator.SetBool("Is_Attack",true);
        }
        if(Input.GetKeyUp(KeyCode.E)){
            animator.SetBool("Is_Attack",false);
        }
        if(!attached){
            transform.position += new Vector3(horizontal, 0,0)*Time.deltaTime*speed;
        }
        
        if(horizontal!=0){
            animator.SetBool("Is_Idle",false);
            animator.SetBool("Is_Walk",true);
            
            if(horizontal>0){
                if(attached){
                    body.AddRelativeForce(new Vector3(1,0,0) * attachForce);
                }
                P_renderer.flipX=false;
            }
            else if(horizontal<0){
                if(attached){
                    body.AddRelativeForce(new Vector3(-1,0,0) * attachForce);
                }
               P_renderer.flipX=true;
            }
        }
        if(Input.GetButtonDown("Jump")){
            animator.SetBool("Is_Idle",false);
            animator.SetBool("Is_Jump",true);
            animator.SetBool("Is_Walk",false);
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
            if(Mathf.Abs(body.velocity.y)<0.001f){
                body.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            }
            
            
        }
        else{
            animator.SetBool("Is_Jump",false);
        }
        
        
    }
    public void attack(){

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
        transform.position = GameObject.FindWithTag("Start").transform.position;
    }
}
