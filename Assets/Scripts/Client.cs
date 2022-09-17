using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    private MainGame mg;
    private Item item;

    public void Init(Item item)
    {
        mg = FindObjectOfType<MainGame>();
        this.item = item;
        // TODO: animate first
        SpawnItem();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3( Time.fixedDeltaTime * mg.conveyorSpeed,0,0);

    }

    public void SpawnItem()
    {
        //create item
        Instantiate(item, transform.localPosition, Quaternion.identity);
        //init item
        item.Spawn(this, mg);
    }
}
