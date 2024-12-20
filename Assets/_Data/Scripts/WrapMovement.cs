using UnityEngine;

public class WrapMovement : MonoBehaviour
{
    Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        //Debug.Log("Viewport position: " + viewportPosition);

        Vector3 wrapPosition = Vector3.zero;
        if (viewportPosition.x < -0.05f) {
            wrapPosition.x += 1.04f;
        }
        else if(viewportPosition.x > 1.05f) {
            wrapPosition.x -= 1.04f;
        }
        else if(viewportPosition.y < -0.1f)
        {
            wrapPosition.y += 1.05f;
        }
        else if(viewportPosition.y > 1.1f)
        {
            wrapPosition.y -= 1.05f;
        }

        Vector3 worldPoint = mainCamera.ViewportToWorldPoint(viewportPosition + wrapPosition);
        transform.position = worldPoint;

    }
}

