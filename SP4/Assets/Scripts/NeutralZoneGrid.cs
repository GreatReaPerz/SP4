using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeutralZoneGrid : MonoBehaviour {

    [SerializeField]
    public Canvas thisCanvas;

    [SerializeField]
    public Sprite GridTileTexture;

    [SerializeField]
    public Image SampleImage;

    uint PossibleRowTilesinCanvas = 0;
    uint NeutralZoneGridsColNum = 0, NeutralZoneGridsRowNum = 0;

    private GridSystem theGridSystem = null;
    private enemyGridSystem theEnemyGridSystem = null;

    public Image[] NeutralGrid;

    // Use this for initialization
    void Start () {
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        theEnemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();

        Debug.Assert(theGridSystem != null);
        Debug.Assert(theEnemyGridSystem != null);

        //Calculate the number of possible rows the canvas can hold
        //PossibleRowTilesinCanvas = (uint)(thisCanvas.GetComponent<RectTransform>().rect.height / GridSystem.tileHeight);

        //Calculate the number of rows of neutral grid based on the number of tiles left the canvas can hold
        NeutralZoneGridsRowNum = 5;
        NeutralZoneGridsColNum = GridSystem.col;

        NeutralGrid = new Image[NeutralZoneGridsRowNum * NeutralZoneGridsColNum];

        //Initialize the images in NeutralGrid
        InitGrid();
    }
	
    void InitGrid()
    {
        NeutralGrid[0] = Instantiate(SampleImage);

        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();

        Vector2 Grid0Pos = new Vector2(objectRectTransform.transform.position.x - (0.5f * (NeutralZoneGridsColNum - 1) * GridSystem.tileWidth), 
            objectRectTransform.transform.position.y - ((NeutralZoneGridsRowNum * GridSystem.tileHeight) * 0.5f) + GridSystem.halfTileHeight);

        NeutralGrid[0].transform.position = Grid0Pos;

        for (uint i = 0; i < (NeutralZoneGridsColNum * NeutralZoneGridsRowNum); ++i)
        {
            if (i != 0)
            {
                NeutralGrid[i] = Instantiate(SampleImage);
            }

            //Adjusts the individual NeutralGrid block's size
            NeutralGrid[i].rectTransform.sizeDelta = new Vector2(GridSystem.tileWidth, GridSystem.tileHeight);

            //Anchor to middle
            NeutralGrid[i].rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            NeutralGrid[i].rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            NeutralGrid[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);

            NeutralGrid[i].transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            NeutralGrid[i].sprite = GridTileTexture;

            if (i == 0)
            {
                continue;
            }

            //Adjusts the individual NeutralGrid block's position
            if (i < NeutralZoneGridsColNum)
            {
                NeutralGrid[i].transform.position = new Vector2(NeutralGrid[0].transform.position.x + (i * GridSystem.tileWidth), NeutralGrid[0].transform.position.y);
            }
            else
            {
                NeutralGrid[i].transform.position = new Vector2(NeutralGrid[i - NeutralZoneGridsColNum].transform.position.x, NeutralGrid[i - NeutralZoneGridsColNum].transform.position.y +  GridSystem.tileHeight);
            }
        }
    }

	// Update is called once per frame
	void Update () {
	}
}
