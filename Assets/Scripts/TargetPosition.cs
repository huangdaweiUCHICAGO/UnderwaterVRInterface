using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPosition : MonoBehaviour {

    Vector3 targetScreenPos;
    Vector3 camMidPos;
    public Transform enemyPrefab;
    Image im;

    void Start () {
        im = GetComponent<Image>();
        camMidPos = new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0) / 2f;
	}
	
	void Update () {
        targetScreenPos = Camera.main.WorldToScreenPoint(enemyPrefab.position);
        Debug.Log(targetScreenPos);
        if (targetScreenPos.x < 0 || targetScreenPos.y < 0 || targetScreenPos.x > Screen.width || targetScreenPos.y > Screen.height)
            pointingArrow();
        else
            im.enabled = false;
	}

    void pointingArrow () {
        im.enabled = true;
        Vector3 dir = (targetScreenPos - camMidPos).normalized;
        float yDist = camMidPos.y;
        float xDist = camMidPos.x;
        float xMult = Mathf.Abs(xDist / dir.x);
        float yMult = Mathf.Abs(yDist / dir.y);

        float multiplier = Mathf.Min(xMult, yMult);
        // Debug.Log(targetScreenPos);

        transform.position = camMidPos + dir * multiplier;
    }
}