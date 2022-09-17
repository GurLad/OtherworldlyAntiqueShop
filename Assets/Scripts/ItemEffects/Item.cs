using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Objects")]
    public ItemEffect itemEffect;
    public SpriteRenderer ghostobject;
    public ItemAnim anim;
    [Header("Values")]
    public float conveyorSpeed;
    public float RegisterXpoint;
    public Vector3 ClientOffset;
    private Camera maincam;
    private SpriteRenderer sr;
    private Vector3 realpos;
    private bool dragging;
    private SpriteRenderer ghost;
    private Client client;


    public float RegisterXpoint;
    private void Start()
    {
        maincam = Camera.main;
    }
    
    private void FixedUpdate()
    {
        if (!dragging)
        {
            transform.position += new Vector3( Time.fixedDeltaTime * conveyorSpeed,0,0);
            realpos = transform.position;

        }
        else
        {

            ghost.transform.position += new Vector3(Time.fixedDeltaTime * conveyorSpeed, 0, 0);
            realpos = ghost.transform.position;

        }

        if (realpos.x >= RegisterXpoint)
        {
            BuyEffect();
        }
    }

    private void OnMouseDown()
    {
        if (!anim.Finished)
        {
            return;
        }
        ghost = Instantiate(ghostobject,transform.position,Quaternion.identity);
        ghost.sprite = sr.sprite;
        dragging = true;

    }
    private void OnMouseDrag()
    {
        if (!anim.Finished)
        {
            return;
        }
        Vector3 pos = maincam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        transform.position = ghost.transform.position;
        Destroy(ghost);
        dragging = false;

        //if mosue on top of the client do something

        //if mouse on top of cash register do someting

        //if mouse up on nothing return to previous position + movement (show movement with a translucent copy)
    }



    public void BuyEffect()
    {
        itemEffect.Buy();
    }

    public void RejectEffect()
    {
        //animations and stuff 
    }

  
}
