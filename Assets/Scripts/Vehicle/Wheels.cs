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
        wheel._angularVelocity = angularVelocity;
    }

    public void VisualStep(Transform mount, Wheel wheel)
    {
        wheel._wheelTransform.position = mount.position - transform.up * wheel._displacement;
    }
}
