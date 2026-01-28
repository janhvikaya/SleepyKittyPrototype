using NUnit.Framework;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class KittyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, InputManager.target, moveSpeed * Time.deltaTime);
    }

    /*IEnumerator OnShift()
    {
        //PlayAnim
        //set target to waypoint
        //change current col
        //move to clicked pos
    }*/
}
