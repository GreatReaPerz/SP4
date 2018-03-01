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

    [SerializeField]
    Sprite PaperBg;
    private GridSystem theGridSystem = null;

     public string NeutralZoneTerrainType;

    GameObject TerrainInformation = null;

    public struct TerrainModifierValue
    {
        public float attackDamage;
        public float speed;
        public float attackSpeed;
    }

    public TerrainModifierValue TMV_Cavalry;
    public TerrainModifierValue TMV_Infantry;
    public TerrainModifierValue TMV_Bowmen;

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
            NeutralZone.rectTransform.sizeDelta = new Vector2((objectRectTransform.rect.width) * canvasLocalScale.x * 2, (objectRectTransform.rect.height * canvasLocalScale.y) * 0.53f);
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
            TMV_Cavalry.attackDamage = 1.1f;
            TMV_Infantry.attackDamage = 1.1f;
            TMV_Bowmen.attackDamage = 1.1f;

            TMV_Cavalry.speed = 1.15f;
            TMV_Infantry.speed = 1.2f;
            TMV_Bowmen.speed = 1.1f;

            TMV_Cavalry.attackSpeed = 1.1f;
            TMV_Infantry.attackSpeed = 1.1f;
            TMV_Bowmen.attackSpeed = 1.2f;
            NeutralZone.sprite = PlainsSprite;
        }
        else if (ranNum > 2.0f)
        {
            NeutralZoneTerrainType = "River";
            TMV_Cavalry.attackDamage = 1.1f;
            TMV_Infantry.attackDamage = 0.9f;
            TMV_Bowmen.attackDamage = 0.9f;

            TMV_Cavalry.speed = 0.9f;
            TMV_Infantry.speed = 0.85f;
            TMV_Bowmen.speed = 0.9f;

            TMV_Cavalry.attackSpeed = 0.9f;
            TMV_Infantry.attackSpeed = 0.9f;
            TMV_Bowmen.attackSpeed = 0.9f;
            NeutralZone.sprite = RiverSprite;
        }
        else if (ranNum > 1.0f)
        {
            NeutralZoneTerrainType = "Hills";
            TMV_Cavalry.attackDamage = 0.85f;
            TMV_Infantry.attackDamage = 0.9f;
            TMV_Bowmen.attackDamage = 1.1f;

            TMV_Cavalry.speed = 0.75f;
            TMV_Infantry.speed = 0.75f;
            TMV_Bowmen.speed = 1.05f;

            TMV_Cavalry.attackSpeed = 0.85f;
            TMV_Infantry.attackSpeed = 0.9f;
            TMV_Bowmen.attackSpeed = 0.9f;
            NeutralZone.sprite = HillsSprite;
        }
        else if (ranNum > 0.0f)
        {
            NeutralZoneTerrainType = "Forest";
            TMV_Cavalry.attackDamage = 0.9f;
            TMV_Infantry.attackDamage = 1.1f;
            TMV_Bowmen.attackDamage = 0.85f;

            TMV_Cavalry.speed = 0.85f;
            TMV_Infantry.speed = 0.9f;
            TMV_Bowmen.speed = 0.9f;

            TMV_Cavalry.attackSpeed = 0.95f;
            TMV_Infantry.attackSpeed = 0.95f;
            TMV_Bowmen.attackSpeed = 0.85f;
            NeutralZone.sprite = ForestSprite;
        }
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
    public void ShowInfo()
    {
        if (!TerrainInformation)                                                        //If TerrainInformation is null, create the necessary
        {
            TerrainInformation = new GameObject("TerrainInfo");                         //Creates new blank Gameobject with a name
            TerrainInformation.transform.SetParent(GameObject.Find("UIPanelsHolder").transform);               //Parent to this canvas
            TerrainInformation.transform.position = NeutralZone.transform.position;     //Centralise on NeutralZoneImage position
            Image BG = TerrainInformation.AddComponent<Image>();
            BG.sprite = PaperBg;
            BG.rectTransform.sizeDelta = NeutralZone.rectTransform.sizeDelta;
            Vector2 canvasLocalScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;
            BG.rectTransform.sizeDelta= new Vector2(BG.rectTransform.rect.width * canvasLocalScale.x, BG.rectTransform.rect.height * canvasLocalScale.y);
            Color BGColor = BG.color;
            BGColor.a = 0.6f;
            BG.color = BGColor;
            CreateTerrainInfoFor("Cavalry", new Vector2(-320, 100), TerrainInformation, TMV_Cavalry, canvasLocalScale);  //Create texts for "Cavalry"
            CreateTerrainInfoFor("Infantry", new Vector2(0, 100), TerrainInformation, TMV_Infantry, canvasLocalScale);    //Create texts for "Infantry"
            CreateTerrainInfoFor("Bowmen", new Vector2(330, 100), TerrainInformation, TMV_Bowmen, canvasLocalScale);    //Create texts for "Bowmen"
            SetTerrainInfo();                                                           //Sets information to be displayed to player
        }
        if (TerrainInformation.activeInHierarchy)                                       //If already active dont do anything
            return;
        else
        {
            SetTerrainInfo();                                                           //Calls function to asign variables to appropriate string
        }
    }
    public void HideInfo()
    {
        ResetTerrainInfo();                     //Calls function to reset string 
        TerrainInformation.SetActive(false);    //Set parent to not active(so cannot be seen)
    }
    Text CreateText(GameObject _parent, Vector3 _displacement, Vector2 CanvasScale)
    {
        Text text = new GameObject().AddComponent<Text>();                                                                                      //Create empty Go wtith Text component
        text.transform.SetParent(_parent.transform);                                                                                            //Parent to background
        text.transform.position = _parent.transform.position + _displacement;                                                                   //Reposition text placement
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;                                                            //Set font
        text.rectTransform.localScale = TerrainInformation.gameObject.transform.localScale;                                                                     //
        text.rectTransform.sizeDelta = CanvasScale;
        //text.resizeTextForBestFit = true;                                                                                                     //Allow text to resize to fit to rectTranform scaling
        text.color = Color.black;                                                                                                               //Default text colour to black
        return text;
    }
    void CreateTerrainInfoFor(string _troopName, Vector2 _textPos, GameObject _parent, TerrainModifierValue _myTMV, Vector2 CanvasScale)
    {
        Text result = CreateText(_parent, _textPos,CanvasScale);            //Create text object
        result.rectTransform.sizeDelta = new Vector2(350, 56);  //Size from 'text' object
        result.fontSize = 50;                                   //Set text size to 50
        result.text = _troopName + ":";                         //Set text to display
        result.transform.name = _troopName;                     //Give the gameObject a unique name

        Text Info1 = CreateText(result.gameObject, new Vector2(0, -result.rectTransform.sizeDelta.y), CanvasScale);          //Create text object
        Info1.rectTransform.sizeDelta = result.rectTransform.sizeDelta;                                         //Size from 'text' object
        Info1.fontSize = 30;                                                                                    //Set text size to 50
        Info1.text = "Attack Damage:";                                                                          //Set text to display
        Info1.transform.name = "Info1";                                                                         //Give the gameObject a unique name

        Text Info2 = CreateText(result.gameObject, new Vector2(0, -(2f * result.rectTransform.sizeDelta.y)), CanvasScale);   //Create text object
        Info2.rectTransform.sizeDelta = Info1.rectTransform.sizeDelta;                                          //Size from 'text' object
        Info2.fontSize = Info1.fontSize;                                                                        //Set text size to 50
        Info2.text = "Movement Speed:";                                                                         //Set text to display
        Info2.transform.name = "Info2";                                                                         //Give the gameObject a unique name

        Text Info3 = CreateText(result.gameObject, new Vector2(0, -(3f * result.rectTransform.sizeDelta.y)), CanvasScale);   //Create text object
        Info3.rectTransform.sizeDelta = Info1.rectTransform.sizeDelta;                                          //Size from 'text' object
        Info3.fontSize = Info1.fontSize;                                                                        //Set text size to 50
        Info3.text = "Attack Speed:";                                                                           //Set text to display
        Info3.transform.name = "Info3";                                                                         //Give the gameObject a unique name

    }
    void SetTerrainInfo()
    {
        SetTerrainInfo("Cavalry", TMV_Cavalry);         //Sets info for each troop
        SetTerrainInfo("Infantry", TMV_Infantry);
        SetTerrainInfo("Bowmen", TMV_Bowmen);
        TerrainInformation.SetActive(true); //Set parent active to true
    }
    void SetTerrainInfo(string _name, TerrainModifierValue _myTMV)
    {
        GameObject objParent = TerrainInformation.transform.Find(_name).gameObject;             //Find for GO with [_name] name
        if (objParent)                                                                          //If found
        {
            objParent.transform.Find("Info1").gameObject.GetComponent<Text>().text += ((int)(_myTMV.attackDamage * 100f)).ToString() + "%";     //Append to string the values
            objParent.transform.Find("Info2").gameObject.GetComponent<Text>().text += ((int)(_myTMV.speed * 100f)).ToString() + "%";     //
            objParent.transform.Find("Info3").gameObject.GetComponent<Text>().text += ((int)(_myTMV.attackSpeed * 100f)).ToString() + "%";     //
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
