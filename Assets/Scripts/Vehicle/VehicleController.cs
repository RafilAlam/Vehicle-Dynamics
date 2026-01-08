using UnityEngine;

public class VehicleController : MonoBehaviour
{
    Suspension suspensionController;
    Wheels wheelsController;
    Rigidbody rb;

    void Awake()
    {
        suspensionController = GetComponent<Suspension>();
        wheelsController = GetComponent<Wheels>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        suspensionController.Init();
        wheelsController.Init();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        for (int i = 0; i < wheelsController._mounts.Length; i++)
        {
            Transform mount = wheelsController._mounts[i];
            Wheel wheel = wheelsController._wheels[i];

            Vector3 upForce = Vector3.zero;
            suspensionController.Step(mount, wheel, ref upForce);
            rb.AddForceAtPosition(upForce, mount.position);
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < wheelsController._mounts.Length; i++)
        {
            Transform mount = wheelsController._mounts[i];
            Wheel wheel = wheelsController._wheels[i];

            wheelsController.Step(mount, wheel);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i=0; i<wheelsController._mounts.Length; i++)
        {
            Transform mount = wheelsController._mounts[i];
            Wheel wheel = wheelsController._wheels[i];

            Gizmos.DrawRay(mount.position, -transform.up * 2);
            // Sphere represents rest position for wheel
            Gizmos.DrawSphere(mount.position + -transform.up * 1, 0.1f);
        }
    }
}
