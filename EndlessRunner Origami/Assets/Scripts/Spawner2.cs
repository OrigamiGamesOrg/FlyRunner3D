using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject[] obstacle, FlyObstacle;
    public Transform[] Lanes;
    public float MinDelay, MaxDelay;
    public float HalfLength;
    private GameObject player;
    private float Spawnspeed;
    public GameObject coin;
    private float timer=0f;
    public float flyObstacleDelay=1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<PlayerController>().Stage == 1 || player.GetComponent<PlayerController>().Stage == 2)
            StartCoroutine(waitonly());


    }

    // Update is called once per frame
    void Update()
    {
        Spawnspeed = player.GetComponent<PlayerController>().speed.z;
        
       if (player.GetComponent<PlayerController>().Stage == 3)
       {
            if (timer > flyObstacleDelay)
            {
                int rand = Random.Range(0, FlyObstacle.Length);
                int rand2 = Random.Range(0, 15);
                if (rand2 >= 0 && rand2 < 11)
                {
                    GameObject go = Instantiate(FlyObstacle[rand], new Vector3((Lanes[Random.Range(0, Lanes.Length)]).transform.position.x, 2f, player.transform.position.z + HalfLength), Quaternion.Euler(0, 0, 0));
                    Destroy(go, 3f);
                    timer = 0;
                }else if (rand2 >= 11)
                {
                    GameObject coinsprefab= Instantiate(coin, new Vector3(Lanes[Random.Range(0, Lanes.Length)].transform.position.x, 2.17f, player.transform.position.z + HalfLength), Quaternion.identity);
                    Destroy(coinsprefab, 3f);
                    timer = 0;
                }
            }
            timer += Time.deltaTime;
              
       }
        

    }

    IEnumerator GenerateObstacles()
    {
        float timer = (Random.Range(MinDelay, MaxDelay)) / (Spawnspeed);
        yield return new WaitForSeconds(timer);
        CreateObstacles(player.transform.position.z + HalfLength);
        StartCoroutine("GenerateObstacles");
    }

    void CreateObstacles(float zpos)
    {
        int Randomnumber = Random.Range(0, 20);
        if(Randomnumber>=0&&Randomnumber<15)
        Instantiate(obstacle[Random.Range(0, obstacle.Length)], new Vector3(Lanes[Random.Range(0, Lanes.Length)].transform.position.x, 0.173f, zpos), Quaternion.Euler(0f,90f,0f));
        if (Randomnumber >= 15 && Randomnumber < 20)
            Instantiate(coin, new Vector3(Lanes[Random.Range(0, Lanes.Length)].transform.position.x, 0.173f, zpos), Quaternion.identity);
    }

    IEnumerator waitonly()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(GenerateObstacles());
    }

  
}
