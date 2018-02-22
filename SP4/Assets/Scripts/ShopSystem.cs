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
    Text ItemDescription;

    [SerializeField]
    int UpgradeInfantryHealthPrice = 100;
    [SerializeField]
    int UpgradeInfantryHealthAmount = 10;
    [SerializeField]
    int UpgradeInfantryAttackPrice = 200;
    [SerializeField]
    int UpgradeInfantryDamageAmount = 10;

    [SerializeField]
    int UpgradeCavalryHealthPrice = 100;
    [SerializeField]
    int UpgradeCavalryHealthAmount = 10;
    [SerializeField]
    int UpgradeCavalryAttackPrice = 200;
    [SerializeField]
    int UpgradeCavalryDamageAmount = 10;

    [SerializeField]
    int UpgradeBowmenHealthPrice = 100;
    [SerializeField]
    int UpgradeBowmenHealthAmount = 10;
    [SerializeField]
    int UpgradeBowmenAttackPrice = 200;
    [SerializeField]
    int UpgradeBowmenDamageAmount = 10;

    [SerializeField]
    Image GoldBorder = null;

    string CurrentSelectedItem = "";
        
    // Use this for initialization
    void Start () {
        //for (int i = 0; i < 10; ++i)
        //    CreateButton("New Button " + i);

        PlayerPrefs.SetInt("Gold", 1000);
        //Get the gold the player and save it to text gameobject to display on screen
        playerGold = PlayerPrefs.GetInt("Gold");
        playerGoldText = GameObject.Find("GoldAmount");
        playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //Get Item Description Text object
        ItemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        ItemDescription.enabled = false;

        //Set the gold border such that it doesn't appear at first
        GoldBorder.enabled = false;

        //Add shop items that the player can buy
        shopItems.Add("UpgradeInfantryHealth", UpgradeInfantryHealthPrice);
        shopItems.Add("UpgradeInfantryAttack", UpgradeInfantryAttackPrice);
        shopItems.Add("UpgradeCavalryHealth", UpgradeCavalryHealthPrice);
        shopItems.Add("UpgradeCavalryAttack", UpgradeCavalryAttackPrice);
        shopItems.Add("UpgradeBowmenHealth", UpgradeBowmenHealthPrice);
        shopItems.Add("UpgradeBowmenAttack", UpgradeBowmenAttackPrice);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public int BuyItem(string ItemName)
    {
        //Update the amount of gold the player has left after buying
        playerGold -= (int)shopItems[ItemName];
        playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //The bought item modifications to player
        switch(ItemName)
        {
            case "UpgradeInfantryHealth":
                {
                    //Increase base health of infantry

                    //Update the item description with the new stats
                    ItemDescription.text = "Infantry Health: " + PlayerPrefs.GetFloat("infantryHealth") + " (+" + UpgradeInfantryHealthAmount.ToString() + ")";
                    break;
                }
            case "UpgradeInfantryAttack":
                {
                    //Increase base damage of infantry

                    //Update the item description with the new stats
                    ItemDescription.text = "Infantry Damage: " + PlayerPrefs.GetFloat("infantryDamage") + " (+" + UpgradeInfantryDamageAmount.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryHealth":
                {
                    //Increase base health of cavalry

                    //Update the item description with the new stats
                    ItemDescription.text = "Cavalry Health: " + PlayerPrefs.GetFloat("cavalryHealth") + " (+" + UpgradeCavalryHealthAmount.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryAttack":
                {
                    //Increase base damage of cavalry

                    //Update the item description with the new stats
                    ItemDescription.text = "Cavalry Damage: " + PlayerPrefs.GetFloat("cavalryDamage") + " (+" + UpgradeCavalryDamageAmount.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenHealth":
                {
                    //Increase base health of bowmen

                    //Update the item description with the new stats
                    ItemDescription.text = "Bowmen Health: " + PlayerPrefs.GetFloat("bowmenHealth") + " (+" + UpgradeBowmenHealthAmount.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenAttack":
                {
                    //Increase base damage of bowmen

                    //Update the item description with the new stats
                    ItemDescription.text = "Bowmen Damage: " + PlayerPrefs.GetFloat("bowmenDamage") + " (+" + UpgradeBowmenDamageAmount.ToString() + ")";
                    break;
                }
            default:
                break;
        }

        return playerGold;
    }

    public void ItemButtonOnClick()
    {
        //Get the button that was clicked
        GameObject CurrentButton = EventSystem.current.currentSelectedGameObject;
        RectTransform CurrentButtonRectTransform = CurrentButton.GetComponent<RectTransform>();

        //Get name of button
        CurrentSelectedItem = CurrentButton.name;
        //Set position of border to button's postion
        GoldBorder.transform.position = CurrentButtonRectTransform.transform.position;

        //Set scale of border to button
        GoldBorder.rectTransform.sizeDelta = new Vector3(CurrentButtonRectTransform.rect.width * CurrentButtonRectTransform.localScale.x * 1.05f, 
            CurrentButtonRectTransform.rect.height * CurrentButtonRectTransform.localScale.y * 1.05f, 1);

        //If the border is disabled, enable it
        if (!GoldBorder.enabled)
        {
            GoldBorder.enabled = true;
        }

        //Display Item description
        switch (CurrentSelectedItem)
        {
            case "UpgradeInfantryHealth":
                {
                    ItemDescription.text = "Infantry Health: " + PlayerPrefs.GetFloat("infantryHealth") + " (+" + UpgradeInfantryHealthAmount.ToString() + ")";
                    break;
                }
            case "UpgradeInfantryAttack":
                {
                    ItemDescription.text = "Infantry Damage: " + PlayerPrefs.GetFloat("infantryDamage") + " (+" + UpgradeInfantryDamageAmount.ToString() + ")";
                    break;
                }

            case "UpgradeCavalryHealth":
                {
                    ItemDescription.text = "Cavalry Health: " + PlayerPrefs.GetFloat("cavalryHealth") + " (+" + UpgradeCavalryHealthAmount.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryAttack":
                {
                    ItemDescription.text = "Cavalry Damage: " + PlayerPrefs.GetFloat("cavalryDamage") + " (+" + UpgradeCavalryDamageAmount.ToString() + ")";
                    break;
                }

            case "UpgradeBowmenHealth":
                {
                    ItemDescription.text = "Bowmen Health: " + PlayerPrefs.GetFloat("bowmenHealth") + " (+" + UpgradeBowmenHealthAmount.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenAttack":
                {
                    ItemDescription.text = "Bowmen Damage: " + PlayerPrefs.GetFloat("bowmenDamage") + " (+" + UpgradeBowmenDamageAmount.ToString() + ")";
                    break;
                }
            default:
                break;
        }

        if(!ItemDescription.enabled)
        {
            ItemDescription.enabled = true;
        }
    }

    public void BuyButtonOnClick()
    {
        //Pass in the name of the button that was clicked
        BuyItem(CurrentSelectedItem);
    }

    public void ExitButtonOnClick()
    {
        //Save the player gold to playerpref
        PlayerPrefs.SetInt("Gold", playerGold);
        PlayerPrefs.Save();

        //Return to main menu

    }

    //void CreateButton(string name)
    //{
    //    GameObject newButton = Instantiate(ButtonPrefab) as GameObject;
    //    newButton.transform.SetParent(ContentGrid.gameObject.transform, false);
    //    newButton.transform.name = name;
    //    newButton.GetComponentInChildren<Text>().text = name;
    //}

}
