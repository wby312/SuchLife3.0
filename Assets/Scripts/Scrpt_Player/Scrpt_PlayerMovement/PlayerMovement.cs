using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerInput input;

    public float moveSpeed = 5f;  // Speed at which the player moves
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component
    private Vector2 moveDirection; // Direction the player will move in

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        //input.onActionTriggered += ActionComitted;
    }

    void OnDisable()
    {
        //input.onActionTriggered -= ActionComitted;
    }

    void ActionComitted(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(callbackContext.action);
    }

    // Update is called once per frame
    void Update()
    {        // Get input from the player (WASD or Arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Set the movement direction
        moveDirection = new Vector2(moveX, moveY).normalized;
        
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

    }

}
