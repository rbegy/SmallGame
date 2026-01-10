using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool openUI = false;
    public int messageNum = 0;
    public float weight = 0;
    public float maxWeight = 100;
    public int selected = 0;
    public List<GameObject> guns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        openUI = false;
    }

    // Update is called once per frame
    void Update()
    {   
        int previousWeapon = selected;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selected >= guns.Count - 1)
            {                 
                selected = 0;
            }
            else
            {
                selected++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selected <= 0)
            {
                selected = guns.Count - 1;
            }
            else
            {
                selected--;
            }
        }

        if (previousWeapon != selected)
        {
            SelectWeapon(previousWeapon);
        }


        if(Input.GetKeyDown(KeyCode.G))
        {
            if (guns.Count == 0)
            {
                return;
            }

            GameObject weaponToDrop = guns[selected];
            guns.RemoveAt(selected);

            weaponToDrop.transform.SetParent(null);
            weaponToDrop.SetActive(true);

            GunPickUp gunPickUp = weaponToDrop.GetComponent<GunPickUp>();
            weight = gunPickUp.Drop(weight);

            if (selected >= guns.Count)
            { 
                selected = guns.Count - 1;
            }
    
            UpdateWeaponVisibility();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            messageNum = 0;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                GunPickUp gun = hit.collider.GetComponent<GunPickUp>();
                if (gun != null)
                {
                    weight = gun.PickUp(weight, maxWeight, transform);
                    gun.gameObject.SetActive(false);
                    guns.Add(gun.gameObject);
                }
                else if (interactable != null)
                {
                    interactable.Interact();
                    openUI = true;
                }
                else
                {
                    return;
                }
            }
        }

        if (openUI && Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable.message.Length > 0 && messageNum < interactable.message.Length - 1)
                {
                    messageNum++;
                    interactable.interactText.text = interactable.message[messageNum];
                }
                else if (messageNum == interactable.message.Length)
                {
                    interactable.CloseUI();
                    openUI = false;
                    messageNum = 0;
                }
                else 
                {
                    interactable.CloseUI();
                    openUI = false;
                    messageNum = 0;
                }
            }
        }
    }



    void SelectWeapon(int previousWeapon)
    {
        transform.GetChild(previousWeapon).gameObject.SetActive(false);
        transform.GetChild(selected).gameObject.SetActive(true);
    }

    void UpdateWeaponVisibility()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetActive(i == selected);
        }
    }

}
