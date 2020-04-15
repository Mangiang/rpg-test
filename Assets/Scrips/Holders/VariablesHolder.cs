using UnityEngine;

[CreateAssetMenu(menuName = "Game/Variables Holder")]
public class VariablesHolder : ScriptableObject
{
    public float cameraMoveSpeed = 15;

    [Header("Scriptables Variables")]
    #region Scriptables
    public TransformVariable cameraTransform;

    public FloatVariable horizontal;

    public FloatVariable vertical;
    public IntVariable actionPointsInt;
    #endregion
}
