using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ForwardContinuousMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;
    private Camera mainCamera;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = rb.transform.forward * movementSpeed * Time.fixedDeltaTime * 100f;
        rb.linearVelocity = newVelocity;
    }

    private bool IsOffScreen()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        if(viewportPosition.x < -0.1f || viewportPosition.x > 1.1f || viewportPosition.y < -0.1f || viewportPosition.y > 1.1f)
        {
            return true;
        }

        return false;

    }
    protected virtual void OnOffScreen()
    {
        Debug.Log("Is Off Scene");
    }
}
