using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    [SerializeField] private float padding = 0.5f;

    private Camera cam;
    private float objectHalfWidth;
    private float objectHalfHeight;

    void Awake()
    {
        cam = Camera.main;

        // Auto detect sprite size
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectHalfWidth = sr.bounds.extents.x;
            objectHalfHeight = sr.bounds.extents.y;
        }
        else
        {
            objectHalfWidth = padding;
            objectHalfHeight = padding;
        }
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 pos = transform.position;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - cam.transform.position.z));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - cam.transform.position.z));

        pos.x = Mathf.Clamp(pos.x, min.x + objectHalfWidth, max.x - objectHalfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + objectHalfHeight, max.y - objectHalfHeight);

        transform.position = pos;
    }
}