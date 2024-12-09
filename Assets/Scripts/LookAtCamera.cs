using UnityEngine;

public class LookAtCamera : MonoBehaviour
{   
    [Header ("- Camera")]
    public Camera targetCamera;

    void Start()
    {
        // targetCamera가 Inspector에서 지정되지 않은 경우
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        if (targetCamera != null)
        {
            transform.rotation = targetCamera.transform.rotation;
        }
    }
}
