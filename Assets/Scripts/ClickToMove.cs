using UnityEngine;
using UnityEngine.InputSystem;


public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector3 target;

    private string clicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider == null) return;

        clicked = hit.collider.gameObject.name;

        if (clicked == "Floor")
        {
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            target.z = transform.position.z;
        }
    }
}
