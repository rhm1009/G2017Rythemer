using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour {

    public TextMesh ComboNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ComboNum.text = GameObject.Find("GamePanel").GetComponent<GamePanelSet>().iCombo.ToString();
	}
}
