using System;
using UnityEngine;

public class Wheel
{
    public Transform _wheelTransform;
    public float _displacement = 0;
    public float _velocity = 0;
    public float _wheelRadius = 0;

    public Wheel(Transform wheeltransform, float wheelradius)
    {
        _wheelTransform = wheeltransform;
        _wheelRadius = wheelradius;
    }
}

public class Wheels : MonoBehaviour
{
    [Header("Containers")]
    public Transform mountsContainer;
    public Transform wheelsContainer;

    [Header("Properties")]
    public float wheelRadius;

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
            _wheels[i] = new Wheel(wheel, 0.5f);
            i++;
        }
    }

    public void Step(Transform mount, Wheel wheel)
    {
        wheel._wheelTransform.position = mount.position - transform.up * wheel._displacement;
    }
}
