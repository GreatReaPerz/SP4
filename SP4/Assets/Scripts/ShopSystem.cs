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
    Image GoldBorder = null;

    string CurrentSelectedItem = "";
        
    // Use this for initialization
    void Start () {
        //for (int i = 0; i < 10; ++i)
        //    CreateButton("New Button " + i);

        PlayerPrefs.SetInt("Gold", 1000);
        //Set the gold the player has as displayed text on screen
        playerGold = PlayerPrefs.GetInt("Gold");
        playerGoldText = GameObject.Find("GoldAmount");
        playerGoldText.GetComponent<Text>().text = playerGold.ToString();

        //Get Item Description Text object
        ItemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        ItemDescription.enabled = false;

        //Set the gold border size
        //RectTransform a = GameObject.FindGameObjectWithTag("viewport").GetComponent<RectTransform>();
        //GoldBorder.rectTransform.sizeDelta = new Vector3(a.rect.width, a.rect.height * 0.25f, 1);
        GoldBorder.enabled = false;

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

    public void ItemButtonOnClick()
    {
        GameObject CurrentButton = EventSystem.current.currentSelectedGameObject;
        RectTransform CurrentButtonRectTransform = CurrentButton.GetComponent<RectTransform>();

        //Set name of button
        CurrentSelectedItem = CurrentButton.name;
        //Set position of border to button's postion
        GoldBorder.transform.position = CurrentButtonRectTransform.transform.position;

        //Set scale of border to button
        GoldBorder.rectTransform.sizeDelta = new Vector3(CurrentButtonRectTransform.rect.width * CurrentButtonRectTransform.localScale.x * 1.05f, 
            CurrentButtonRectTransform.rect.height * CurrentButtonRectTransform.localScale.y * 1.05f, 1);

        if (!GoldBorder.enabled)
        {
            GoldBorder.enabled = true;
        }

        //Item description
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

    //void CreateButton(string name)
    //{
    //    GameObject newButton = Instantiate(ButtonPrefab) as GameObject;
    //    newButton.transform.SetParent(ContentGrid.gameObject.transform, false);
    //    newButton.transform.name = name;
    //    newButton.GetComponentInChildren<Text>().text = name;
    //}

}
