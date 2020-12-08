using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    private Transform backgroundTransform;
    private Transform northArrowTransform;
    public Transform player;

    private Vector3 northDirection = Vector3.zero;
    private float prevAngle = 0f;
    private Vector3 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        backgroundTransform = GameObject.Find("Minimap Background").GetComponent<Transform>();
        northArrowTransform = GameObject.Find("North Arrow").GetComponent<Transform>();
        prevAngle = player.transform.eulerAngles.y;
        prevPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = - (prevAngle - player.transform.eulerAngles.y);

        //backgroundTransform.localRotation = Quaternion.Euler(player.transform);

        backgroundTransform.Rotate(new Vector3(0f, 0f, rotation));
        northArrowTransform.Rotate(new Vector3(0f, 0f, rotation));
        
        Vector3 positionDifference = (prevPosition - player.transform.position);
        positionDifference.y = positionDifference.z;
        positionDifference.z = 0f;
        //Debug.Log(positionDifference);
        backgroundTransform.Translate(positionDifference);
        
        prevAngle = player.transform.eulerAngles.y;
        prevPosition = player.position;
    }
}
