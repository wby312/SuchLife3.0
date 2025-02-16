using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Input")]
    [SerializeField]
    PlayerInput input;

    [SerializeField] 
    float deadZone = 0.1f; //When we start to apply stopping friction

    [Header("Speed Values")]
    [SerializeField] 
    float maxSpeed = 5f;
    [SerializeField] 
    float accel = 27f;
    [SerializeField] 
    float stopingAccel = 20f;

    [Header("Collision")]
    [SerializeField] 
    CircleCollider2D plrCollider;
    [SerializeField] 
    Rigidbody2D rigidBody;

    [Header("Rotation")]
    [SerializeField]
    float rotationSpeed = 360.0f;

    InputHandler inputHandler;

    float previousRotationZ;
    float goalRotationZ;
    float moveTowardsVal;

    Vector3 plrVelocity;

    Vector3 debugPoint;

    Vector3 previousPosition;
    List<ContactPoint2D> contactPoints;

    bool isDebugOn = false;
    bool debugHitDeadZone = false;
    void Start()
    {
        //Time.timeScale = 0.1f;

        plrCollider = plrCollider ? plrCollider : GetComponent<CircleCollider2D>();
        rigidBody = rigidBody ? rigidBody : GetComponent<Rigidbody2D>();

        contactPoints = new List<ContactPoint2D>();

        previousPosition = transform.position;

        inputHandler = InputHandler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleRotation();
        //handleCollision();
    }

    private void handleMovement()
    {
        Vector3 plrMoveInput = inputHandler.MoveInput;

        if (plrMoveInput.magnitude > deadZone)
        {
            plrVelocity = Vector3.ClampMagnitude(plrVelocity + plrMoveInput * accel * Time.deltaTime, maxSpeed);
        }
        else
        {
            Vector3 slowDownVector = plrVelocity.normalized * stopingAccel * Time.deltaTime;


            plrVelocity.x = plrVelocity.x > 0 ? Mathf.Max(plrVelocity.x - slowDownVector.x, 0f) :
                                                    Mathf.Min(plrVelocity.x - slowDownVector.x, 0f);

            plrVelocity.y = plrVelocity.y > 0 ? Mathf.Max(plrVelocity.y - slowDownVector.y, 0f) :
                                                    Mathf.Min(plrVelocity.y - slowDownVector.y, 0f);
        }

        previousPosition = transform.position;
        rigidBody.linearVelocity = plrVelocity;
        //transform.position += plrVelocity * Time.deltaTime;
    }

    private void handleRotation()
    {
        if (inputHandler.IsMouseEnabled == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            goalRotationZ = Mathf.Rad2Deg * Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) + 90f;
        }
        else
        {
            goalRotationZ = plrVelocity.magnitude == 0.0f ? goalRotationZ : 90f + Mathf.Rad2Deg * Mathf.Atan2(plrVelocity.y, plrVelocity.x);
        }

        float num = Mathf.Repeat(goalRotationZ - transform.rotation.eulerAngles.z, 360f);
        goalRotationZ = Mathf.Repeat(goalRotationZ, 360f);
        if (num > 180f)
        {
            num -= 360f;
        }

        if (goalRotationZ > 180f)
        {
            goalRotationZ -= 360f;
        }

        if (Mathf.Repeat(Mathf.Abs(num),360f) < 1f) {
            moveTowardsVal = 0f;
        }
        else
        {
            moveTowardsVal = rotationSpeed * Mathf.Sign(num) * MathHelper.BezierBuff(Mathf.Abs(num) / 180f);

        }

        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));

        /*System.Type type = assembly.GetType("UnityEditor.LogEntries");
        System.Reflection.MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

        Debug.Log(moveTowardsVal);
        Debug.Log(Mathf.Abs(num) / 180f);
        Debug.Log(MathHelper.BezierBuff(Mathf.Abs(num) / 180f));*/

        float clampedCurrentVal = Mathf.Repeat(transform.rotation.eulerAngles.z, 360f);
        if (clampedCurrentVal > 180f)
        {
            clampedCurrentVal -= 360f;
        }

        //if (moveTowardsVal > 0)
        //{
        bool valIsLessThanGoal = clampedCurrentVal < goalRotationZ;
        bool hadToBeFlipped = false;

        float newMoveTowardsVal = clampedCurrentVal + moveTowardsVal * Time.deltaTime;

        if (newMoveTowardsVal > 180f)
        {
            hadToBeFlipped = true;
            newMoveTowardsVal -= 360f;
        }
        if (newMoveTowardsVal < -180f)
        {
            hadToBeFlipped = true;
            newMoveTowardsVal += 360f;
        }

        bool newValIsLessThanGoal = newMoveTowardsVal < goalRotationZ;

        bool didNotOverShoot = (!hadToBeFlipped && valIsLessThanGoal == newValIsLessThanGoal) || (hadToBeFlipped && valIsLessThanGoal != newValIsLessThanGoal);

        Debug.DrawLine(new Vector3(-1f, 0f), new Vector3(2f, 0f), valIsLessThanGoal ? Color.blue : Color.red);
        Debug.DrawLine(new Vector3(-1f, 1f), new Vector3(2f, 1f), newValIsLessThanGoal ? Color.blue : Color.red);

        transform.rotation = Quaternion.Euler(0f, 0f, didNotOverShoot ? newMoveTowardsVal : goalRotationZ);
        //}

        //Debug.Log(goalRotationZ);
        //transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + moveTowardsVal * Time.deltaTime);

        previousRotationZ = goalRotationZ;
    }

    private void handleCollision()
    {
        foreach (var item in Physics2D.CircleCastAll(transform.position, plrCollider.radius, Vector2.zero))
        {
            if (item.transform != transform)
            {
                Vector3 itemPointVec3 = (Vector3)item.point;

                Vector3 closestPoint = item.collider.bounds.ClosestPoint((Vector3)item.point);

                Vector3 moveToPoint = closestPoint - itemPointVec3;

                transform.position = transform.position - new Vector3(0, moveToPoint.y);

                debugPoint = item.point;

                //transform.position = previousPosition;
            }
        }
        Debug.DrawLine(debugPoint, debugPoint + new Vector3(1f,0f), Color.red);
        Debug.DrawLine(transform.position, transform.position + new Vector3(1f, 0f), Color.red);
    }
}
