using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    [SerializeField] GameObject GhostObject;
    public GameObject[] GhostTargets = new GameObject[3];
    public GameObject[] GhostSpawners = new GameObject[3];
    static public GhostManager instance;
    int LastWaveCount = 0;
    int NewWaveCount = 1;
    public int SpawnWaveCount;
    // Use this for initialization
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

    // Update is called once per frame
    void Update()
    {
        if (GhostTargets[0].GetComponent<Target>().Durability <= 0.0f && GhostTargets[1].GetComponent<Target>().Durability <= 0.0f && GhostTargets[2].GetComponent<Target>().Durability <= 0.0f)
        {
            Debug.Log("Ghosts Win!");
            CSS_GameManager.instance.RestartGame();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P)) { SpawnGhost(3); }
        }
    }

    public void SpawnWaveDelay()
    {
        Debug.Log("Spawn Wave Delay - 5 Seconds");
        Invoke("SpawnWave", 0.05f);
    }

    void SpawnWave()
    {
        Debug.Log("Spawn Wave");
        SpawnWaveCount = NewWaveCount + LastWaveCount;
        SpawnGhost(SpawnWaveCount);
        NewWaveCount = NewWaveCount + LastWaveCount;
        LastWaveCount = NewWaveCount - LastWaveCount;
    }

    void SpawnGhost(int i)
    {
        int randomspawner;
        for (int j = 0; j < i; j++)
        {
            randomspawner = Random.Range(1, 6);
            Debug.Log(randomspawner);
            switch (randomspawner)
            {
                case 1:
                    {
                        Instantiate(GhostObject, GhostSpawners[0].transform.position, Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(GhostObject, GhostSpawners[1].transform.position, Quaternion.identity);
                        break;
                    }
                case 3:
                    {
                        Instantiate(GhostObject, GhostSpawners[2].transform.position, Quaternion.identity);
                        break;
                    }
                case 4:
                    {
                        Instantiate(GhostObject, GhostSpawners[3].transform.position, Quaternion.identity);
                        break;
                    }
                case 5:
                    {
                        Instantiate(GhostObject, GhostSpawners[4].transform.position, Quaternion.identity);
                        break;
                    }
                default: break;
            }
        }
    }
}