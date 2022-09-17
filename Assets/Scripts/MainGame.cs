using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public int currentCustomer;

    public float conveyorSpeed;
    public float customerWait;

    public Vector3 clientSpawnPos;
    public Client client;


    public List<Item> items = new List<Item>();
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
        


    }

    public void SpawnClient()
    {
        currentCustomer++;
      Client c =Instantiate(client, clientSpawnPos, Quaternion.identity);
        Item randomitem = items[Random.Range(0, items.Count)];
        c.init(randomitem);
    }
}
