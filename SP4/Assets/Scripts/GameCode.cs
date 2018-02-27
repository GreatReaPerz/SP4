﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameCode : MonoBehaviour {

    [SerializeField]
    Canvas Ui;


    [SerializeField]
    GameObject Terrain;
    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;
    [SerializeField]
    Animator UIPanelAnimator;
    [SerializeField]
    Sprite PaperBG;
    enum GameState
    {
        PLANNING,
        ATTACK
    };
    private float timer;
    private bool destroyed;
    private TetrisSpawner theSpawner = null;
    private GridSystem theGridSystem = null;
    private enemyGridSystem enemyGridSystem = null;
    private TroopAI troop = null;
    public bool player1 = false;
    public bool player2 = false;
    public int effect;
    public string info;

    public bool ready = false;
    public bool melee = true;
    public int side = 0;
    public bool blockRespawn = false;

    private string TerrainName;
    int state;
    public List<GameObject> objects;

    [System.Serializable]
    public struct TrapTypes
    {
        public string name;
        public GameObject trapPrefab;
    }
    public List<TrapTypes> typesOfTraps; //Defines the individual kind of traps that will exist in game
    public bool Gameover = false;
    //CheckWinLose winLoseChecker;
    HealthSystem P1Health;
    HealthSystem P2Health;
    // Use this for initialization
    void Start () {
        theSpawner = GameObject.Find("EventSystem").GetComponent<TetrisSpawner>();
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        enemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();
        //winLoseChecker = GameObject.Find("EventSystem").GetComponent<CheckWinLose>();
        P1Health = Player1.GetComponent<HealthSystem>();
        P2Health = Player2.GetComponent<HealthSystem>();
        timer = 0;
        destroyed = false;
        state = (int)GameState.PLANNING;
        TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
        //effect = 0;
    }

    // Update is called once per frame
    void Update() {
        if (CheckWin())
            return;
        if (state == (int)GameState.PLANNING)
        {
            UIPanelAnimator.SetBool("UIPanelEnabled", true);
            //if (timer > 10.0f && !destroyed)
            //{
            //    ready = true;
            //}
            //effect = 1;// Random.Range(0, 0);
            switch (effect)
            {
                case 0:
                    info = "Normal";
                    melee = false;
                    side = 0;
                    break;
                case 1:
                    info = "onlyfoward";
                    melee = false;
                    side = 1;
                    break;
                case 2:
                    info = "onlyside";
                    melee = false;
                    side = 2;
                    break;
                case 3:
                    info = "onlymelee";
                    melee = true;
                    side = 0;
                    break;
                case 4:
                    info = "calvaryspeed";
                    break;
                case 5:
                    info = "bowmenspeed";
                    break;
                case 6:
                    info = "infantryspeed";
                    break;
            }

            if (ready && !destroyed)
            {
                Vector2 currentCubeSIze = theSpawner.playerList[0].partOne.GetComponent<RectTransform>().sizeDelta;
                Vector2 canvasLocalScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;

                for (int i = 0; i < theSpawner.playerList.Count; ++i)
                {
                    if (theSpawner.playerList[i].sav)
                    {
                        //theSpawner.playerList[i].partOne.transform.localscale
                        GameObject theTetrisObject = GameObject.Find(theSpawner.playerList[i].troopName);

                        GameObject newObj;
                        Vector3 hello = theSpawner.playerList[i].partOne.transform.position;
                        hello.z = 100;
                        newObj = (GameObject)Instantiate(theTetrisObject, hello, Quaternion.identity);
                        newObj.transform.parent = Ui.transform;

                        newObj.transform.localScale = new Vector3(newObj.transform.localScale.x * canvasLocalScale.x, newObj.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        if(troop.type == "Bowmen")               
                            troop.gameObject.AddComponent<Projectile>();
                        
                        float dist = 0;
                        for (uint j = 0; j < enemyGridSystem.GridSize; ++j)
                        {
                            float yDist = enemyGridSystem.grid[j].transform.position.y - troop.originPos.y;
                            if (Mathf.Abs(enemyGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !enemyGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = enemyGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if(effect == 4)
                                {
                                    if(troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if(effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if(effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }

                        objects.Add(newObj);

                        GameObject newObj1;
                        hello = theSpawner.playerList[i].partTwo.transform.position;
                        hello.z = 100;
                        newObj1 = (GameObject)Instantiate(theTetrisObject, hello, Quaternion.identity);
                        newObj1.transform.parent = Ui.transform;
                        newObj1.transform.localScale = new Vector3(newObj1.transform.localScale.x * canvasLocalScale.x, newObj1.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj1.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        dist = 0;
                        for (uint j = 0; j < enemyGridSystem.GridSize; ++j)
                        {
                            float yDist = enemyGridSystem.grid[j].transform.position.y - troop.originPos.y;
                            if (Mathf.Abs(enemyGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !enemyGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = enemyGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj1);

                        GameObject newObj2;
                        hello = theSpawner.playerList[i].partThree.transform.position;
                        hello.z = 100;
                        newObj2 = (GameObject)Instantiate(theTetrisObject, hello, Quaternion.identity);
                        newObj2.transform.parent = Ui.transform;
                        newObj2.transform.localScale = new Vector3(newObj2.transform.localScale.x * canvasLocalScale.x, newObj2.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj2.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        dist = 0;
                        for (uint j = 0; j < enemyGridSystem.GridSize; ++j)
                        {
                            float yDist = enemyGridSystem.grid[j].transform.position.y - troop.originPos.y;
                            if (Mathf.Abs(enemyGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !enemyGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = enemyGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj2);

                        GameObject newObj3;
                        hello = theSpawner.playerList[i].partFour.transform.position;
                        hello.z = 100;
                        newObj3 = (GameObject)Instantiate(theTetrisObject, hello, Quaternion.identity);
                        newObj3.transform.parent = Ui.transform;
                        newObj3.transform.localScale = new Vector3(newObj3.transform.localScale.x * canvasLocalScale.x, newObj3.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj3.GetComponent<TroopAI>();

                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        dist = 0;
                        for (uint j = 0; j < enemyGridSystem.GridSize; ++j)
                        {
                            float yDist = enemyGridSystem.grid[j].transform.position.y - troop.originPos.y;
                            if (Mathf.Abs(enemyGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !enemyGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = enemyGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj3);
                    }
                }
                for (int i = 0; i < theSpawner.enemyList.Count; ++i)
                {
                    if (theSpawner.enemyList[i].sav)
                    {
                        GameObject newObj;
                        Vector3 hello = theSpawner.enemyList[i].partOne.transform.position;
                        hello.z = 100;
                        newObj = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj.transform.parent = Ui.transform;
                        newObj.transform.localScale = new Vector3(newObj.transform.localScale.x * canvasLocalScale.x, newObj.transform.localScale.y * canvasLocalScale.y,0);

                        troop = newObj.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        float dist = 0;
                        for (uint j = 0; j < theGridSystem.GridSize; ++j)
                        {
                            float yDist = Mathf.Abs(theGridSystem.grid[j].transform.position.y - troop.originPos.y);
                            if (Mathf.Abs(theGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !theGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = theGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj);

                        GameObject newObj1;
                        hello = theSpawner.enemyList[i].partTwo.transform.position;
                        hello.z = 100;
                        newObj1 = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj1.transform.parent = Ui.transform;
                        newObj1.transform.localScale = new Vector3(newObj1.transform.localScale.x * canvasLocalScale.x, newObj1.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj1.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        dist = 0;
                        for (uint j = 0; j < theGridSystem.GridSize; ++j)
                        {
                            float yDist = Mathf.Abs(theGridSystem.grid[j].transform.position.y - troop.originPos.y);
                            if (Mathf.Abs(theGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !theGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = theGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj1);

                        GameObject newObj2;
                        hello = theSpawner.enemyList[i].partThree.transform.position;
                        hello.z = 100;
                        newObj2 = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj2.transform.parent = Ui.transform;
                        newObj2.transform.localScale = new Vector3(newObj2.transform.localScale.x * canvasLocalScale.x, newObj2.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj2.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        dist = 0;
                        for (uint j = 0; j < theGridSystem.GridSize; ++j)
                        {
                            float yDist = Mathf.Abs(theGridSystem.grid[j].transform.position.y - troop.originPos.y);
                            if (Mathf.Abs(theGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !theGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = theGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj2);

                        GameObject newObj3;
                        hello = theSpawner.enemyList[i].partFour.transform.position;
                        hello.z = 100;
                        newObj3 = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj3.transform.parent = Ui.transform;
                        newObj3.transform.localScale = new Vector3(newObj3.transform.localScale.x * canvasLocalScale.x, newObj3.transform.localScale.y * canvasLocalScale.y, 0);
                        troop = newObj3.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        if (troop.type == "Bowmen")
                            troop.gameObject.AddComponent<Projectile>();

                        dist = 0;
                        for (uint j = 0; j < theGridSystem.GridSize; ++j)
                        {
                            float yDist = Mathf.Abs(theGridSystem.grid[j].transform.position.y - troop.originPos.y);
                            if (Mathf.Abs(theGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !theGridSystem.IsGreyedOut(j) && yDist > dist)
                            {
                                dist = yDist;
                                troop.targetPos = theGridSystem.grid[j].transform.position;
                                troop.targetIndex = j;
                                if (effect == 4)
                                {
                                    if (troop.type == "Calvary")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 5)
                                {
                                    if (troop.type == "Bowmen")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                                else if (effect == 6)
                                {
                                    if (troop.type == "Infantry")
                                    {
                                        troop.speed *= 2;
                                    }
                                }
                            }
                        }
                        objects.Add(newObj3);
                    }
                }
                if (!blockRespawn)
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        Destroy(theSpawner.playerList[i].parentCube);
                        Destroy(theSpawner.enemyList[i].parentCube);
                        destroyed = true;
                    }
                }
                else
                {

                    List<int> indexToDelete = new List<int>();                  //Hold indexes to delete
                    for (int i = 0; i < theSpawner.playerList.Count; ++i) //Run through 
                    {
                        if (theGridSystem.InGridCheck(theSpawner.playerList[i]))
                        {
                            theSpawner.playerList[i].parentCube.SetActive(false);
                            detachEventTrigger(theSpawner.playerList[i].parentCube);
                            destroyed = true;
                        }
                        else
                        {
                            Destroy(theSpawner.playerList[i].parentCube);
                            indexToDelete.Add(i);
                        }
                    }
                    if (indexToDelete.Count > 0)
                    {
                        indexToDelete.Sort(new SortIntDescending()); //sort ascending order
                        if (indexToDelete[0] == theSpawner.playerList.Count)
                        {
                            theSpawner.playerList.Clear();
                        }
                        else
                            foreach (int i in indexToDelete)
                            {
                                theSpawner.playerList.RemoveAt(i);
                            }
                        indexToDelete.Clear();
                    }
                    for (int i = 0; i < theSpawner.enemyList.Count; ++i)
                    {
                        if (enemyGridSystem.InGridCheck(theSpawner.enemyList[i]))
                        {
                            theSpawner.enemyList[i].parentCube.SetActive(false);
                            detachEventTrigger(theSpawner.enemyList[i].parentCube);
                            destroyed = true;
                        }
                        else
                        {
                            Destroy(theSpawner.enemyList[i].parentCube);
                            indexToDelete.Add(i);
                        }
                    }
                    if (indexToDelete.Count > 0)
                    {
                        indexToDelete.Sort(new SortIntDescending()); //sort ascending order
                        if (indexToDelete[0] == theSpawner.enemyList.Count)
                        {
                            theSpawner.enemyList.Clear();
                        }
                        else
                            foreach (int i in indexToDelete)
                            {
                                theSpawner.enemyList.RemoveAt(i);
                            }
                        indexToDelete.Clear();
                    }
                }

                //Destroy(theGridSystem);
                //Destroy(enemyGridSystem);
                if (ready && destroyed)
                {
                    timer = 0.0f;
                    state = (int)GameState.ATTACK;
                }
            }
            if (!destroyed)
            {
                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    //If the platform is Windows
                    theGridSystem.GameUpdate();
                    enemyGridSystem.GameUpdate();
                }
                else if(Application.platform == RuntimePlatform.Android)
                {
                    //If the platform is android
                    theGridSystem.GameUpdateAndroid();
                    enemyGridSystem.GameUpdateAndroid();
                }
            }
        }

        if (state == (int)GameState.ATTACK)
        {
            UIPanelAnimator.SetBool("UIPanelEnabled", false);
            if (destroyed)
            {
                if (objects.Count > 0)
                {
                    for (int i = 0; i < objects.Count; ++i)
                    {
                        troop = objects[i].GetComponent<TroopAI>();
                        if (troop.activ)
                        {
                            if (troop.team == 1)
                            {
                                //Player 1 reach enemy base, reduce enemy base health
                                if (objects[i].transform.position.y > 2000)
                                {
                                    troop.activ = false;
                                    Player2.GetComponent<HealthSystem>().addHealth(-1);
                                }
                            }
                            if (troop.team == -1)
                            {
                                //Player 2 reach enemy base, reduce enemy base health
                                if (objects[i].transform.position.y < 100)
                                {
                                    troop.activ = false;
                                    Player1.GetComponent<HealthSystem>().addHealth(-1);
                                }
                            }
                        }
                    }
                    for (int i = 0; i < objects.Count;) //Remove dead/not needed troops from scene
                    {
                        troop = objects[i].GetComponent<TroopAI>();
                        if (troop.activ)
                        {
                            ++i;
                        }
                        else
                        {
                            Destroy(objects[i]);
                            objects.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < objects.Count; ++i)
                    {
                        troop = objects[i].GetComponent<TroopAI>();
                        if (troop.activ)
                        {
                            //If the unit reaches its target grid
                            if (Mathf.Abs(troop.targetPos.y - troop.transform.position.y) < 10)
                            {
                                if (troop.team == 1)
                                {
                                    //Set the target grid to grey
                                    enemyGridSystem.SetIsGreyOut(troop.targetIndex);
                                    troop.activ = false;
                                }
                                if (troop.team == -1)
                                {
                                    //Debug.Log("kill");
                                    //Set the target grid to grey
                                    theGridSystem.SetIsGreyOut(troop.targetIndex);
                                    troop.activ = false;
                                }
                            }
                        }
                    }
                    theGridSystem.CheckGreyedGrid();
                    enemyGridSystem.CheckGreyedGrid();
                    for (int i = 0; i < objects.Count; ++i)
                    {
                        troop = objects[i].GetComponent<TroopAI>();
                        if (troop.activ)
                        {
                            if (troop.team == 1)
                            {
                                float dist = 0;
                                for (uint j = 0; j < enemyGridSystem.GridSize; ++j)
                                {
                                    float yDist = enemyGridSystem.grid[j].transform.position.y - troop.originPos.y;
                                    if (Mathf.Abs(enemyGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !enemyGridSystem.IsGreyedOut(j) && yDist > dist)
                                    {
                                        dist = yDist;
                                        troop.targetPos = enemyGridSystem.grid[j].transform.position;
                                        troop.targetIndex = j;
                                    }
                                }
                            }
                            if (troop.team == -1)
                            {
                                float dist = 0;
                                for (uint j = 0; j < theGridSystem.GridSize; ++j)
                                {
                                    float yDist = Mathf.Abs(theGridSystem.grid[j].transform.position.y - troop.originPos.y);
                                    if (Mathf.Abs(theGridSystem.grid[j].transform.position.x - troop.originPos.x) < 10 && !theGridSystem.IsGreyedOut(j) && yDist > dist)
                                    {
                                        dist = yDist;
                                        troop.targetPos = theGridSystem.grid[j].transform.position;
                                        troop.targetIndex = j;
                                    }
                                }
                            }
                            if (troop.type == "Bowmen")
                            {
                                if(melee)
                                {
                                    troop.range = 100;
                                    Debug.Log("chkeck");
                                }
                                else
                                {
                                    troop.range = 300;
                                }
                            }
                            if (side == 0)
                            {
                                troop.attackWidth = 150;
                                troop.attackHeight = troop.range;
                            }
                            else if(side == 1)
                            {
                                troop.attackWidth = 50;
                                troop.attackHeight = troop.range;
                            }
                            else if(side == 2)
                            {
                                troop.attackWidth = 50 * 2;
                                troop.attackHeight = troop.range/2;
                            }
                        }
                    }
                }
            }
            if (objects.Count <= 0 && destroyed)
            {
                if(blockRespawn)
                {
                    for (int i = 0; i < theSpawner.playerList.Count; ++i)
                    {
                        //Debug.Log(theTetrisSpawner.tetrisList[i].parentCube);
                        //Debug.Log("Isa Me again:" + theTetrisSpawner.tetrisList[i].parentCube.name);
                        theSpawner.playerList[i].parentCube.SetActive(true);
                    }
                    for (int i = 0; i < theSpawner.enemyList.Count; ++i)
                    {
                        //Debug.Log("Isa Me again:" + theTetrisSpawner.tetrisList[i].parentCube.name);
                        theSpawner.enemyList[i].parentCube.SetActive(true);
                    }

                }
                else
                {
                    theSpawner.playerList.Clear();
                    theSpawner.enemyList.Clear();
                }
                theSpawner.Start();
                if (!blockRespawn)
                {
                    for (int i = 0; i < enemyGridSystem.GridSize; ++i)
                    {
                        enemyGridSystem.taken[i] = false;
                    }
                    for (int i = 0; i < theGridSystem.GridSize; ++i)
                    {
                        theGridSystem.taken[i] = false;
                    }
                }
                for (int i = 0; i < 3; ++i)
                {
                    enemyGridSystem.check[i] = false;
                }
                //enemyTetrisSpawner.Start();
                Terrain.GetComponent<MainGame>().Start();
                TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
                destroyed = false;
                timer = 0.0f;
                ready = false;
                state = (int)GameState.PLANNING;
            }
        }

        timer += Time.deltaTime;
    }

    public void ReadyButton()
    {
        if(!ready && !destroyed)
        {
            ready = true;
        }
    }
    //public void OnlyFront()
    //{
    //    front = true;
    //}
    //public void Adj()
    //{
    //    front = false;
    //}
    //public void ranged()
    //{
    //    melee = false;
    //}
    //public void Melee()
    //{
    //    melee = true;
    //}

    private class SortIntDescending : IComparer<int>
    {
        int IComparer<int>.Compare(int a, int b) //implement Compare
        {
            if (a > b)
                return -1; //normally greater than = 1
            if (a < b)
                return 1; // normally smaller than = -1
            else
                return 0; // equal
        }
    }
    void detachEventTrigger(GameObject parent)
    {
        Destroy(parent.transform.Find("partOne").GetComponent<EventTrigger>());
        Destroy(parent.transform.Find("partTwo").GetComponent<EventTrigger>());
        Destroy(parent.transform.Find("partThree").GetComponent<EventTrigger>());
        Destroy(parent.transform.Find("partFour").GetComponent<EventTrigger>());
    }
    bool CheckWin()
    {
        if (P1Health.getHealth() > 0 && P2Health.getHealth() > 0)
            return false;
        if (!Gameover)
        {
            Image background = new GameObject().AddComponent<Image>();                                                                                  //Create empty Go with Image element
            background.gameObject.transform.SetParent(Ui.gameObject.transform);                                                                         //Parent to UI canvas
            background.transform.name = "Win/Lose";                                                                                                     //Give it a name
            background.sprite = PaperBG;                                                                                                                //Assign background image
            background.transform.position = Ui.transform.position;                                                                                      //Centralise image
            background.transform.localScale = new Vector2(Ui.gameObject.transform.localScale.x, Ui.gameObject.transform.localScale.x);                  //
            background.rectTransform.sizeDelta = new Vector2(Ui.GetComponent<RectTransform>().rect.width, Ui.GetComponent<RectTransform>().rect.width); //Scale by canvas width

            //Text text = new GameObject().AddComponent<Text>();                                                                                      //Create rmpty Go wtith Text component
            //text.transform.SetParent(background.gameObject.transform);                                                                              //Parent to background
            //text.transform.position = background.gameObject.transform.position + new Vector3(0, 360, 0);                                            //Reposition text placement
            //text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;                                                            //Set font
            //text.rectTransform.localScale = Ui.gameObject.transform.localScale;                                                                     //
            //text.resizeTextForBestFit = true;                                                                                                       //Allow text to resize to fit to rectTranform scaling
            Text text = CreateText(background.gameObject, new Vector3(0, 360, 0));                                                                                          //Create text object
            text.resizeTextForBestFit = true;
            text.resizeTextMaxSize = 150;                                                                                                           //Set maximum size of text
            text.rectTransform.sizeDelta = new Vector2(0.7f * background.rectTransform.sizeDelta.x, 0.2f * background.rectTransform.sizeDelta.y);   //Scale based on background width
            if (P1Health.getHealth() <= 0)                                                                                                          //Choose to set text to lose or win
            {
                text.text = "You Lose";
            }
            else if (P2Health.getHealth() <= 0)
            {
                text.text = "You Win";
            }
            text.transform.name = "Outcome";

            Text currGoldText = CreateText(background.gameObject, new Vector3(0, 200, 0));                                                      //Create text object
            currGoldText.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, 0.5f * text.rectTransform.sizeDelta.y);          //Size from 'text' object
            currGoldText.fontSize = 50;                                                                                                         //Set text size to 50
            currGoldText.text = "Your current Gold balance: " + Player1.GetComponent<GoldSystem>().getGold();                                   //Set text to display
            currGoldText.transform.name = "CurrentBalance";                                                                                     //Give the gameObject a unique name

            Text Earnings = CreateText(background.gameObject, new Vector3(0, 100, 0));                                                          //Create text object
            Earnings.rectTransform.sizeDelta = currGoldText.rectTransform.sizeDelta;                                                            //Size base on currGoldText
            Earnings.fontSize = 50;                                                                                                             //Set text size to 50
            Earnings.text = "Gold earned in this game: " + Player1.GetComponent<InGameCash>().getAmount();                                      //Set text to display
            Earnings.transform.name = "Earnings";                                                                                               //Give the gameObject a unique name

            Player1.GetComponent<InGameCash>().cashoutToGold();                                                                                 //Function call to output to gold

            Text Balance = CreateText(background.gameObject, new Vector3(0, 0, 0));                                                             //Create text object
            Balance.rectTransform.sizeDelta = currGoldText.rectTransform.sizeDelta;                                                             //Size base on currGoldText
            Balance.fontSize = 50;                                                                                                              //Set text size to 50
            Balance.text = "Your final balance: " + Player1.GetComponent<GoldSystem>().getGold();                                                //Set text to display
            Balance.transform.name = "Balance";                                                                                                 //Give the gameObject a unique name

            Gameover = true;                                                                                                                    //To prevent more instatiation of this panel
        }
        return true;
    }
    Text CreateText(GameObject _parent, Vector3 _displacement)
    {
        Text text = new GameObject().AddComponent<Text>();                                                                                      //Create empty Go wtith Text component
        text.transform.SetParent(_parent.transform);                                                                                            //Parent to background
        text.transform.position = _parent.transform.position + _displacement;                                                                   //Reposition text placement
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;                                                            //Set font
        text.rectTransform.localScale = Ui.gameObject.transform.localScale;                                                                     //
        //text.resizeTextForBestFit = true;                                                                                                     //Allow text to resize to fit to rectTranform scaling
        text.color = Color.black;                                                                                                               //Default text colour to black
        return text;
    }
}
