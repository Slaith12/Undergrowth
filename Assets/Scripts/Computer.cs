using UnityEngine;
using UnityEngine.InputSystem;

public class Computer : MonoBehaviour
{
    private Camera mainCamera;
    private BoxCollider computerCollider; 

    private void Awake()
    {
        mainCamera = Camera.main;
        computerCollider = GetComponent<BoxCollider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the left mouse button was pressed 
        if (Pointer.current != null & Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 screenPosition = Pointer.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit; 

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == computerCollider)
                {
                    OpenComputer();
                }
            }
        }
    }

    private void OpenComputer()
    {
        Debug.Log("OpenComputer called");
    }
}
