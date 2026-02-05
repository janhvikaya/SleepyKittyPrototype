using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void SetFalse()
    {
        animator.SetBool("isInteracting", false);
    }

    void CabinetCloses()
    {
        animator.SetBool("canOpen", true);
    }

    void CabinetClosed()
    {
        CabinetDoor door = GetComponent<CabinetDoor>();

        animator.SetBool("isOpen", false);
        animator.SetBool("isClosing", false);

        door.wasInteracted = false;

        InteractionManager.cabinetOpen = false;
    }
}
