using UnityEngine;

public class VehicleController : MonoBehaviour
{
    Engine engine;
    Drivetrain drivetrain;
    Suspension suspension;
    Wheels wheels;
    Brush tyres;
    Rigidbody rb;

    void Awake()
    {
        engine = GetComponent<Engine>();
        drivetrain = GetComponent<Drivetrain>();
        suspension = GetComponent<Suspension>();
        wheels = GetComponent<Wheels>();
        tyres = GetComponent<Brush>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        suspension.Init();
        wheels.Init();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        engine.Step();
        drivetrain.forwardPropagation(engine);
        for (int i = 0; i < wheels._mounts.Length; i++)
        {
            Transform mount = wheels._mounts[i];
            Wheel wheel = wheels._wheels[i];

            Vector3 upForce = Vector3.zero;
            Vector3 tyreForce = Vector3.zero;
            wheels.Step(wheel);
            suspension.Step(mount, wheel, ref upForce);
            tyres.Step(rb, mount, wheel, ref tyreForce);
            rb.AddForceAtPosition(upForce, wheel._wheelTransform.position);
            if (wheel._grounded) rb.AddForceAtPosition(tyreForce, wheel._contactPosition);
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < wheels._mounts.Length; i++)
        {
            Transform mount = wheels._mounts[i];
            Wheel wheel = wheels._wheels[i];

            wheels.VisualStep(mount, wheel);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i=0; i< wheels._mounts.Length; i++)
        {
            Transform mount = wheels._mounts[i];
            Wheel wheel = wheels._wheels[i];
            Gizmos.DrawRay(wheel._wheelTransform.position, mount.forward * 1f);
            Gizmos.DrawRay(wheel._wheelTransform.position, mount.right * 1f);

            Gizmos.DrawRay(mount.position, -transform.up * 0.7f);
            // Sphere represents rest position for wheel
            Gizmos.DrawSphere(mount.position + -transform.up * 0.5f, 0.1f);
        }
    }
}
