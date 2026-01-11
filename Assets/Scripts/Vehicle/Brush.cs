using UnityEngine;

public class Bristle
{

}

public class Brush : MonoBehaviour
{
    [Header("Brush Properties")]
    [SerializeField]
    int numRings = 8;
    [SerializeField]
    int numSections = 4;
    public void Step(Rigidbody rb, Transform mount, Wheel wheel, ref Vector3 netForce)
    {
        //Debug.DrawRay(wheel._wheelTransform.position, wheel._wheelTransform.forward * 1f);
        //Debug.DrawRay(wheel._wheelTransform.position, wheel._wheelTransform.right * 1f);
        for (int i = 0; i < numRings; i++)
        {
            float offsetOrigin = -wheel._wheelTransform.localScale.x;
            float offsetRing = ((2 * wheel._wheelTransform.localScale.x) / (numRings - 1)) * i;
            for (int j = 0; j < 360; j += 360 / numSections)
            {
                Vector3 dir = wheel._wheelTransform.rotation * new Vector3(0, Mathf.Sin(j * Mathf.Deg2Rad), Mathf.Cos(j * Mathf.Deg2Rad));
                Debug.DrawRay(wheel._wheelTransform.position + wheel._wheelTransform.right * (offsetOrigin + offsetRing), dir * wheel._wheelRadius, Color.orange);
            }
        }
    }
}
