using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
	public InformationManager infoManager;

	
	void Start()
	{
		transform.Rotate (90, 0, 0);
	}
	
	private void Update()
	{
		Vector3 target = infoManager.GetTracking().worldLocation;
		Vector3 direction = target - transform.position;
		Quaternion rotation = Quaternion.LookRotation(direction);
		transform.rotation = rotation;
		transform.Rotate (270, 0, 0);
		// gameObject.active = visible;
		gameObject.SetActive(infoManager.IsTracking());
		// GetComponent<Renderer>().enabled = visible;
	}
}
