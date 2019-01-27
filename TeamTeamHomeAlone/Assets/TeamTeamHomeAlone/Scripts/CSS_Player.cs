using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class CSS_Player : MonoBehaviour
{

    float fThrowForce = 666;
    public GameObject m_PickedUpObj;
    public GameObject m_LeftHand;
    private bool m_bLeftHandFree = true;
    public Collider m_PickUpArea;
    public bool Firing = false;

    [SerializeField]
    AudioSource WeaponLoop;

    void start()
    {
        m_PickUpArea = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (m_bLeftHandFree == false)
            {
                Throw();
                DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.Throw);
            }
            else
            {
                PickUp();

            }
        }

        if (this.GetComponent<FirstPersonController>().m_bTorchLight)
        {
            this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ConeCollider>().m_angle = 10f;//transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Light>().enabled;
            Firing = true;

            //Unmute to play the sound
            WeaponLoop.mute = false;
        }
        else
        {
            this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ConeCollider>().m_angle = 25f;//transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Light>().enabled;
            Firing = false;

            //Mute while not firing
            WeaponLoop.mute = true;
        }
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Item" || Col.gameObject.tag == "Moveable" || Col.gameObject.tag == "Lure")
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
        if (col.gameObject.tag == "Item" && col.gameObject == m_PickedUpObj ||
            col.gameObject.tag == "Moveable" && col.gameObject == m_PickedUpObj ||
            col.gameObject.tag == "Lure" && col.gameObject == m_PickedUpObj)
        {
            if (m_bLeftHandFree == true)
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

            DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.PickUp);

            m_bLeftHandFree = false;
        }
        else if (m_PickedUpObj.tag == "Lure")
        {
            if (m_PickedUpObj.GetComponent<Lure>().On == false)
            {
                // Setting Position
                m_PickedUpObj.transform.SetParent(m_LeftHand.transform);
                m_PickedUpObj.transform.localRotation = m_LeftHand.transform.rotation;
                m_PickedUpObj.transform.position = m_LeftHand.transform.position;
                m_PickedUpObj.GetComponent<Rigidbody>().useGravity = false;
                m_PickedUpObj.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
                m_PickedUpObj.GetComponent<BoxCollider>().enabled = false;

                DigitalRuby.SoundManagerNamespace.SoundMusicPlayer.Instance.PlaySound(SoundEffects.PickUp);

                m_bLeftHandFree = false;
            }
        }



        //Debug.Log("PickingUp");
    }

    private void Throw()
    {
        if (!m_PickedUpObj)
        {
            return;
        }


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
        m_PickedUpObj.transform.SetParent(null);
        m_PickedUpObj = null;
        m_bLeftHandFree = true;
    }
}