using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour {

    public float Durability = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Durability < 0f)
        {
            Debug.Log("Lure Broke");
            Destroy(this.gameObject);
        }
    }
}