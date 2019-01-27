using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAfter : MonoBehaviour {

    [SerializeField]
    float m_FadeAfterSeconds = 3f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, m_FadeAfterSeconds);
	}
	
	// Update is called once per frame
	void Update () {
       
    }
}
