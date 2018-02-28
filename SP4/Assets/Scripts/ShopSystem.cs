using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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

        //PlayerPrefs.SetInt("Gold", 1000);
        //Get the gold the player and save it to text gameobject to display on screen
        playerGold = PlayerPrefs.GetInt("Gold");
        playerGoldText = GameObject.Find("GoldAmount");
        playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //Get Item Description Text object
        ItemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        ItemDescription.enabled = false;

        //Set the gold border such that it doesn't appear at first
        GoldBorder.enabled = false;

        //PlayerPrefs.SetInt("Gold", 7000);
        //PlayerPrefs.SetInt("UpgradeInfantryHealth", UpgradeInfantryHealthPrice);
        //PlayerPrefs.SetInt("UpgradeInfantryAttack", UpgradeInfantryAttackPrice);
        //PlayerPrefs.SetInt("UpgradeCavalryHealth", UpgradeCavalryHealthPrice);
        //PlayerPrefs.SetInt("UpgradeCavalryAttack", UpgradeCavalryAttackPrice);
        //PlayerPrefs.SetInt("UpgradeBowmenHealth", UpgradeBowmenHealthPrice);
        //PlayerPrefs.SetInt("UpgradeBowmenAttack", UpgradeBowmenAttackPrice);
        //PlayerPrefs.Save();

        //PlayerPrefs.SetFloat("calvaryHP", 50);
        //PlayerPrefs.SetFloat("infantryHP", 60);
        //PlayerPrefs.Save();

        UpgradeInfantryHealthPrice = PlayerPrefs.GetInt("UpgradeInfantryHealth");
        UpgradeInfantryAttackPrice = PlayerPrefs.GetInt("UpgradeInfantryAttack");
        UpgradeCavalryHealthPrice = PlayerPrefs.GetInt("UpgradeCavalryHealth");
        UpgradeCavalryAttackPrice = PlayerPrefs.GetInt("UpgradeCavalryAttack");
        UpgradeBowmenHealthPrice = PlayerPrefs.GetInt("UpgradeBowmenHealth");
        UpgradeBowmenAttackPrice = PlayerPrefs.GetInt("UpgradeBowmenAttack");

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
        if(playerGold < (int)shopItems[ItemName])
        {
            return playerGold;
        }

        //Update the amount of gold the player has left after buying
        playerGold -= (int)shopItems[ItemName];
        //playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //The bought item modifications to player
        switch(ItemName)
        {
            case "UpgradeInfantryHealth":
                {
                    //Increase base health of infantry
                    PlayerPrefs.SetFloat("infantryHP", PlayerPrefs.GetFloat("infantryHP") + UpgradeInfantryHealthAmount);
                    
                    //Update the item description with the new stats
                    ItemDescription.text = "Infantry Health: " + PlayerPrefs.GetFloat("infantryHP") + " (+" + UpgradeInfantryHealthAmount.ToString() + ")";

                    //Update the new gold price
                    PlayerPrefs.SetInt("UpgradeInfantryHealth", PlayerPrefs.GetInt("UpgradeInfantryHealth") + 50);
                    UpgradeInfantryHealthPrice = PlayerPrefs.GetInt("UpgradeInfantryHealth");

                    shopItems["UpgradeInfantryHealth"] = UpgradeInfantryHealthPrice;


                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeInfantryHealthPrice.ToString() + ")";
                    break;
                }
            case "UpgradeInfantryAttack":
                {
                    //Increase base damage of infantry
                    PlayerPrefs.SetFloat("infantryAtt", PlayerPrefs.GetFloat("infantryAtt") + UpgradeInfantryDamageAmount);

                    //Update the item description with the new stats
                    ItemDescription.text = "Infantry Damage: " + PlayerPrefs.GetFloat("infantryAtt") + " (+" + UpgradeInfantryDamageAmount.ToString() + ")";
                    
                    //Update the new gold price
                    PlayerPrefs.SetInt("UpgradeInfantryAttack", PlayerPrefs.GetInt("UpgradeInfantryAttack") + 50);
                    UpgradeInfantryAttackPrice = PlayerPrefs.GetInt("UpgradeInfantryAttack");

                    shopItems["UpgradeInfantryAttack"] = UpgradeInfantryAttackPrice;

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeInfantryAttackPrice.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryHealth":
                {
                    //Increase base health of cavalry
                    PlayerPrefs.SetFloat("calvaryHP", PlayerPrefs.GetFloat("calvaryHP") + UpgradeCavalryHealthAmount);

                    //Update the item description with the new stats
                    ItemDescription.text = "Cavalry Health: " + PlayerPrefs.GetFloat("calvaryHP") + " (+" + UpgradeCavalryHealthAmount.ToString() + ")";
                    
                    //Update the new gold price
                    PlayerPrefs.SetInt("UpgradeCavalryHealth", PlayerPrefs.GetInt("UpgradeCavalryHealth") + 50);
                    UpgradeCavalryHealthPrice = PlayerPrefs.GetInt("UpgradeCavalryHealth");

                    shopItems["UpgradeCavalryHealth"] = UpgradeCavalryHealthPrice;

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeCavalryHealthPrice.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryAttack":
                {
                    //Increase base damage of cavalry
                    PlayerPrefs.SetFloat("calvaryAtt", PlayerPrefs.GetFloat("calvaryAtt") + UpgradeCavalryDamageAmount);

                    //Update the item description with the new stats
                    ItemDescription.text = "Cavalry Damage: " + PlayerPrefs.GetFloat("calvaryAtt") + " (+" + UpgradeCavalryDamageAmount.ToString() + ")";
                    
                    //Update the new gold price
                    PlayerPrefs.SetInt("UpgradeCavalryAttack", PlayerPrefs.GetInt("UpgradeCavalryAttack") + 50);
                    UpgradeCavalryAttackPrice = PlayerPrefs.GetInt("UpgradeCavalryAttack");

                    shopItems["UpgradeCavalryAttack"] = UpgradeCavalryAttackPrice;

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeCavalryAttackPrice.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenHealth":
                {
                    //Increase base health of bowmen
                    PlayerPrefs.SetFloat("bowmenHP", PlayerPrefs.GetFloat("bowmenHP") + UpgradeBowmenHealthAmount);

                    //Update the item description with the new stats
                    ItemDescription.text = "Bowmen Health: " + PlayerPrefs.GetFloat("bowmenHP") + " (+" + UpgradeBowmenHealthAmount.ToString() + ")";

                    //Update the new gold price
                    PlayerPrefs.SetInt("UpgradeBowmenHealth", PlayerPrefs.GetInt("UpgradeBowmenHealth") + 50);
                    UpgradeBowmenHealthPrice = PlayerPrefs.GetInt("UpgradeBowmenHealth");

                    shopItems["UpgradeBowmenHealth"] = UpgradeBowmenHealthPrice;

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeBowmenHealthPrice.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenAttack":
                {
                    //Increase base damage of bowmen
                    PlayerPrefs.SetFloat("bowmenAtt", PlayerPrefs.GetFloat("bowmenAtt") + UpgradeBowmenDamageAmount);

                    //Update the item description with the new stats
                    ItemDescription.text = "Bowmen Damage: " + PlayerPrefs.GetFloat("bowmenAtt") + " (+" + UpgradeBowmenDamageAmount.ToString() + ")";
                    
                    //Update the new gold price
                    PlayerPrefs.SetInt("UpgradeBowmenAttack", PlayerPrefs.GetInt("UpgradeBowmenAttack") + 50);
                    UpgradeBowmenAttackPrice = PlayerPrefs.GetInt("UpgradeBowmenAttack");

                    shopItems["UpgradeBowmenAttack"] = UpgradeBowmenAttackPrice;

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeBowmenAttackPrice.ToString() + ")";
                    
                    break;
                }
            default:
                break;
        }

        PlayerPrefs.Save();
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
                    //Update the item description with the new stats
                    ItemDescription.text = "Infantry Health: " + PlayerPrefs.GetFloat("infantryHP") + " (+" + UpgradeInfantryHealthAmount.ToString() + ")";

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeInfantryHealthPrice.ToString() + ")";
                    break;
                }
            case "UpgradeInfantryAttack":
                {
                    //Update the item description with the new stats
                    ItemDescription.text = "Infantry Damage: " + PlayerPrefs.GetFloat("infantryAtt") + " (+" + UpgradeInfantryDamageAmount.ToString() + ")";

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeInfantryAttackPrice.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryHealth":
                {
                    //Update the item description with the new stats
                    ItemDescription.text = "Cavalry Health: " + PlayerPrefs.GetFloat("calvaryHP") + " (+" + UpgradeCavalryHealthAmount.ToString() + ")";

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeCavalryHealthPrice.ToString() + ")";
                    break;
                }
            case "UpgradeCavalryAttack":
                {
                    //Update the item description with the new stats
                    ItemDescription.text = "Cavalry Damage: " + PlayerPrefs.GetFloat("calvaryAtt") + " (+" + UpgradeCavalryDamageAmount.ToString() + ")";

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeInfantryAttackPrice.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenHealth":
                {
                    //Update the item description with the new stats
                    ItemDescription.text = "Bowmen Health: " + PlayerPrefs.GetFloat("bowmenHP") + " (+" + UpgradeBowmenHealthAmount.ToString() + ")";

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeBowmenHealthPrice.ToString() + ")";
                    break;
                }
            case "UpgradeBowmenAttack":
                {
                    //Update the item description with the new stats
                    ItemDescription.text = "Bowmen Damage: " + PlayerPrefs.GetFloat("bowmenAtt") + " (+" + UpgradeBowmenDamageAmount.ToString() + ")";

                    //Update the gold text
                    playerGoldText.GetComponent<Text>().text = playerGold.ToString() + " (-" + UpgradeBowmenAttackPrice.ToString() + ")";
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
        SceneManager.LoadScene("Menu");
        
    }
}
