using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public static Vector3 target;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider == null) return;

        GameManager.clicked = hit.collider.gameObject.name;

        if (GameManager.currentCol == GameManager.clicked)
        {
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            target.z = transform.position.z;
        }
        else
        {
            GameManager.dest = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            GameManager.dest.z = transform.position.z;
            GameManager.instance.Compare();
        }
    }
}
