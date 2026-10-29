using UnityEngine;

/// <summary>
/// Placed on a World-Space UI Canvas to make sure it always faces the camera
/// </summary>
[RequireComponent(typeof(Canvas))]
public class UIRotater : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void Update()
    {
        FaceCamera();
    }

    private void FaceCamera()
    {
        Camera camera = Camera.main;
        transform.rotation = camera.transform.rotation;
    }
}
