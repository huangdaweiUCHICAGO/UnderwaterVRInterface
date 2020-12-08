﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
	
	void Start()
	{
		transform.Rotate (90, 0, 0);
	}
	
	private void Update()
	{
		Vector3 direction = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = rotation;
		transform.Rotate (270, 0, 0);
	}
}
