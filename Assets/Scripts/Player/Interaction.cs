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
    public GameObject ammoPanel;
    public Health health;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        openUI = false;
        ammoPanel.SetActive(false);
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth == 0)
        {
            dead = true;
        }

        int previousWeapon = selected;
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
                    float newWeight = gun.PickUp(weight, maxWeight, transform);
                    if (newWeight == weight)
                    {
                        return;
                    }

                    if (guns.Count > 0)
                    {
                        guns[selected].SetActive(false);
                    }

                    weight = newWeight;

                    guns.Add(gun.gameObject);
                    selected = guns.Count - 1;

                    ammoPanel.SetActive(true);
                    gun.gameObject.SetActive(true);
                }
                else if (interactable != null)
                {
                    if (guns.Count > 0)
                    {
                        guns[selected].SetActive(false);
                    }
                    ammoPanel.SetActive(false);
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
                if (interactable.message.Length > 0 && messageNum < interactable.message.Length - 1)
                {
                    messageNum++;
                    interactable.interactText.text = interactable.message[messageNum];
                }
                else if (messageNum == interactable.message.Length)
                {
                    interactable.CloseUI();
                    openUI = false;
                    if (guns.Count > 0)
                    {
                        guns[selected].SetActive(true);
                    }
                    ammoPanel.SetActive(guns.Count > 0);
                    messageNum = 0;
                }
                else
                {
                    interactable.CloseUI();
                    openUI = false;
                    if (guns.Count > 0)
                    {
                        guns[selected].SetActive(true);
                    }
                    ammoPanel.SetActive(guns.Count > 0);
                    messageNum = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.G) || dead)
        {
            if (guns.Count == 0)
            {
                selected = 0;
                return;
            }


            GameObject weaponToDrop = guns[selected];
            Gun gun = weaponToDrop.GetComponent<Gun>();
            if (gun.reloading)
            {
                Debug.Log("Can't drop it, idiot");
                return;
            }

            guns.RemoveAt(selected);

            weaponToDrop.SetActive(true);

            GunPickUp gunPickUp = weaponToDrop.GetComponent<GunPickUp>();
            weight = gunPickUp.Drop(weight);

            ammoPanel.SetActive(guns.Count > 0);

            if (selected >= guns.Count)
            {
                selected = guns.Count - 1;
            }

            WeaponIsVisible();
            if (guns.Count == 0)
            {
                selected = 0;
                return;
            }
            return;
        }

        if (guns.Count == 0)
        {
            return;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !openUI)
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
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !openUI)
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
    }



    void SelectWeapon(int previousWeapon)
    {
        guns[previousWeapon].SetActive(false);
        guns[selected].SetActive(true);
    }

    void WeaponIsVisible()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].SetActive(i == selected);
        }
    }

}
