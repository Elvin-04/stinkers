using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
        transform.position = transform.parent.position + new Vector3(0f, 1.5f, 0f);
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

}
