using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public static string currentCol = "Floor";
    public static string clicked;
    public static Vector3 dest;

    public GameObject player;
    public GameObject targetObj;

    public Transform waypointsParent;

    private KittyMovement kittyMov;
    private ClickToMove clkMov;

    //private PlayerInput targetPlayerInput;
    //private PlayerInput kittyPlayerInput;

    private Transform[] waypoints;
    public Dictionary<string, Vector3> waypointDict = new Dictionary<string, Vector3>();

    private Behaviour[] astarScripts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void Start()
    {
        waypoints = new Transform[waypointsParent.childCount];

        //kittyPlayerInput = player.GetComponent<PlayerInput>();
        kittyMov = player.GetComponent<KittyMovement>();

        //targetPlayerInput = targetObj.GetComponent<PlayerInput>();
        clkMov = targetObj.GetComponent<ClickToMove>();

        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            waypoints[i] = waypointsParent.GetChild(i).gameObject.transform;
        }

        foreach (Transform way in waypoints)
        {
            waypointDict.Add(way.name, way.position);
        }

        astarScripts = new Behaviour[] { player.GetComponent<Seeker>(), player.GetComponent<AIPath>(), player.GetComponent<AIDestinationSetter>(), player.GetComponent<RaycastModifier>(), player.GetComponent<SimpleSmoothModifier>() };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Compare()
    {
        if (GameManager.currentCol == "Floor" && GameManager.clicked == "Table")
        {
            StartCoroutine(GameManager.instance.ShiftControlToKitty("FT", "TF", 2));
        }
    }

    public void ShiftToAstar(bool control)
    {
        foreach (Behaviour beha in astarScripts)
        {
            beha.enabled = control;
        }

        //kittyPlayerInput.enabled = !control;
        kittyMov.enabled = !control;

        //targetPlayerInput.enabled = control;
        clkMov.enabled = control;
    }
    
    public IEnumerator ShiftControlToKitty(string wp1, string wp2, int order)
    {
        InputManager.target = waypointDict[wp1];

        yield return new WaitForSeconds(0.1f);

        yield return new WaitUntil(() => player.transform.position == targetObj.transform.position);

        ShiftToAstar(false);

        yield return new WaitForSeconds(0.1f);

        //PlayAnim

        player.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order;

        currentCol = clicked;

        InputManager.target = waypointDict[wp2];

        yield return new WaitUntil(() => player.transform.position == waypointDict[wp2]);

        InputManager.target = dest;
    }
}
