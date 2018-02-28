using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Do stuff for the main game here
public class MainGame : MonoBehaviour {

    [SerializeField]
    Canvas thisCanvas;

    [SerializeField]
    Canvas UICanvas;

    [SerializeField]
    Sprite HillsSprite;
    [SerializeField]
    Sprite ForestSprite;
    [SerializeField]
    Sprite RiverSprite;
    [SerializeField]
    Sprite PlainsSprite;
    [SerializeField]
    Image NeutralZone;

    [SerializeField]
    GameObject healthTexture;

    private GridSystem theGridSystem = null;

     public string NeutralZoneTerrainType;

    GameObject TerrainInformation = null;

    // Use this for initialization
    public void Start()
    {

        //theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem_Temp>();
        //Debug.Assert(theGridSystem != null);
        
        //Create neutral zone
        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        Vector2 canvasLocalScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;

        NeutralZone.transform.position = objectRectTransform.transform.position;
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //If the platform is Windows
            NeutralZone.rectTransform.sizeDelta = new Vector2((objectRectTransform.rect.width * 0.62f) * canvasLocalScale.x, (objectRectTransform.rect.height * canvasLocalScale.y) * 0.54f);
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            NeutralZone.rectTransform.sizeDelta = new Vector2((objectRectTransform.rect.width * 0.92f) * canvasLocalScale.x, (objectRectTransform.rect.height * canvasLocalScale.y) * 0.25f);
        }
        //GameObject healthTexture = Resources.Load("prefabs/Health") as GameObject;

        healthTexture.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(healthTexture.GetComponent<Image>().rectTransform.rect.width * canvasLocalScale.x, healthTexture.GetComponent<Image>().rectTransform.rect.height * canvasLocalScale.y);

        float ranNum = Random.Range(0.0f, 4.0f);

        if (ranNum > 3.0f)
        {
            NeutralZoneTerrainType = "Plains";
            NeutralZone.sprite = PlainsSprite;
        }
        else if (ranNum > 2.0f)
        {
            NeutralZoneTerrainType = "River";
            NeutralZone.sprite = RiverSprite;
        }
        else if (ranNum > 1.0f)
        {
            NeutralZoneTerrainType = "Hills";
            NeutralZone.sprite = HillsSprite;
        }
        else if (ranNum > 0.0f)
        {
            NeutralZoneTerrainType = "Forest";
            NeutralZone.sprite = ForestSprite;
        }
        EventTrigger neutralZonetrigger = NeutralZone.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry mouseEnter = new EventTrigger.Entry();                               //Create trigger
        mouseEnter.eventID = EventTriggerType.PointerDown;                                      //Define trigger type   (Pointer down)
        mouseEnter.callback.AddListener((data) => { ShowInfo(); });                             //Add listener to call function/ do something(changes text)
        neutralZonetrigger.triggers.Add(mouseEnter);                                            //Add to Event Trigger
        EventTrigger.Entry mouseExit = new EventTrigger.Entry();                                //Create trigger
        mouseExit.eventID = EventTriggerType.PointerUp;                                         //Define trigger type   (Pointer up)
        mouseExit.callback.AddListener((data) => { HideInfo(); });                              //Add listener to call function/ do something(changes text)
        neutralZonetrigger.triggers.Add(mouseExit);                                             //Add to Event Trigger
        //for (uint i = 0; i < GridSystem.gridSize; ++i)
        //{
        //    if (theGridSystem.theGridData.gridData[i] == null)
        //    {
        //        continue;
        //    }

