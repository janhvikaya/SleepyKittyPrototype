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
    public static bool useRB;

    public GameObject player;
    public GameObject targetObj;

    public Transform waypointsParent;

    //private Vector3 change;

    private Animator animator;

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
        animator = player.transform.GetChild(0).GetComponent<Animator>();

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
            StartCoroutine(GameManager.instance.ShiftControlToKitty(true, "FT", "C", "TF", "isJumpingUp", 2, false));
        }

        if (GameManager.currentCol == "Table" && GameManager.clicked == "Floor")
        {
            StartCoroutine(GameManager.instance.ShiftControlToKitty(true, "TF", "C", "FT", "isJumpingDown", 0, true));
        }

        if (GameManager.currentCol == "Floor" && GameManager.clicked == "Counter")
        {
            StartCoroutine(GameManager.instance.ShiftControlToKitty(false, "FC", "CF", null, "isJumpingUp", 1, false));
        }

        if (GameManager.currentCol == "Counter" && GameManager.clicked == "Floor")
        {
            StartCoroutine(GameManager.instance.ShiftControlToKitty(false, "CF", "FC", null, "isJumpingDown", 0, true));
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
        useRB = !control;

        //targetPlayerInput.enabled = control;
        clkMov.enabled = control;
    }
    
    public IEnumerator ShiftControlToKitty(bool hasThree, string wp1, string wp2, string wp3, string jump, int order, bool control)
    {
        InputManager.target = waypointDict[wp1];

        yield return new WaitForSeconds(0.1f);

        yield return new WaitUntil(() => Vector3.Distance(player.transform.position, waypointDict[wp1]) <= 0.01f);

        animator.SetBool(jump, true);
        yield return new WaitForSeconds(0.15f);
        kittyMov.moveSpeed = 5f;
        currentCol = clicked;

        if (order > 0)
        {
            ShiftToAstar(control);
        }
        
        yield return new WaitForSeconds(0.1f);

        
        if (order > 0)
        {
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order;
            yield return null;
        }

        InputManager.target = waypointDict[wp2];

        yield return new WaitUntil(() => Vector3.Distance(player.transform.position, waypointDict[wp2]) <= 0.01f);

        if (hasThree)
        {
            animator.SetBool(jump, false);
            kittyMov.moveSpeed = 3f;

            yield return new WaitForSeconds(0.5f);
            animator.SetBool(jump, true);
            yield return new WaitForSeconds(0.15f);

            kittyMov.moveSpeed = 5f;

            InputManager.target = waypointDict[wp3];
            yield return new WaitUntil(() => Vector3.Distance(player.transform.position, waypointDict[wp3]) <= 0.01f);
        }

        if (order < 1)
        {
            ShiftToAstar(control);
            player.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = order;
        }

        animator.SetBool(jump, false);
        kittyMov.moveSpeed = 3f;

        yield return new WaitForSeconds(0.1f);
        InputManager.target = dest;
    }
}
