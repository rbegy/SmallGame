using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public Rigidbody rb;
    public Collider coll;
    public float gunWeight = 0;
    public Gun gun;
    public GunType gunType;
    public enum GunType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float PickUp(float weight, float max, Transform weaponHolder)
    {
        Debug.Log("Gun picked up");
        coll.enabled = false;
        transform.SetParent(weaponHolder);
        transform.localPosition = new Vector3(0, 0, 0);
        gun.enabled = true;
        rb.isKinematic = true;
        weight += gunWeight; 

        if (weight >= max)
        {
            weight -= gunWeight;
            Drop(weight);
        }

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
