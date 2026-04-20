using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Animator animator;

    private Vector2 direccion;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Shoot", true);
    }

    public void SetDirection(Vector2 dir)
    {
        direccion = dir.normalized;

        // Hace que la bala mire hacia donde va
        transform.right = direccion;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(direccion * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyControl>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
