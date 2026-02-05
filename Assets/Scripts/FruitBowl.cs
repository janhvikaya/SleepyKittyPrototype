using UnityEngine;

public class FruitBowl : MonoBehaviour, IInteractable
{
    public bool wasInteracted;
    public GameObject player;
    public int layerOrd;

    private GameObject kitty;
    private Animator animator;
    private SpriteRenderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
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
            animator.SetBool("interacted", true);
            renderer.sortingOrder = 0;
            InteractionManager.bowlPushed = true;
        }

        if (!wasInteracted)
        {
            animator.SetBool("interacted", false);
            renderer.sortingOrder = 2;
            InteractionManager.bowlPushed = false;
        }
    }
}
