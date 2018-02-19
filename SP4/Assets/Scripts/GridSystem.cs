using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour {

    //Note: Increasing the num of rows & col means that you also need to add more images into the array in the inspector
    const ushort row = 6, col = 10;
    public const uint gridSize = row * col;

    const float tileWidth = 100;
    const float tileHeight = 100;
    
    private GridData theGridData = null;
    private TetrisSpawner theTetrisSpawner = null;

    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;
    bool[] taken = new bool[gridSize];

    [SerializeField]
    Image[] grid = new Image[gridSize];
    
    [SerializeField]
    Canvas thisCanvas;

    // Use this for initialization
    void Start () {

        theGridData = new GridData();
        theGridData.Init();
        for(int i = 0; i < gridSize; ++i)
        {
            taken[i] = false;
        }
        theTetrisSpawner = GameObject.Find("Spawner").GetComponent<TetrisSpawner>();
        
        Debug.Assert(theGridData != null);
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

    }

    private uint objectIndex = 0;
    bool isMouseMovingAnObject = false;

    // Update is called once per frame
    void Update()
    {
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
                        case "btmLeft":
                            {
                                if(taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                  //  theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.MovePosition(grid[objectIndex].transform.position);
                                }
                               
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "btmRight":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                   // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "topLeft":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                   // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.position = grid[objectIndex].transform.position;
                                break;
                            }
                        case "topRight":
                            {
                                if (taken[objectIndex])
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                   // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                                }
                                else
                                {
                                    theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.MovePosition(grid[objectIndex].transform.position);
                                }
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.position = grid[objectIndex].transform.position;
                                break;
                            }
                        default:
                            break;
                    };
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            Debug.Log("hello");
                            if(taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                               // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            Debug.Log("hello");
                            if (taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                                //theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            Debug.Log("hello");
                            if (taken[j])
                            {
                                theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].returning = true;
                               // theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.position = theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].origin;
                            }
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            Debug.Log("hello");
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
                theTetrisSpawner.tetrisList[i].returning = true;
                theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
            }
            if(theTetrisSpawner.tetrisList[i].returning)
            {
                theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
            }
            if(theTetrisSpawner.tetrisList[i].btmLeft.transform.position.x ==  theTetrisSpawner.tetrisList[i].origin.x 
                && theTetrisSpawner.tetrisList[i].btmLeft.transform.position.y == theTetrisSpawner.tetrisList[i].origin.y)
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
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
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
                for(int j = 0; j < gridSize; ++j)
                {
                    Vector2 distance;
                    distance.x = theTetrisSpawner.tetrisList[i].btmLeft.position.x - grid[j].transform.position.x;
                    distance.y = theTetrisSpawner.tetrisList[i].btmLeft.position.y - grid[j].transform.position.y;
                    float hello = distance.SqrMagnitude();
                    if (hello < nearest)
                    {
                        nearest = hello;
                        index = j;
                    }
                }
                if(taken[i])
                {
                    theTetrisSpawner.tetrisList[i].returning = true;
                    theTetrisSpawner.tetrisList[i].btmLeft.position = theTetrisSpawner.tetrisList[i].origin;
                }
                else
                {
                    theTetrisSpawner.tetrisList[i].btmLeft.MovePosition(grid[index].transform.position);
                }
            }
            for (int k = 0; k < 3; ++k)
            {
                if (InGridCheck(theTetrisSpawner.tetrisList[k]) && theTetrisSpawner.tetrisList[k].isMoving == false && !theTetrisSpawner.tetrisList[k].returning)
                {
                    for (int j = 0; j < gridSize; ++j)
                    {
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmLeft.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].btmRight.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topLeft.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                        if (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.x - grid[j].transform.position.x) < 50
                            && (Mathf.Abs(theTetrisSpawner.tetrisList[k].topRight.transform.position.y - grid[j].transform.position.y) < 50))
                        {
                            taken[j] = true;
                        }
                    }
                }
            }

        }
    }

    bool InGridCheck(TetrisCube cube)
    {
        if (cube.btmLeft.position.x > grid[0].transform.position.x - halfTileWidth && cube.btmLeft.position.x < grid[col - 1].transform.position.x + halfTileWidth
            && cube.btmLeft.position.y > grid[0].transform.position.y - halfTileHeight && cube.btmLeft.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
            && cube.btmRight.position.x > grid[0].transform.position.x - halfTileWidth && cube.btmRight.position.x < grid[col - 1].transform.position.x + halfTileWidth
            && cube.btmRight.position.y > grid[0].transform.position.y - halfTileHeight && cube.btmRight.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
            && cube.topLeft.position.x > grid[0].transform.position.x - halfTileWidth && cube.topLeft.position.x < grid[col - 1].transform.position.x + halfTileWidth
            && cube.topLeft.position.y > grid[0].transform.position.y - halfTileHeight && cube.topLeft.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight
            && cube.topRight.position.x > grid[0].transform.position.x - halfTileWidth && cube.topRight.position.x < grid[col - 1].transform.position.x + halfTileWidth
            && cube.topRight.position.y > grid[0].transform.position.y - halfTileHeight && cube.topRight.position.y < grid[gridSize - 1].transform.position.y + halfTileHeight)
        {
            return true;
        }
        return false;

    }
}

