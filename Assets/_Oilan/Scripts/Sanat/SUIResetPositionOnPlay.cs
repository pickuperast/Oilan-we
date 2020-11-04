using UnityEngine;
using Unity.VideoHelper;
public class SUIResetPositionOnPlay : MonoBehaviour
{
    Transform trans;
    RectTransform rect;

    void Awake()
    {
        gameObject.TryGetComponent(out trans);
        gameObject.TryGetComponent(out rect);
        //if(rect != null)
        //    rect.sizeDelta = new Vector2(0, 0);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

}
