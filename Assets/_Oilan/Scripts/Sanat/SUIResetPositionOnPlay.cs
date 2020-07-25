using UnityEngine;

public class SUIResetPositionOnPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
