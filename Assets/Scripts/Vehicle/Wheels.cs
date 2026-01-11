using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Wheel
{
    public Transform _wheelTransform;
    public Vector3 _contactPosition;
    public bool _grounded;
    public float _frictionCoeff;
    public float _load;
    public float _displacement = 0;
    public float _velocity = 0;
    public float _angularVelocity = 0;
    public float _wheelRadius = 0;

    public Wheel(Transform wheeltransform, float wheelradius, float frictionCoeff)
    {
        _wheelTransform = wheeltransform;
        _wheelRadius = wheelradius;
        _frictionCoeff = frictionCoeff;
    }
}

public class Wheels : MonoBehaviour
{
    [Header("Containers")]
    public Transform mountsContainer;
    public Transform wheelsContainer;

    [Header("Properties")]
    public float Radius;
    public float frictionCoeff;
    public float angularVelocity;

    public Transform[] _mounts = new Transform[4];
    public Wheel[] _wheels = new Wheel[4];

    public void Init()
    {
        int i = 0;
        foreach (Transform mount in mountsContainer)
        {
            _mounts[i] = mount;
            i++;
        }

        i = 0;
        foreach (Transform wheel in wheelsContainer)
        {
            _wheels[i] = new Wheel(wheel, 0.5f, frictionCoeff);
            i++;
        }
    }

    public void Step(Wheel wheel)
    {
        Debug.DrawRay(wheel._wheelTransform.position, wheel._wheelTransform.forward * 1f);
        Debug.DrawRay(wheel._wheelTransform.position, wheel._wheelTransform.right * 1f);
        wheel._angularVelocity = angularVelocity;
    }

    public void VisualStep(Transform mount, Wheel wheel)
    {
        Transform wheelTransform = wheel._wheelTransform;
        wheelTransform.position = mount.position - transform.up * wheel._displacement;
        wheelTransform.Rotate(wheel._angularVelocity * Mathf.Rad2Deg * Time.deltaTime, 0f, 0f, Space.Self);
    }
}
