using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debuger.Log(NetUtils.SelfIP);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
