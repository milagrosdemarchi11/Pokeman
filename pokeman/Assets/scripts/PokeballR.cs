using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pokeball : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoRecolectar;
    private void OnTriggerEnter2D(Collider2D other)
    {
        ControlJugador pikachu = other.GetComponent<ControlJugador>();

        if (pikachu != null)
        {
            AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);
            pikachu.ActivarPoder();
            Destroy(gameObject);
        }
    }
}