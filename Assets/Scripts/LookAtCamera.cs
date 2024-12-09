using UnityEngine;

public class LookAtCamera : MonoBehaviour
{   
    [Header ("- Camera")]
    public Camera targetCamera;

    void Start()
    {
        // targetCamera�� Inspector���� �������� ���� ���
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
