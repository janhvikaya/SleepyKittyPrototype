using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector3 target;
    public GameObject player;
    public Transform waypointsParent;


    private string clicked;
    private string currentCol = "Floor";
    private PlayerInput playerInput;

    private Transform[] waypoints;
    private Dictionary<string, Vector3> waypointDict = new Dictionary<string, Vector3>();

    private List<Component> astarScripts = new List<Component>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = transform.position;
        waypoints = new Transform[waypointsParent.childCount];

        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i).gameObject.transform;
        }

        foreach (Transform way in waypoints)
        {
            waypointDict.Add(way.name, way.position);
        }

        astarScripts.AddRange(new Component[] { player.GetComponent<Seeker>(), player.GetComponent<AIPath>(),
            player.GetComponent<AIDestinationSetter>(), player.GetComponent<RaycastModifier>(), player.GetComponent<SimpleSmoothModifier>() });

        playerInput = player.GetComponent<PlayerInput>();
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

        if (currentCol == "Floor" && clicked == "Floor")
        {
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            target.z = transform.position.z;
        }
        else
        {
            Compare();
        }
    }

    private void Compare()
    {
        if (currentCol == "Floor" && clicked == "Table")
        {
            target = waypointDict["FT"];
        }
    }
}
