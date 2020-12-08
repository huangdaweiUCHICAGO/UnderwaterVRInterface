using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    private Transform backgroundTransform;
    public Transform player;

    private Vector3 northDirection = Vector3.zero;
    private float prevAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        backgroundTransform = GameObject.Find("Minimap Background").GetComponent<Transform>();
        prevAngle = player.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = - (prevAngle - player.transform.eulerAngles.y);
        
        backgroundTransform.Rotate(new Vector3(0f, 0f, rotation));
        prevAngle = player.transform.eulerAngles.y;
    }
}
