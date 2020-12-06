using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public GameObject bouncyBallPrefab;
    public int numBalls = 5;
    public float height = 50f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numBalls; i++)
        {
            Vector3 ballPos = new Vector3(Random.Range(-10f, 10f), height, Random.Range(-10f, 10f));
            Instantiate(bouncyBallPrefab, ballPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
