using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballsC : MonoBehaviour
{
    //[SerializeField] private GameObject efecto;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;
    [SerializeField] private AudioClip sonidoRecolectar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            puntaje.SumarPuntos(cantidadPuntos);
            //Instantiate(efecto, transform.position, Quaternion.identity);
            ReproducirSonido();
            //AudioSource.PlayClipAtPoint(sonidoRecolectar, transform.position);
            Destroy(gameObject);

        }
    }

    private void ReproducirSonido()
    {
        GameObject tempAudio = new GameObject("AudioTemp");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();

        audioSource.clip = sonidoRecolectar;
        audioSource.pitch = Random.Range(0.9f, 1.1f); // variación
        audioSource.Play();

        Destroy(tempAudio, sonidoRecolectar.length);
    }

}
