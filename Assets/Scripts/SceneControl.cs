using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DebugManager.Instance.Create();
        FacebookManager.Instance.Create();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClearLog()
    {
        DebugManager.Instance.ClearLog();
    }

    public void FBInit()
    {
        FacebookManager.Instance.FBInit();
    }

    public void FBLogIn()
    {
        FacebookManager.Instance.FBLogIn();
    }

    public void FBLogOut()
    {
        FacebookManager.Instance.FBLogOut();
    }
}
