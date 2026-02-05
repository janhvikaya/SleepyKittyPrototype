using NUnit.Framework;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class KittyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Vector3 newPos;
    private bool moveUp;
    private Vector3 change;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    /*private void OnEnable()
    {
        moveUp = true;
        change = new Vector3(0f,0f,0f);    
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        InputManager.direction = InputManager.target - transform.position;
        /*newPos = InputManager.target;
        newPos.y += 0.07f;*/
        transform.position = Vector3.MoveTowards(transform.position, InputManager.target, moveSpeed * Time.deltaTime);
        

        /*if(moveUp)
        {
            gameObject.transform.GetChild(0).localPosition = Vector3.MoveTowards(gameObject.transform.GetChild(0).localPosition, change, moveSpeed * Time.deltaTime);
        }
        if(gameObject.transform.GetChild(0).localPosition == change)
        {
            moveUp = false;
        }*/
    }
}
