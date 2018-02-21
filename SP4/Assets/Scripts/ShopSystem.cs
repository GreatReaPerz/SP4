using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour {

    //[SerializeField]
    //GameObject ButtonPrefab;

    //[SerializeField]
    //GridLayoutGroup ContentGrid;
    
    private int playerGold = 0;
    GameObject playerGoldText = null;
    Hashtable shopItems = new Hashtable();

    [SerializeField]
    int UpgradeInfantryHealthPrice = 100;

    [SerializeField]
    int UpgradeInfantryAttackPrice = 200;
    
    // Use this for initialization
    void Start () {
        //for (int i = 0; i < 10; ++i)
        //    CreateButton("New Button " + i);

        PlayerPrefs.SetInt("Gold", 1000);
        //Set the gold the player has as displayed text on screen
        playerGold = PlayerPrefs.GetInt("Gold");
        playerGoldText = GameObject.Find("GoldAmount");
        playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //Add shop items that the player can buy
        shopItems.Add("UpgradeInfantryHealth", UpgradeInfantryHealthPrice);
        shopItems.Add("UpgradeInfantryAttack", UpgradeInfantryAttackPrice);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public int BuyItem(string ItemName)
    {
        //Update the amount of gold the player has left
        playerGold -= (int)shopItems[ItemName];
        PlayerPrefs.SetInt("Gold", playerGold);
        PlayerPrefs.Save();
        playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //The bought item modifications to player
        switch(ItemName)
        {
            case "UpgradeInfantryHealth":
                {
                    //Increase base health of infantry
                    break;
                }
            case "UpgradeInfantryAttack":
                {
                    //Increase base damage of infantry
                    break;
                }
            default:
                break;
        }

        return playerGold;
    }

    public void ButtonOnClick()
    {
        //Pass in the name of the button that was clicked
        BuyItem(EventSystem.current.currentSelectedGameObject.name);
    }
   
    
    //void CreateButton(string name)
    //{
    //    GameObject newButton = Instantiate(ButtonPrefab) as GameObject;
    //    newButton.transform.SetParent(ContentGrid.gameObject.transform, false);
    //    newButton.transform.name = name;
    //    newButton.GetComponentInChildren<Text>().text = name;
    //}

}
