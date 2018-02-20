using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyGridSystem : MonoBehaviour
{

    //Note: Increasing the num of rows & col means that you also need to add more images into the array in the inspector
    const ushort row = 4, col = 10;
    public const uint gridSize = row * col;

    const float tileWidth = 100;
    const float tileHeight = 100;

    private enemyGridData theGridData = null;
    private enemyTetrisSpawner theTetrisSpawner = null;
    bool check;
    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;
    bool[] taken = new bool[gridSize];

    [SerializeField]
    Image[] grid = new Image[gridSize];

    [SerializeField]
    Canvas thisCanvas;

    // Use this for initialization
    public void Start()
    {
        check = false;
        theGridData = new enemyGridData();
        theGridData.Init();
        for (int i = 0; i < gridSize; ++i)
        {
            taken[i] = false;
        }
        theTetrisSpawner = GameObject.Find("enemySpawner").GetComponent<enemyTetrisSpawner>();

        Debug.Assert(theGridData != null);
        Debug.Assert(theTetrisSpawner != null);

        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        Vector2 Grid0Pos = new Vector2(objectRectTransform.transform.position.x - (0.5f * (col - 1) * tileWidth), objectRectTransform.transform.position.y - ((row * tileHeight)) - (2 * (tileHeight)) + 1024);
        grid[0].transform.position = Grid0Pos;

        for (uint i = 0; i < gridSize; ++i)
        {
            //Adjusts the individual grid block's size
            grid[i].rectTransform.sizeDelta = new Vector2(tileWidth, tileHeight);

            //Anchor to bottom
            grid[i].rectTransform.anchorMin = new Vector2(0.5f, 0);
            grid[i].rectTransform.anchorMax = new Vector2(0.5f, 0);
            grid[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);

            if (i == 0)
            {
                continue;
            }

            //Adjusts the individual grid block's position
            if (i < col)
            {
                grid[i].transform.position = new Vector2(grid[0].transform.position.x + (i * tileWidth), grid[0].transform.position.y);
                //Debug.Log(grid[i].transform.position);
            }
            else
            {
                grid[i].transform.position = new Vector2(grid[i - col].transform.position.x, grid[i - col].transform.position.y + tileHeight);
                //Debug.Log(grid[i].transform.position);
            }
        }

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
    //            theTetrisSpawner.tetrisList[i].btmLeft.MovePosition(grid[(i * 3) + 11].transform.position);
    //            theTetrisSpawner.tetrisList[i].isMoving = true;
    //            if (Mathf.Abs(theTetrisSpawner.tetrisList[i].btmLeft.position.x - grid[(i * 3) + 11].transform.position.x) < 10
    //                && (Mathf.Abs(theTetrisSpawner.tetrisList[i].btmLeft.position.y - grid[(i * 3) + 11].transform.position.y) < 10))
    //            {
    //                check = true;
    //                theTetrisSpawner.tetrisList[i].isMoving = false;
    //                theTetrisSpawner.tetrisList[i].returning = false;
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
    //            if (InGridCheck(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject]) && !theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning)
    //            {
    //                switch (theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].Whatisbeingmoved)
    //                {
    //                    case "btmLeft":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                                //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.MovePosition(grid[objectIndex].transform.position);
    //                            }

    //                            //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "btmRight":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                                // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "topLeft":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                                // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    case "topRight":
    //                        {
    //                            if (taken[objectIndex])
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                                // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                            }
    //                            else
    //                            {
    //                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.MovePosition(grid[objectIndex].transform.position);
    //                            }
    //                            //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.position = grid[objectIndex].transform.position;
    //                            break;
    //                        }
    //                    default:
    //                        break;
    //                };
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                            // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                            //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                            // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
    //                        }
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        if (taken[j])
    //                        {
    //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
    //                            //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
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
    //        theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].isMoving = false;
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

    //        if (!InGridCheck(theTetrisSpawner.tetrisList[i]) && theTetrisSpawner.tetrisList[i].isMoving == false && !theTetrisSpawner.tetrisList[i].returning)
    //        {
    //            Debug.Log(theTetrisSpawner.tetrisList[i].btmLeft.position);
    //            Debug.Log(theTetrisSpawner.tetrisList[i].btmRight.position);
    //            Debug.Log(theTetrisSpawner.tetrisList[i].topLeft.position);
    //            Debug.Log(theTetrisSpawner.tetrisList[i].topRight.position);
    //            theTetrisSpawner.tetrisList[i].returning = true;
    //            theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
    //        }
    //        if (theTetrisSpawner.tetrisList[i].returning)
    //        {
    //            theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
    //        }
    //        if (theTetrisSpawner.tetrisList[i].btmLeft.transform.position.x == theTetrisSpawner.tetrisList[i].origin.x
    //            && theTetrisSpawner.tetrisList[i].btmLeft.transform.position.y == theTetrisSpawner.tetrisList[i].origin.y)
    //        {
    //            theTetrisSpawner.tetrisList[i].returning = false;
    //        }
    //        for (int j = 0; j < gridSize; ++j)
    //        {
    //            taken[j] = false;
    //        }
    //        for (int k = 0; k < 3; ++k)
    //        {
    //            if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
    //            {
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                }
    //            }
    //        }
    //        if (InGridCheck(theTetrisSpawner.tetrisList[i]) && !theTetrisSpawner.tetrisList[i].returning && theTetrisSpawner.tetrisList[i].isMoving == false)
    //        {
    //            float nearest = 1000000;
    //            int index = 0;
    //            for (int j = 0; j < gridSize; ++j)
    //            {
    //                Vector2 distance;
    //                distance.x = theTetrisSpawner.tetrisList[i].btmLeft.position.x - grid[j].transform.position.x;
    //                distance.y = theTetrisSpawner.tetrisList[i].btmLeft.position.y - grid[j].transform.position.y;
    //                float hello = distance.SqrMagnitude();
    //                if (hello < nearest)
    //                {
    //                    nearest = hello;
    //                    index = j;
    //                }
    //            }
    //            if (taken[i])
    //            {
    //                theTetrisSpawner.tetrisList[i].returning = true;
    //                theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
    //            }
    //            else
    //            {
    //                theTetrisSpawner.tetrisList[i].btmLeft.MovePosition(grid[index].transform.position);
    //            }
    //        }
    //        for (int k = 0; k < 3; ++k)
    //        {
    //            if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
    //            {
    //                for (int j = 0; j < gridSize; ++j)
    //                {
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                    if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
    //                        && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
    //                    {
    //                        taken[j] = true;
    //                    }
    //                }
    //            }
    //        }

    //    }
    //}

    public void GameUpdate()
    {
        if (!check)
        {
            for (int i = 0; i < 3; ++i)
            {
                theTetrisSpawner.tetrisList[i].partOne.MovePosition(grid[(i * 3) + 21].transform.position);
                theTetrisSpawner.tetrisList[i].isMoving = true;
                if (Mathf.Abs(theTetrisSpawner.tetrisList[i].partOne.position.x - grid[(i * 3) + 21].transform.position.x) < 10
                    && (Mathf.Abs(theTetrisSpawner.tetrisList[i].partOne.position.y - grid[(i * 3) + 21].transform.position.y) < 10))
                {
                    check = true;
                    theTetrisSpawner.tetrisList[i].isMoving = false;
                    theTetrisSpawner.tetrisList[i].returning = false;
                }
            }
            // check = true;
        }

        if (Input.GetMouseButton(0) == true)
        {
            if (theTetrisSpawner.SomethingIsMoving == true)
            {
                uint colNum = 0, rowNum = 0;

                //Check col
                for (uint it = 0; it < col; ++it)
                {
                    if (grid[0].transform.position.x + ((tileWidth * (it + 1)) - halfTileWidth) > Input.mousePosition.x)
                    {
                        colNum = it;
                        break;
                    }
                }

                //Check row
                for (uint it = 0; it < row; ++it)
                {
                    if (grid[0].transform.position.y + ((tileHeight * (it + 1)) - halfTileHeight) > Input.mousePosition.y)
                    {
                        rowNum = it;
                        break;
                    }
                }

                objectIndex = colNum + (rowNum * col);
                //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
                if (InGridCheck(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject]) && !theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning)
                {
                    switch (theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].Whatisbeingmoved)
                    {
                        case "partOne":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                    //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partOne.MovePosition(grid[objectIndex].transform.position);
                                }

                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partTwo":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                    // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partTwo.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partThree":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                    // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partThree.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "partFour":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                    // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partFour.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        default:
                            break;
                    };
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            if (taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                    }
                    isMouseMovingAnObject = true;
                }
            }

        }
        else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
        {
            //Set the tetris block to not moving
            //tetrisBlock.isMoving = false;
            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].isMoving = false;
            //Pass the tetris block data into the grid data
            if (theGridData.AddTetrisBlockData(objectIndex, "Test01", "UnitTest", 100, 20, 35, 45, grid[objectIndex].transform.position))
            {
                Debug.Log("Successful Save");
            }
            else
            {
                Debug.Log("Unsuccessful Save");
            }
        }
        //When the tetris block is picked up from the grid, it removes the data from that tile
        if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
        {
            //Remove the data from the tile
            if (theGridData.RemoveTetrisBlockData(objectIndex))
            {
                Debug.Log("Successful Removal");
            }
            else
            {
                Debug.Log("Unsuccessful Removal");
            }

            isMouseMovingAnObject = false;
        }
        for (int i = 0; i < 3; ++i)
        {

            if (!InGridCheck(theTetrisSpawner.tetrisList[i]) && theTetrisSpawner.tetrisList[i].isMoving == false && !theTetrisSpawner.tetrisList[i].returning)
            {
                Debug.Log(theTetrisSpawner.tetrisList[i].partOne.position);
                Debug.Log(theTetrisSpawner.tetrisList[i].partTwo.position);
                Debug.Log(theTetrisSpawner.tetrisList[i].partThree.position);
                Debug.Log(theTetrisSpawner.tetrisList[i].partFour.position);
                theTetrisSpawner.tetrisList[i].returning = true;
                theTetrisSpawner.tetrisList[i].partOne.position = theTetrisSpawner.tetrisList[i].origin;
            }
            if (theTetrisSpawner.tetrisList[i].returning)
            {
                theTetrisSpawner.tetrisList[i].partOne.position = theTetrisSpawner.tetrisList[i].origin;
            }
            if (theTetrisSpawner.tetrisList[i].partOne.transform.position.x == theTetrisSpawner.tetrisList[i].origin.x
                && theTetrisSpawner.tetrisList[i].partOne.transform.position.y == theTetrisSpawner.tetrisList[i].origin.y)
            {
                theTetrisSpawner.tetrisList[i].returning = false;
            }
            for (int j = 0; j < gridSize; ++j)
            {
                taken[j] = false;
            }
            for (int k = 0; k < 3; ++k)
            {
                if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            if (InGridCheck(theTetrisSpawner.tetrisList[i]) && !theTetrisSpawner.tetrisList[i].returning && theTetrisSpawner.tetrisList[i].isMoving == false)
            {
                float nearest = 1000000;
                int index = 0;
                for (int j = 0; j < gridSize; ++j)
                {
                    Vector2 distance;
                    distance.x = theTetrisSpawner.tetrisList[i].partOne.position.x - grid[j].transform.position.x;
                    distance.y = theTetrisSpawner.tetrisList[i].partOne.position.y - grid[j].transform.position.y;
                    float hello = distance.SqrMagnitude();
                    if (hello < nearest)
                    {
                        nearest = hello;
                        index = j;
                    }
                }
                if (taken[i])
                {
                    theTetrisSpawner.tetrisList[i].returning = true;
                    theTetrisSpawner.tetrisList[i].partOne.position = theTetrisSpawner.tetrisList[i].origin;
                }
                else
                {
                    theTetrisSpawner.tetrisList[i].partOne.MovePosition(grid[index].transform.position);
                }
            }
            for (int k = 0; k < 3; ++k)
            {
                if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partOne.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partOne.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partTwo.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partTwo.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partThree.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partThree.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].partFour.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].partFour.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }
            if (InGridCheck(theTetrisSpawner.tetrisList[i]) && theTetrisSpawner.tetrisList[i].isMoving == false)
            {
                theTetrisSpawner.tetrisList[i].sav = true;
            }
            else
            {
                theTetrisSpawner.tetrisList[i].sav = false;
            }

        }
    }

    bool InGridCheck(TetrisCube cube)
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
        if (cube.partOne.position.x > grid[0].transform.position.x - halfTileWidth && cube.partOne.position.x < grid[col - 1].transform.position.x + halfTileWidth
    && cube.partOne.position.y > grid[0].transform.position.y - halfTileHeight && cube.partOne.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
    && cube.partTwo.position.x > grid[0].transform.position.x - halfTileWidth && cube.partTwo.position.x < grid[col - 1].transform.position.x + halfTileWidth
    && cube.partTwo.position.y > grid[0].transform.position.y - halfTileHeight && cube.partTwo.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
    && cube.partThree.position.x > grid[0].transform.position.x - halfTileWidth && cube.partThree.position.x < grid[col - 1].transform.position.x + halfTileWidth
    && cube.partThree.position.y > grid[0].transform.position.y - halfTileHeight && cube.partThree.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
    && cube.partFour.position.x > grid[0].transform.position.x - halfTileWidth && cube.partFour.position.x < grid[col - 1].transform.position.x + halfTileWidth
    && cube.partFour.position.y > grid[0].transform.position.y - halfTileHeight && cube.partFour.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight)
        {
            return true;
        }
        return false;

    }
}

