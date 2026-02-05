using UnityEngine;

public class CabinetDoor : MonoBehaviour, IInteractable
{
    public bool wasInteracted;
    public GameObject player;
    public int layerOrd;

    private GameObject kitty;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        kitty = player.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public bool CanInteract()
    {
        if (!wasInteracted && kitty.GetComponent<SpriteRenderer>().sortingOrder == layerOrd)
        {
            return true;
        }
        else return false;
    }

    void IInteractable.Interact()
    {
        if (!CanInteract()) return;
        Invoke(nameof(PlayAnim), 0.1f);
    }

    private void PlayAnim()
    {
        SetInteracted(true);
    }
    public void Undo()
    {
        SetInteracted(false);
    }

    public void SetInteracted(bool interacted)
    {
        wasInteracted = interacted;

        if (wasInteracted)
        {
            if (InteractionManager.bowlPushed && !InteractionManager.cabinetOpen)
            {
                animator.SetBool("canOpen", false);
                InteractionManager.instance.interacted.RemoveAt(InteractionManager.instance.interacted.Count - 1);

                wasInteracted = false;
            }
            else if (!InteractionManager.bowlPushed)
            {
                animator.SetBool("isOpen", true);
                InteractionManager.cabinetOpen = true;
            }
        }

        if (!wasInteracted)
        {
            animator.SetBool("isOpen", false);
            InteractionManager.cabinetOpen = false;
        }
    }
}
