using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{

    public bool gamePaused = false;
    public GameObject pauseMenu;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
	}

    public void Resume()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1.0f;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
    }

    public void LoadingGame()
    {
        Debug.Log("LoadGame");
    }

}
