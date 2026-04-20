using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer : MonoBehaviour
{
    [SerializeField] private Transform shootControler;
    [SerializeField] private GameObject shootPrefab;

    private ControlJugador player;

    [SerializeField] private float tiempoEntreDisparos = 3f;
    private float ultimoDisparo = -Mathf.Infinity;
    

    private void Start() 
    { 
        player = GetComponent<ControlJugador>(); 
    }
 
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= ultimoDisparo + tiempoEntreDisparos && player.tieneRayo)
        {
            Shoot();
            ultimoDisparo = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject bala = Instantiate(shootPrefab, shootControler.position, Quaternion.identity); 
        Vector2 direccion = player.ObtenerDireccion(); 
        bala.GetComponent<Shoot>().SetDirection(direccion);
        
    }
}
