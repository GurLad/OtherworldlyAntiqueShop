using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public int currentCustomer;

    public float conveyorSpeed;
    public float customerWait;

    public IEnumerator MainGameLoop()
    {
        while (true)
        {
            //grace period
            yield return new WaitForSeconds(1);
            SpawnClient();
            
            //new client wait
            yield return new WaitForSeconds(5);

            if (currentCustomer % 5 == 0)
            {
                //values will change
                conveyorSpeed += (0.5f/currentCustomer);
                customerWait -= (5/currentCustomer);

            }
        }
        //client comes up 
        //gives item 
        //initializes item so he moves with it 


        //every x seconds a new  client comes 


    }

    public void SpawnClient()
    {
        currentCustomer++;
    }
}
