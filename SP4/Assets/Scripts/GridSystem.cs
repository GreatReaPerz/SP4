using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour {
    
    const ushort row = 6, col = 10;
    const uint gridSize = row * col;

    const float tileWidth = 100;
    const float tileHeight = 100;

    float halfTileWidth = tileWidth * 0.5f, halfTileHeight = tileHeight * 0.5f;

    [SerializeField]
    Image[] grid = new Image[gridSize];

	// Use this for initialization
	void Start () { 

        uint rowCount = 1, heightCount = 0;

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
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
