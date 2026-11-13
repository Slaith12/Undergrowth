using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverClick : MonoBehaviour
{
    public event Action LeverClicked;

    private Camera mainCamera;
    public bool canClickLever;
    [SerializeField]
    private GameObject rotatingGroup;
    [SerializeField]
    public float rotateSpeed = 5f;
    private bool rotating;
    public Vector3 targetAngle = new Vector3(260f, 0f, 0f);
    public Vector3 currentAngle;

    private void Awake()
    {
        mainCamera = Camera.main;
        canClickLever = false;
        rotating = false;
        currentAngle = rotatingGroup.transform.eulerAngles;
    }


    private void Update()
    {
        if (canClickLever)
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
                        PullLever();
                        LeverClicked?.Invoke();
                    }
                }
            }
        }

        if (rotating)
        {
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));

            rotatingGroup.transform.eulerAngles = currentAngle;
            
            if (currentAngle == targetAngle)
            {
                rotating = false;
            }
        }
    }

    private void PullLever()
    {
        rotating = true;
    }
}
