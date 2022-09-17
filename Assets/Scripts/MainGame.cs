using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public int currentCustomer;

    public float conveyorSpeed;
    public float customerWait;

    public Vector3 clientSpawnPos;
    public Vector3 itemSpawnPos;


    public List<Client> clients = new List<Client>();

    public List<Item> items = new List<Item>();


    private int counter = 0;
    


    private void Start()
    {
        StartCoroutine(MainGameLoop());
    }
    public IEnumerator MainGameLoop()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            //grace period
            SpawnClient();
            
            //new client wait
            yield return new WaitForSeconds(customerWait);

            if (currentCustomer % 5 == 0)
            {
                //values will change
                conveyorSpeed += (0.2f);
                customerWait -= (0.2f);
                customerWait = Mathf.Clamp(customerWait, 0.8f, 3f);
                conveyorSpeed = Mathf.Clamp(conveyorSpeed, 1.5f, 8f);
                // Music change
                if (++counter < 5)
                {
                    CrossfadeMusicPlayer.Instance.Play("Main" + counter);
                }

            }
        }
        


    }

    public void SpawnClient()
    {
        currentCustomer++;
        Client c =Instantiate(clients[Random.Range(0,clients.Count)], clientSpawnPos, Quaternion.identity);
        Item randomitem = items[Random.Range(0, items.Count)];
        Debug.Log(randomitem.name);
        c.Init(randomitem);
    }
}