//The grid where data is stored
public class enemyGridData
{
    public enemyTetrisData[] gridData;
    uint gridDataSize;

    public void Init()
    {
        gridDataSize = GridSystem.gridSize;
        gridData = new enemyTetrisData[gridDataSize];
    }

    public bool AddTetrisBlockData(uint Index, string NameID, string UnitType, uint Health, uint moveSpeed, uint attackDamage, uint attackRate, Vector2 Position)
    {
        //Check if there is already a unit in that tile
        if (gridData[Index] != null)
        {
            return false;
        }

        gridData[Index] = new enemyTetrisData();
        gridData[Index].CreateUnit(NameID, UnitType, Health, moveSpeed, attackDamage, attackRate, Position);

        return true;
    }

    public bool RemoveTetrisBlockData(uint Index)
    {
        //Check if there is no unit inside that tile
        if (gridData[Index] == null)
        {
            return false;
        }

        gridData[Index] = null;

        return true;
    }
}

//Terrain basic unit modifiers: Hills favourable to Bowmen, Forest favourable to Combat Infantry, Rivers favourable to Cavalry
//Plains favour all units

//Storage class for Tetris Block data
public class enemyTetrisData
{
    string NameID, UnitType;
    float Health, moveSpeed, attackDamage, attackRate;
    float OriginalHealth, OriginalMoveSpeed, OriginalAttackDamage, OriginalAttackRate;
    Vector2 Position = new Vector2();

