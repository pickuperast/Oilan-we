using UnityEngine;
using Unity.VideoHelper;
public class SUIResetPositionOnPlay : MonoBehaviour
{
    void Awake()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

}
