using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit tempHit;
        Physics.Raycast(tempRay, out tempHit);
        if (tempHit.collider != null)
        {
            Vector3 tempStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.DrawLine(tempStart, tempHit.point, Color.red);
        }
	}
}
