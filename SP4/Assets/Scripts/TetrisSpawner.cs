using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TetrisSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] TetrisTypes;

    [SerializeField]
    Sprite[] troopImages;

    [SerializeField]
    GameObject playerSpawner;

    [SerializeField]
    GameObject enemySpawner;

    public List<TetrisCube> playerList = new List<TetrisCube>(3);
    public List<TetrisCube> enemyList = new List<TetrisCube>(3);

    float timer = 0;
    int playerSpawned = 0;
    int enemySpawned = 0;
    private Vector3 pil;
    private Vector3 pil1;
    public bool playerIsMoving = false;
    public bool enemyIsMoving = false;

    public int IndexofPlayerObject = 0;
    public int IndexofEnemyObject = 0;

    public bool SpawnSpecific = false;
    public bool SpawnForEnemy = false;
    public bool SpawnForPlayer = false;
    public bool SpawnInfantry = false;
    public bool SpawnBowmen = false;
    public bool SpawnCavalry = false;
    public int SpawnCountPlayer = 0;
    public int SpawnCountEnemy = 0;



    // Use this for initialization
    public void Start()
    {
        playerSpawned = 0;
        enemySpawned = 0;
        if (!SpawnSpecific) //Spawn random
        {
            for (int i = 0; i < 3; ++i)
            {

                int rand = Random.Range(0, TetrisTypes.Length); //Rand piece            
                int randUnitType = Random.Range(0, 3);  //Rand unit 
                //Key for setting instantiated unit in array 
                //Spawner for the pos of spawner in scene
                //Type for which team the unit is (0 for player , 1 for enemy)
                //Shape is the shape to instantiate
                playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, randUnitType, TetrisTypes[rand]);
                enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, randUnitType, TetrisTypes[rand]);
            }
        }
        else  //Spawn Specific
        {
            if (SpawnForPlayer)
            {
                for (int count = 0; count < SpawnCountPlayer; ++count)
                {
                    if (SpawnCavalry && SpawnInfantry)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        int randUnitType = Random.Range(0, 2); // Rand Unit
                        if (randUnitType == 0)   //Spawn cavalry                      
                            playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 0, TetrisTypes[rand]);
                        else //Spawn infantry                       
                            playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 1, TetrisTypes[rand]);
                    }
                    else if (SpawnCavalry && SpawnBowmen)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        int randUnitType = Random.Range(0, 2);
                        if (randUnitType == 0)   //Spawn cavalry
                            playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 0, TetrisTypes[rand]);
                        else //Spawn Bowmen 
                            playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 2, TetrisTypes[rand]);
                    }
                    else if (SpawnInfantry && SpawnBowmen)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        int randUnitType = Random.Range(0, 2);
                        if (randUnitType == 0)   //Spawn infanry
                            playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 1, TetrisTypes[rand]);
                        else //Spawn Bowmen 
                            playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 2, TetrisTypes[rand]);
                    }
                    else if (SpawnCavalry)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 0, TetrisTypes[rand]);
                    }
                    else if (SpawnInfantry)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 1, TetrisTypes[rand]);
                    }

                    else if (SpawnBowmen)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        playerSpawned = SpawnTetris(playerSpawned, playerSpawner, 0, 2, TetrisTypes[rand]);
                    }
                }
            }
            if (SpawnForEnemy)
            {
                for (int count = 0; count < SpawnCountEnemy; ++count)
                {
                    if (SpawnCavalry && SpawnInfantry)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        int randUnitType = Random.Range(0, 2); // Rand Unit
                        if (randUnitType == 0)   //Spawn cavalry                      
                            enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 0, TetrisTypes[rand]);
                        else //Spawn infantry                       
                            enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 1, TetrisTypes[rand]);
                    }
                    else if (SpawnCavalry && SpawnBowmen)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        int randUnitType = Random.Range(0, 2);
                        if (randUnitType == 0)   //Spawn cavalry
                            enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 0, TetrisTypes[rand]);
                        else //Spawn Bowmen 
                            enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 2, TetrisTypes[rand]);
                    }
                    else if (SpawnInfantry && SpawnBowmen)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        int randUnitType = Random.Range(0, 2);
                        if (randUnitType == 0)   //Spawn infanry
                            enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 1, TetrisTypes[rand]);
                        else //Spawn Bowmen 
                            enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 2, TetrisTypes[rand]);
                    }
                    else if (SpawnCavalry)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 0, 0, TetrisTypes[rand]);
                    }
                    else if (SpawnInfantry)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 1, TetrisTypes[rand]);
                    }

                    else if (SpawnBowmen)
                    {
                        int rand = Random.Range(0, TetrisTypes.Length);  //Rand Piece
                        enemySpawned = SpawnTetris(enemySpawned, enemySpawner, 1, 2, TetrisTypes[rand]);                        
                    }
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playerList.Count; ++i)
        {
            playerIsMoving = false;

            if (playerList[i] == null)
            {
                continue;
            }

            if (playerList[i].isMoving)
            {
                playerIsMoving = true;
                IndexofPlayerObject = i;
                break;
            }

            playerList[i].isMoving = false;
        }

        for (int i = 0; i < enemyList.Count; ++i)
        {
            enemyIsMoving = false;

            if (enemyList[i] == null)
            {
                continue;
            }

            if (enemyList[i].isMoving)
            {
                enemyIsMoving = true;
                IndexofEnemyObject = i;
                break;
            }

            enemyList[i].isMoving = false;
        }
    }

    //Key for setting instantiated unit in array 
    //Spawner for the pos of spawner in scene
    //Team for which team the unit is (0 for player , 1 for enemy)
    //Type is for unit type
    //Shape is the shape to instantiate
    int SpawnTetris(int key, GameObject spawner, int team, int type, GameObject shape)
    {
        TetrisCube theCube = new TetrisCube();
        //Instantiate base on spawner pos 
        if (team == 0)
            theCube.parentCube = Instantiate(shape, spawner.transform.position, spawner.transform.rotation);
        else    //Rotate by 180 on x axis if is enemy piece to mirror player
            theCube.parentCube = Instantiate(shape, spawner.transform.position, Quaternion.Euler(180, 0, 0));

        //So that it appears within canvas
        theCube.parentCube.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);

        //Off set x pos base on key(index)
        pil.Set(-300 + (key * 300), 0, 0);
        //Set the origin (snapping)
        pil1 = theCube.parentCube.transform.position + pil;
        theCube.origin.Set(pil1.x, pil1.y, pil1.z);
        //Translate to origin
        theCube.parentCube.transform.position = theCube.origin;

        //Store parts locally
        GameObject cubeOne = theCube.parentCube.transform.Find("partOne").gameObject;
        GameObject cubeTwo = theCube.parentCube.transform.Find("partTwo").gameObject;
        GameObject cubeThree = theCube.parentCube.transform.Find("partThree").gameObject;
        GameObject cubeFour = theCube.parentCube.transform.Find("partFour").gameObject;


        //Set up the 4 cubes based on theCube.parentCube's child
        theCube.setTheCubes(cubeOne.GetComponent<Rigidbody2D>(), cubeTwo.GetComponent<Rigidbody2D>(), cubeThree.GetComponent<Rigidbody2D>(), cubeFour.GetComponent<Rigidbody2D>());

        theCube.origin = theCube.partOne.position;

        //Assigning types base on shape
        if (shape == TetrisTypes[0])
            theCube.setTheObjectType(TetrisCube.objectType.TETRIS_4X4);
        else if (shape == TetrisTypes[1])
            theCube.setTheObjectType(TetrisCube.objectType.TETRIS_L);
        else if (shape == TetrisTypes[2])
            theCube.setTheObjectType(TetrisCube.objectType.TETRIS_T);
        else
            theCube.setTheObjectType(TetrisCube.objectType.TETRIS_Z);


        //Set the unit's type
        switch (type)
        {
            case 0:
                {
                    theCube.troopName = "Cavalry";
                    foreach (Transform child in theCube.parentCube.transform)
                    {
                        Image theSprite = child.GetComponent<Image>();
                        theSprite.sprite = troopImages[0];
                    }
                    break;
                }
            case 1:
                {
                    theCube.troopName = "Infantry";
                    foreach (Transform child in theCube.parentCube.transform)
                    {
                        Image theSprite = child.GetComponent<Image>();
                        theSprite.sprite = troopImages[1];
                    }
                    break;
                }
            case 2:
                {
                    theCube.troopName = "Bowmen";
                    foreach (Transform child in theCube.parentCube.transform)
                    {
                        Image theSprite = child.GetComponent<Image>();
                        theSprite.sprite = troopImages[2];
                    }
                    break;
                }
        };


        //Could use raycast instead 
        //Also cause the only thing changing is the movement function, could try to make a switch instead
        //Trigger and entry for bottom left 
        EventTrigger BtmLTrig = cubeOne.GetComponent<EventTrigger>();
        EventTrigger.Entry BtmLEntry = new EventTrigger.Entry();
        BtmLEntry.eventID = EventTriggerType.Drag;

        BtmLEntry.callback.AddListener((data) =>
        {
            theCube.DragObject(theCube.partOne);
        });
        BtmLTrig.triggers.Add(BtmLEntry);

        //Trigger and entry for bottom Right 
        EventTrigger BtmRTrig = cubeTwo.GetComponent<EventTrigger>();
        EventTrigger.Entry BtmREntry = new EventTrigger.Entry();
        BtmREntry.eventID = EventTriggerType.Drag;
        BtmREntry.callback.AddListener((data) =>
        {
            theCube.DragObject(theCube.partTwo);
        });
        BtmRTrig.triggers.Add(BtmREntry);

        //Trigger and entry for top Left 
        EventTrigger TopLTrig = cubeThree.GetComponent<EventTrigger>();
        EventTrigger.Entry TopLEntry = new EventTrigger.Entry();
        TopLEntry.eventID = EventTriggerType.Drag;
        TopLEntry.callback.AddListener((data) =>
        {
            theCube.DragObject(theCube.partThree);
        });
        TopLTrig.triggers.Add(TopLEntry);

        //Trigger and entry for top Right 
        EventTrigger TopRTrig = cubeFour.GetComponent<EventTrigger>();
        EventTrigger.Entry TopREntry = new EventTrigger.Entry();
        TopREntry.eventID = EventTriggerType.Drag;
        TopREntry.callback.AddListener((data) =>
        {
            theCube.DragObject(theCube.partFour);
        });
        TopRTrig.triggers.Add(TopREntry);

        //Rotate the cubes back if is enemy (event trigger will not trigger if this is not done for enemy)
        if (team == 1)
        {
            cubeOne.transform.localRotation = Quaternion.Euler(180, 0, 0);
            cubeTwo.transform.localRotation = Quaternion.Euler(180, 0, 0);
            cubeThree.transform.localRotation = Quaternion.Euler(180, 0, 0);
            cubeFour.transform.localRotation = Quaternion.Euler(180, 0, 0);
        }

        Vector2 currentCubeSIze = cubeOne.GetComponent<RectTransform>().sizeDelta;
        Vector2 canvasLocalScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;

        cubeOne.GetComponent<RectTransform>().sizeDelta = new Vector2(currentCubeSIze.x * canvasLocalScale.x, currentCubeSIze.y * canvasLocalScale.y);
        cubeTwo.GetComponent<RectTransform>().sizeDelta = new Vector2(currentCubeSIze.x * canvasLocalScale.x, currentCubeSIze.y * canvasLocalScale.y);
        cubeThree.GetComponent<RectTransform>().sizeDelta = new Vector2(currentCubeSIze.x * canvasLocalScale.x, currentCubeSIze.y * canvasLocalScale.y);
        cubeFour.GetComponent<RectTransform>().sizeDelta = new Vector2(currentCubeSIze.x * canvasLocalScale.x, currentCubeSIze.y * canvasLocalScale.y);

        theCube.parentCube.transform.SetParent(spawner.transform);

        //Storing into the respective lists
        if (team == 0)
            //playerList[key] = theCube;
            playerList.Add(theCube);
        else
            //enemyList[key] = theCube;
            enemyList.Add(theCube);

        //Increment index for next object 
        ++key;
        return key;
    }

    public void TetrisSpawnOffset()
    {

    }
}