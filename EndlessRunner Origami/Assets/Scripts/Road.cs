using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Transform otherRoad;
    private Transform player;
    public float HalfLength=5f;
    public float endoffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z + HalfLength < player.transform.position.z - endoffset)
        {
            transform.position = new Vector3(otherRoad.position.x, otherRoad.position.y, otherRoad.position.z + HalfLength * 2);
        }
    }
}
