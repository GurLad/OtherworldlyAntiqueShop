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

        }
        else
        {
            ghost.transform.position += new Vector3(Time.fixedDeltaTime * conveyorSpeed, 0, 0);

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
        transform.position = ghost.transform.position;
        Destroy(ghost);
        dragging = false;

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
        //animations and stuff yee
    }

  
}
