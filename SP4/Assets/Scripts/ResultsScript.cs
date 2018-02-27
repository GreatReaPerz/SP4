using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsScript : MonoBehaviour {

    int gold = PlayerPrefs.GetInt("Gold", 0);
    int IGC = PlayerPrefs.GetInt("IGC", 0);

    // Use this for initialization
    void Start ()
    {

        PlayerPrefs.SetInt("Gold", gold + IGC);
        PlayerPrefs.SetInt("IGC", 0);
        PlayerPrefs.Save();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
