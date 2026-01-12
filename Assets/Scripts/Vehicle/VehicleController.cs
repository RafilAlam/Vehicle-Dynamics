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

        foreach (Wheel wheel in wheels._wheels)
            tyres.Init(wheel);
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
            if (wheel._grounded)
            {
                rb.AddForceAtPosition(tyreForce, wheel._contactPosition);
                //Debug.DrawRay(wheel._contactPosition, tyreForce);
            }
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
}
