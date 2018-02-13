using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	// Use this for initialization
	//void Start () {
	//}
	
	// Update is called once per frame
	//void Update () {
	//}

    public void TransitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void TransitToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
