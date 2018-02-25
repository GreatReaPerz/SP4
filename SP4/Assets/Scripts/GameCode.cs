using System.Collections;
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


    public bool ready = false;
    public bool melee = true;
    public bool front = true;
    public bool blockRespawn = false;

    private string TerrainName;
    int state;
    public List<GameObject> objects;

    [System.Serializable]
    public struct TrapTypes
    {
        public string name;
        public Sprite texture;
    }
    public List<TrapTypes> typesOfTraps; //Defines the individual kind of traps that will exist in game

    // Use this for initialization
    void Start () {
        theSpawner = GameObject.Find("EventSystem").GetComponent<TetrisSpawner>();
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        enemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();
        timer = 0;
        destroyed = false;
        state = (int)GameState.PLANNING;
        TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
    }

    // Update is called once per frame
    void Update() {
        if (state == (int)GameState.PLANNING)
        {
            UIPanelAnimator.SetBool("UIPanelEnabled", true);
            //if (timer > 10.0f && !destroyed)
            //{
            //    ready = true;
            //}
            if (ready && !destroyed)
            {
                for (int i = 0; i < theSpawner.playerList.Count; ++i)
                {
                    if (theSpawner.playerList[i].sav)
                    {
                        GameObject newObj;
                        Vector3 hello = theSpawner.playerList[i].partOne.transform.position;
                        hello.z = 100;
                        newObj = (GameObject)Instantiate(GameObject.Find(theSpawner.playerList[i].troopName), hello, Quaternion.identity);
                        newObj.transform.parent = Ui.transform;
                        troop = newObj.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
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

                        objects.Add(newObj);

                        GameObject newObj1;
                        hello = theSpawner.playerList[i].partTwo.transform.position;
                        hello.z = 100;
                        newObj1 = (GameObject)Instantiate(GameObject.Find(theSpawner.playerList[i].troopName), hello, Quaternion.identity);
                        newObj1.transform.parent = Ui.transform;
                        troop = newObj1.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        dist = 0;
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
                        objects.Add(newObj1);

                        GameObject newObj2;
                        hello = theSpawner.playerList[i].partThree.transform.position;
                        hello.z = 100;
                        newObj2 = (GameObject)Instantiate(GameObject.Find(theSpawner.playerList[i].troopName), hello, Quaternion.identity);
                        newObj2.transform.parent = Ui.transform;
                        troop = newObj2.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        dist = 0;
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
                        objects.Add(newObj2);

                        GameObject newObj3;
                        hello = theSpawner.playerList[i].partFour.transform.position;
                        hello.z = 100;
                        newObj3 = (GameObject)Instantiate(GameObject.Find(theSpawner.playerList[i].troopName), hello, Quaternion.identity);
                        newObj3.transform.parent = Ui.transform;
                        troop = newObj3.GetComponent<TroopAI>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        dist = 0;
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
                        troop = newObj.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
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
                        objects.Add(newObj);

                        GameObject newObj1;
                        hello = theSpawner.enemyList[i].partTwo.transform.position;
                        hello.z = 100;
                        newObj1 = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj1.transform.parent = Ui.transform;
                        troop = newObj1.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        dist = 0;
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
                        objects.Add(newObj1);

                        GameObject newObj2;
                        hello = theSpawner.enemyList[i].partThree.transform.position;
                        hello.z = 100;
                        newObj2 = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj2.transform.parent = Ui.transform;
                        troop = newObj2.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        dist = 0;
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
                        objects.Add(newObj2);

                        GameObject newObj3;
                        hello = theSpawner.enemyList[i].partFour.transform.position;
                        hello.z = 100;
                        newObj3 = (GameObject)Instantiate(GameObject.Find(theSpawner.enemyList[i].troopName), hello, Quaternion.identity);
                        newObj3.transform.parent = Ui.transform;
                        troop = newObj3.GetComponent<TroopAI>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        dist = 0;
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
                            if(troop.type == "Bowmen")
                            {
                                if(melee)
                                {
                                    troop.range = 100;
                                }
                                else
                                {
                                    troop.range = 300;
                                }
                            }
                            if (front)
                            {
                                troop.attackWidth = 50;
                            }
                            else
                            {
                                troop.attackWidth = 150;
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
    public void OnlyFront()
    {
        front = true;
    }
    public void Adj()
    {
        front = false;
    }
    public void ranged()
    {
        melee = false;
    }
    public void Melee()
    {
        melee = true;
    }

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
}
