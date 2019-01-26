using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSS_GameManager : MonoBehaviour {

    static public CSS_GameManager instance;
    public bool m_bGameOver = false;


    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Destroy()
    {
        if (instance == this)
            instance = null;
    }

    void update()
    {
        //Debug
        QuitGame();
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
	
}
