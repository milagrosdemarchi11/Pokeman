using UnityEngine;
using UnityEngine.AI;

public class EnemyEyesAnimation : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector2 velocity = agent.velocity;

        // Evitar ruido cuando está casi quieto
        if (velocity.magnitude < 0.1f) return;

        // Elegir eje dominante (sin diagonales)
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            animator.SetFloat("MoveX", Mathf.Sign(velocity.x));
            animator.SetFloat("MoveY", 0);
        }
        else
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", Mathf.Sign(velocity.y));
        }
    }
}