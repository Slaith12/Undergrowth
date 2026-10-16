using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverClick : MonoBehaviour
{
    public event Action LeverClicked;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        if (Pointer.current != null & Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 screenPosition = Pointer.current.position.ReadValue();

            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            // Cast the ray and check if it hit my lever 
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Lever")
                {
                    LeverClicked.Invoke();
                }
            }
        }
    }
}
