using UnityEngine;

public class Aim : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
