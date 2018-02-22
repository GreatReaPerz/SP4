using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCode_Temp : MonoBehaviour {

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
    private TetrisSpawner_Temp theTetrisSpawner = null;
    private GridSystem_Temp theGridSystem = null;
    private enemyTetrisSpawner_Temp enemyTetrisSpawner = null;
    private enemyGridSystem_Temp enemyGridSystem = null;
    private TroopAI_Temp troop = null;
    private string TerrainName;
    int state;
    public List<GameObject> objects;
    // Use this for initialization
    void Start()
    {
        theTetrisSpawner = GameObject.Find("Spawner").GetComponent<TetrisSpawner_Temp>();
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem_Temp>();
        enemyTetrisSpawner = GameObject.Find("enemySpawner").GetComponent<enemyTetrisSpawner_Temp>();
        enemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem_Temp>();
        timer = 0;
        destroyed = false;
        state = (int)GameState.PLANNING;
        TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
        theGridSystem.Init();
        enemyGridSystem.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == (int)GameState.PLANNING)
        {
            UIPanelAnimator.SetBool("UIPanelEnabled", true);
            if (timer > 10.0f && !destroyed)
            {
                //for (int i = 0; i < theTetrisSpawner.tetrisList.Length; ++i)
                //    Debug.Log("TetriList:"+theTetrisSpawner.tetrisList[i].parentCube.name);
                for (int i = 0; i < theTetrisSpawner.tetrisList.Count; ++i)
                {
                    if (theTetrisSpawner.tetrisList[i].sav)
                    {
                        GameObject newObj;
                        Vector3 hello = theTetrisSpawner.tetrisList[i].partOne.transform.position;
                        hello.z = 100;
                        Debug.Log(theTetrisSpawner.tetrisList[i].troopName);
                        newObj = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj.transform.parent = Ui.transform;
                        troop = newObj.GetComponent<TroopAI_Temp>();
                        troop.team = 1;
    
                        troop.terrainName = TerrainName;
                        objects.Add(newObj);

                        GameObject newObj1;
                        hello = theTetrisSpawner.tetrisList[i].partTwo.transform.position;
                        hello.z = 100;
                        newObj1 = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj1.transform.parent = Ui.transform;
                        troop = newObj1.GetComponent<TroopAI_Temp>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj1);

                        GameObject newObj2;
                        hello = theTetrisSpawner.tetrisList[i].partThree.transform.position;
                        hello.z = 100;
                        newObj2 = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj2.transform.parent = Ui.transform;
                        troop = newObj2.GetComponent<TroopAI_Temp>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj2);

                        GameObject newObj3;
                        hello = theTetrisSpawner.tetrisList[i].partFour.transform.position;
                        hello.z = 100;
                        newObj3 = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj3.transform.parent = Ui.transform;
                        troop = newObj3.GetComponent<TroopAI_Temp>();
                        troop.team = 1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj3);
                    }
                }
                for (int i = 0; i < enemyTetrisSpawner.tetrisList.Count; ++i)
                {
                    if (enemyTetrisSpawner.tetrisList[i].sav)
                    {
                        GameObject newObj;
                        Vector3 hello = enemyTetrisSpawner.tetrisList[i].partOne.transform.position;
                        hello.z = 100;
                        newObj = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj.transform.parent = Ui.transform;
                        troop = newObj.GetComponent<TroopAI_Temp>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj);

                        GameObject newObj1;
                        hello = enemyTetrisSpawner.tetrisList[i].partTwo.transform.position;
                        hello.z = 100;
                        newObj1 = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj1.transform.parent = Ui.transform;
                        troop = newObj1.GetComponent<TroopAI_Temp>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj1);

                        GameObject newObj2;
                        hello = enemyTetrisSpawner.tetrisList[i].partThree.transform.position;
                        hello.z = 100;
                        newObj2 = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj2.transform.parent = Ui.transform;
                        troop = newObj2.GetComponent<TroopAI_Temp>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj2);

                        GameObject newObj3;
                        hello = enemyTetrisSpawner.tetrisList[i].partFour.transform.position;
                        hello.z = 100;
                        newObj3 = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                        newObj3.transform.parent = Ui.transform;
                        troop = newObj3.GetComponent<TroopAI_Temp>();
                        troop.team = -1;
                        troop.terrainName = TerrainName;
                        objects.Add(newObj3);
                    }
                }
                List<int> indexToDelete = new List<int>();                  //Hold indexes to delete
                for (int i = 0; i < theTetrisSpawner.tetrisList.Count; ++i) //Run through 
                {
                    if (theGridSystem.InGridCheck(theTetrisSpawner.tetrisList[i]))
                    {
                        theTetrisSpawner.tetrisList[i].parentCube.SetActive(false);
                        detachEventTrigger(theTetrisSpawner.tetrisList[i].parentCube);
                        destroyed = true;
                    }
                    else
                    {
                        Destroy(theTetrisSpawner.tetrisList[i].parentCube);
                        indexToDelete.Add(i);
                    }
                }
                if (indexToDelete.Count > 0)
                {
                    indexToDelete.Sort(new SortIntDescending()); //sort ascending order
                    if (indexToDelete[0] == theTetrisSpawner.tetrisList.Count)
                    {
                        theTetrisSpawner.tetrisList.Clear();
                    }
                    else
                        foreach (int i in indexToDelete)
                        {
                            theTetrisSpawner.tetrisList.RemoveAt(i);
                        }
                    indexToDelete.Clear();
                }
                for (int i = 0; i < enemyTetrisSpawner.tetrisList.Count; ++i)
                {
                    if (enemyGridSystem.InGridCheck(enemyTetrisSpawner.tetrisList[i]))
                    {
                        enemyTetrisSpawner.tetrisList[i].parentCube.SetActive(false);
                        detachEventTrigger(enemyTetrisSpawner.tetrisList[i].parentCube);
                        destroyed = true;
                    }
                    else
                    {
                        Destroy(enemyTetrisSpawner.tetrisList[i].parentCube);
                        indexToDelete.Add(i);
                    }
                }
                if (indexToDelete.Count > 0)
                {
                    indexToDelete.Sort(new SortIntDescending()); //sort ascending order
                    if (indexToDelete[0] == enemyTetrisSpawner.tetrisList.Count)
                    {
                        enemyTetrisSpawner.tetrisList.Clear();
                    }
                    else
                        foreach (int i in indexToDelete)
                        {
                            enemyTetrisSpawner.tetrisList.RemoveAt(i);
                        }
                    indexToDelete.Clear();
                }
                //Destroy(theGridSystem);
                //Destroy(enemyGridSystem);

                timer = 0.0f;
                state = (int)GameState.ATTACK;
            }
            if (!destroyed)
            {
                theGridSystem.GameUpdate();
                enemyGridSystem.GameUpdate();
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
                        troop = objects[i].GetComponent<TroopAI_Temp>();
                        if (troop.activ)
                        {
                            if (troop.team == 1)
                            {
                                if (objects[i].transform.position.y > 2000)
                                {
                                    troop.activ = false;
                                    Player2.GetComponent<HealthSystem>().addHealth(-1);
                                }
                            }
                            if (troop.team == -1)
                            {
                                if (objects[i].transform.position.y < 100)
                                {
                                    troop.activ = false;
                                    Player1.GetComponent<HealthSystem>().addHealth(-1);
                                }
                            }
                        }
                    }
                    for (int i = 0; i < objects.Count;)
                    {
                        troop = objects[i].GetComponent<TroopAI_Temp>();
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
                    theGridSystem.CheckGreyedGrid();
                    enemyGridSystem.CheckGreyedGrid();
                }
            }
            if (objects.Count <= 0 && destroyed)
            {
                for (int i = 0; i < theTetrisSpawner.tetrisList.Count; ++i)
                {
                    //Debug.Log(theTetrisSpawner.tetrisList[i].parentCube);
                    //Debug.Log("Isa Me again:" + theTetrisSpawner.tetrisList[i].parentCube.name);
                    theTetrisSpawner.tetrisList[i].parentCube.SetActive(true);
                }
                for (int i = 0; i < enemyTetrisSpawner.tetrisList.Count; ++i)
                {
                    //Debug.Log("Isa Me again:" + theTetrisSpawner.tetrisList[i].parentCube.name);
                    enemyTetrisSpawner.tetrisList[i].parentCube.SetActive(true);
                }
                theTetrisSpawner.Start();
                //for (int i = 0; i < enemyGridSystem.GridSize; ++i)
                //{
                //    enemyGridSystem.taken[i] = false;
                //}
                //for (int i = 0; i < theGridSystem.GridSize; ++i)
                //{
                //    theGridSystem.taken[i] = false;
                //}
                enemyTetrisSpawner.Start();
                //enemyGridSystem.Start();
                Terrain.GetComponent<MainGame>().Start();
                TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
                destroyed = false;
                timer = 0.0f;
                state = (int)GameState.PLANNING;
                //for (int i = 0; i < theTetrisSpawner.tetrisList.Count; ++i)
                //    Debug.Log("TetriList:" + theTetrisSpawner.tetrisList[i].parentCube.name);
            }
        }

        timer += 1.0f / 60.0f;
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
