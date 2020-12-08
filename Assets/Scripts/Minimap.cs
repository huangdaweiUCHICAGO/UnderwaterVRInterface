using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    private Transform backgroundTransform;
    private Transform northArrowTransform;
    private Transform greenArrowTransform;
    public Transform player;


    private Vector3 mapOrigPos;

    // Start is called before the first frame update
    void Start()
    {
        backgroundTransform = GameObject.Find("Minimap Background").GetComponent<Transform>();
        northArrowTransform = GameObject.Find("North Arrow").GetComponent<Transform>();
        greenArrowTransform = GameObject.Find("Arrow").GetComponent<Transform>();

        //Reset frame of reference
        backgroundTransform.rotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        northArrowTransform.rotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        mapOrigPos = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransforms();
        

    }

    void UpdateTransforms()
    {
        Vector3 positionDifference = player.transform.position - mapOrigPos;
        positionDifference.y = positionDifference.z % 110;
        positionDifference.x %= 110;
        positionDifference.z = 0f;

        backgroundTransform.localPosition = -positionDifference;


        this.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        greenArrowTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
