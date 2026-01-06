using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public GameObject UI;
    public TextMeshProUGUI interactText;
    public string[] message;
    public MonoBehaviour look;
    public MonoBehaviour playerMovement;
    public bool isOpen = false;

    public void Interact()
    {
        UI.SetActive(true);
        interactText.text = message[0];
        look.enabled = false;
        playerMovement.enabled = false;
    }

    public void CloseUI()
    {
        UI.SetActive(false);
        look.enabled = true;
        playerMovement.enabled = true;
    }
}
