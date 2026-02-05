using UnityEngine;

public class Interact : MonoBehaviour, IInteractable
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
            animator.SetBool("interacted", true);
        }
        if (!wasInteracted)
        {
            animator.SetBool("interacted", false);
        }
    }
}
