using UnityEngine;

public class PlayerArmRotation : MonoBehaviour
{
    [Header("Parent")]
    [SerializeField]
    Transform parent;

    [Header("Config")]
    [SerializeField]
    float angleMoveSpeed = 5.0f;

    [SerializeField]
    float catchUpAngle = 30.0f;

    [SerializeField]
    float deadStopVelocity = 15.0f;

    [SerializeField]
    float closenessBuff = 5.0f;

    float currentArmRotation = 0f;

    float pastRotation = 0f;

    float velocity = 0f;

    void Start()
    {
        parent = parent == null ? transform.parent : parent;
    }

    void Update()
    {

        float armRotDif = MathHelper.RotationTo180Scale(currentArmRotation - parent.rotation.eulerAngles.z);
        float absArmRotDif = Mathf.Abs(armRotDif);
        float rotSign = Mathf.Sign(armRotDif);

        Debug.Log("Cubic Buff : " + MathHelper.CubicBuff(absArmRotDif / catchUpAngle, closenessBuff));
        if (absArmRotDif > catchUpAngle+0.1f)
        {
            currentArmRotation = parent.rotation.eulerAngles.z + catchUpAngle * rotSign;
            velocity = 270f * rotSign;
        }
        else
        {
            Debug.Log("Hello!?!? : " + Mathf.Sign(velocity) * angleMoveSpeed * Time.deltaTime);
            velocity = Mathf.Abs(velocity) > 50f ? velocity - Mathf.Sign(velocity) * angleMoveSpeed * Time.deltaTime :
                                                   velocity + rotSign * MathHelper.CubicBuff(Mathf.Clamp01(absArmRotDif/catchUpAngle), closenessBuff) * angleMoveSpeed * Time.deltaTime;// * MathHelper.CubicBuff(absArmRotDif / catchUpAngle);
            if (Mathf.Abs(velocity) < deadStopVelocity && absArmRotDif < 0.2)
            {
                velocity = 0;
            }
            else
            {
                currentArmRotation -= velocity * Time.deltaTime;
            }
            //currentArmRotation = Mathf.Abs(velocity) < deadStopVelocity && absArmRotDif < 0.2 ? currentArmRotation : currentArmRotation - velocity * Time.deltaTime;

            /*currentArmRotation = absArmRotDif < 0.2f ? currentArmRotation :
                                 currentArmRotation - Mathf.Sign(armRotDif) * angleMoveSpeed *
                                 Time.deltaTime * MathHelper.CubicBuff(absArmRotDif / catchUpAngle);*/
        }

        Debug.Log((currentArmRotation - pastRotation) / Time.deltaTime);
        Debug.Log("Velocity : " + velocity);
        //Debug.Log("Rot Dif Bezier Buff : " + MathHelper.CubicBuff(absArmRotDif / catchUpAngle));
        //Debug.Log("Re calced 180 scale : " + MathHelper.RotationTo180Scale(currentArmRotation - parent.rotation.eulerAngles.z));

        transform.rotation = Quaternion.Euler(0f,0f, currentArmRotation);
        pastRotation = currentArmRotation;
    }
}
