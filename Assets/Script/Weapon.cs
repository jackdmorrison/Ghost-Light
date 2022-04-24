using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //float will change the side the weapon is on whether it is 1 or -1 
    private float f=1f;
    public int damage = 60;
    //this is the weapon animator
    public Animator Animator;
    
    //the player class of the player object that the weapon is attached to
    public Player p;
    //this is the renderer of the weapon
    public SpriteRenderer W_Renderer;
    //this is the renderer of the player
    public SpriteRenderer F_Renderer;
    //this is the differential of the players position to the weapons position it changes relative to the players animation 
    private Vector2 differential;
    
    //whether or not to flip the side the weapon is on
    public bool flipx =false;
    //name of the sprite that the players render is displaying each frame
    private string spriteName;
    private Vector2[] Diff_Arr={new Vector2(-0.1f,-0.15f),new Vector2(-32f,0.52f),new Vector2(-0.07f,0.54f),new Vector2(0.26f,0.46f),new Vector2(0.42f,0.06f)};
    //dictionary used to determine the differential positon relative to the players sprite
    IDictionary<string,Vector2> allignment= new Dictionary<string,Vector2>();
    //transform of the player that the weapon is attached to 
    public Transform followTransform;
    // Start is called before the first frame update
    void Start() 
    {
        DontDestroyOnLoad(gameObject);
        Animator=GetComponent<Animator>();
        W_Renderer=GetComponent<SpriteRenderer>();
        F_Renderer = p.GetComponent<SpriteRenderer>();
        followTransform=p.gameObject.transform;
        allignment.Add("advnt_full_0",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_1",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_2",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_3",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_4",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_5",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_6",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_7",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_8",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_9",new Vector2(-0.1f,-0.15f));

        allignment.Add("advnt_full_10",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_11",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_12",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_13",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_14",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_15",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_16",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_17",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_18",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_19",new Vector2(-0.1f,-0.15f));
        //stand attack
        allignment.Add("advnt_full_20",new Vector2(-32f,0.52f));
        allignment.Add("advnt_full_21",new Vector2(-0.07f,0.54f));
        allignment.Add("advnt_full_22",new Vector2(0.26f,0.46f));
        allignment.Add("advnt_full_23",new Vector2(0.42f,0.06f));

        allignment.Add("advnt_full_24",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_25",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_26",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_27",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_28",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_29",new Vector2(-0.1f,-0.15f));
        //crouch attack
        allignment.Add("advnt_full_30",new Vector2(-0.32f,0.46f));
        allignment.Add("advnt_full_31",new Vector2(-0.1f,0.47f));
        allignment.Add("advnt_full_32",new Vector2(0.24f,0.4f));
        allignment.Add("advnt_full_33",new Vector2(0.38f,0f));

        allignment.Add("advnt_full_34",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_35",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_36",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_37",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_38",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_39",new Vector2(-0.1f,-0.15f));
        //jump attack
        allignment.Add("advnt_full_40",new Vector2(-0.35f,-0.53f));
        allignment.Add("advnt_full_41",new Vector2(-0.14f,-0.56f));
        allignment.Add("advnt_full_42",new Vector2(-0.22f,-0.52f));
        allignment.Add("advnt_full_43",new Vector2(0.35f,0.1f));

        allignment.Add("advnt_full_44",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_45",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_46",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_47",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_48",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_49",new Vector2(-0.1f,-0.15f));

        allignment.Add("advnt_full_50",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_51",new Vector2(-0.1f,-0.15f));
        allignment.Add("advnt_full_52",new Vector2(-0.1f,-0.15f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spriteName=F_Renderer.sprite.name;
        if(name !=null){
            differential= allignment[spriteName];

        }else{
            differential = new Vector2(-0.1f,-0.15f);
        }
        if(flipx){
            W_Renderer.flipX=true;
            f=-1f;
        }
        else{
            W_Renderer.flipX=false;
            f=1f;
        }   

        this.transform.position= new Vector3( followTransform.position.x + ( differential.x*f), followTransform.position.y + differential.y , 0f );
    }
}
