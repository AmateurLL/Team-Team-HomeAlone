using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaZeClAn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {        
        this.transform.Rotate(new Vector3(0f, 0.1f, 0f));
	}

    public void Play()
    {
        SceneManager.LoadScene(("MainGame"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
