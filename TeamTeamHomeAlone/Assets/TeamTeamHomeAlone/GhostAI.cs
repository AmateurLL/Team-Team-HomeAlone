using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{
    [SerializeField] private float PopProgress = 0.0f;
    [SerializeField] private float Tic = 0.0f;
    [SerializeField] float VectorMultiplier;
    [SerializeField] GameObject GhostBody;
    public bool CaughtInUV = false;
    NavMeshAgent agent;
    
	// Use this for initialization
	void Start ()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        int h = Random.Range(0, 3);
        agent.destination = GhostManager.instance.GhostTargets[h].transform.position;
        Debug.Log("Ghost Spawned! Target:" + h);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Tic += Time.deltaTime;

        if (PopProgress > 100f)
        {
            //do stuff
        }       

        if (!CaughtInUV)
        {//caught in uv light
            VectorMultiplier = Mathf.Sin(Time.time) / 100;// ghost bobbing
            GhostBody.transform.position += new Vector3(0, VectorMultiplier, 0);

            if (PopProgress > 0.0f && Tic > 1.0f)//not in light, lower pop progress
            {
                PopProgress--;
            }
        }
        else
        {//not caught
            VectorMultiplier = 0.01f;
            GhostBody.transform.localScale += (new Vector3(VectorMultiplier, VectorMultiplier, VectorMultiplier));
            PopProgress += Time.deltaTime * 100;
            if (PopProgress > 100f)
            {
                Pop();
            }
        }

        if (Tic > 1.0f)
        {
            Tic = 0f;
        }
	}

    void OnTriggerStay(Collider _col)
    {
        Debug.Log("Ghost Colliding");
        if (_col.gameObject.tag == "Lure")
        {
            //navmesh target changed to lure
            agent.destination = _col.gameObject.transform.position;
            Debug.Log("Breaking Lure");
            _col.gameObject.GetComponent<Lure>().Durability -= 0.1f;
            if (_col.gameObject.GetComponent<Lure>().Durability < 0f)
            {
                int h = Random.Range(0, 3);
                agent.destination = GhostManager.instance.GhostTargets[h].transform.position;
                Debug.Log("Lure Broke! Target:" + h);
            }
        }
        else if (_col.gameObject.tag == "Trap")
        {

        }
        else if (_col.gameObject.tag == "Target")
        {

        }
        else if (_col.gameObject.tag == "Ghost")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), _col);
        }
    }    

    void Pop()
    {
        Debug.Log("Popped Ghost!");
        //Destroy(this.gameObject);
    }
}