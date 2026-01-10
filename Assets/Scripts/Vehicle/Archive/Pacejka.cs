using UnityEngine;
using System.Collections.Generic;
using System;

public class Pacejka : MonoBehaviour
{
    [Header("Properties")]
    [Tooltip("stiffness factor (affects slope at origin)")]
    [SerializeField]
    float ratioB;
    [Tooltip("shape factor (curvature)")]
    [SerializeField]
    float ratioC;
    [Tooltip("magnitude of curve")]
    [SerializeField]
    float ratioD;
    [Tooltip("curvature factor (flattening near peak)")]
    [SerializeField]
    float ratioE;
    [Tooltip("stiffness factor (affects slope at origin)")]
    [SerializeField]
    float angleB;
    [Tooltip("shape factor (curvature)")]
    [SerializeField]
    float angleC;
    [Tooltip("magnitude of curve")]
    [SerializeField]
    float angleD;
    [Tooltip("curvature factor (flattening near peak)")]
    [SerializeField]
    float angleE;

    float PacejkaFormula(float B, float C, float D, float E, float x)
    {
        return D * Mathf.Sin(C * Mathf.Atan(B * x - E * (B * x - Mathf.Atan(B * x))));
    }

    public void CalculateCombinedSlip(float slipRatio, float slipAngle, ref float Fx, ref float Fy)
    {
        float GxAngle = Mathf.Cos(angleC * Mathf.Atan(angleB * slipAngle));

        float GyRatio = Mathf.Cos(ratioC * Mathf.Atan(ratioB * slipRatio));

        Fx = Fx * GxAngle;
        Fy = Fy * GyRatio;
    }

    public void Step(Rigidbody rb, Transform mount, Wheel wheel, ref Vector3 netForce)
    {
        if (!wheel._grounded) return;

        Vector3 mountForward = mount.forward;
        Vector3 mountRight = mount.right;
        Vector3 wheelVelocity = rb.GetPointVelocity(wheel._wheelTransform.position);
        wheelVelocity = new Vector3(wheelVelocity.x, 0, wheelVelocity.z);
        float forwardVelocity = Vector3.Dot(wheelVelocity, mountForward);

        float slipRatio = ((wheel._angularVelocity * wheel._wheelRadius) / forwardVelocity) - 1;
        float longitudinalForce = PacejkaFormula(ratioB, ratioC, ratioD, ratioE, slipRatio);

        //float slipAngle = Vector3.Angle(wheel._wheelTransform.forward, wheelVelocity);
        float slipAngle = Mathf.Atan2(Vector3.Dot(wheelVelocity, mountRight), Vector3.Dot(wheelVelocity, mountForward));
        float lateralForce = PacejkaFormula(angleB, angleC, angleD, angleE, slipAngle);

        CalculateCombinedSlip(slipRatio, slipAngle, ref lateralForce, ref longitudinalForce);
        float maxForce = wheel._load * wheel._frictionCoeff;
        float magForce = Mathf.Sqrt(longitudinalForce * longitudinalForce + lateralForce * lateralForce);
        float scale = Mathf.Abs(maxForce / magForce);
        if (magForce > maxForce) 
        {
            longitudinalForce *= scale;
            lateralForce *= scale;
        }

        Debug.Log($"LongForce: {longitudinalForce}, LatForce: {lateralForce}");

        netForce = longitudinalForce * mountForward + lateralForce * mountRight;
    }
}
