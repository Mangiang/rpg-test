using UnityEngine;

public class OnEnableAssignTransformVariable : MonoBehaviour
{
    public TransformVariable targetVariable;

    private void Awake()
    {
        targetVariable.value = this.transform;
        Destroy(this);
    }
}
