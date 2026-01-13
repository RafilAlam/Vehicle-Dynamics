/*using UnityEngine;

public class Bristle
{
    public RaycastHit _hitInfo;
    public Vector3 _dir;
    public float _offset;
    public Vector3 _displacement = Vector3.zero;
    public Vector3 _lastPosition;
}

public class Brush : MonoBehaviour
{
    int numRings = 4;
    int numSections = 32;
    public float bristleStiffness = 1f;
    public float bristleDamping = 1f;
    public float elasticLimit = 0.3f;

    public void Init(Wheel wheel)
    {
        wheel.bristles = new Bristle[numRings * numSections];
        int bristleCount = -1;
        for (float i = 1; i <= numRings; i++)
        {
            float offsetOrigin = -wheel._wheelTransform.localScale.x;
            float offsetRing = ((2 * wheel._wheelTransform.localScale.x) / (numRings + 1)) * i;
            for (float j = 0; j < 360; j += 360f / numSections)
            {
                bristleCount++;
                Bristle bristle = new();
                wheel.bristles[bristleCount] = bristle;

                bristle._dir = new Vector3(0, Mathf.Sin(j * Mathf.Deg2Rad), Mathf.Cos(j * Mathf.Deg2Rad));
                bristle._offset = (offsetOrigin + offsetRing);

                Vector3 origin = wheel._wheelTransform.position + wheel._wheelTransform.right * bristle._offset;
                Vector3 dir = wheel._wheelTransform.rotation * bristle._dir;

                bristle._lastPosition = origin + dir * wheel._wheelRadius;
            }
        }
    }

    public void Step(Rigidbody rb, Transform mount, Wheel wheel, ref Vector3 netForce)
    {
        int bristleCount = -1;
        for (float i = 0; i < numRings; i++)
        {
            for (float j = 0; j < 360; j += 360f / numSections)
            {
                bristleCount++;
                Bristle bristle = wheel.bristles[bristleCount];

                Vector3 origin = wheel._wheelTransform.position + wheel._wheelTransform.right * bristle._offset;
                Vector3 dir = wheel._wheelTransform.rotation * bristle._dir;
                if (Physics.Raycast(origin, dir, out RaycastHit bristleHitInfo, wheel._wheelRadius))
                {
                    bristle._hitInfo = bristleHitInfo;
                    Vector3 frameDisplacement = bristle._lastPosition - bristleHitInfo.point;
                    Vector3 tangentialDisplacement = frameDisplacement - Vector3.Dot(frameDisplacement, bristleHitInfo.normal) * bristleHitInfo.normal;
                    Vector3 velocity = tangentialDisplacement / Time.deltaTime;
                    bristle._displacement += tangentialDisplacement;
                    bristle._lastPosition = bristleHitInfo.point;

                    Debug.DrawLine(bristleHitInfo.point, bristleHitInfo.point + bristle._displacement, Color.orange);
                    if (bristle._displacement.magnitude > elasticLimit)
                        bristle._displacement = bristle._displacement.normalized * elasticLimit;

                    netForce += bristleStiffness * bristle._displacement + bristleDamping * velocity;
                } else
                {
                    bristle._displacement = Vector3.zero;
                    bristle._lastPosition = origin + dir * wheel._wheelRadius;
                    Debug.DrawLine(origin, origin + dir * wheel._wheelRadius + bristle._displacement, Color.orange);
                }
            }
        }
        float maxForce = wheel._frictionCoeff * wheel._load;
        if (netForce.magnitude > maxForce)
            netForce = netForce.normalized * maxForce;
    }
}*/
