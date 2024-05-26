using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTon : MonoBehaviour
{

    // Boolean to enable or disable the functionality
    public bool VtonISEnabled;

    // The UI GameObject to be shown/hidden
    public GameObject vtonPanelGameObject;

    // Awake method to implement the singleton pattern
    private void Awake()
    {
        VtonISEnabled = false;
    }

    // OnTriggerEnter method to detect when the player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (VtonISEnabled && other.CompareTag("Player"))
        {
            ShowVtonPanel();
        }
    }

    // OnTriggerExit method to hide the panel when the player exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (VtonISEnabled && other.CompareTag("Player"))
        {
            HideVtonPanel();
        }
    }

    // Function to show the vtonPanelGameObject
    private void ShowVtonPanel()
    {
        if (vtonPanelGameObject != null)
        {
            vtonPanelGameObject.SetActive(true);
        }
    }

    // Function to hide the vtonPanelGameObject
    private void HideVtonPanel()
    {
        if (vtonPanelGameObject != null)
        {
            vtonPanelGameObject.SetActive(false);
        }
    }
}
