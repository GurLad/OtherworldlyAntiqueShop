using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    public MainGame mg;
    public Item item;
   public void init(Item item)
    {
        mg = FindObjectOfType<MainGame>();
        this.item = item;
        //create item
        //init item
    }

    private void FixedUpdate()
    {
       transform.position += new Vector3( Time.fixedDeltaTime * mg.conveyorSpeed,0,0);

    }

    public void Leave()
    {

    }


}
