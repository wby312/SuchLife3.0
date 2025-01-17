using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerInput input;

    void OnEnable()
    {
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
    {
        
    }
}
