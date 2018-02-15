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

    private Tetris tetrisBlock = null;
    private GridData theGridData = null;
    private TetrisSpawner theTetrisSpawner = null;

    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;

    [SerializeField]
    Image[] grid = new Image[gridSize];
    
    [SerializeField]
    Canvas thisCanvas;

    // Use this for initialization
    void Start () {
        tetrisBlock =  GameObject.Find("RedQuad").GetComponent<Tetris>();

        theGridData = new GridData();
        theGridData.Init();

        theTetrisSpawner = GameObject.Find("Spawner").GetComponent<TetrisSpawner>();

        Debug.Assert(tetrisBlock != null);
        Debug.Assert(theGridData != null);
        Debug.Assert(theTetrisSpawner != null);

        RectTransform objectRectTransform = thisCanvas.GetComponent<RectTransform>();
        Vector2 Grid0Pos = new Vector2(objectRectTransform.transform.position.x - (0.5f * (col - 1) * tileWidth) , objectRectTransform.transform.position.y  - ((row * tileHeight)) - 2 * (tileHeight));
        
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
            
            //Adjusts the individual grid block's position\
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

                switch(theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].Whatisbeingmoved)
                {
                    case "btmLeft":
                        {
                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmLeft.MovePosition(grid[objectIndex].transform.position);
                            break;
                        }
                    case "btmRight":
                        {
                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].btmRight.MovePosition(grid[objectIndex].transform.position);
                            break;
                        }
                    case "topLeft":
                        {
                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topLeft.MovePosition(grid[objectIndex].transform.position);
                            break;
                        }
                    case "topRight":
                        {
                            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].topRight.MovePosition(grid[objectIndex].transform.position);
                            break;
                        }
                    default:
                        break;
                };
                isMouseMovingAnObject = true;
            }

        }
        else if (isMouseMovingAnObject && Input.GetMouseButtonUp(0)) //When the touch is released, it saves the tetris data into the data grid tile
        {
            //Set the tetris block to not moving
            // tetrisBlock.isMoving = false;
            theTetrisSpawner.tetrisList[theTetrisSpawner.IndexofMovingObject].isMoving = false;

            //Pass the tetris block data into the grid data
            if (theGridData.AddTetrisBlockData(objectIndex, "Test01", "UnitTest", 100, 20, 35, 45))
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
    
    public bool AddTetrisBlockData(uint Index, string NameID, string UnitType, uint Health, uint moveSpeed, uint attackDamage, uint attackRate)
    {
        //Check if there is already a unit in that tile
        if (gridData[Index] != null)
        {
            return false;
        }

        gridData[Index] = new TetrisData();
        gridData[Index].CreateUnit(NameID, UnitType, Health, moveSpeed, attackDamage, attackRate);
        
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

    public TetrisData()
    {
        NameID = UnitType = "";
        Health = moveSpeed = attackDamage = attackRate = 0;
        OriginalHealth = OriginalMoveSpeed = OriginalAttackDamage = OriginalAttackRate = 0;
    }
    
    public void CreateUnit(string NameID, string UnitType, uint Health, uint moveSpeed, uint attackDamage, uint attackRate)
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


