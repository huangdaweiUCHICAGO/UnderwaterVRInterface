using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForces : MonoBehaviour
{
    Rigidbody ballRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = this.GetComponent<Rigidbody>();

        Vector3 randomForce = 25.0f * Random.insideUnitSphere;
        ballRigidBody.AddForce(randomForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
