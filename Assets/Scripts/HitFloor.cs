using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
    Renderer ballRenderer;
    // Start is called before the first frame update
    void Start()
    {
        ballRenderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (this.transform.position.y == 0f) {
            Debug.Log("hit");
        } */
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            ballRenderer.material.color = Color.blue;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        ballRenderer.material.color = Color.green;
        
    }

    void OnTriggerExit(Collider collider)
    {
        ballRenderer.material.color = Color.gray;
        
    }
}
