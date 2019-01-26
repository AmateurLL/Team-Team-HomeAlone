using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Player : MonoBehaviour {

    float fThrowForce = 666;
    public GameObject m_PickedUpObj;
    public GameObject m_LeftHand;
    private bool m_bLeftHandFree =  true;
    public Collider m_PickUpArea;

    void start()
    {
        m_PickUpArea = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (m_bLeftHandFree == false)
            {
                //Throw
                Throw();
                //Debug.Log("Fast BALL!!");

                //PutDown Neatly
                

            }
            else
            {
                PickUp();
                //Debug.Log("Touch!!!");
            }
            
        }

        //if (!m_bLeftHandFree && m_PickedUpObj)
        //{
        //    m_PickedUpObj.transform.position = m_LeftHand.transform.position;
        //}
        this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ConeCollider>().enabled = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Light>().enabled;
    }

    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Item" || Col.gameObject.tag == "Moveable" || Col.gameObject.tag == "Lure")
        {
            if (!m_PickedUpObj)
            {
                m_PickedUpObj = Col.gameObject;
                //Debug.Log("Looking at = " + m_PickedUpObj);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Item" && col.gameObject == m_PickedUpObj ||
            col.gameObject.tag == "Moveable" && col.gameObject == m_PickedUpObj ||
            col.gameObject.tag == "Lure" && col.gameObject == m_PickedUpObj)
        {
            if(m_bLeftHandFree == true)
            {
                m_PickedUpObj = null;
            }

        }
    }

    private void PickUp()
    {
        if (!m_PickedUpObj)
        {
            return;
        }

        

        // Setting Physical properties
        if (m_PickedUpObj.tag == "Item" || m_PickedUpObj.tag == "Moveable")
        {
            // Setting Position
            m_PickedUpObj.transform.SetParent(m_LeftHand.transform);
            m_PickedUpObj.transform.localRotation = m_LeftHand.transform.rotation;
            m_PickedUpObj.transform.position = m_LeftHand.transform.position;
            m_PickedUpObj.GetComponent<Rigidbody>().useGravity = false;
            m_PickedUpObj.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            m_PickedUpObj.GetComponent<Collider>().enabled = false;
        }
        else if (m_PickedUpObj.tag == "Lure")
        {

            // Setting Position
            m_PickedUpObj.transform.SetParent(m_LeftHand.transform);
            m_PickedUpObj.transform.localRotation = m_LeftHand.transform.rotation;
            m_PickedUpObj.transform.position = m_LeftHand.transform.position;
            m_PickedUpObj.GetComponent<Rigidbody>().useGravity = false;
            m_PickedUpObj.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            m_PickedUpObj.GetComponent<BoxCollider>().enabled = false;
        }
        

        m_bLeftHandFree = false;
        Debug.Log("PickingUp");
    }

    private void Throw()
    {
        if (!m_PickedUpObj)
        {
            return;
        }
        m_PickedUpObj.transform.SetParent(null);

        if (m_PickedUpObj.tag == "Item" || m_PickedUpObj.tag == "Moveable")
        {
            m_PickedUpObj.GetComponent<Rigidbody>().useGravity = true;
            m_PickedUpObj.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            m_PickedUpObj.GetComponent<Collider>().enabled = true;
            m_PickedUpObj.GetComponent<Rigidbody>().AddForce(m_LeftHand.transform.forward * fThrowForce);
        }
        else if (m_PickedUpObj.tag == "Lure")
        {
            m_PickedUpObj.GetComponent<Rigidbody>().useGravity = true;
            m_PickedUpObj.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            m_PickedUpObj.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            m_PickedUpObj.GetComponent<BoxCollider>().enabled = true;
        }
        
        m_PickedUpObj = null;


        m_bLeftHandFree = true;
    }

}
