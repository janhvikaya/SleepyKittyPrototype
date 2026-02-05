using UnityEngine;

public class Broom : MonoBehaviour, IInteractable
{
    public bool wasInteracted;
    public GameObject player;
    public GameObject cabinet;

    public int layerOrd;


    private GameObject kitty;
    private Animator animator;
    private Animator cabinetAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        cabinetAnimator = cabinet.GetComponent<Animator>();
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
            if(InteractionManager.cabinetOpen && InteractionManager.bowlPushed)
            {
                animator.SetBool("canLean",true);
                InteractionManager.broomPushed = true;
                InteractionManager.instance.interacted.Clear();
            }
            else
            {
                animator.SetBool("canFall", true);
                if(InteractionManager.cabinetOpen && !InteractionManager.bowlPushed)
                {
                    cabinetAnimator.SetBool("isClosing", true);
                }
                InteractionManager.broomPushed = true;
            }
        }

        if (!wasInteracted)
        {
            animator.SetBool("canFall", false);
            InteractionManager.broomPushed = false;
        }
    }

    
}
