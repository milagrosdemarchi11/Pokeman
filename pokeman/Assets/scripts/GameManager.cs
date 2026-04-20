using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private ControlJugador controlJugador;
    //private GameObject player;
    

    

    private void Start()
    {
        
        //controlJugador = player.GetComponent<ControlJugador>();
    }

    // Update is called once per frame
    private void Update()
    {

        GameObject[] pokeballs = GameObject.FindGameObjectsWithTag("Pokeball");
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");

        // Si no queda ninguno → pasar de nivel
        if (pokeballs.Length == 0 || enemigos.Length == 0)
        {
            LevelComplete();
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<ControlJugador>().muerto == true)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("Muerte");
        }
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Nivel1");
    }

    // public void RestartLevel2()
    // {
    //     SceneManager.LoadScene("Nivel2");
    // }

    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