        //    switch (theGridSystem.theGridData.gridData[i].GetUnitType())
        //    {
        //        case "Infantry":
        //            {
        //                theGridSystem.theGridData.gridData[i].thisGameObject = Instantiate(Infantry , transform.position, Quaternion.identity);
        //                theGridSystem.theGridData.gridData[i].thisGameObject.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform, true);
        //                theGridSystem.theGridData.gridData[i].thisGameObject.transform.position = theGridSystem.theGridData.gridData[i].GetPosition();
        //                //theGridSystem.theGridData.gridData[i].thisGameObject.transform. = new Vector3(100, 100, 100);
        //                theGridSystem.theGridData.gridData[i].thisGameObject.transform.localScale = new Vector3(100, 100, 100);
        //                break;
        //            }
        //        case "Cavalry":
        //            {
        //                break;
        //            }
        //        case "Bowmen":
        //            {
        //                break;
        //            }

        //        default:
        //            break;
        //    }

        //}
    }
	// Update is called once per frame
	void Update () {
        //Do unit updates here
        //    for (uint i = 0; i < GridSystem.gridSize; ++i)
        //    {
        //        if (theGridSystem.theGridData.gridData[i] == null)
        //        {
        //            continue;
        //        }
        //        //If the unit health drops to 0
        //        if (theGridSystem.theGridData.gridData[i].GetHealth() <= 0)
        //        {
        //            theGridSystem.theGridData.gridData[i] = null;
        //            continue;
        //        }

        //        //Unit Movement
        //        //theGridSystem.theGridData.gridData[i].AddPosition(0, Time.deltaTime * theGridSystem.theGridData.gridData[i].GetMoveSpeed());

        //        //If unit reaches the neutral zone
        //        if (theGridSystem.theGridData.gridData[i].GetPosition().y > NeutralZone.rectTransform.transform.position.y - NeutralZone.rectTransform.rect.width * 0.5f &&
        //        theGridSystem.theGridData.gridData[i].GetPosition().y < NeutralZone.rectTransform.transform.position.y + NeutralZone.rectTransform.rect.width * 0.5f)
        //        {
        //            if (!theGridSystem.theGridData.gridData[i].neutralZoneStatsChanged)
        //            {
        //                theGridSystem.theGridData.gridData[i].TerrainStatsModify(NeutralZoneTerrainType);
        //            }
        //        }
        //        else if (theGridSystem.theGridData.gridData[i].neutralZoneStatsChanged)
        //        {
        //            theGridSystem.theGridData.gridData[i].ResetStats();
        //        }
        //    }
    }
    void ShowInfo()
    {
        if (!TerrainInformation)                                                        //If TerrainInformation is null, create the necessary
        {
            TerrainInformation = new GameObject("TerrainInfo");                         //Creates new blank Gameobject with a name
            TerrainInformation.transform.SetParent(thisCanvas.transform);               //Parent to this canvas
            TerrainInformation.transform.position = NeutralZone.transform.position;     //Centralise on NeutralZoneImage position
            CreateTerrainInfoFor("Cavalry", new Vector2(-350, 0), TerrainInformation);  //Create texts for "Cavalry"
            CreateTerrainInfoFor("Infantry", new Vector2(0, 0), TerrainInformation);    //Create texts for "Infantry"
            CreateTerrainInfoFor("Bowmen", new Vector2(350, 0), TerrainInformation);    //Create texts for "Bowmen"
            SetTerrainInfo();                                                           //Sets information to be displayed to player
        }
        if (TerrainInformation.activeInHierarchy)                                       //If already active dont do anything
            return;
        else
        {
            SetTerrainInfo();                                                           //Calls function to asign variables to appropriate string
        }
    }
    void HideInfo()
    {
        ResetTerrainInfo();                     //Calls function to reset string 
        TerrainInformation.SetActive(false);    //Set parent to not active(so cannot be seen)
    }
    Text CreateText(GameObject _parent, Vector3 _displacement)
    {
        Text text = new GameObject().AddComponent<Text>();                                                                                      //Create empty Go wtith Text component
        text.transform.SetParent(_parent.transform);                                                                                            //Parent to background
        text.transform.position = _parent.transform.position + _displacement;                                                                   //Reposition text placement
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;                                                            //Set font
        text.rectTransform.localScale = TerrainInformation.gameObject.transform.localScale;                                                                     //
        //text.resizeTextForBestFit = true;                                                                                                     //Allow text to resize to fit to rectTranform scaling
        text.color = Color.black;                                                                                                               //Default text colour to black
        return text;
    }
    void CreateTerrainInfoFor(string _troopName, Vector2 _textPos, GameObject _parent)
    {
        Text result = CreateText(_parent, _textPos);            //Create text object
        result.rectTransform.sizeDelta = new Vector2(300, 56);  //Size from 'text' object
        result.fontSize = 50;                                   //Set text size to 50
        result.text = _troopName + ":";                         //Set text to display
        result.transform.name = _troopName;                     //Give the gameObject a unique name

        Text Info1 = CreateText(result.gameObject, new Vector2(0, -result.rectTransform.sizeDelta.y));          //Create text object
        Info1.rectTransform.sizeDelta = result.rectTransform.sizeDelta;                                         //Size from 'text' object
        Info1.fontSize = 30;                                                                                    //Set text size to 50
        Info1.text = "Attack Damage:";                                                                          //Set text to display
        Info1.transform.name = "Info1";                                                                         //Give the gameObject a unique name

        Text Info2 = CreateText(result.gameObject, new Vector2(0, -(2f * result.rectTransform.sizeDelta.y)));   //Create text object
        Info2.rectTransform.sizeDelta = Info1.rectTransform.sizeDelta;                                          //Size from 'text' object
        Info2.fontSize = Info1.fontSize;                                                                        //Set text size to 50
        Info2.text = "Movement Speed:";                                                                         //Set text to display
        Info2.transform.name = "Info2";                                                                         //Give the gameObject a unique name

        Text Info3 = CreateText(result.gameObject, new Vector2(0, -(3f * result.rectTransform.sizeDelta.y)));   //Create text object
        Info3.rectTransform.sizeDelta = Info1.rectTransform.sizeDelta;                                          //Size from 'text' object
        Info3.fontSize = Info1.fontSize;                                                                        //Set text size to 50
        Info3.text = "Attack Speed:";                                                                           //Set text to display
        Info3.transform.name = "Info3";                                                                         //Give the gameObject a unique name

    }
    void SetTerrainInfo()
    {
        SetTerrainInfo("Cavalry");         //Sets info for each troop
        SetTerrainInfo("Infantry");
        SetTerrainInfo("Bowmen");
        TerrainInformation.SetActive(true); //Set parent active to true
    }
    void SetTerrainInfo(string _name)
    {
        GameObject objParent = TerrainInformation.transform.Find(_name).gameObject;             //Find for GO with [_name] name
        if (objParent)                                                                          //If found
        {
            objParent.transform.Find("Info1").gameObject.GetComponent<Text>().text += "10";     //Append to string the values
            objParent.transform.Find("Info2").gameObject.GetComponent<Text>().text += "20";     //
            objParent.transform.Find("Info3").gameObject.GetComponent<Text>().text += "30";     //
        }
    }
    void ResetTerrainInfo()
    {
        ResetTerrainInfo("Cavalry");   //Resets the strings to default string
        ResetTerrainInfo("Infantry");
        ResetTerrainInfo("Bowmen");
    }
    void ResetTerrainInfo(string _name)
    {
        GameObject objParent = TerrainInformation.transform.Find(_name).gameObject;                     //Find for GO with [_name] name
        if (objParent)                                                                                  //If found
        {
            objParent.transform.Find("Info1").gameObject.GetComponent<Text>().text = "Attack Damage:";  //Reset the text to default string
            objParent.transform.Find("Info2").gameObject.GetComponent<Text>().text = "Movement Speed:"; //
            objParent.transform.Find("Info3").gameObject.GetComponent<Text>().text = "Attack Speed:";   //
        }
    }
}
