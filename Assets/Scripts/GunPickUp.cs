using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public Rigidbody rb;
    public Collider coll;
    public float gunWeight = 0;
    public Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun.enabled = false;
    }

    // Update is called once per frame
    void Update()
    { 
    
    }
    public float PickUp(float weight, float max, Transform weaponHolder)
    {
        if (weight + gunWeight > max)
        {
            return weight;
        }

        Debug.Log("Gun picked up");

        coll.enabled = false;
        rb.isKinematic = true;

        transform.SetParent(weaponHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        gun.enabled = true;

        weight += gunWeight;
        return weight;
    }

    public float Drop(float weight)
    {
        rb.isKinematic = false;
        gun.enabled = false;
        coll.enabled = true;
        Debug.Log("Gun dropped");
        transform.parent = null;
        weight -= gunWeight;

        return weight;
    }
}
