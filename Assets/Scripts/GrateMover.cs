using System.Collections;
using UnityEngine;

// This is a script meant to move the grate up and down
public class GrateMover : MonoBehaviour
{
    public enum GrateState
    {
        Closed, 
        Opened
    } 

    // The current state of the grate 
    public GrateState state;

    [SerializeField]
    public Vector3 openedPosition;
    [SerializeField]
    public Vector3 closedPosition;

    [SerializeField]
    public float duration = 2.0f; 
    private void Awake()
    {
        openedPosition = new Vector3(-0.19f, 1.29f, -10.91f);
        closedPosition = new Vector3(-0.19f, -0.61f, -10.91f);
        state = GrateState.Opened;
    }

    public void SnapGrateClosed()
    {
        gameObject.transform.localPosition = new Vector3(-0.19f, -0.61f, -10.91f);
        state = GrateState.Closed;
    }

    public void SnapGrateOpened()
    {
        gameObject.transform.localPosition = new Vector3(-0.19f, 1.29f, -10.91f);
        state = GrateState.Opened;
    }

    public void CloseGrate()
    {
        Debug.Log("[GrateMover] - CloseGrate() Called");
        state = GrateState.Closed;
        StartCoroutine(LerpPosition(closedPosition, duration));
    }

    public void OpenGrate()
    {
        Debug.Log("[GrateMover] - OpenGrate() Called");
        state = GrateState.Opened;
        StartCoroutine(LerpPosition(openedPosition, duration));
    }

    public void DetermineAction()
    {
        Debug.Log("[GrateMover] - DetermineAction() Called - Current State: " + state.ToString());
        if (state == GrateState.Closed)
        {
            OpenGrate();
        }
        else
        {
            CloseGrate();
        }
    }

    IEnumerator LerpPosition(Vector3 position, float duration)
    {
        float timeElapsed = 0;
        // 1. Cache the EXACT starting position
        Vector3 startPosition = transform.localPosition;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition, position, t);

            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.localPosition = position;
    }
}
