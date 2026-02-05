using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance { get; private set; }

    public static bool bowlPushed;
    public static bool cabinetOpen;
    public static bool broomPushed;

    public List<IInteractable> interacted = new List<IInteractable>();
    int i;

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

    public void Undo()
    {
        if (interacted.Count > 0)
        {
            i = interacted.Count - 1;

            interacted[i].Undo();
            interacted.RemoveAt(i);
        }
        else return;
    }
}
