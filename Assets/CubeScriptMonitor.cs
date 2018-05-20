using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScriptMonitor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A))
        {
            var cube = GameObject.Find("Cube");
            Debug.LogError(cube.GetKiteAnimation<KiteCommonAnimation>());
            cube.RemoveKiteAnimation<KiteCommonAnimation>();
            Debug.LogError(cube.GetKiteAnimation<KiteCommonAnimation>());
        }
	}
}
