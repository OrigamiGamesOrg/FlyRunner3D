using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject[] obstacle;
    public Transform[] Lanes;
    public float MinDelay, MaxDelay;
    public float HalfLength;
    private GameObject player;
    private float Spawnspeed;
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
            Debug.Log("Spawn3");
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
        Instantiate(obstacle[Random.Range(0, obstacle.Length)], new Vector3(Lanes[Random.Range(0, Lanes.Length)].transform.position.x, 0.173f, zpos), Quaternion.Euler(0f,90f,0f));
    }

    IEnumerator waitonly()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(GenerateObstacles());
    }
}
