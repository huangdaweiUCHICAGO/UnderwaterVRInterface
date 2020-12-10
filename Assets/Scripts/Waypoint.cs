using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{

	private Image iconImg;
	private Text distanceText;
	
	public Transform player;
	
	public Camera cam;
	private Vector3 target;
	
	public float closeEnoughDist;
    // Start is called before the first frame update
    void Start()
    {
        iconImg = GetComponent<Image>();
		distanceText = GetComponentInChildren<Text>();
    }

	// Update is called once per frame
	public InformationManager infoManager;
	void Update()
    {
		if (infoManager.IsTracking())
		{
			ToggleUI(true);
			target = infoManager.GetTracking().worldLocation;
			GetDistance();
			CheckOnScreen();
		} else
        {
			ToggleUI(false);
		}
    }
	
	private void GetDistance()
	{
		Vector3 adjustedPosition = target;
		adjustedPosition.y = player.position.y;
		float dist = Vector3.Distance(player.position, target);
		float adjustedDist = Vector3.Distance(player.position, adjustedPosition);
		
		distanceText.text = dist.ToString("f0") + " m";
		if(adjustedDist <= closeEnoughDist)
        {
			string targetName = infoManager.GetTracking().name;
			Debug.Log(targetName + " found!");
			
			infoManager.ClearTracking(true);

        }

	}
	
	private void CheckOnScreen()
	{
		float thing = Vector3.Dot((target - cam.transform.position).normalized, cam.transform.forward);
		if (thing <= 0)
		{
			ToggleUI(false);
		}
		else{
			ToggleUI(true);
			transform.position = cam.WorldToScreenPoint(target);
		}
	}
	
	private void ToggleUI(bool _value)
	{
		iconImg.enabled = _value;
		distanceText.enabled = _value;
		// distanceText.gameObject.SetActive(_value);
	}
}
