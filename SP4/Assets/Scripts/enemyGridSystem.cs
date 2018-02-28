using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGridSystem : MonoBehaviour
{

    //Note: Increasing the num of rows & col means that you also need to add more images into the array in the inspector
    public const ushort row = 4, col = 10;
    public const uint gridSize = row * col;
    public uint GridSize = gridSize;
    const float tileWidth = 100;
    const float tileHeight = 100;
    public bool multi;
    private TetrisSpawner theTetrisSpawner = null;
    public bool[] check = new bool[3];
    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;
    public bool[] taken = new bool[gridSize];

    private HealthSystem EnemyHealth;

    [SerializeField]
    public Image[] grid = new Image[gridSize];

    [SerializeField]
    Canvas thisCanvas;
    
    [SerializeField]
    Sprite GreyGridSprite;

    [SerializeField]
    Sprite BlueGridSprite;

    GameCode theGameCode;
    // Use this for initialization
    public void Awake()
    {
        theGameCode = GameObject.Find("EventSystem").GetComponent<GameCode>();
        bool respawnBlock = theGameCode.blockRespawn;
        for(int i =0; i< 3; ++ i)
        {
            check[i] = false;
        }
        if (!respawnBlock)
        {
            for (int i = 0; i < gridSize; ++i)
            {
                taken[i] = false;
            }
        }
        theTetrisSpawner = GameObject.Find("EventSystem").GetComponent<TetrisSpawner>();
        EnemyHealth = GameObject.Find("Enemy").GetComponent<HealthSystem>();
        Debug.Assert(theTetrisSpawner != null);

        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        Vector2 Grid0Pos = new Vector2(objectRectTransform.transform.position.x + (0.5f * (col - 1) * tileWidth), objectRectTransform.transform.position.y + ((row * tileHeight)) + (2 * (tileHeight)));
        grid[0].transform.position = Grid0Pos;

        for (uint i = 0; i < gridSize; ++i)
        {
            //Adjusts the individual grid block's size
            grid[i].rectTransform.sizeDelta = new Vector2(tileWidth, tileHeight);

            //Anchor to top
            grid[i].rectTransform.anchorMin = new Vector2(0.5f, 1);
            grid[i].rectTransform.anchorMax = new Vector2(0.5f, 1);
            grid[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);

            if (i == 0)
            {
                continue;
            }

            //Adjusts the individual grid block's position
            if (i < col)
            {
                grid[i].transform.position = new Vector2(grid[0].transform.position.x - (i * tileWidth), grid[0].transform.position.y);
                //Debug.Log(grid[i].transform.position);
            }
            else
            {
                grid[i].transform.position = new Vector2(grid[i - col].transform.position.x, grid[i - col].transform.position.y - tileHeight);
                //Debug.Log(grid[i].transform.position);
            }
        }
        Init();
    }

    private uint objectIndex = 0;
    bool isMouseMovingAnObject = false;

    // Update is called once per frame
    //void Update()
    //{
    //    if(!check)
    //    {
    //        for (int i = 0; i < 3; ++i)
    //        {
    //            theTetrisSpawner.enemyList[i].btmLeft.MovePosition(grid[(i * 3) + 11].transform.position);
    //            theTetrisSpawner.enemyList[i].isMoving = true;
    //            if (Mathf.Abs(theTetrisSpawner.enemyList[i].btmLeft.position.x - grid[(i * 3) + 11].transform.position.x) < 10
    //                && (Mathf.Abs(theTetrisSpawner.enemyList[i].btmLeft.position.y - grid[(i * 3) + 11].transform.position.y) < 10))
    //            {
    //                check = true;
    //                theTetrisSpawner.enemyList[i].isMoving = false;
    //                theTetrisSpawner.enemyList[i].returning = false;
    //            }
    //        }
    //       // check = true;
    //    }

    //    if (Input.GetMouseButton(0) == true)
    //    {
    //        Debug.Log("ki");
    //        if (theTetrisSpawner.SomethingIsMoving == true)
    //        {
    //            uint colNum = 0, rowNum = 0;

    //            //Check col
    //            for (uint it = 0; it < col; ++it)
    //            {
    //                if (grid[0].transform.position.x + ((tileWidth * (it + 1)) - halfTileWidth) > Input.mousePosition.x)
    //                {
    //                    colNum = it;
    //                    break;
    //                }
    //            }

    //            //Check row
    //            for (uint it = 0; it < row; ++it)
    //            {
    //                if (grid[0].transform.position.y + ((tileHeight * (it + 1)) - halfTileHeight) > Input.mousePosition.y)
    //                {
    //                    rowNum = it;
    //                    break;
    //                }
    //            }

    //            objectIndex = colNum + (rowNum * col);
    //            //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
    //            if (InGridCheck(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject]) && !theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning)
    //            {
    //                switch (theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].Whatisbeingmoved)
    //                {
    //                    case "btmLeft":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.MovePosition(grid[objectIndex].transform.position);
    //                            }

    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "btmRight":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "topLeft":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "topRight":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    default:
    //                        break;
    //                };
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                }
    //                isMouseMovingAnObject = true;
    //            }
    //        }

    //    }
    //    else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
    //    {
    //        //Set the tetris block to not moving
    //        //tetrisBlock.isMoving = false;
    //        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].isMoving = false;
    //        //Pass the tetris block data into the grid data
    //        if (theGridData.AddTetrisBlockData(objectIndex, "Test01", "UnitTest", 100, 20, 35, 45, grid[objectIndex].transform.position))
    //        {
    //            Debug.Log("Successful Save");
    //        }
    //        else
    //        {
    //            Debug.Log("Unsuccessful Save");
    //        }
    //    }
    //    //When the tetris block is picked up from the grid, it removes the data from that tile
    //    if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
    //    {
    //        //Remove the data from the tile
    //        if (theGridData.RemoveTetrisBlockData(objectIndex))
    //        {
    //            Debug.Log("Successful Removal");
    //        }
    //        else
    //        {
    //            Debug.Log("Unsuccessful Removal");
    //        }

    //        isMouseMovingAnObject = false;
    //    }
    //    for (int i = 0; i < 3; ++i)
    //    {

    //        if (!InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false && !theTetrisSpawner.enemyList[i].returning)
    //        {
    //            Debug.Log(theTetrisSpawner.enemyList[i].btmLeft.position);
    //            Debug.Log(theTetrisSpawner.enemyList[i].btmRight.position);
    //            Debug.Log(theTetrisSpawner.enemyList[i].topLeft.position);
    //            Debug.Log(theTetrisSpawner.enemyList[i].topRight.position);
    //            theTetrisSpawner.enemyList[i].returning = true;
    //            theTetrisSpawner.enemyList[i].btmLeft.position = theTetrisSpawner.enemyList[i].origin;
    //        }
    //        if (theTetrisSpawner.enemyList[i].returning)
    //        {
    //            theTetrisSpawner.enemyList[i].btmLeft.position = theTetrisSpawner.enemyList[i].origin;
    //        }
    //        if (theTetrisSpawner.enemyList[i].btmLeft.transform.position.x == theTetrisSpawner.enemyList[i].origin.x
    //            && theTetrisSpawner.enemyList[i].btmLeft.transform.position.y == theTetrisSpawner.enemyList[i].origin.y)
    //        {
    //            theTetrisSpawner.enemyList[i].returning = false;
    //        }
    //        for (int j = 0; j < gridSize; ++j)
    //        {
    //            taken[j] = false;
    //        }
    //        for (int k = 0; k < 3; ++k)
    //        {
    //            if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
    //            {
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                }
    //            }
    //        }
    //        if (InGridCheck(theTetrisSpawner.enemyList[i]) && !theTetrisSpawner.enemyList[i].returning && theTetrisSpawner.enemyList[i].isMoving == false)
    //        {
    //            float nearest = 1000000;
    //            int index = 0;
    //            for (int j = 0; j < gridSize; ++j)
    //            {
    //                Vector2 distance;
    //                distance.x = theTetrisSpawner.enemyList[i].btmLeft.position.x - grid[j].transform.position.x;
    //                distance.y = theTetrisSpawner.enemyList[i].btmLeft.position.y - grid[j].transform.position.y;
    //                float hello = distance.SqrMagnitude();
    //                if (hello < nearest)
    //                {
    //                    nearest = hello;
    //                    index = j;
    //                }
    //            }
    //            if (taken[i])
    //            {
    //                theTetrisSpawner.enemyList[i].returning = true;
    //                theTetrisSpawner.enemyList[i].btmLeft.position = theTetrisSpawner.enemyList[i].origin;
    //            }
    //            else
    //            {
    //                theTetrisSpawner.enemyList[i].btmLeft.MovePosition(grid[index].transform.position);
    //            }
    //        }
    //        for (int k = 0; k < 3; ++k)
    //        {
    //            if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
    //            {
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                }
    //            }
    //        }

    //    }
    //}

    //public void GameUpdate()
    //{
    //    //if (!check)
    //    //{
    //    //    for (int i = 0; i < 3; ++i)
    //    //    {
    //    //        theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[(i * 3) + 21].transform.position);
    //    //        theTetrisSpawner.enemyList[i].isMoving = true;
    //    //        if (Mathf.Abs(theTetrisSpawner.enemyList[i].partOne.position.x - grid[(i * 3) + 21].transform.position.x) < 10
    //    //            && (Mathf.Abs(theTetrisSpawner.enemyList[i].partOne.position.y - grid[(i * 3) + 21].transform.position.y) < 10))
    //    //        {
    //    //            check = true;
    //    //            theTetrisSpawner.enemyList[i].isMoving = false;
    //    //            theTetrisSpawner.enemyList[i].returning = false;
    //    //        }
    //    //    }
    //    //    // check = true;
    //    //}

    //    if (Input.GetMouseButton(0) == true)
    //    {
    //        if (theTetrisSpawner.SomethingIsMoving == true)
    //        {
    //            uint colNum = 0, rowNum = 0;

    //            //Check col
    //            for (uint it = 0; it < col; ++it)
    //            {
    //                if (grid[0].transform.position.x + ((tileWidth * (it + 1)) - halfTileWidth) > Input.mousePosition.x)
    //                {
    //                    colNum = it;
    //                    break;
    //                }
    //            }

    //            //Check row
    //            for (uint it = 0; it < row; ++it)
    //            {
    //                if (grid[0].transform.position.y + ((tileHeight * (it + 1)) - halfTileHeight) > Input.mousePosition.y)
    //                {
    //                    rowNum = it;
    //                    break;
    //                }
    //            }

    //            objectIndex = colNum + (rowNum * col);
    //            //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
    //            if (InGridCheck(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject]) && !theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning)
    //            {
    //                switch (theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].Whatisbeingmoved)
    //                {
    //                    case "partOne":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.MovePosition(grid[objectIndex].transform.position);
    //                            }

    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "partTwo":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "partThree":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "partFour":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                                // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    default:
    //                        break;
    //                };
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
    //                            //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
    //                        }
    //                    }
    //                }
    //                isMouseMovingAnObject = true;
    //            }
    //        }

    //    }
    //    else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
    //    {
    //        //Set the tetris block to not moving
    //        //tetrisBlock.isMoving = false;
    //        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].isMoving = false;

    //    }
    //    //When the tetris block is picked up from the grid, it removes the data from that tile
    //    if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
    //    {
    //        isMouseMovingAnObject = false;
    //    }
    //    for (int i = 0; i < 3; ++i)
    //    {

    //        if (!InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false && !theTetrisSpawner.enemyList[i].returning)
    //        {
    //            Debug.Log(theTetrisSpawner.enemyList[i].partOne.position);
    //            Debug.Log(theTetrisSpawner.enemyList[i].partTwo.position);
    //            Debug.Log(theTetrisSpawner.enemyList[i].partThree.position);
    //            Debug.Log(theTetrisSpawner.enemyList[i].partFour.position);
    //            theTetrisSpawner.enemyList[i].returning = true;
    //            theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
    //        }
    //        if (theTetrisSpawner.enemyList[i].returning)
    //        {
    //            theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
    //        }
    //        if (theTetrisSpawner.enemyList[i].partOne.transform.position.x == theTetrisSpawner.enemyList[i].origin.x
    //            && theTetrisSpawner.enemyList[i].partOne.transform.position.y == theTetrisSpawner.enemyList[i].origin.y)
    //        {
    //            theTetrisSpawner.enemyList[i].returning = false;
    //        }
    //        for (int j = 0; j < gridSize; ++j)
    //        {
    //            taken[j] = false;
    //        }
    //        for (int k = 0; k < 3; ++k)
    //        {
    //            if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
    //            {
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                }
    //            }
    //        }
    //        if (InGridCheck(theTetrisSpawner.enemyList[i]) && !theTetrisSpawner.enemyList[i].returning && theTetrisSpawner.enemyList[i].isMoving == false)
    //        {
    //            float nearest = 1000000;
    //            int index = 0;
    //            for (int j = 0; j < gridSize; ++j)
    //            {
    //                Vector2 distance;
    //                distance.x = theTetrisSpawner.enemyList[i].partOne.position.x - grid[j].transform.position.x;
    //                distance.y = theTetrisSpawner.enemyList[i].partOne.position.y - grid[j].transform.position.y;
    //                float hello = distance.SqrMagnitude();
    //                if (hello < nearest)
    //                {
    //                    nearest = hello;
    //                    index = j;
    //                }
    //            }
    //            if (taken[i])
    //            {
    //                theTetrisSpawner.enemyList[i].returning = true;
    //                theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
    //            }
    //            else
    //            {
    //                theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[index].transform.position);
    //            }
    //        }
    //        for (int k = 0; k < 3; ++k)
    //        {
    //            if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
    //            {
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                }
    //            }
    //        }
    //        if (InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false)
    //        {
    //            theTetrisSpawner.enemyList[i].sav = true;
    //        }
    //        else
    //        {
    //            theTetrisSpawner.enemyList[i].sav = false;
    //        }

    //    }
    //}

    public void GameUpdate()
    {
        if (!multi)
        {
            if (!check[0] || !check[1] || !check[2])
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (!check[i])
                    {
                        theTetrisSpawner.enemyList[i].isMoving = true;
                        theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[(i * 3) + 22].transform.position);
                        theTetrisSpawner.enemyList[i].Whatisbeingmoved = "partOne";
                        if (Mathf.Abs(theTetrisSpawner.enemyList[i].partOne.position.x - grid[(i * 3) + 22].transform.position.x) < 10
                            && (Mathf.Abs(theTetrisSpawner.enemyList[i].partOne.position.y - grid[(i * 3) + 22].transform.position.y) < 10))
                        {
                            check[i] = true;
                            theTetrisSpawner.enemyList[i].isMoving = false;
                            theTetrisSpawner.enemyList[i].returning = false;
                        }
                    }
                }
                // check = true;
            }
        }
        else
        {
            if (Input.GetMouseButton(0) == true)
            {
                if (theTetrisSpawner.enemyIsMoving == true)
                {
                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].sav = false;
                    uint colNum = 0, rowNum = 0;

                    //Check col
                    for (uint it = 0; it < col; ++it)
                    {
                        if (grid[0].transform.position.x - ((tileWidth * (it + 1)) - halfTileWidth) < Input.mousePosition.x)
                        {
                            colNum = it;
                            break;
                        }
                    }

                    //Check row
                    for (uint it = 0; it < row; ++it)
                    {
                        if (grid[0].transform.position.y - ((tileHeight * (it + 1)) - halfTileHeight) < Input.mousePosition.y)
                        {
                            rowNum = it;
                            break;
                        }
                    }



                    objectIndex = colNum + (rowNum * col);
                    //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
                    bool mouse = false;

                    if (Input.mousePosition.x > grid[0].transform.position.x - halfTileWidth - 50 || Input.mousePosition.x < grid[col - 1].transform.position.x + halfTileWidth + 50
                     && Input.mousePosition.y > grid[0].transform.position.y - halfTileHeight - 50 && Input.mousePosition.y < grid[gridSize - 1].transform.position.y + halfTileHeight + 50)
                    {
                        mouse = true;
                    }
                    if (InGridCheck(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject]) && !theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning && !mouse)
                    {
                        switch (theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].Whatisbeingmoved)
                        {
                            case "partOne":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.MovePosition(Input.mousePosition);
                                        //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.MovePosition(grid[objectIndex].transform.position);
                                    }

                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            case "partTwo":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.MovePosition(Input.mousePosition);
                                        // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.MovePosition(grid[objectIndex].transform.position);
                                    }
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            case "partThree":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.MovePosition(Input.mousePosition);
                                        // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.MovePosition(grid[objectIndex].transform.position);
                                    }
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            case "partFour":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.MovePosition(Input.mousePosition);
                                        // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.MovePosition(grid[objectIndex].transform.position);
                                    }
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            default:
                                break;
                        };
                        for (int j = 0; j < gridSize; ++j)
                        {
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
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
                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].isMoving = false;
                if (!InGridCheck(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject]))
                {
                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                }

            }
            //When the tetris block is picked up from the grid, it removes the data from that tile
            if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
            {
                isMouseMovingAnObject = false;
            }
        }
        for (int i = 0; i < theTetrisSpawner.enemyList.Count; ++i)
        {

            if (!InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false && !theTetrisSpawner.enemyList[i].returning)
            {
                theTetrisSpawner.enemyList[i].returning = true;
                theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
            }
            if (theTetrisSpawner.enemyList[i].returning && theTetrisSpawner.enemyList[i].isMoving == false)
            {

                theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
            }
            if (theTetrisSpawner.enemyList[i].partOne.transform.position.x == theTetrisSpawner.enemyList[i].origin.x
                && theTetrisSpawner.enemyList[i].partOne.transform.position.y == theTetrisSpawner.enemyList[i].origin.y)
            {
                theTetrisSpawner.enemyList[i].returning = false;
            }
            for (int j = 0; j < gridSize; ++j)
            {
                taken[j] = false;
            }
            if (InGridCheck(theTetrisSpawner.enemyList[i]) && !theTetrisSpawner.enemyList[i].returning && theTetrisSpawner.enemyList[i].isMoving == false)
            {
                switch (theTetrisSpawner.enemyList[i].Whatisbeingmoved)
                {
                    case "partOne":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partOne.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partOne.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    case "partTwo":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partTwo.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partTwo.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                Debug.Log("choochoo");
                                theTetrisSpawner.enemyList[i].partTwo.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    case "partThree":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partThree.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partThree.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                theTetrisSpawner.enemyList[i].partThree.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    case "partFour":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partFour.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partFour.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                theTetrisSpawner.enemyList[i].partFour.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    default:
                        break;
                };
                //float nearest = 1000000;
                //int index = 0;
                //for (int j = 0; j < gridSize; ++j)
                //{
                //    Vector2 distance;
                //    distance.x = theTetrisSpawner.enemyList[i].partOne.position.x - grid[j].transform.position.x;
                //    distance.y = theTetrisSpawner.enemyList[i].partOne.position.y - grid[j].transform.position.y;
                //    float hello = distance.SqrMagnitude();
                //    if (hello < nearest)
                //    {
                //        nearest = hello;
                //        index = j;
                //    }
                //}
                //if (taken[index])
                //{
                //    theTetrisSpawner.enemyList[i].returning = true;
                //    theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
                //}
                //else
                //{
                //    Debug.Log("choochoo");
                //    theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[index].transform.position);
                //}
            }
            for (int k = 0; k < theTetrisSpawner.enemyList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            for (int k = 0; k < theTetrisSpawner.enemyList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            if (InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false)
            {
                theTetrisSpawner.enemyList[i].sav = true;
            }
            else
            {
                theTetrisSpawner.enemyList[i].sav = false;
            }
        }
    }

    public bool InGridCheck(TetrisCube cube)
    {
        //Debug.Log(grid[0].transform.position);
        //Debug.Log(grid[col - 1].transform.position.x + halfTileWidth);
        //Debug.Log(grid[gridSize - 1].transform.position.y + halfTileHeight);
        //Debug.Log(cube.btmLeft.position);
        //Debug.Log(cube.btmRight.position);
        //Debug.Log(cube.topLeft.position);
        //Debug.Log(cube.topRight.position);
        //if (cube.btmLeft.position.x > grid[0].transform.position.x - halfTileWidth && cube.btmLeft.position.x < grid[col - 1].transform.position.x + halfTileWidth
        //    && cube.btmLeft.position.y > grid[0].transform.position.y - halfTileHeight && cube.btmLeft.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
        //    && cube.btmRight.position.x > grid[0].transform.position.x - halfTileWidth && cube.btmRight.position.x < grid[col - 1].transform.position.x + halfTileWidth
        //    && cube.btmRight.position.y > grid[0].transform.position.y - halfTileHeight && cube.btmRight.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
        //    && cube.topLeft.position.x > grid[0].transform.position.x - halfTileWidth && cube.topLeft.position.x < grid[col - 1].transform.position.x + halfTileWidth
        //    && cube.topLeft.position.y > grid[0].transform.position.y - halfTileHeight && cube.topLeft.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
        //    && cube.topRight.position.x > grid[0].transform.position.x - halfTileWidth && cube.topRight.position.x < grid[col - 1].transform.position.x + halfTileWidth
        //    && cube.topRight.position.y > grid[0].transform.position.y - halfTileHeight && cube.topRight.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight)
        if (cube.partOne.position.x < grid[0].transform.position.x + halfTileWidth && cube.partOne.position.x > grid[col - 1].transform.position.x - halfTileWidth
    && cube.partOne.position.y < grid[0].transform.position.y + halfTileHeight && cube.partOne.position.y > grid[gridSize - 1].transform.position.y - halfTileHeight
    && cube.partTwo.position.x < grid[0].transform.position.x + halfTileWidth && cube.partTwo.position.x > grid[col - 1].transform.position.x - halfTileWidth
    && cube.partTwo.position.y < grid[0].transform.position.y + halfTileHeight && cube.partTwo.position.y > grid[gridSize - 1].transform.position.y - halfTileHeight
    && cube.partThree.position.x < grid[0].transform.position.x + halfTileWidth && cube.partThree.position.x > grid[col - 1].transform.position.x - halfTileWidth
    && cube.partThree.position.y < grid[0].transform.position.y + halfTileHeight && cube.partThree.position.y > grid[gridSize - 1].transform.position.y - halfTileHeight
    && cube.partFour.position.x < grid[0].transform.position.x + halfTileWidth && cube.partFour.position.x > grid[col - 1].transform.position.x - halfTileWidth
    && cube.partFour.position.y < grid[0].transform.position.y + halfTileHeight && cube.partFour.position.y > grid[gridSize - 1].transform.position.y - halfTileHeight)
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
        //Check Column
        for (uint x = 0; x < col; ++x)
        {
            for (uint numRow = 1; numRow < row; ++numRow)
            {
                if (IsGreyedOut(x + numRow * 10) && IsGreyedOut((x - 1) + numRow * 10))
                {
                    UnSetIsGreyOut(x + numRow * 10);
                    SetIsGreyOut((x - 1) + numRow * 10);
                }
            }
        }
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
                EnemyHealth.addHealth(-2);
                theGameCode.Player1.GetComponent<InGameCash>().addAmount(40);
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
                EnemyHealth.addHealth(-5);
                theGameCode.Player1.GetComponent<InGameCash>().addAmount(100);
                for (uint numCol = 0; numCol < col; ++numCol)
                {
                    UnSetIsGreyOut(numCol + y * 10);
                }
            }
        }
    }

    public void GameUpdateAndroid()
    {
        if (!multi)
        {
            if (!check[0] || !check[1] || !check[2])
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (!check[i])
                    {
                        theTetrisSpawner.enemyList[i].isMoving = true;
                        theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[(i * 3) + 22].transform.position);
                        theTetrisSpawner.enemyList[i].Whatisbeingmoved = "partOne";
                        if (Mathf.Abs(theTetrisSpawner.enemyList[i].partOne.position.x - grid[(i * 3) + 22].transform.position.x) < 10
                            && (Mathf.Abs(theTetrisSpawner.enemyList[i].partOne.position.y - grid[(i * 3) + 22].transform.position.y) < 10))
                        {
                            check[i] = true;
                            theTetrisSpawner.enemyList[i].isMoving = false;
                            theTetrisSpawner.enemyList[i].returning = false;
                        }
                    }
                }
                // check = true;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (theTetrisSpawner.enemyIsMoving == true)
                {
                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].sav = false;
                    uint colNum = 0, rowNum = 0;

                    //Check col
                    for (uint it = 0; it < col; ++it)
                    {
                        if (grid[0].transform.position.x - ((tileWidth * (it + 1)) - halfTileWidth) < Input.GetTouch(0).position.x)
                        {
                            colNum = it;
                            break;
                        }
                    }

                    //Check row
                    for (uint it = 0; it < row; ++it)
                    {
                        if (grid[0].transform.position.y - ((tileHeight * (it + 1)) - halfTileHeight) < Input.GetTouch(0).position.y)
                        {
                            rowNum = it;
                            break;
                        }
                    }



                    objectIndex = colNum + (rowNum * col);
                    //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
                    bool mouse = false;

                    if (Input.GetTouch(0).position.x > grid[0].transform.position.x - halfTileWidth - 50 || Input.GetTouch(0).position.x < grid[col - 1].transform.position.x + halfTileWidth + 50
                     && Input.GetTouch(0).position.y > grid[0].transform.position.y - halfTileHeight - 50 && Input.GetTouch(0).position.y < grid[gridSize - 1].transform.position.y + halfTileHeight + 50)
                    {
                        mouse = true;
                    }
                    if (InGridCheck(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject]) && !theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning && !mouse)
                    {
                        switch (theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].Whatisbeingmoved)
                        {
                            case "partOne":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.MovePosition(Input.GetTouch(0).position);
                                        //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.MovePosition(grid[objectIndex].transform.position);
                                    }

                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            case "partTwo":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.MovePosition(Input.GetTouch(0).position);
                                        // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.MovePosition(grid[objectIndex].transform.position);
                                    }
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmRight.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            case "partThree":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.MovePosition(Input.GetTouch(0).position);
                                        // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.MovePosition(grid[objectIndex].transform.position);
                                    }
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topLeft.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            case "partFour":
                                {
                                    if (taken[objectIndex])
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.MovePosition(Input.GetTouch(0).position);
                                        // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                    }
                                    else
                                    {
                                        theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.MovePosition(grid[objectIndex].transform.position);
                                    }
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].topRight.position = grid[objectIndex].transform.position;
                                    break;
                                }
                            default:
                                break;
                        };
                        for (int j = 0; j < gridSize; ++j)
                        {
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    //theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    // theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                            if (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.transform.position.x - grid[j].transform.position.x) < 50
                                && (Mathf.Abs(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                            {
                                if (taken[j])
                                {
                                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                                    //  theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].btmLeft.position = theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].origin;
                                }
                            }
                        }
                    }
                    isMouseMovingAnObject = true;
                }

            }
            else if (isMouseMovingAnObject && Input.GetTouch(0).phase == TouchPhase.Ended) //When the touch is released, it saves the tetris data into the data grid tile
            {
                //Set the tetris block to not moving
                //tetrisBlock.isMoving = false;
                theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].isMoving = false;
                if (!InGridCheck(theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject]))
                {
                    theTetrisSpawner.enemyList[theTetrisSpawner.IndexofEnemyObject].returning = true;
                }

            }
            //When the tetris block is picked up from the grid, it removes the data from that tile
            if (isMouseMovingAnObject && Input.touchCount > 0)
            {
                isMouseMovingAnObject = false;
            }
        }
        for (int i = 0; i < theTetrisSpawner.enemyList.Count; ++i)
        {

            if (!InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false && !theTetrisSpawner.enemyList[i].returning)
            {
                theTetrisSpawner.enemyList[i].returning = true;
                theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
            }
            if (theTetrisSpawner.enemyList[i].returning && theTetrisSpawner.enemyList[i].isMoving == false)
            {

                theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
            }
            if (theTetrisSpawner.enemyList[i].partOne.transform.position.x == theTetrisSpawner.enemyList[i].origin.x
                && theTetrisSpawner.enemyList[i].partOne.transform.position.y == theTetrisSpawner.enemyList[i].origin.y)
            {
                theTetrisSpawner.enemyList[i].returning = false;
            }
            for (int j = 0; j < gridSize; ++j)
            {
                taken[j] = false;
            }
            if (InGridCheck(theTetrisSpawner.enemyList[i]) && !theTetrisSpawner.enemyList[i].returning && theTetrisSpawner.enemyList[i].isMoving == false)
            {
                switch (theTetrisSpawner.enemyList[i].Whatisbeingmoved)
                {
                    case "partOne":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partOne.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partOne.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    case "partTwo":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partTwo.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partTwo.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                Debug.Log("choochoo");
                                theTetrisSpawner.enemyList[i].partTwo.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    case "partThree":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partThree.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partThree.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                theTetrisSpawner.enemyList[i].partThree.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    case "partFour":
                        {
                            float nearest = 1000000;
                            int index = 0;
                            for (int j = 0; j < gridSize; ++j)
                            {
                                Vector2 distance;
                                distance.x = theTetrisSpawner.enemyList[i].partFour.position.x - grid[j].transform.position.x;
                                distance.y = theTetrisSpawner.enemyList[i].partFour.position.y - grid[j].transform.position.y;
                                float hello = distance.SqrMagnitude();
                                if (hello < nearest)
                                {
                                    nearest = hello;
                                    index = j;
                                }
                            }
                            if (taken[index])
                            {
                                theTetrisSpawner.enemyList[i].returning = true;
                            }
                            else
                            {
                                theTetrisSpawner.enemyList[i].partFour.MovePosition(grid[index].transform.position);
                            }
                            break;
                        }
                    default:
                        break;
                };
                //float nearest = 1000000;
                //int index = 0;
                //for (int j = 0; j < gridSize; ++j)
                //{
                //    Vector2 distance;
                //    distance.x = theTetrisSpawner.enemyList[i].partOne.position.x - grid[j].transform.position.x;
                //    distance.y = theTetrisSpawner.enemyList[i].partOne.position.y - grid[j].transform.position.y;
                //    float hello = distance.SqrMagnitude();
                //    if (hello < nearest)
                //    {
                //        nearest = hello;
                //        index = j;
                //    }
                //}
                //if (taken[index])
                //{
                //    theTetrisSpawner.enemyList[i].returning = true;
                //    theTetrisSpawner.enemyList[i].partOne.position = theTetrisSpawner.enemyList[i].origin;
                //}
                //else
                //{
                //    Debug.Log("choochoo");
                //    theTetrisSpawner.enemyList[i].partOne.MovePosition(grid[index].transform.position);
                //}
            }
            for (int k = 0; k < theTetrisSpawner.enemyList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            for (int k = 0; k < theTetrisSpawner.enemyList.Count; ++k)
            {
                if (InGridCheck(theTetrisSpawner.enemyList[k]) && theTetrisSpawner.enemyList[k].isMoving == false && !theTetrisSpawner.enemyList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.enemyList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            if (InGridCheck(theTetrisSpawner.enemyList[i]) && theTetrisSpawner.enemyList[i].isMoving == false)
            {
                theTetrisSpawner.enemyList[i].sav = true;
            }
            else
            {
                theTetrisSpawner.enemyList[i].sav = false;
            }
        }
    }
}

