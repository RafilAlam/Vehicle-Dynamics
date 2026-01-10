using UnityEditor;
using UnityEngine;

public class Suspension : MonoBehaviour
{

    [Header("Suspension Properties")]
    public float vehicleMass;
    public float restLength;
    public float maxExtension;
    public float minLength;
    public float frequency;
    public float dampingRatio;

    float wheelMass;

    public void Init()
    {

    }

    public void Step(Transform mount, Wheel wheel, ref Vector3 UpForce)
    {
        wheelMass = vehicleMass / 4;
        wheel._grounded = Physics.SphereCast(mount.position, wheel._wheelRadius, -transform.up, out RaycastHit hitInfo, restLength + maxExtension);
        if (wheel._grounded)
        {
            float naturalFrequency = 2 * Mathf.PI * frequency;
            float stiffness = wheelMass * naturalFrequency * naturalFrequency;
            float damping = 2 * wheelMass * dampingRatio * naturalFrequency;
            float compression = hitInfo.distance - restLength;
            wheel._load = -stiffness * compression - damping * wheel._velocity;

            wheel._velocity = (hitInfo.distance - wheel._displacement)/Time.deltaTime;
            wheel._displacement = hitInfo.distance;
            wheel._contactPosition = hitInfo.point;

            UpForce = hitInfo.normal * Mathf.Max(0, wheel._load);
        } else
        {
            float naturalFrequency = 2 * Mathf.PI * frequency;
            float stiffness = wheelMass * naturalFrequency * naturalFrequency;
            float damping = 2 * wheelMass * dampingRatio * naturalFrequency;
            float compression = wheel._displacement - (restLength + maxExtension);
            float springForce = -stiffness * compression - damping * wheel._velocity;

            wheel._velocity += (springForce / wheelMass) * Time.deltaTime;
            wheel._displacement += wheel._velocity * Time.deltaTime;
        }
        wheel._displacement = Mathf.Clamp(wheel._displacement, minLength, restLength + maxExtension);
    }
}
