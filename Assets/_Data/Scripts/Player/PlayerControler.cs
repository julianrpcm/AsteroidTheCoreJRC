using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class NewMonoBehaviourScript : MonoBehaviour
{

    const float MultSpeedFactor = 100f;

    [Header("Ship Movement Parameters")]
    [Tooltip("The Velocity of the ship")]
    [SerializeField]
    private float shipSpeed = 3f;

    [SerializeField]
    private float shipRotationSpeed = 10f;

    [Header("Fire Parameters")]
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform spawnBulletPoint;

    private Rigidbody rb;

    private Vector3 shipMovementDirection = Vector3.zero;
    private Quaternion shipRotation;
    private Camera mainCamera;

    private Vector3 lastPositionMouse;

    //Editor
    //private void Reset()
   // {
   //     
   // }

    //Initialization
    private void Awake() 
    {
        
    }
    void Start() 
    {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        mainCamera = Camera.main; //the best, variable global y se accede en el start.
        //Los dos son correctos son lo mismo, pero mejor la primera ya que con el camera.main se hace lo de abajo pero de manera interna de unity.
        //Camera camera = Camera.main;
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 

        

    }
    //Game logic
    void Update() 
    {
        
    }

    private void LateUpdate() 
    {
        
    }
    //Physics
    private void FixedUpdate() 
    {
        rb.linearVelocity = shipMovementDirection * (shipSpeed * MultSpeedFactor) * Time.fixedDeltaTime;
        
        Vector3 shipForward = lastPositionMouse - transform.position;
        shipForward.y = 0f;
        

        if (shipForward != Vector3.zero) {

            shipRotation = Quaternion.LookRotation(shipForward);

            if (shipRotation.eulerAngles.sqrMagnitude > 0){
                rb.rotation = Quaternion.RotateTowards(rb.rotation, shipRotation, Time.fixedUnscaledDeltaTime * shipRotationSpeed * MultSpeedFactor);
                //rb.rotation = Quaternion.Lerp(rb.rotation, rb.rotation * shipRotation, Time.fixedDeltaTime * shipRotationSpeed * MultSpeedFactor);
            }
        }
    }
    
    public void OnMove(InputValue inputValue)
    {

        Vector2 vectorInput = inputValue.Get<Vector2>();
        shipMovementDirection = new Vector3(vectorInput.x, 0f, vectorInput.y);
        shipMovementDirection = shipMovementDirection.normalized;


        Debug.Log("On Move!");
    }
    
    public void OnFire(InputValue inputValue)
    {
        Instantiate(bulletPrefab, spawnBulletPoint.position, spawnBulletPoint.rotation);
        //Debug.Log("On Fire!");
        //Debug.LogError("Error test");
    }

    public void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        float zCoord = mainCamera.transform.position.y - rb.position.y;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, zCoord));

        lastPositionMouse = worldMousePosition;
     

        //Debug.Log("MousePosition " + mousePosition);
        //Debug.Log("WorldPosition " + worldMousePosition);
    }

}

