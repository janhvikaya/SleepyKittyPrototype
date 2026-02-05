using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ClickToMove : MonoBehaviour
{
    public GameObject player;

    private bool moveDown;
    private Vector3 change;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.target = transform.position;
    }
    /*private void OnEnable()
    {
        moveDown = true;
        change = new Vector3(0.08f, -0.8f, 0f);
    }*/
    // Update is called once per frame
    void Update()
    {
        transform.position = InputManager.target;

        /*if (moveDown)
        {
            player.transform.GetChild(0).localPosition = Vector3.MoveTowards(player.transform.GetChild(0).localPosition, change, 3f * Time.deltaTime);
        }
        if (player.transform.GetChild(0).localPosition == change)
        {
            moveDown = false;
        }*/
    }

}
