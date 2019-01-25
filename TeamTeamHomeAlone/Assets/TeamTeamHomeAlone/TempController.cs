using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour {

    [SerializeField] float m_MovementSpeed = 1f;

	
	// Update is called once per frame
	void Update () {

        float h = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        this.transform.position += new Vector3(h, 0, y) * Time.deltaTime * m_MovementSpeed;


	}
}
