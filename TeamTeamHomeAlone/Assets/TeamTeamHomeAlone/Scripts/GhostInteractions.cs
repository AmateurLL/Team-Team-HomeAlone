using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractions : MonoBehaviour {


    [Header("Needed References", order = 1)]
    [SerializeField]
    [Tooltip("The Sphere Collider used for interactions")]
    SphereCollider m_InteractCollider; 

    [Header("Ghost Variables", order = 2)]
    [Range(1f, 10f)]
    [Tooltip("The Range of the 'pushing' trigger box")]
    [SerializeField]
    float m_PushRange = 2f;
    [Range(5f, 50f)]
    [Tooltip("How hard the ghost 'pushes' on 'Moveable' objects around it")]
    [SerializeField]
    float m_PushMultiplier = 1f;

    [Tooltip("The type of force exerted on 'Moveable' objects")]
    [SerializeField]
    ForceMode m_ForceMode;

    //A list of objects that the ghost can interact with in various ways
    [SerializeField]
    List<GameObject> m_InteractableObjects = new List<GameObject>();



    private bool PushWaveActivated = false;

    void OnDestroy() {
        foreach (var _object in m_InteractableObjects)
        {
            if (_object.tag.Contains("Moveable"))
            {
                _object.tag = "Moveable";
                _object.GetComponent<Rigidbody>().useGravity = true;
            }
            else if (_object.CompareTag("FlickeringLight"))
            {
                _object.GetComponent<LightFlicker>().StopFlickering();
            }
        }

        int rand = Random.Range(1, 5);

        switch (rand)
        {
            case 1:
                DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.GhostDeath1);
                break;
            case 2:
                DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.GhostDeath2);
                break;
            case 3:
                DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.GhostDeath3);
                break;
            case 4:
                DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.GhostDeath4);
                break;
            default:
                break;
        }
         
    }


    void Start() {
        if (m_InteractCollider == null) {
            Debug.LogError("Missing Variable Reference for Sphere Collider - Ghost");
            this.enabled = false;
            Destroy(this.gameObject, 3f);
        }


        m_InteractCollider.radius = m_PushRange;
    }

	// Update is called once per frame
	void Update () {
        //Debug.Log("Time = " + Time.time);
        //Debug.Log("Sin = " + Mathf.Sin((180f * Time.time) * Mathf.Deg2Rad));

        if (m_InteractableObjects.Count > 0)
        {
            //Calculate push force and apply to all 'Moveable_Push' Objects
            float Pushforce = (Mathf.Sin((90f * Time.time) * Mathf.Deg2Rad)) * m_PushMultiplier * Time.deltaTime;

            //Only pushes if it's larger than 0, gives a pulsing effect
            if (Pushforce > 0f)
            {
                PushWaveActivated = true;

                foreach (var _object in m_InteractableObjects)
                {
                    if (_object.tag.Contains("Moveable"))
                    {

                        switch (_object.tag)
                        {
                            case "Moveable":
                                int rand = Random.Range(1, 4);
                                if (rand == 1 || rand == 2)
                                    _object.tag = "Moveable_Push";
                                else
                                {
                                    _object.tag = "Moveable_Float";
                                    _object.GetComponent<Rigidbody>().useGravity = false;
                                    float randX = Random.Range(-45f, 45f);
                                    float randY = Random.Range(-45f, 45f);
                                    float randZ = Random.Range(-45f, 45f);
                                    _object.GetComponent<Rigidbody>().angularVelocity += new Vector3(randX, randY, randZ);
                                }
                                break;


                            case "Moveable_Push":
                                //The closer the object, the more force is applied
                                float DistanceRatio = 1f - ((_object.transform.position - this.transform.position).magnitude / m_PushRange);

                                //The vector between the ghost and the current object
                                Vector3 pushVector = (_object.transform.position - this.transform.position).normalized * Pushforce * DistanceRatio;
                                _object.GetComponent<Rigidbody>().AddForce(pushVector, m_ForceMode);
                                break;

                            case "Moveable_Float":
                                float DistanceRatioFloat = 1f - ((_object.transform.position - this.transform.position).magnitude / m_PushRange);

                                Vector3 floatVector = Vector3.up * Pushforce * DistanceRatioFloat * 1.5f;
                                _object.GetComponent<Rigidbody>().AddForce(floatVector);

                                break;

                            default:
                                break;
                        }



                    }
                    else {
                        if (_object.CompareTag("FlickeringLight")) {
                            //This object if a flickering Spot Light
                            _object.GetComponent<LightFlicker>().StartFlickering();
                            Debug.Log("Start Flciker");

                        }




                    }
                }
            }
            else {
                //Pushing Force has ended. Reset List of objects
                if (PushWaveActivated) {
                    PushWaveActivated = false;
                    foreach (var _object in m_InteractableObjects)
                    {
                        if (_object.tag.Contains("Moveable"))
                        {
                            _object.tag = "Moveable";
                            _object.GetComponent<Rigidbody>().useGravity = true;
                        }
                        else if (_object.CompareTag("FlickeringLight")) {
                            _object.GetComponent<LightFlicker>().StopFlickering();
                        }
                    }
                }


            }


        }


        //For Testing
        m_InteractCollider.radius = m_PushRange;
    }



    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag.Contains("Moveable")) {
            //Add the object to the list of interactable objects for this ghost
            collider.gameObject.tag = "Moveable";
            m_InteractableObjects.Add(collider.gameObject);

        }

        if (collider.gameObject.CompareTag("FlickeringLight"))
        {
            m_InteractableObjects.Add(collider.gameObject);
            Debug.Log("added Spotlight");
        }

    }



    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag.Contains("Moveable")) {
            //Remove the object from the list of interactable objects for this ghost
            collider.gameObject.tag = "Moveable";
            collider.gameObject.GetComponent<Rigidbody>().useGravity = true;
            m_InteractableObjects.Remove(collider.gameObject);
            
         }


        if (collider.gameObject.CompareTag("FlickeringLight"))
        {
            m_InteractableObjects.Remove(collider.gameObject);
            collider.gameObject.GetComponent<LightFlicker>().StopFlickering();
        }

    }


    //// Gizmos /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_InteractCollider.gameObject.transform.position, m_PushRange);
    }

}
