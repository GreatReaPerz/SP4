using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWinLose : MonoBehaviour {
    [SerializeField]
    GameObject Player1; //Main player(default)
    [SerializeField]
    GameObject Player2; //Enemy(default

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (Player1.GetComponent<HealthSystem>().getHealth() <= 0)
        //    SceneManager.LoadScene("Lose");
        //if (Player2.GetComponent<HealthSystem>().getHealth() <= 0)
        //    SceneManager.LoadScene("Win");
    }
    public string CheckWin()
    {
        if (Player1.GetComponent<HealthSystem>().getHealth() <= 0)
            return "Lose";
        if (Player2.GetComponent<HealthSystem>().getHealth() <= 0)
            return "Win";
        else
            return "";
    }
}
