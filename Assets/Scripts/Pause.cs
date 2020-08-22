using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Pause menu class
 */
public class Pause : MonoBehaviour
{
    public static bool  pause;

    bool                isDead = false;
    bool                isWon = false;

    public GameObject   pauseMenu;
    public GameObject   deathMenu;
    public GameObject   victoryMenu;

    /*
     * At the start don't set to pause
     */
    void Start()
    {
        SetPause(false);
    }

    /*
     * Check death, won and if keydown("escape") is pressed
     * then display the right menu
     */
    void Update()
    {
        if (isDead)
        {
            pauseMenu.SetActive(false);
            deathMenu.SetActive(true);
            Time.timeScale = 0;
            return;
        } else if (isWon)
        {
            pauseMenu.SetActive(false);
            victoryMenu.SetActive(true);
        }
        if (Input.GetKeyDown("escape"))
        {
            SetPause();
        }
    }

    /*
     * Set pause set to 0 our 1 the flow of time
     */
    void SetPause()
    {
        pause = !pause;
        pauseMenu.SetActive(pause);

        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /*
     * Set pause and force a bool
     */
    public void SetPause(bool set)
    {
        pause = set;
        pauseMenu.SetActive(pause);

        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /*
     * Set the death var
     */
    public void PlayerDead()
    {
        isDead = true;
    }

    /*
     * Set the Victory var
     */
    public void Victory()
    {
        isWon = true;
    }

    /*
     * Restart button relaunch the scene and map to start
     */
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetPause(false);
    }
    /*
     * Go out to the main menu
     */
    public void QuitMainMenu()
    {
        SceneManager.LoadScene("ChooseWorld");
    }
}
