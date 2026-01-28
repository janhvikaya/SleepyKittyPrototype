using UnityEngine;

public class Interact : MonoBehaviour, IInteractable
{
    public bool wasInteracted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CanInteract()
    {
        return !wasInteracted;
    }

    void IInteractable.Interact()
    {
        if (!CanInteract()) return;
        PlayAnim();
    }

    private void PlayAnim()
    {
        SetInteracted(true);
    }

    public void SetInteracted(bool interacted)
    {
        wasInteracted = interacted;
        if (wasInteracted)
        {
            //play animation
        }
    }
}
