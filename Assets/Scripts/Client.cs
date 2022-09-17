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

    public void Init(Item item)
    {
        mg = FindObjectOfType<MainGame>();
        this.item = item;
        anim.Spawn(this);
    }

    private void FixedUpdate()
    {
        //transform.position += new Vector3( Time.fixedDeltaTime * mg.conveyorSpeed,0,0);

    }

    public void leave()
    {
        //anim.leave
    }

    public void SpawnItem()
    {
        //create item
        Instantiate(item, transform.localPosition, Quaternion.identity);
        //init item
        item.Spawn(this, mg);
    }
}
