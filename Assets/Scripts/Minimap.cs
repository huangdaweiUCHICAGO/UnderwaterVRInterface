using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject markerPrefab;

    public CallTowerManager towerManager;
    public InformationManager infoManager;

    private Transform backgroundTransform;
    private Transform northArrowTransform;
    private Transform maskTransform;
    public Transform player;


    private Vector3 mapOrigPos;
    private float windowSize = 150;
    private float scale;
    private float origAngle;

    private ArrayList oldMarkers;

    // Start is called before the first frame update
    void Start()
    {
        backgroundTransform = GameObject.Find("Minimap Background").GetComponent<Transform>();
        maskTransform = GameObject.Find("Minimap Mask").GetComponent<Transform>();
        northArrowTransform = GameObject.Find("North Arrow").GetComponent<Transform>();
        

        //Reset frame of reference
        backgroundTransform.rotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        northArrowTransform.rotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        mapOrigPos = player.transform.position;

        scale = 100 / windowSize;
        oldMarkers = new ArrayList();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransforms();
        DestroyMarkers();
        AddMarkers();

    }

    void UpdateTransforms()
    {
        Vector3 positionDifference = (player.transform.position - mapOrigPos) * scale;
        positionDifference.y = positionDifference.z % 110;
        positionDifference.x %= 110;
        positionDifference.z = 0f;

        backgroundTransform.localPosition = -positionDifference;

        origAngle = player.transform.eulerAngles.y;
        maskTransform.localRotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        northArrowTransform.localRotation = Quaternion.Euler(0f, 0f, player.transform.eulerAngles.y);
        
    }

    void DestroyMarkers()
    {
        foreach (GameObject marker in oldMarkers)
        {
            Destroy(marker);
        }
        oldMarkers = new ArrayList();
    }

    void AddMarkers()
    {
        oldMarkers = new ArrayList();

        foreach (CrewInfo crew in towerManager.GetCrewmatesInformation())
        {
            Vector3 target = crew.worldLocation;
            target.y = player.transform.position.y;
            bool isTarget = infoManager.IsTracking() && crew.name.Equals(infoManager.GetTracking().name);
            
            Vector3 direction = target - player.transform.position;

            float dist = direction.magnitude * scale;
            float theta = Mathf.Atan2(direction.x, direction.z) * 180 / Mathf.PI;
            theta -= player.transform.eulerAngles.y;

            if (dist > 50)
            {
                dist = 60;    
            }
            
            float y = dist * Mathf.Cos(theta * Mathf.PI / 180);
            float x = dist * Mathf.Sin(theta * Mathf.PI / 180);
            GameObject marker = CreateMarker(crew.name, x, y, isTarget);
            

            oldMarkers.Add(marker);
        }        

    }

    private GameObject CreateMarker(string text, float x, float y, bool isSelected)
    {
        Vector3 markerPos = new Vector3(x, y, 0f);
        GameObject marker = Instantiate(markerPrefab);
        MapMarker markerScript = marker.GetComponent<MapMarker>();
        if (isSelected)
        {
            markerScript.SetSelected();
        }
        markerScript.SetText(text);

        marker.transform.SetParent(this.transform);
        //marker.transform.localScale = new Vector3(1, 1, 1);
        marker.transform.localPosition = markerPos;
        return marker;
    }
}
