using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool openUI = false;
    public int messageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        openUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            messageNum = 0;
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                interactable.Interact();
                openUI = true;
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
}
