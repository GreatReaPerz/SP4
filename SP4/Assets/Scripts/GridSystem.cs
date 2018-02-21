using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GridSystem : MonoBehaviour {

    //Note: Increasing the num of rows & col means that you also need to add more images into the array in the inspector
    const ushort row = 4, col = 10;
    public const uint gridSize = row * col;
    public uint GridSize = gridSize;
    const float tileWidth = 100;
    const float tileHeight = 100;
    public bool[] taken = new bool[gridSize];
    private TetrisSpawner theTetrisSpawner = null;
    private HealthSystem PlayerHealth;

    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;

    [SerializeField]
    public Image[] grid = new Image[gridSize];
    
    [SerializeField]
    Canvas thisCanvas;

    [SerializeField]
    Sprite GreyGridSprite;

    [SerializeField]
    Sprite BlueGridSprite;

    // Use this for initialization
    public void Start () {
                for(int i = 0; i < gridSize; ++i)
        {
            taken[i] = false;
        }
        theTetrisSpawner = GameObject.Find("Spawner").GetComponent<TetrisSpawner>();
        PlayerHealth = GameObject.Find("Player").GetComponent<HealthSystem>();
        
        Debug.Assert(theTetrisSpawner != null);

        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        Vector2 Grid0Pos = new Vector2(objectRectTransform.transform.position.x - (0.5f * (col - 1) * tileWidth) , objectRectTransform.transform.position.y  - ((row * tileHeight)) - (2 * (tileHeight)));
        grid[0].transform.position = Grid0Pos;

        for (uint i = 0; i < gridSize; ++i)
        {
            //Adjusts the individual grid block's size
            grid[i].rectTransform.sizeDelta = new Vector2(tileWidth, tileHeight);
            
            //Anchor to bottom
            grid[i].rectTransform.anchorMin = new Vector2(0.5f, 0);
            grid[i].rectTransform.anchorMax = new Vector2(0.5f, 0);
            grid[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);

            if(i == 0)
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


        //if (Input.GetMouseButton(0) == true)
        //{
        //    if (theTetrisSpawner.SomethingIsMoving == true)
        //    {
        //        uint colNum = 0, rowNum = 0;

        //        //Check col
        //        for (uint it = 0; it < col; ++it)
        //        {
        //            if (grid[0].transform.position.x + ((tileWidth * (it + 1)) - halfTileWidth) > Input.mousePosition.x)
        //            {
        //                colNum = it;
        //                break;
        //            }
        //        }

        //        //Check row
        //        for (uint it = 0; it < row; ++it)
        //        {
        //            if (grid[0].transform.position.y + ((tileHeight * (it + 1)) - halfTileHeight) > Input.mousePosition.y)
        //            {
        //                rowNum = it;
        //                break;
        //            }
        //        }

        //        objectIndex = colNum + (rowNum * col);
        //        //FirstTetrisBlock.transform.position = grid[objectIndex].transform.position;
        //        if (InGridCheck(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject]) && !theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning)
        //        {
        //            switch (theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].Whatisbeingmoved)
        //            {
        //                case "btmLeft":
        //                    {
        //                        if(taken[objectIndex])
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                          //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                        }
        //                        else
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.MovePosition(grid[objectIndex].transform.position);
        //                        }

        //                        //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = grid[objectIndex].transform.position;
        //                        break;
        //                    }
        //                case "btmRight":
        //                    {
        //                        if (taken[objectIndex])
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                           // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                        }
        //                        else
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.MovePosition(grid[objectIndex].transform.position);
        //                        }
        //                        //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.position = grid[objectIndex].transform.position;
        //                        break;
        //                    }
        //                case "topLeft":
        //                    {
        //                        if (taken[objectIndex])
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                           // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                        }
        //                        else
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.MovePosition(grid[objectIndex].transform.position);
        //                        }
        //                        //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.position = grid[objectIndex].transform.position;
        //                        break;
        //                    }
        //                case "topRight":
        //                    {
        //                        if (taken[objectIndex])
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                           // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                        }
        //                        else
        //                        {
        //                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.MovePosition(grid[objectIndex].transform.position);
        //                        }
        //                        //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.position = grid[objectIndex].transform.position;
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            };
        //            for (int j = 0; j < gridSize; ++j)
        //            {
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    Debug.Log("hello");
        //                    if(taken[j])
        //                    {
        //                        theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                       // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                    }
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    Debug.Log("hello");
        //                    if (taken[j])
        //                    {
        //                        theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                        //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                    }
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    Debug.Log("hello");
        //                    if (taken[j])
        //                    {
        //                        theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                       // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                    }
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    Debug.Log("hello");
        //                    if (taken[j])
        //                    {
        //                        theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
        //                      //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
        //                    }
        //                }
        //            }
        //            isMouseMovingAnObject = true;
        //        }
        //    }

        //}
        //else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
        //{
        //    //Set the tetris block to not moving
        //    //tetrisBlock.isMoving = false;
        //    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].isMoving = false;
        //    //Pass the tetris block data into the grid data
        //    if (theGridData.AddTetrisBlockData(objectIndex, "Test01", "UnitTest", 100, 20, 35, 45, grid[objectIndex].transform.position))
        //    {
        //        Debug.Log("Successful Save");
        //    }
        //    else
        //    {
        //        Debug.Log("Unsuccessful Save");
        //    }
        //}
        ////When the tetris block is picked up from the grid, it removes the data from that tile
        //if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
        //{
        //    //Remove the data from the tile
        //    if (theGridData.RemoveTetrisBlockData(objectIndex))
        //    {
        //        Debug.Log("Successful Removal");
        //    }
        //    else
        //    {
        //        Debug.Log("Unsuccessful Removal");
        //    }

        //    isMouseMovingAnObject = false;
        //}
        //for (int i = 0; i < 3; ++i)
        //{

        //    if (!InGridCheck(theTetrisSpawner.tetrisList[i]) && theTetrisSpawner.tetrisList[i].isMoving == false && !theTetrisSpawner.tetrisList[i].returning)
        //    {
        //        theTetrisSpawner.tetrisList[i].returning = true;
        //        theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
        //    }
        //    if(theTetrisSpawner.tetrisList[i].returning)
        //    {
        //        theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
        //    }
        //    if(theTetrisSpawner.tetrisList[i].btmLeft.transform.position.x ==  theTetrisSpawner.tetrisList[i].origin.x 
        //        && theTetrisSpawner.tetrisList[i].btmLeft.transform.position.y == theTetrisSpawner.tetrisList[i].origin.y)
        //    {
        //        theTetrisSpawner.tetrisList[i].returning = false;
        //    }
        //    for (int j = 0; j < gridSize; ++j)
        //    {
        //        taken[j] = false;
        //    }
        //    for (int k = 0; k < 3; ++k)
        //    {
        //        if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
        //        {
        //            for (int j = 0; j < gridSize; ++j)
        //            {
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //            }
        //        }
        //    }
        //    if (InGridCheck(theTetrisSpawner.tetrisList[i]) && !theTetrisSpawner.tetrisList[i].returning && theTetrisSpawner.tetrisList[i].isMoving == false)
        //    {
        //        float nearest = 1000000;
        //        int index = 0;
        //        for(int j = 0; j < gridSize; ++j)
        //        {
        //            Vector2 distance;
        //            distance.x = theTetrisSpawner.tetrisList[i].btmLeft.position.x - grid[j].transform.position.x;
        //            distance.y = theTetrisSpawner.tetrisList[i].btmLeft.position.y - grid[j].transform.position.y;
        //            float hello = distance.SqrMagnitude();
        //            if (hello < nearest)
        //            {
        //                nearest = hello;
        //                index = j;
        //            }
        //        }
        //        if(taken[i])
        //        {
        //            theTetrisSpawner.tetrisList[i].returning = true;
        //            theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
        //        }
        //        else
        //        {
        //            theTetrisSpawner.tetrisList[i].btmLeft.MovePosition(grid[index].transform.position);
        //        }
        //    }
        //    for (int k = 0; k < 3; ++k)
        //    {
        //        if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
        //        {
        //            for (int j = 0; j < gridSize; ++j)
        //            {
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //                if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
        //                    && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
        //                {
        //                    taken[j] = true;
        //                }
        //            }
        //        }
        //    }

        //}
    }

    public void GameUpdate()
    {
        if (Input.GetMouseButton(0) == true)
        {
            if (theTetrisSpawner.SomethingIsMoving == true)
            {
                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].sav = false;
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
                bool mouse = false;

                if(Input.mousePosition.x < grid[0].transform.position.x - halfTileWidth - 50 || Input.mousePosition.x > grid[col - 1].transform.position.x + halfTileWidth + 50
                 && Input.mousePosition.y < grid[0].transform.position.y - halfTileHeight - 50 && Input.mousePosition.y > grid[gridSize - 1].transform.position.y + halfTileHeight + 50)
                {
                    mouse = true;
                }
                if (InGridCheck(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject]) && !theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning && !mouse)
                {
                    switch (theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].Whatisbeingmoved)
                    {
                        case "partOne":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partOne.MovePosition(Input.mousePosition);
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
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partTwo.MovePosition(Input.mousePosition);
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
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partThree.MovePosition(Input.mousePosition);
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
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].partFour.MovePosition(Input.mousePosition);
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
                }
                isMouseMovingAnObject = true;
            }

        }
        else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
        {
            //Set the tetris block to not moving
            //tetrisBlock.isMoving = false;
            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].isMoving = false;
            if(!InGridCheck(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject]))
            {
                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
            }
          
        }
        //When the tetris block is picked up from the grid, it removes the data from that tile
        if (isMouseMovingAnObject && Input.GetMouseButtonDown(0))
        {
            isMouseMovingAnObject = false;
        }
        for (int i = 0; i < 3; ++i)
        {

            if (!InGridCheck(theTetrisSpawner.tetrisList[i]) && theTetrisSpawner.tetrisList[i].isMoving == false && !theTetrisSpawner.tetrisList[i].returning)
            {
                theTetrisSpawner.tetrisList[i].returning = true;
                theTetrisSpawner.tetrisList[i].partOne.position = theTetrisSpawner.tetrisList[i].origin;
            }
            if (theTetrisSpawner.tetrisList[i].returning && theTetrisSpawner.tetrisList[i].isMoving == false)
            {

                theTetrisSpawner.tetrisList[i].partOne.position = theTetrisSpawner.tetrisList[i].origin;
            }
            if(theTetrisSpawner.tetrisList[i].partOne.transform.position.x ==  theTetrisSpawner.tetrisList[i].origin.x 
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
            if(InGridCheck(theTetrisSpawner.tetrisList[i]) && theTetrisSpawner.tetrisList[i].isMoving == false)
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
        for (uint x = 0; x < col - 1; ++x)
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
                PlayerHealth.addHealth(-10);
                for (uint numCol = 0; numCol < col; ++numCol)
                {
                    UnSetIsGreyOut(numCol + y * 10);
                }
            }
        }
        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.ToString());
        //}
    }
}

//Terrain basic unit modifiers: Hills favourable to Bowmen, Forest favourable to Combat Infantry, Rivers favourable to Cavalry
//Plains favour all units


