using System;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    Engine engine;
    Drivetrain drivetrain;
    Suspension suspension;
    Wheels wheels;
    Pacejka tyres;
    Rigidbody rb;

    void Awake()
    {
        engine = GetComponent<Engine>();
        drivetrain = GetComponent<Drivetrain>();
        suspension = GetComponent<Suspension>();
        wheels = GetComponent<Wheels>();
        tyres = GetComponent<Pacejka>();
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
            Debug.Log(upForce.magnitude);
            if (wheel._grounded)
            {
                rb.AddForceAtPosition(upForce, wheel._hitInfo.point, ForceMode.Force);
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
