using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    [Header("Objects")]
    public SpriteRenderer ghostobject;
    public ItemAnim anim;
    [Header("Values")]
    public bool corrupted;
    [HideInInspector]
    public float RegisterXpoint;
    public Vector3 ClientOffset;
    private Camera maincam;
    private SpriteRenderer sr;
    private Vector3 realpos;
    private bool dragging;
    private SpriteRenderer ghost;
    [SerializeField]
    private Client client;

    [SerializeField]
    private int Score;

    MainGame mg;

    Collider2D col;
    public void Spawn(Client client, MainGame mg)
    {
        anim.Spawn();
        this.client = client;
    }

    private void Reset()
    {
        
    }

    private void Start()
    {
        mg = FindObjectOfType<MainGame>();
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
            transform.position += new Vector3(Time.fixedDeltaTime * mg.conveyorSpeed, 0, 0);
            realpos = transform.position;

        }
        else
        {

            ghost.transform.position += new Vector3(Time.fixedDeltaTime * mg.conveyorSpeed, 0, 0);
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

            if (r.transform.tag == "Register")
            {
                Destroy(ghost.gameObject);
                dragging = false;
                BuyEffect();
                return;

            }
        }

        Destroy(ghost.gameObject);
        dragging = false;
        transform.position = ghost.transform.position;
      

        //if mosue on top of the client do something

        //if mouse on top of cash register do someting

        //if mouse up on nothing return to previous position + movement (show movement with a translucent copy)
    }


    public void BuyEffect()
    {
        if (corrupted)
        {


            StartCoroutine(DestroyAnimLoop());
            FindObjectOfType<GameUI>().LoseLife();

            // Take damage
        }
        else
        {
            StartCoroutine(DestroyAnimLoop());
            FindObjectOfType<GameUI>().IncreaseScore(Score);

            // Remove lives
        }
    }

    public void RejectEffect()
    {
        StartCoroutine(DestroyAnimLoop());




        //animations and stuff 
    }


    public IEnumerator DestroyAnimLoop()
    {

        this.transform.DOScale(0, 0.5f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.5f);
        client.leave();
        Destroy(this.gameObject);


    }


}