//The grid where data is stored
public class GridData
{
    public TetrisData[] gridData;
    uint gridDataSize;
    
    public void Init()
    {
        gridDataSize = GridSystem.gridSize;
        gridData = new TetrisData[gridDataSize];
    }
    
    public bool AddTetrisBlockData(uint Index, string NameID, string UnitType, uint Health, uint moveSpeed, uint attackDamage, uint attackRate, Vector2 Position)
    {
        //Check if there is already a unit in that tile
        if (gridData[Index] != null)
        {
            return false;
        }

        gridData[Index] = new TetrisData();
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

//Storage class for Tetris Block data
public class TetrisData
{
    string NameID, UnitType;
    uint Health, moveSpeed, attackDamage, attackRate;
    uint OriginalHealth, OriginalMoveSpeed, OriginalAttackDamage, OriginalAttackRate;
    Vector2 Position = new Vector2();

    public TetrisData()
    {
        NameID = UnitType = "";
        Health = moveSpeed = attackDamage = attackRate = 0;
        OriginalHealth = OriginalMoveSpeed = OriginalAttackDamage = OriginalAttackRate = 0;
    }
    
    public void CreateUnit(string NameID, string UnitType, uint Health, uint moveSpeed, uint attackDamage, uint attackRate, Vector2 position)
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

    //Getters
    uint GetHealth()
    {
        return Health;
    }
    uint GetMoveSpeed()
    {
        return moveSpeed;
    }
    uint GetAttackDamage()
    {
        return attackDamage;
    }
    uint GetAttackRate()
    {
        return attackRate;
    }
    string GetNameID()
    {
        return NameID;
    }
    string GetUnitType()
    {
        return UnitType;
    }
    Vector2 GetPosition()
    {
        return Position;
    }

    //Setters
    void SetHealth(uint newHealthValue)
    {
        Health = newHealthValue;
    }
    void SetMoveSpeed(uint newMoveSpeedValue)
    {
        moveSpeed = newMoveSpeedValue;
    }
    void SetAttackDamage(uint newAttackDamageValue)
    {
        attackDamage = newAttackDamageValue;
    }
    void SetAttackRate(uint newAttackRateValue)
    {
        attackRate = newAttackRateValue;
    }
    void SetNameID(string newNameIDValue)
    {
        NameID = newNameIDValue;
    }
    void SetUnitType(string newUnitTypeValue)
    {
        UnitType = newUnitTypeValue;
    }
    void SetPosition(Vector2 newPosition)
    {
        Position = newPosition;
    }

    //Subtractors & Adders
    void SubractHealth(uint HealthToSubtract)
    {
        Health -= HealthToSubtract;
    }
    void AddHealth(uint HealthToAdd)
    {
        Health += HealthToAdd;
    }
    void SubractMoveSpeed(uint MoveSpeedToSubtract)
    {
        moveSpeed -= MoveSpeedToSubtract;
    }
    void AddMoveSpeed(uint MoveSpeedToAdd)
    {
        moveSpeed += MoveSpeedToAdd;
    }
    void SubractAttackRate(uint AttackRateToSubtract)
    {
        attackRate -= AttackRateToSubtract;
    }
    void AddAttackRate(uint AttackRateToAdd)
    {
        attackRate += AttackRateToAdd;
    }
    void SubractAttackDamage(uint AttackDamageToSubtract)
    {
        attackDamage -= AttackDamageToSubtract;
    }
    void AddAttackDamage(uint AttackDamageToAdd)
    {
        attackDamage += AttackDamageToAdd;
    }
}


