using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/*
 * Main Menu manager
 */
public class MenuManager : MonoBehaviour
{

    /*
     * Called by the play button to launch the map
     */
    public void Play(){
        SceneManager.LoadScene("GameWorld");
    }
}
