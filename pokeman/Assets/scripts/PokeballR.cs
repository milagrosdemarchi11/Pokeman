using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pokeball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ControlJugador pikachu = other.GetComponent<ControlJugador>();

        if (pikachu != null)
        {
            pikachu.ActivarRayo();
            Destroy(gameObject);
        }
    }
}