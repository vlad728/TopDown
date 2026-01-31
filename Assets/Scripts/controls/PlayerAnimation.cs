using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private float horizontal;
    private float vertical;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            animator.SetBool("front", true);
            animator.SetBool("back", false);
        }
        else if (vertical < 0)
        {
            animator.SetBool("back", true);
            animator.SetBool("front", false);
        }
        if (horizontal > 0)
        {
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        }
        else if (horizontal < 0)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", true);
        }
        if (vertical == 0 &&  horizontal == 0)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("back", false);
            animator.SetBool("front", false);
        }
    }
}
