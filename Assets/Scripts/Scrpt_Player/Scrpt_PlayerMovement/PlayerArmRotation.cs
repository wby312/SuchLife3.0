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

    float currentArmRotation = 0f;

    void Start()
    {
        parent = parent == null ? transform.parent : parent;
    }

    void Update()
    {

        float armRotDif = MathHelper.RotationTo180Scale(currentArmRotation - parent.rotation.eulerAngles.z);
        float absArmRotDif = Mathf.Abs(armRotDif);

        if (absArmRotDif > catchUpAngle+0.1f)
        {
            currentArmRotation = parent.rotation.eulerAngles.z + catchUpAngle * Mathf.Sign(armRotDif);
        }
        else
        {
            currentArmRotation = absArmRotDif < 0.2f ? currentArmRotation :
                                 currentArmRotation - Mathf.Sign(armRotDif) * angleMoveSpeed *
                                 Time.deltaTime * MathHelper.CubicBuff(absArmRotDif / catchUpAngle);
        }

        Debug.Log("Rot Dif Bezier Buff : " + MathHelper.CubicBuff(absArmRotDif / catchUpAngle));
        Debug.Log("Re calced 180 scale : " + MathHelper.RotationTo180Scale(currentArmRotation - parent.rotation.eulerAngles.z));

        transform.rotation = Quaternion.Euler(0f,0f, currentArmRotation);
    }
}
