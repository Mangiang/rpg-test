using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDirectionVector
{
    static NodeDirectionVector _singleton;

    public static NodeDirectionVector singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = new NodeDirectionVector();
            }
            return _singleton;
        }
    }

    public Vector3[] directions;

    NodeDirectionVector()
    {
        directions = new Vector3[(int)NodeDirectionEnum.LENGTH];
        directions[(int)NodeDirectionEnum.FRONT] = Vector3.forward;
        directions[(int)NodeDirectionEnum.RIGHT] = Vector3.right;
        directions[(int)NodeDirectionEnum.BACK] = Vector3.back;
        directions[(int)NodeDirectionEnum.LEFT] = Vector3.left;
        directions[(int)NodeDirectionEnum.FRONT_LEFT] = Vector3.forward;
        directions[(int)NodeDirectionEnum.FRONT_RIGHT] = Vector3.Normalize(directions[(int)NodeDirectionEnum.FRONT] + directions[(int)NodeDirectionEnum.RIGHT]);
        directions[(int)NodeDirectionEnum.FRONT_LEFT] = Vector3.Normalize(directions[(int)NodeDirectionEnum.FRONT] + directions[(int)NodeDirectionEnum.LEFT]);
        directions[(int)NodeDirectionEnum.BACK_RIGHT] = Vector3.Normalize(directions[(int)NodeDirectionEnum.BACK] + directions[(int)NodeDirectionEnum.RIGHT]);
        directions[(int)NodeDirectionEnum.BACK_LEFT] = Vector3.Normalize(directions[(int)NodeDirectionEnum.BACK] + directions[(int)NodeDirectionEnum.LEFT]);
    }
}
