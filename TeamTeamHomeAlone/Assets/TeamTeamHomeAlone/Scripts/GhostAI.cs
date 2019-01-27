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
    public int Target = 0;
    public bool CaughtInUV = false;
    public bool Beamed = false;
    NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        Target = Random.Range(0, 3);
        agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
        //Debug.Log("Ghost Spawned! Target:" + Target);
    }

    // Update is called once per frame
    void Update()
    {
        Tic += Time.deltaTime;

        if (PopProgress > 100f)
        {
            //do stuff
        }

        if (!CaughtInUV)
        {//not caught in uv light            
            agent.speed = 3.5f;
            agent.acceleration = 8f;

            if (PopProgress > 0.0f && Tic > 1.0f)//not in light, lower pop progress
            {
                PopProgress--;
            }
            this.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else
        {//caught
            //VectorMultiplier = Mathf.Sin(Time.time) / 100;// ghost bobbing
            //GhostBody.transform.position += new Vector3(0, VectorMultiplier, 0);
            if (Beamed)
            {
                VectorMultiplier = 0.02f;
                GhostBody.transform.localScale += (new Vector3(VectorMultiplier, VectorMultiplier, VectorMultiplier));
                PopProgress += Time.deltaTime * 100;
                agent.velocity = new Vector3(0f, 0f, 0f);
                agent.speed = 0f;
                agent.acceleration = 0f;
            }            
            if (PopProgress > 100f)
            {
               Pop();
            }
            this.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = true;
        }

        if (Tic > 1.0f)
        {
            agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
            Tic = 0f;
        }
    }
   
    void OnTriggerStay(Collider _col)
    {
        //Debug.Log("Ghost Colliding");
        if (_col.gameObject.tag == "Lure" && _col.GetComponent<Lure>().On)
        {
            if (_col.gameObject.GetComponent<Lure>().Durability < 0f)
            {
                Target++;
                if (Target > 2)
                {
                    Target = 0;
                }
                agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
            }
            else
            {
                //navmesh target changed to lure
                agent.destination = _col.gameObject.transform.position;
                //Debug.Log("Breaking Lure");
                _col.gameObject.GetComponent<Lure>().Durability -= 0.1f;
                if (_col.gameObject.GetComponent<Lure>().Durability < 0f)
                {
                    agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
                    // Debug.Log("Lure Broke! Target:" + Target);
                }
            }
        }
        else if (_col.gameObject.tag == "Target")
        {
            if (_col.gameObject.GetComponent<Target>().Durability < 0f)
            {
                Target++;
                if (Target > 2)
                {
                    Target = 0;
                }
                agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
            }
            else
            {
                _col.gameObject.GetComponent<Target>().Durability -= 0.1f;
                if (_col.gameObject.GetComponent<Target>().Durability < 0f)
                {
                    _col.gameObject.tag = "BrokenTarget";
                    Target = Random.Range(0, 3);
                    agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
                    //Debug.Log("Target Destroyed! New Target:" + Target);
                }
            }
        }
        else if (_col.gameObject.tag == "BrokenTarget" && Tic > 1.0f)
        {
            Target = Random.Range(0, 3);
            agent.destination = GhostManager.instance.GhostTargets[Target].transform.position;
        }
        else if (_col.gameObject.tag == "Ghost")
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), _col);
        }
        else if (_col.gameObject.tag == "Flashlight" && _col.gameObject.GetComponent<Light>().enabled)
        {
            //   Debug.Log("Ghost stuck in light");
            this.gameObject.GetComponent<GhostAI>().CaughtInUV = true;
            Beamed = _col.transform.parent.parent.parent.GetComponent<CSS_Player>().Firing;
        }
    }
    void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Flashlight" && _col.gameObject.GetComponent<Light>().enabled)
        {
            //Debug.Log("Ghost entered light");
            this.gameObject.GetComponent<GhostAI>().CaughtInUV = true;
        }
    }
    void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject.tag == "Flashlight" && _col.gameObject.GetComponent<Light>().enabled)
        {
            //Debug.Log("Ghost left light");
            this.gameObject.GetComponent<GhostAI>().CaughtInUV = false;
        }
    }
    void Pop()
    {
        //Debug.Log("Popped Ghost!");
        if (FindObjectsOfType<GhostAI>().Length == 1)
        {
            GhostManager.instance.SpawnWaveDelay();
        }
        Destroy(this.gameObject);
    }
}