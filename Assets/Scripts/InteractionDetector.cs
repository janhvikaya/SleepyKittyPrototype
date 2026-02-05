using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    public IInteractable interactableInRange = null;

    public GameObject kitty;
    private GameObject interactionIcon;

    private Animator kittyAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon = gameObject.transform.GetChild(0).gameObject;
        interactionIcon.SetActive(false);

        kitty = gameObject.transform.parent.gameObject;
        kittyAnimator = kitty.GetComponent<Animator>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactableInRange != null)
            {
                kittyAnimator.SetBool("isInteracting", true);
                InteractionManager.instance.interacted.Add(interactableInRange);
                interactableInRange.Interact();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
