using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float life;

    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))

        { 
            collision.gameObject.GetComponent<ControlJugador>().RecibeDaño(1);
            Debug.Log("Daño recibido por el jugador");
        }

    }
    

    
    private void Update()
    {
    
    }
}
