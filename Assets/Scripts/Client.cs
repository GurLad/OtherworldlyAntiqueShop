using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public ClientAnim anim;
    private MainGame mg;
    private Item item;

    private void Reset()
    {
        anim = GetComponent<ClientAnim>();
    }

    private void Start()
    {
        // TEMP!!!!
     //   Init(null);
    }

    public void Init(Item item)
    {
        Debug.Log("yoo");
        mg = FindObjectOfType<MainGame>();
        this.item = item;
        Debug.Log(this.item);
        anim.Spawn(this);
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3( Time.fixedDeltaTime * mg.conveyorSpeed,0,0);

    }

    public void leave()
    {
        anim.Leave();
    }

    public void SpawnItem()
    {
        //create item
        Item item = Instantiate(this.item);
        item.transform.position = new Vector3(transform.position.x, item.transform.position.y, item.transform.position.z);
        //init item
        item.Spawn(this, mg);
    }
}
