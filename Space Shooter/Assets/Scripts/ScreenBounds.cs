using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public float padding = 0.5f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        pos.x = Mathf.Clamp(pos.x, min.x + padding, max.x - padding);
        pos.y = Mathf.Clamp(pos.y, min.y + padding, max.y - padding);

        transform.position = pos;
    }
}
