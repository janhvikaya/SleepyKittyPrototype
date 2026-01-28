using NUnit.Framework;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpMovement : MonoBehaviour
{
    public GameObject player;

    private List<Component> astarScripts = new List<Component>();
    private PlayerInput playerInput;

    /*private Seeker seeker;
    private AIPath path;
    private AIDestinationSetter destinationSetter;
    private RaycastModifier raycastModifier;
    private SimpleSmoothModifier simpleSmoothModifier;*/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        astarScripts.AddRange(new Component[] { player.GetComponent<Seeker>(), player.GetComponent<AIPath>(),   
            player.GetComponent<AIDestinationSetter>(), player.GetComponent<RaycastModifier>(), player.GetComponent<SimpleSmoothModifier>() });

        playerInput = player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
