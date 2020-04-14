using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    public GameObject wakableChild;

    public void activate()
    {
        wakableChild.SetActive(true);
    }

    public void deActivate()
    {
        wakableChild.SetActive(false);
    }
}
