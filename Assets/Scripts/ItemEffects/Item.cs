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


    Collider2D col;
    public void Spawn(Client client, MainGame mg)
    {
        anim.Spawn();
        this.client = client;
        conveyorSpeed = mg.conveyorSpeed;
    }

    private void Reset()
    {
        
    }

    private void Start()
    {
        anim = GetComponent<ItemAnim>();
        sr = GetComponentInChildren<SpriteRenderer>();
        maincam = Camera.main;

        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {


        if (!anim.Finished)
        {
            return;
        }

        if (!dragging)
        {
            transform.position += new Vector3(Time.fixedDeltaTime * conveyorSpeed, 0, 0);
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
        ghost = Instantiate(ghostobject, transform.position, Quaternion.identity);
        ghost.sprite = sr.sprite;
        dragging = true;

    }
    private void OnMouseDrag()
    {
        if (!dragging) return;
        Vector3 pos = maincam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        if (!dragging) return;
        col.enabled = false;
        RaycastHit2D r = Physics2D.Raycast(transform.position, -Vector2.up);
        col.enabled = true;

        if (r)
        {
            if (r.transform.tag == "Client")
            {
                if (r.transform.GetComponent<Client>() == client)
                {
                    Destroy(ghost.gameObject);
                    dragging = false;
                    RejectEffect();
                    return;
                }
            }

            if (r.transform.tag == "Client")
            {
                Destroy(ghost.gameObject);
                dragging = false;
                BuyEffect();
                return;

            }
        }

        Destroy(ghost);
        dragging = false;
        transform.position = ghost.transform.position;
      

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
        Destroy(this.gameObject);
        //animations and stuff 
    }


}