    public enemyTetrisData()
    {
        NameID = UnitType = "";
        Health = moveSpeed = attackDamage = attackRate = 0;
        OriginalHealth = OriginalMoveSpeed = OriginalAttackDamage = OriginalAttackRate = 0;
    }

    public void CreateUnit(string NameID, string UnitType, float Health, float moveSpeed, float attackDamage, float attackRate, Vector2 position)
    {
        this.NameID = NameID;
        this.UnitType = UnitType;
        this.Health = Health;
        this.moveSpeed = moveSpeed;
        this.attackDamage = attackDamage;
        this.attackRate = attackRate;

        OriginalHealth = Health;
        OriginalMoveSpeed = moveSpeed;
        OriginalAttackDamage = attackDamage;
        OriginalAttackRate = attackRate;

        this.Position = position;
    }

    public bool neutralZoneStatsChanged = false;
    public void TerrainStatsModify(string NeutralZoneTerrainType)
    {
        //Slow down units based on terrain modifiers
        switch (UnitType)
        {
            case "Infantry":
                {
                    if (NeutralZoneTerrainType == "Hills")
                    {
                        //Subtract 10% attack damage
                        attackDamage -= (attackDamage * 0.1f);

                        //Subtract 15% movement
                        moveSpeed -= (moveSpeed * 0.15f);

                        //Subtrack 10% attack rate
                        attackRate -= (attackRate * 0.1f);
                    }
                    else if (NeutralZoneTerrainType == "Forest")
                    {
                        //Add 15% attack damage
                        attackDamage += (attackDamage * 0.1f);

                        //Subtract 10% movement
                        moveSpeed -= (moveSpeed * 0.1f);

                        //Subtrack 5% attack rate
                        attackRate -= (attackRate * 0.05f);
                    }
                    else if (NeutralZoneTerrainType == "River")
                    {
                        //Subtract 10% attack damage
                        attackDamage -= (attackDamage * 0.1f);

                        //Subtract 15% movement
                        moveSpeed -= (moveSpeed * 0.15f);

                        //Subtract 15% attack rate
                        attackRate -= (attackRate * 0.1f);
                    }
                    else if (NeutralZoneTerrainType == "Plains")
                    {
                        //Add 10% attack damage
                        attackDamage += (attackDamage * 0.1f);

                        //Add 20% movement
                        moveSpeed += (moveSpeed * 0.2f);

                        //Add 10% attack rate
                        attackRate += (attackRate * 0.1f);
                    }
                    break;
                }
            case "Cavalry":
                {
                    if (NeutralZoneTerrainType == "Hills")
                    {
                        //Subtract 15% attack damage
                        attackDamage -= (attackDamage * 0.15f);

                        //Subtract 25% movement
                        moveSpeed -= (moveSpeed * 0.25f);

                        //Subtrack 15% attack rate
                        attackRate -= (attackRate * 0.15f);
                    }
                    else if (NeutralZoneTerrainType == "Forest")
                    {
                        //Subtract 10% attack damage
                        attackDamage -= (attackDamage * 0.1f);

                        //Subtract 15% movement
                        moveSpeed -= (moveSpeed * 0.15f);

                        //Subtrack 5% attack rate
                        attackRate -= (attackRate * 0.05f);
                    }
                    else if (NeutralZoneTerrainType == "River")
                    {
                        //Add 10% attack damage
                        attackDamage += (attackDamage * 0.1f);

                        //Subtract 10% movement
                        moveSpeed -= (moveSpeed * 0.1f);

                        //Subtrack 10% attack rate
                        attackRate -= (attackRate * 0.1f);
                    }
                    else if (NeutralZoneTerrainType == "Plains")
                    {
                        //Add 10% attack damage
                        attackDamage += (attackDamage * 0.1f);

                        //Add 15% movement
                        moveSpeed += (moveSpeed * 0.15f);

                        //Add 15% attack rate
                        attackRate += (attackRate * 0.1f);
                    }
                    break;
                }
            case "Bowmen":
                {
                    if (NeutralZoneTerrainType == "Hills")
                    {
                        //Add 10% attack damage
                        attackDamage += (attackDamage * 0.1f);

                        //Add 5% attack rate
                        attackRate += (attackRate * 0.05f);

                        //Subtract 10% movement speed
                        moveSpeed -= (moveSpeed * 0.1f);
                    }
                    else if (NeutralZoneTerrainType == "Forest")
                    {
                        //Sutract 15% attack damage
                        attackDamage -= (attackDamage * 0.15f);

                        //Subtract 10% movement
                        moveSpeed -= (moveSpeed * 0.1f);

                        //Subtrack 15% attack rate
                        attackRate -= (attackRate * 0.15f);
                    }
                    else if (NeutralZoneTerrainType == "River")
                    {
                        //Subtract 10% attack damage
                        attackDamage -= (attackDamage * 0.1f);

                        //Subtract 10% movement
                        moveSpeed -= (moveSpeed * 0.1f);

                        //Subtrack 5% attack rate
                        attackRate -= (attackRate * 0.1f);
                    }
                    else if (NeutralZoneTerrainType == "Plains")
                    {
                        //Add 10% attack damage
                        attackDamage += (attackDamage * 0.1f);

                        //Add 10% movement
                        moveSpeed += (moveSpeed * 0.1f);

                        //Add 20% attack rate
                        attackRate += (attackRate * 0.2f);
                    }
                    break;
                }
            default:
                break;
        }

        neutralZoneStatsChanged = true;
    }

