using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header ("Config Inputs")]
    public InputAction MoveAction;  

    [Header ("Float configuration")]
    public float MoveSpeed = 10f;   
    public float PitchSpeed = 50f;  


    //Private Properties
    private Rigidbody rb;
    private float pitchInput;

    void Start()
    {
        MoveAction.Enable();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        pitchInput = MoveAction.ReadValue<Vector2>().y;
        
    }

    void FixedUpdate()
    {
        float pitch = pitchInput * PitchSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, 0, pitch);

        Vector3 forwardMovement = transform.right * MoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }
}
