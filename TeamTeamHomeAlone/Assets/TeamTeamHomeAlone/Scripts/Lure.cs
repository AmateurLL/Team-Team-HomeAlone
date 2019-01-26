using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lure : MonoBehaviour
{
    public bool On = false;
    public float Durability = 100f;
    public int Uses = 3;
    // Update is called once per frame
    void Update()
    {
        if (Durability < 0f)
        {
            //Debug.Log("Lure Broke");
            //Destroy(this.gameObject);
            On = false;
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void OnTriggerStay(Collider _col)
    {
        if (_col.gameObject.tag == "Item" && !On && Uses > 0)
        {
            Debug.Log("Lure on");
            Uses--;
            On = true;
            this.GetComponent<CapsuleCollider>().enabled = true;
            Durability = 100f;
            Destroy(_col.gameObject);
        }
    }
}