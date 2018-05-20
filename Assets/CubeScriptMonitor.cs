using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScriptMonitor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var cubeObj = GameObject.Find("Cube");
            cubeObj.RemoveKiteAnimation<KiteCommonAnimation>();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            using (new KiteAnimationSkipSetter())
            {
                var cubeObj = GameObject.Find("Cube");
                var animation = cubeObj.AddKiteAnimation<KiteCommonAnimation>();
                animation.Duration = 3.0f;
                animation.NeedLinearLerpEuler = true;
                animation.CompleteCallBack = (callbackType, obj, percent) =>
                {
                    Debug.LogErrorFormat("Callback type is {0}, obj name is {1}, percent is {2}", callbackType, obj.name, percent);
                };
                animation.StartKey = new KiteAnimationKeyInfo() { Position = new Vector3(0, 0, 0), Rotation = new Vector3(0, 0, 0) };
                animation.EndKey = new KiteAnimationKeyInfo() { Position = new Vector3(4, 4, 4), Rotation = new Vector3(0, 1080, 0) };
                animation.Play();
            }
        }
	}
}
