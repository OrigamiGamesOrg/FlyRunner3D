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
    private bool stage3encountered = false;
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
        if (stage3encountered == false)
        {
            if (player.GetComponent<PlayerController>().Stage == 3)
            {
                
                 StartCoroutine(Wait2());
              
            }
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

    IEnumerator GenerateFlyObstacles()
    {
        float timer = 5f;
        yield return new WaitForSeconds(timer);
        CreateFlyObstacles(player.transform.position.z + HalfLength);
        StartCoroutine("GenerateFlyObstacles");
    }
    void CreateFlyObstacles(float zpos)
    {
        int Randomnumber = Random.Range(0, 20);
        if (Randomnumber >= 0 && Randomnumber < 15)
            Instantiate(FlyObstacle[Random.Range(0, obstacle.Length)], new Vector3(Lanes[Random.Range(0, Lanes.Length)].transform.position.x, 2f, zpos), Quaternion.Euler(0f, 0f, 0f));
        if (Randomnumber >= 15 && Randomnumber < 20)
            Instantiate(coin, new Vector3(Lanes[Random.Range(0, Lanes.Length)].transform.position.x,2f, zpos), Quaternion.identity);
    }
    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(GenerateFlyObstacles());
    }
}
