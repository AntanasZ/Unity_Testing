using UnityEngine;

[CreateAssetMenu(fileName = "CorrectLeverOrder",
    menuName = "CollabProj/Types/CorrectLeverOrder",
    order = 1)]

public class CorrectLeverOrder : ScriptableObject
{
    private string[] leverOrder = new string[] { "RedLever", "GreenLever", "YellowLever", "BlueLever", "PurpleLever" };

    public string[] LeverOrder
    {
        get
        {
            return leverOrder;
        }
    }
}
