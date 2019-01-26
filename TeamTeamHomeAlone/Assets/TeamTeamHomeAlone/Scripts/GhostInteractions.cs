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
            //Calculate push force and apply to all 'Moveable' Objects
            float Pushforce = (Mathf.Sin((90f * Time.time) * Mathf.Deg2Rad)) * m_PushMultiplier * Time.deltaTime;

            //Only pushes if it's larger than 0, gives a pulsing effect
            if (Pushforce > 0f)
            {
                foreach (var _object in m_InteractableObjects)
                {
                    if (_object.CompareTag("Moveable"))
                    {
                        //The closer the object, the more force is applied
                        float DistanceRatio = 1f - ((_object.transform.position - this.transform.position).magnitude / m_PushRange);

                        //The vector between the ghost and the current object
                        Vector3 pushVector = (_object.transform.position - this.transform.position).normalized * Pushforce * DistanceRatio;
                        _object.GetComponent<Rigidbody>().AddForce(pushVector, m_ForceMode);
                    }
                }
            }


        }


        //For Testing
        m_InteractCollider.radius = m_PushRange;
    }



    void OnTriggerEnter(Collider collider) {
        Debug.Log("Blah");
        if (collider.gameObject.CompareTag("Moveable")) {
            //Add the object to the list of interactable objects for this ghost
            m_InteractableObjects.Add(collider.gameObject);

        }

    }



    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.CompareTag("Moveable")) {
            //Remove the object from the list of interactable objects for this ghost
            m_InteractableObjects.Remove(collider.gameObject);
            
         }

    }


    //// Gizmos /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_InteractCollider.gameObject.transform.position, m_PushRange);
    }

}
