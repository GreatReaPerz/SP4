using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GridSystem : MonoBehaviour {

    //Note: Increasing the num of rows & col means that you also need to add more images into the array in the inspector
    public const ushort row = 4, col = 10;
    public const uint gridSize = row * col;
    public uint GridSize = gridSize;
    public const float tileWidth = 100;
    public const float tileHeight = 100;

    float scaledTileWidth; 
    float scaledTileHeight;

    float scaledHalfTileWidth; 
    float scaledHalfTileHeight;
    Vector3 CanvasScale;
    public bool[] taken = new bool[gridSize];
    private TetrisSpawner theTetrisSpawner = null;
    private HealthSystem PlayerHealth;

    public const float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;

    [SerializeField]
    public Image[] grid = new Image[gridSize];
    
    [SerializeField]
    Canvas thisCanvas;

    [SerializeField]
    Sprite GreyGridSprite;

    [SerializeField]
    Sprite BlueGridSprite;

    [SerializeField]
    TrapSystem theTrapSystem;

    // Use this for initialization
    public void Awake () {
        bool respawnBlock = GameObject.Find("EventSystem").GetComponent<GameCode>().blockRespawn;

        if (!respawnBlock)
        {
            for (int i = 0; i < gridSize; ++i)
            {
                taken[i] = false;
            }
        }
        theTetrisSpawner = GameObject.Find("EventSystem").GetComponent<TetrisSpawner>();
        PlayerHealth = GameObject.Find("Player").GetComponent<HealthSystem>();
        
        //Debug.Assert(theTetrisSpawner != null);

        CanvasScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;

        //scaledTileWidth = tileWidth * CanvasScale.x;
        //scaledTileHeight = tileHeight * CanvasScale.y;

        //scaledHalfTileWidth = tileWidth * CanvasScale.x * 0.5f;
        //scaledHalfTileHeight = tileHeight * CanvasScale.y * 0.5f;

        scaledTileWidth = tileWidth * ((float)Screen.width / 1080);
        scaledTileHeight = tileHeight * ((float)Screen.height / 1920);

        scaledHalfTileWidth = tileWidth * ((float)Screen.width / 1080) * 0.5f;
        scaledHalfTileHeight = tileHeight * ((float)Screen.height / 1920) * 0.5f;


        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        Vector2 Grid0Pos = new Vector2(objectRectTransform.transform.position.x - (0.5f * (col - 1) * tileWidth) , objectRectTransform.transform.position.y  - ((row * tileHeight)) - (2 * (tileHeight)));
        grid[0].transform.position = Grid0Pos;
        //EventTrigger.Entry mouseClick = new EventTrigger.Entry();                                                                       //Create trigger
        //mouseClick.eventID = EventTriggerType.PointerClick;                                                                             //Define trigger type   (Pointer click)
        //mouseClick.callback.AddListener((data) => { theTrapSystem.setToChooseTrap(); });                                                //Add listener to call function/ do something(changes text)
        for (uint i = 0; i < gridSize; ++i)
        {
            //Adjusts the individual grid block's size
            grid[i].rectTransform.sizeDelta = new Vector2(tileWidth, tileHeight);
            
            //Anchor to bottom
            grid[i].rectTransform.anchorMin = new Vector2(0.5f, 0);
            grid[i].rectTransform.anchorMax = new Vector2(0.5f, 0);
            grid[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);

            //EventTrigger mytrigger = grid[i].gameObject.AddComponent<EventTrigger>();
            //mytrigger.triggers.Add(mouseClick);                                                                                             //Add to Event Trigger
            if (i == 0)
            {
                continue;
            }
            
            //Adjusts the individual grid block's position
            if (i < col)
            {
                grid[i].transform.position = new Vector2(grid[0].transform.position.x + (i * tileWidth), grid[0].transform.position.y);
            }
            else
            {
                grid[i].transform.position = new Vector2(grid[i - col].transform.position.x, grid[i - col].transform.position.y + tileHeight);
            }

        }
        Init();
    }

    private uint objectIndex = 0;
    bool isMouseMovingAnObject = false;

    // Update is called once per frame
    void Update()
    {
    }

    public void GameUpdate()
    {

        if (Input.GetMouseButton(0) == true)
        {
            if (theTetrisSpawner.playerIsMoving == true)
            {
                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].sav = false;
                uint colNum = 0, rowNum = 0;

                //Check col
                for (uint it = 0; it < col; ++it)
                {
                    if (grid[0].transform.position.x + ((scaledTileWidth * (it + 1)) - scaledHalfTileWidth) > Input.mousePosition.x)
                    {
                        colNum = it;
                        break;
                    }
                }

                //Check row
                for (uint it = 0; it < row; ++it)
                {
                    if (grid[0].transform.position.y + ((scaledTileHeight * (it + 1)) - scaledHalfTileHeight) > Input.mousePosition.y)
                    {
                        rowNum = it;
                        break;
                    }
                }

                //Text test = GameObject.Find("THIS").GetComponent<Text>();
                //test.text = rowNum.ToString();
                float nearest = 1000000;
                uint index = 0;
                for (uint j = 0; j < gridSize; ++j)
                {
                    Vector2 distance;
                    distance.x = Input.mousePosition.x - grid[j].transform.position.x;
                    distance.y = Input.mousePosition.y - grid[j].transform.position.y;
                    float hello = distance.SqrMagnitude();
                    if (hello < nearest)
                    {
                        nearest = hello;
                        index = j;
                    }
                }
                
               objectIndex = index;

                //objectIndex = colNum + (rowNum * col);

                //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
                bool mouse = false;
                if (Input.mousePosition.x < grid[0].transform.position.x - scaledTileWidth || Input.mousePosition.x > grid[col - 1].transform.position.x + scaledTileWidth
                 && Input.mousePosition.y < grid[0].transform.position.y - scaledTileHeight && Input.mousePosition.y > grid[gridSize - 1].transform.position.y + scaledTileHeight)
                {
                    mouse = true;
                    //Debug.Log("hello");
                }
                if (InGridCheck(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject]) && !theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning && !mouse)
                {
                    switch (theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].Whatisbeingmoved)
                    {
                        case "partOne":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.MovePosition(Input.mousePosition);
                                    //  theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partTwo":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.MovePosition(Input.mousePosition);
                                    // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partThree":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.MovePosition(Input.mousePosition);
                                    // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].topLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partFour":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.MovePosition(Input.mousePosition);
                                    // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].topRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        default:
                            break;


                           // Debug.Log(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.transform.position);
                    };
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                //  theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                    }
                }
                isMouseMovingAnObject = true;
            }

        }
        else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
        {
            //Set the tetris block to not moving
            //tetrisBlock.isMoving = false;
            theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].isMoving = false;
            if(!InGridCheck(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject]))
            {
                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
            }
            isMouseMovingAnObject = false;
        }
        //When the tetris block is picked up from the grid, it removes the data from that tile
        //if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
        //{
        //    isMouseMovingAnObject = false;
        //}
        for (int i = 0; i < theTetrisSpawner.playerList.Count; ++i)
        {

            if (!InGridCheck(theTetrisSpawner.playerList[i]) && theTetrisSpawner.playerList[i].isMoving == false && !theTetrisSpawner.playerList[i].returning)
            {
                theTetrisSpawner.playerList[i].returning = true;
                theTetrisSpawner.playerList[i].partOne.position = theTetrisSpawner.playerList[i].origin;
            }
            if (theTetrisSpawner.playerList[i].returning && theTetrisSpawner.playerList[i].isMoving == false)
            {

                theTetrisSpawner.playerList[i].partOne.position = theTetrisSpawner.playerList[i].origin;
            }
            if(theTetrisSpawner.playerList[i].partOne.transform.position.x ==  theTetrisSpawner.playerList[i].origin.x 
                && theTetrisSpawner.playerList[i].partOne.transform.position.y == theTetrisSpawner.playerList[i].origin.y)
            {
                theTetrisSpawner.playerList[i].returning = false;
            }
            for (int j = 0; j < gridSize; ++j)
            {
                taken[j] = false;
            }
            if (InGridCheck(theTetrisSpawner.playerList[i]) && !theTetrisSpawner.playerList[i].returning && theTetrisSpawner.playerList[i].isMoving == false)
            {
                float nearest = 1000000;
                int index = 0;
                for (int j = 0; j < gridSize; ++j)
                {
                    Vector2 distance;
                    distance.x = theTetrisSpawner.playerList[i].partOne.position.x - grid[j].transform.position.x;
                    distance.y = theTetrisSpawner.playerList[i].partOne.position.y - grid[j].transform.position.y;
                    float hello = distance.SqrMagnitude();
                    if (hello < nearest)
                    {
                        nearest = hello;
                        index = j;
                    }
                }
                if (taken[index])
                {
                    theTetrisSpawner.playerList[i].returning = true;
                    theTetrisSpawner.playerList[i].partOne.position = theTetrisSpawner.playerList[i].origin;
                }
                else
                {
                    theTetrisSpawner.playerList[i].partOne.MovePosition(grid[index].transform.position);
                }
            }
            for (int k = 0; k < theTetrisSpawner.playerList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.playerList[k]) && theTetrisSpawner.playerList[k].isMoving == false && !theTetrisSpawner.playerList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            for (int k = 0; k < theTetrisSpawner.playerList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.playerList[k]) && theTetrisSpawner.playerList[k].isMoving == false && !theTetrisSpawner.playerList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.x - grid[j].transform.position.x) < 10 * CanvasScale.x
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.y - grid[j].transform.position.y) < 10 * CanvasScale.x))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            if(InGridCheck(theTetrisSpawner.playerList[i]) && theTetrisSpawner.playerList[i].isMoving == false)
            {
                theTetrisSpawner.playerList[i].sav = true;
            }
            else
            {
                theTetrisSpawner.playerList[i].sav = false;
            }
        }
    }

    public bool InGridCheck(TetrisCube cube)
    {
        if (cube.partOne.position.x > grid[0].transform.position.x - scaledHalfTileWidth && cube.partOne.position.x < grid[col - 1].transform.position.x + scaledHalfTileWidth
            && cube.partOne.position.y > grid[0].transform.position.y - scaledHalfTileHeight && cube.partOne.position.y < grid[gridSize - 1].transform.position.y + scaledHalfTileHeight
            && cube.partTwo.position.x > grid[0].transform.position.x - scaledHalfTileWidth && cube.partTwo.position.x < grid[col - 1].transform.position.x + scaledHalfTileWidth
            && cube.partTwo.position.y > grid[0].transform.position.y - scaledHalfTileHeight && cube.partTwo.position.y < grid[gridSize - 1].transform.position.y + scaledHalfTileHeight
            && cube.partThree.position.x > grid[0].transform.position.x - scaledHalfTileWidth && cube.partThree.position.x < grid[col - 1].transform.position.x + scaledHalfTileWidth
            && cube.partThree.position.y > grid[0].transform.position.y - scaledHalfTileHeight && cube.partThree.position.y < grid[gridSize - 1].transform.position.y + scaledHalfTileHeight
            && cube.partFour.position.x > grid[0].transform.position.x - scaledHalfTileWidth && cube.partFour.position.x < grid[col - 1].transform.position.x + scaledHalfTileWidth
            && cube.partFour.position.y > grid[0].transform.position.y - scaledHalfTileHeight && cube.partFour.position.y < grid[gridSize - 1].transform.position.y + scaledHalfTileHeight)
        {
            return true;
        }
        return false;

    }

    public bool[] gridData;

    uint gridDataSize;

    public void Init()
    {
        gridDataSize = GridSystem.gridSize;
        gridData = new bool[gridDataSize];
    }

    public bool IsGreyedOut(uint index)
    {
        return gridData[index];
    }

    public uint SetIsGreyOut(uint index, bool GreyOut = true)
    {
        gridData[index] = GreyOut;

        grid[index].sprite = GreyGridSprite;
        return index;
    }
    public uint UnSetIsGreyOut(uint index, bool GreyOut = false)
    {
        gridData[index] = GreyOut;
        grid[index].sprite = BlueGridSprite;

        return index;
    }
    public void CheckGreyedGrid()
    {
        for (uint x = 0; x < col; ++x)
        {
            bool colGreyed = true;
            for (uint numRow = 0; numRow < row; ++numRow)
            {
                if (!IsGreyedOut(x + numRow * 10))
                {
                    colGreyed = false;
                }
            }
            if (colGreyed)
            {
                PlayerHealth.addHealth(-2);
                for (uint numRow = 0; numRow < row; ++numRow)
                {
                    UnSetIsGreyOut(x + numRow * 10);
                }
            }
        }
        //Check Row
        for (uint y = 0; y < row - 1; ++y)
        {
            bool rowGreyed = true;
            for (uint numCol = 0; numCol < col; ++numCol)
            {
                if (!IsGreyedOut(numCol + y * 10))
                {
                    rowGreyed = false;
                    break;
                }
            }
            if (rowGreyed)
            {
                PlayerHealth.addHealth(-5);
                for (uint numCol = 0; numCol < col; ++numCol)
                {
                    UnSetIsGreyOut(numCol + y * 10);
                }
            }
        }

        //Check Column
        for (uint x = 0; x < col; ++x)
        {
            for (uint numRow = 1; numRow < row; ++numRow)
            {
                if (IsGreyedOut(x + numRow * 10) && !IsGreyedOut(x + (numRow - 1) * 10))
                {
                    UnSetIsGreyOut(x + numRow * 10);
                    SetIsGreyOut(x + (numRow - 1) * 10);
                    //Debug.Log("giggy1");
                }
            }
        }
    //}
    //catch (Exception e)
    //{
    //    Debug.Log(e.ToString());
    //}
}

    public void GameUpdateAndroid()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchposition = Input.GetTouch(0).position;

            if (theTetrisSpawner.playerIsMoving == true)
            {
                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].sav = false;
                uint colNum = 0, rowNum = 0;
                
                //Check col
                for (uint it = 0; it < col; ++it)
                {
                    if (grid[0].transform.position.x + ((scaledTileWidth * (it + 1)) - scaledHalfTileWidth) > touchposition.x)
                    {
                        colNum = it;
                        break;
                    }
                }

                //Check row
                for (uint it = 0; it < row; ++it)
                {
                    if (grid[0].transform.position.y + ((scaledTileHeight * (it + 1)) - scaledHalfTileHeight) > touchposition.y)
                    {
                        rowNum = it;
                        break;
                    }
                }
                
                objectIndex = colNum + (rowNum * col);
                //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
                bool mouse = false;

                if (touchposition.x < grid[0].transform.position.x - scaledHalfTileWidth - 50 || touchposition.x > grid[col - 1].transform.position.x + scaledHalfTileWidth + 50
                 && touchposition.y < grid[0].transform.position.y - scaledHalfTileHeight - 50 && touchposition.y > grid[gridSize - 1].transform.position.y + scaledHalfTileHeight + 50)
                {
                    mouse = true;
                }
                if (InGridCheck(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject]) && !theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning && !mouse)
                {
                    switch (theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].Whatisbeingmoved)
                    {
                        case "partOne":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.MovePosition(touchposition);
                                    //  theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.MovePosition(grid[objectIndex].transform.position);
                                }

                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partTwo":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.MovePosition(touchposition);
                                    // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partThree":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.MovePosition(touchposition);
                                    // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].topLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partFour":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.MovePosition(touchposition);
                                    // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].topRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        default:
                            break;
                    };
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                //theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                // theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
                                //  theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].btmLeft.position = theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].origin;
                            }
                        }
                    }
                }
                isMouseMovingAnObject = true;
            }

        }
        else if (isMouseMovingAnObject) //When the touch is released, it saves the tetris data into the data grid tile
        {
            //Set the tetris block to not moving
            //tetrisBlock.isMoving = false;
            theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].isMoving = false;
            if (!InGridCheck(theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject]))
            {
                theTetrisSpawner.playerList[theTetrisSpawner.IndexofPlayerObject].returning = true;
            }
            isMouseMovingAnObject = false;
        }
        //When the tetris block is picked up from the grid, it removes the data from that tile
        //if (isMouseMovingAnObject && Input.touchCount > 0)
        //{
        //    isMouseMovingAnObject = false;
        //}
        for (int i = 0; i < theTetrisSpawner.playerList.Count; ++i)
        {

            if (!InGridCheck(theTetrisSpawner.playerList[i]) && theTetrisSpawner.playerList[i].isMoving == false && !theTetrisSpawner.playerList[i].returning)
            {
                theTetrisSpawner.playerList[i].returning = true;
                theTetrisSpawner.playerList[i].partOne.position = theTetrisSpawner.playerList[i].origin;
            }
            if (theTetrisSpawner.playerList[i].returning && theTetrisSpawner.playerList[i].isMoving == false)
            {

                theTetrisSpawner.playerList[i].partOne.position = theTetrisSpawner.playerList[i].origin;
            }
            if (theTetrisSpawner.playerList[i].partOne.transform.position.x == theTetrisSpawner.playerList[i].origin.x
                && theTetrisSpawner.playerList[i].partOne.transform.position.y == theTetrisSpawner.playerList[i].origin.y)
            {
                theTetrisSpawner.playerList[i].returning = false;
            }
            for (int j = 0; j < gridSize; ++j)
            {
                taken[j] = false;
            }
            if (InGridCheck(theTetrisSpawner.playerList[i]) && !theTetrisSpawner.playerList[i].returning && theTetrisSpawner.playerList[i].isMoving == false)
            {
                float nearest = 1000000;
                int index = 0;
                for (int j = 0; j < gridSize; ++j)
                {
                    Vector2 distance;
                    distance.x = theTetrisSpawner.playerList[i].partOne.position.x - grid[j].transform.position.x;
                    distance.y = theTetrisSpawner.playerList[i].partOne.position.y - grid[j].transform.position.y;
                    float hello = distance.SqrMagnitude();
                    if (hello < nearest)
                    {
                        nearest = hello;
                        index = j;
                    }
                }
                if (taken[index])
                {
                    theTetrisSpawner.playerList[i].returning = true;
                    theTetrisSpawner.playerList[i].partOne.position = theTetrisSpawner.playerList[i].origin;
                }
                else
                {
                    theTetrisSpawner.playerList[i].partOne.MovePosition(grid[index].transform.position);
                }
            }
            for (int k = 0; k < theTetrisSpawner.playerList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.playerList[k]) && theTetrisSpawner.playerList[k].isMoving == false && !theTetrisSpawner.playerList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            for (int k = 0; k < theTetrisSpawner.playerList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.playerList[k]) && theTetrisSpawner.playerList[k].isMoving == false && !theTetrisSpawner.playerList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.playerList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            if (InGridCheck(theTetrisSpawner.playerList[i]) && theTetrisSpawner.playerList[i].isMoving == false)
            {
                theTetrisSpawner.playerList[i].sav = true;
            }
            else
            {
                theTetrisSpawner.playerList[i].sav = false;
            }
        }
    }
}

//Terrain basic unit modifiers: Hills favourable to Bowmen, Forest favourable to Combat Infantry, Rivers favourable to Cavalry
//Plains favour all units