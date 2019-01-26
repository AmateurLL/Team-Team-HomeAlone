using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ConeCollider>().enabled = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Light>().enabled;

    }
}
