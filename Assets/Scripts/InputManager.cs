using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public static Vector3 target;
    public static Vector2 direction;

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
            if(GameManager.clicked == "Floor")
            {
                target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                target.z = transform.position.z;
            }
            else
            {
                target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                target.y += 0.7f;
                target.z = transform.position.z;
            }
        }
        else
        {
            if (GameManager.clicked == "Floor")
            {
                GameManager.dest = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                GameManager.dest.z = transform.position.z;
                GameManager.instance.Compare();
            }
            else
            {
                GameManager.dest = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                GameManager.dest.y += 0.7f;
                GameManager.dest.z = transform.position.z;
                GameManager.instance.Compare();
            }
            
        }
    }
}
