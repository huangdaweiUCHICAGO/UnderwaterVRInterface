using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	Renderer ballRenderer;
	
    // Start is called before the first frame update
    void Start()
    {
        ballRenderer = this.GetComponent<Renderer>();
		ballRenderer.material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
