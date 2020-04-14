using UnityEngine;

public class ModeCameraTransform : StateActions
{
    TransformVariable cameraTransform;

    FloatVariable horizontal;

    FloatVariable vertical;

    VariablesHolder variablesHolder;

    public ModeCameraTransform(VariablesHolder variablesHolder)
    {
        this.variablesHolder = variablesHolder;
        cameraTransform = variablesHolder.cameraTransform;
        horizontal = variablesHolder.horizontal;
        vertical = variablesHolder.vertical;
    }

    public override void Execute(
        StateManager stateManager,
        SessionManager sm,
        Turn turn
    )
    {
        Vector3 tp =
            cameraTransform.value.forward *
            (vertical.value * variablesHolder.cameraMoveSpeed);
        tp +=
            cameraTransform.value.right *
            (horizontal.value * variablesHolder.cameraMoveSpeed);
        cameraTransform.value.position += tp * stateManager.delta;
    }
}
