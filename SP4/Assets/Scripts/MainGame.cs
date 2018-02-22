using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private GridSystem_Temp theGridSystem = null;

     public string NeutralZoneTerrainType;

    // Use this for initialization
    public void Start()
    {

        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem_Temp>();
        Debug.Assert(theGridSystem != null);

        //Create neutral zone
        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        NeutralZone.transform.position = objectRectTransform.transform.position;
        NeutralZone.rectTransform.sizeDelta = new Vector2(objectRectTransform.rect.width, objectRectTransform.rect.height * 0.1f);

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
}