    public void ResetStats()
    {
        neutralZoneStatsChanged = false;

        moveSpeed = OriginalMoveSpeed;
        attackDamage = OriginalAttackDamage;
        attackRate = OriginalAttackRate;
    }

    //Getters
    public float GetHealth()
    {
        return Health;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetAttackDamage()
    {
        return attackDamage;
    }
    public float GetAttackRate()
    {
        return attackRate;
    }
    public string GetNameID()
    {
        return NameID;
    }
    public string GetUnitType()
    {
        return UnitType;
    }
    public Vector2 GetPosition()
    {
        return Position;
    }

    //Setters
    public void SetHealth(float newHealthValue)
    {
        Health = newHealthValue;
    }
    public void SetMoveSpeed(float newMoveSpeedValue)
    {
        moveSpeed = newMoveSpeedValue;
    }
    public void SetAttackDamage(float newAttackDamageValue)
    {
        attackDamage = newAttackDamageValue;
    }
    public void SetAttackRate(float newAttackRateValue)
    {
        attackRate = newAttackRateValue;
    }
    public void SetNameID(string newNameIDValue)
    {
        NameID = newNameIDValue;
    }
    public void SetUnitType(string newUnitTypeValue)
    {
        UnitType = newUnitTypeValue;
    }
    public void SetPosition(Vector2 newPosition)
    {
        Position = newPosition;
    }

    //Subtractors & Adders
    public void SubractHealth(float HealthToSubtract)
    {
        Health -= HealthToSubtract;
    }
    public void AddHealth(float HealthToAdd)
    {
        Health += HealthToAdd;
    }
    public void SubractMoveSpeed(float MoveSpeedToSubtract)
    {
        moveSpeed -= MoveSpeedToSubtract;
    }
    public void AddMoveSpeed(float MoveSpeedToAdd)
    {
        moveSpeed += MoveSpeedToAdd;
    }
    public void SubractAttackRate(float AttackRateToSubtract)
    {
        attackRate -= AttackRateToSubtract;
    }
    public void AddAttackRate(float AttackRateToAdd)
    {
        attackRate += AttackRateToAdd;
    }
    public void SubractAttackDamage(float AttackDamageToSubtract)
    {
        attackDamage -= AttackDamageToSubtract;
    }
    public void AddAttackDamage(float AttackDamageToAdd)
    {
        attackDamage += AttackDamageToAdd;
    }
    public void AddPosition(float PositionXAdd = 0, float PositionYAdd = 0)
    {
        Position.x += PositionXAdd;
        Position.y += PositionYAdd;
    }
    public void SubtractPosition(float PositionXSubtract = 0, float PositionYSubtract = 0)
    {
        Position.x -= PositionXSubtract;
        Position.y -= PositionYSubtract;
    }
}

