using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool  pause;

    bool                isDead = false;
    bool                isWon = false;

    public GameObject   pauseMenu;
    public GameObject   deathMenu;
    public GameObject   victoryMenu;

    void Start()
    {
        SetPause(false);
    }

    // Update is called once per frame
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

    public void PlayerDead()
    {
        isDead = true;
    }

    public void Victory()
    {
        isWon = true;
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SetPause(false);
    }
    public void Options()
    {

    }
}
