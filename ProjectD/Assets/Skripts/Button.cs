using UnityEditor;
using UnityEngine;

public class Button1 : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("player1") | LayerMask.GetMask("player2"));
        Debug.DrawRay(transform.position, Vector2.up, Color.red);

        if (rayHit.collider != null)
        {
            animator.SetBool("ButtonOff", false);
            animator.SetBool("ButtonOn", true);
            MoveBlockX.Button = true;
        }
        else
        {
            animator.SetBool("ButtonOff", true);
            animator.SetBool("ButtonOn", false);
        }
    }
}
