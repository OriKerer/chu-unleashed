using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class PlayerAction : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameManagerScript manager;

    float h;
    float v;
    bool isHorizonMove;
    Vector2 dirVec;
    GameObject scanObject;


    Rigidbody2D rigid;
    Animator anim;


     void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false :Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false :Input.GetButtonUp("Vertical");

        if (Platform.IsMobileBrowser())
        {
            h = manager.isAction ? 0 : TCKInput.GetAction("Left", EActionEvent.Hover) ? -1 : TCKInput.GetAction("Right", EActionEvent.Hover) ? 1 : 0;
            v = manager.isAction ? 0 : TCKInput.GetAction("Down", EActionEvent.Hover) ? -1 : TCKInput.GetAction("Up", EActionEvent.Hover) ? 1 : 0;

            hDown = manager.isAction ? false : TCKInput.GetAction("Left", EActionEvent.Hover) || TCKInput.GetAction("Right", EActionEvent.Hover);
            vDown = manager.isAction ? false : TCKInput.GetAction("Down", EActionEvent.Hover) || TCKInput.GetAction("Up", EActionEvent.Hover);
        }


        //Check Horizontal Move
        if (hDown)
        {
            isHorizonMove = true;
        }
        else if (vDown)
        {
            isHorizonMove = false;

        }
        else if ((hUp || vUp) && h != 0)
            isHorizonMove = true;
        else if ((hUp || vUp) && v != 0)
            isHorizonMove = false;


        //Animation
        if (anim.GetInteger("hAxisRaw") != (int)h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        //Direction
        if (vDown && v > 0)
            dirVec = Vector2.up;
        else if (vDown && v < 0)
            dirVec = Vector2.down;
        else if (hDown && h > 0)
            dirVec = Vector2.right;
        else if (hDown && h <0)
            dirVec = Vector2.left;

        //raycast?? ???????? scan
        if((Input.GetButtonDown("Jump") || (Platform.IsMobileBrowser() && TCKInput.GetAction("Action", EActionEvent.Click))) && scanObject != null)
        {
            manager.Action(scanObject);
        }

        //Move
        Vector3 moveVec = isHorizonMove ? new Vector3(h, 0, 0) : new Vector3(0, v, 0);
        if (Platform.IsMobileBrowser())
        {
            moveVec = new Vector3(h, v, 0);
        }
        //rigid.velocity = moveVec * speed;
        rigid.MovePosition(transform.position + (moveVec * speed));
    }

    void FixedUpdate()
    {


        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f , LayerMask.GetMask("Environment"));

        if (rayHit.collider != null)
        {
            
            //raycast ?? ?????????? ?????? ????
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
}
