using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Camera playerCamera; 
    public float moveSpeed = 6f;
    public float jumpPower = 7f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Rigidbody rb;
    private float rotationX = 0;
    private int jumpCount = 0;

    public float dashSpeed = 1000f;  
    public float dashDuration = 3f;
    public bool isDashing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;       // lock the mouse to prevent it moving freely ouside the game
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing) 
        {
            MovePlayer();
        }
        RotatePlayer();
    }

    void MovePlayer() {
        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX).normalized;
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && jumpCount < 2){
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++; 
        }

        // Dashing logic
        if (Input.GetKeyDown(KeyCode.F) && !isDashing){
            StartCoroutine(Dash(moveDirection));
        }
    }

    void RotatePlayer(){
        
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;                                     // vertical camera roation
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);                            // this puts the contraints for the camera for up and down

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);               // this is used to rotate the camera up and down
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);     // this rotates the player left and right based on mouse
    }

    // this manually detects when the player is on the ground
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    IEnumerator Dash(Vector3 moveDirection){
        float startTime = Time.time;
        isDashing = true; 
        Vector3 dashVelocity = moveDirection * dashSpeed;

        while (Time.time < startTime + dashDuration){
            rb.linearVelocity = new Vector3(dashVelocity.x, rb.linearVelocity.y * 0.8f, dashVelocity.z); 
            yield return null;
        }

        isDashing = false;
    }

}
