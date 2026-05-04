using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private AudioClip sonidoMuerte;

    // public void TakeDamage(float damage)
    // {
    //     life -= damage;

    //     if (life <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControlJugador player = collision.gameObject.GetComponent<ControlJugador>();

            if (player.tienePoder)
            {
                // el enemigo muere
                //AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);
                ReproducirSonidoMuerte();
                Destroy(gameObject);

            }
            else
            {
                // el jugador recibe daño
                player.RecibeDaño(1);
                //Debug.Log("Daño recibido por el jugador");
            }
        }
    }

    private void ReproducirSonidoMuerte()
    {
        GameObject tempAudio = new GameObject("EnemySound");
        AudioSource audio = tempAudio.AddComponent<AudioSource>();

        audio.clip = sonidoMuerte;
        audio.Play();

        Destroy(tempAudio, sonidoMuerte.length);
    }




    private void Update()
    {

    }
}
