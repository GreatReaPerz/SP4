using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefScript : MonoBehaviour {

    private float health = 100.0f;

    private int gold = 0;

    private float volume = 0.0f;

    private float calvaryAtt = 15;

    private float calvaryHP = 40;

    private float calvaryAttSpd = 1;//0.2f;

    private float calvarySpd = 175 * 0.016f;

    private float infantryAtt = 20;

    private float infantryHP = 35;//50;

    private float infantryAttSpd = 1;//0.1f;

    private float infantrySpd = 150 * 0.016f;

    private float bowmenAtt = 10;

    private float bowmenHP = 30;

    private float bowmenAttSpd = 1;//0.5f;

    private float bowmenSpd = 150 * 0.016f;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetFloat("Health", health);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Gold", gold);

        PlayerPrefs.SetFloat("calvaryAtt", calvaryAtt);
        PlayerPrefs.SetFloat("calvaryHP", calvaryHP);
        PlayerPrefs.SetFloat("calvaryAttSpd", calvaryAttSpd);
        PlayerPrefs.SetFloat("calvarySpd", calvarySpd);

        PlayerPrefs.SetFloat("infantryAtt", infantryAtt);
        PlayerPrefs.SetFloat("infantryHP", infantryHP);
        PlayerPrefs.SetFloat("infantryAttSpd", infantryAttSpd);
        PlayerPrefs.SetFloat("infantrySpd", infantrySpd);

        PlayerPrefs.SetFloat("bowmenAtt", bowmenAtt);
        PlayerPrefs.SetFloat("bowmenHP", bowmenHP);
        PlayerPrefs.SetFloat("bowmenAttSpd", bowmenAttSpd);
        PlayerPrefs.SetFloat("bowmenSpd", bowmenSpd);
        PlayerPrefs.Save();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
