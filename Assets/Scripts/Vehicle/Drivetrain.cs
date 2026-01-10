using UnityEngine;

public class Drivetrain : MonoBehaviour
{
    [Header("Drivetrain Properties")]
    [SerializeField]
    float[] gearBox;
    [SerializeField]
    float finalDriveRatio;

    short currGear=2;
    void ShiftGear(bool ShiftUp)
    {
        switch(ShiftUp)
        {
            case true:
                if (currGear == gearBox.Length - 1) return;
                currGear++;
                return;
            case false:
                if (currGear == 0) return;
                currGear--;
                return;
        }
    }

    public float forwardPropagation(Engine engine)
    {
        float engineAV = engine.RPM * (Mathf.PI / 30);
        return engineAV * gearBox[currGear] * finalDriveRatio;
    }

    public void backwardPropagation()
    {

    }
}
