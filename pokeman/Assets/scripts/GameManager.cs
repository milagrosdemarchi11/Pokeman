using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    // Update is called once per frame
    private void Update()
    {
        GameObject[] pokeballs = GameObject.FindGameObjectsWithTag("Pokeball");

        // Si no queda ninguno → pasar de nivel
        if (pokeballs.Length == 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("1");
    }
}
