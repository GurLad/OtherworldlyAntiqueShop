using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool corrupted;
    Camera maincam;
    SpriteRenderer sr;
    Vector3 realpos;
    public float conveyorSpeed;
    bool dragging;
    SpriteRenderer ghost;
    public SpriteRenderer ghostobject;

    public Collider2D col;
    public float RegisterXpoint;

    public Client thisClient;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
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
        ghost = Instantiate(ghostobject,transform.position,Quaternion.identity);
        ghost.sprite = sr.sprite;
        dragging = true;

    }
    private void OnMouseDrag()
    {

        Vector3 pos = maincam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;



    }

    private void OnMouseUp()
    {
        col.enabled = false;

        RaycastHit2D r = Physics2D.Raycast(transform.position, -Vector2.up);
        col.enabled = true;

        if (r)
        {
            if (r.transform.tag == "Client")
            {
                if(r.transform.GetComponent<Client>()== thisClient)
                {
                    Destroy(ghost);
                    dragging = false;
                    RejectEffect();
                    return;

                }
                
            }

            if (r.transform.tag == "Register")
            {
                Destroy(ghost);
                dragging = false;
                BuyEffect();
                return;
            }
        }
        
        transform.position = ghost.transform.position;


        //if mosue on top of the client do something

        //if mouse on top of cash register do someting

        //if mouse up on nothing return to previous position + movement (show movement with a translucent copy)
    }



    public void BuyEffect()
    {
        if (corrupted)
        {
            //bad
        }
        else
        {
            //good
        }
    }

    public void RejectEffect()
    {
        //animations and stuff 
    }

  
}
