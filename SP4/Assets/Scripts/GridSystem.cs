using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour {
    RectTransform panelRectTransform;

    const ushort row = 6, col = 10;
    const uint gridSize = row * col;

    const float tileWidth = 100;
    const float tileHeight = 100;

    private Tetris tetrisBlock = null;

    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;

    [SerializeField]
    Image[] grid = new Image[gridSize];

    [SerializeField]
    Image FirstTetrisBlock;

    Canvas theCanvas;
    
    // Use this for initialization
    void Start () {
        tetrisBlock =  GameObject.Find("RedQuad").GetComponent<Tetris>();
        Debug.Assert(tetrisBlock != null);

        uint rowCount = 1, heightCount = 1;
        
        for (uint i = 0; i < gridSize; ++i)
        {
            //Adjusts the individual grid block's size
            grid[i].rectTransform.sizeDelta = new Vector2(tileWidth, tileHeight);
            
            //Adjusts the individual grid block's position
            grid[i].transform.position = new Vector2(rowCount * halfTileWidth, heightCount * halfTileHeight);

            rowCount += 2;

            if ((i + 1) % (col) == 0)
            {
                rowCount = 1;
                heightCount += 2;
            }

            grid[i].rectTransform.anchorMin = new Vector2(0.5f, 0);
            grid[i].rectTransform.anchorMax = new Vector2(0.5f, 0);
            grid[i].rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            if (tetrisBlock.isMoving == true)
            {
                uint colNum = 0, rowNum = 0;

                //Check col
                for (uint it = 0; it < col; ++it)
                {
                    float gh = grid[0].transform.position.x + ((tileWidth * (it + 1)) - halfTileWidth);
                    if (grid[0].transform.position.x + ((tileWidth * (it + 1)) - halfTileWidth) > Input.mousePosition.x)
                    {
                        colNum = it;
                        break;
                    }
                }

                //Check row
                for (uint it = 0; it < row; ++it)
                {
                    float gh = grid[0].transform.position.y + ((tileHeight * (it + 1)) - halfTileHeight);
                    if (grid[0].transform.position.y + ((tileHeight * (it + 1)) - halfTileHeight) > Input.mousePosition.y)
                    {
                        rowNum = it;
                        break;
                    }
                }

                uint rg = colNum + (rowNum * col);
                Debug.Log(rg.ToString());
                Debug.Log("X: " + grid[rg].transform.position.x.ToString() + " Y: " + grid[rg].transform.position.y.ToString());
                Debug.Log("MouseX: " + Input.mousePosition.x + " MouseY: " + Input.mousePosition.y);
                FirstTetrisBlock.transform.position = grid[colNum + (rowNum * col)].transform.position;
            }

        }
        else
        {
            tetrisBlock.isMoving = false;
        }

    }
}

