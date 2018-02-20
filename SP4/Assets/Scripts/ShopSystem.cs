using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour {

    [SerializeField]
    GameObject ButtonPrefab;

    [SerializeField]
    GridLayoutGroup ContentGrid;

    private float playerGold = 0;
    GameObject playerGoldText = null;
    
    // Use this for initialization
    void Start () {
        for (int i = 0; i < 10; ++i)
            CreateButton("New Button " + i);

        playerGold = PlayerPrefs.GetInt("Gold");
        playerGoldText = GameObject.Find("GoldAmount");
    }
	
	// Update is called once per frame
	void Update () {
        playerGoldText.GetComponent<UnityEngine.UI.Text>();
    }

    void CreateButton(string name)
    {
        GameObject newButton = Instantiate(ButtonPrefab) as GameObject;
        newButton.transform.SetParent(ContentGrid.gameObject.transform, false);
        newButton.transform.name = name;
        newButton.GetComponentInChildren<Text>().text = name;
    }

}
