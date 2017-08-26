using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public string MusicNumber;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    private void Awake()
    {
        Screen.SetResolution(800, 450, false);
    }
}
