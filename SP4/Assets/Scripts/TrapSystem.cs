using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrapSystem : MonoBehaviour {

    public List<GameObject> myTraps;

    [SerializeField]
    GameObject[] trapPrefab;

    [SerializeField]
    Button buttonPrefab;        //To generate button(from prefab)

    [SerializeField]
    GameObject trapSelectionPanel;

    enum executionState
    {
        GRID_CHOOSING,
        TRAP_CHOOSING,
    }
    GridSystem theGridSystem;
    enemyGridSystem theEnemyGridSystem;
    GameObject gameCanvas;
    executionState myState = executionState.GRID_CHOOSING;
    Vector3 trapPos = new Vector3(0, 0, 0);
    GameObject trapToBePlaced;

    List<Button> trapButtons;
    // Use this for initialization
    void Start()
    {
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        Debug.Assert(theGridSystem != null);
        theEnemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();
        Debug.Assert(theEnemyGridSystem != null);
        gameCanvas = GameObject.Find("GameCanvas");
        Debug.Assert(gameCanvas != null);
        //trapSelectionPanel = GameObject.Find("TrapSelectionPanel");
        //Debug.Assert(trapSelectionPanel != null);

        GameCode theGameCode = GameObject.Find("EventSystem").GetComponent<GameCode>();
        Vector3 displacement = new Vector3(-buttonPrefab.GetComponent<RectTransform>().rect.width, 0, 0);
        foreach(GameCode.TrapTypes trap in theGameCode.typesOfTraps)
        {
            Button newTrapBut = Instantiate(buttonPrefab);                                                                                  //Creates Button obj
            Trap theTrap = trap.trapPrefab.GetComponent<Trap>();                                                                            //Get reference to trap object
            newTrapBut.transform.Find("Image").GetComponent<Image>().sprite = theTrap.getSprite();                                          //Sets button's Image to trap's image
            newTrapBut.transform.Find("Text").GetComponent<Text>().text = theTrap.getName();                                                //Sets button's text to trap's name
            EventTrigger buttonEV = newTrapBut.gameObject.AddComponent<EventTrigger>();                                                     //Add EvenTrigger component

            EventTrigger.Entry mouseEnter = new EventTrigger.Entry();                                                                       //Create trigger
            mouseEnter.eventID = EventTriggerType.PointerEnter;                                                                             //Define trigger type   (Pointer Enter)
            mouseEnter.callback.AddListener((data)=> { newTrapBut.transform.Find("Text").GetComponent<Text>().text = "My cost"; });         //Add listener to call function/ do something(changes text)
            buttonEV.triggers.Add(mouseEnter);                                                                                              //Add to Event Trigger

            EventTrigger.Entry mouseClick = new EventTrigger.Entry();                                                                       //Create trigger
            mouseClick.eventID = EventTriggerType.PointerClick;                                                                             //Define trigger type   (Pointer click)
            mouseClick.callback.AddListener((data) => { SetTrapToBePlaced(trap.trapPrefab); });                                             //Add listener to call function/ do something(changes text)
            buttonEV.triggers.Add(mouseClick);                                                                                              //Add to Event Trigger

            EventTrigger.Entry mouseExit = new EventTrigger.Entry();                                                                        //Create trigger
            mouseExit.eventID = EventTriggerType.PointerExit;                                                                               //Define trigger type   (Pointer Exit)
            mouseExit.callback.AddListener((data) => { newTrapBut.transform.Find("Text").GetComponent<Text>().text = theTrap.getName(); }); //Add listener to call function/ do something(changes text)
            buttonEV.triggers.Add(mouseExit);                                                                                               //Add to Event Trigger

            newTrapBut.transform.position = trapSelectionPanel.transform.position + displacement;                                           //Sets position (+ displacement)
            newTrapBut.transform.SetParent(trapSelectionPanel.transform);                                                                   //Parent to panel
            displacement.x += newTrapBut.GetComponent<RectTransform>().rect.width;                                                          //Increment displacement every iteration
        }
        Button close = trapSelectionPanel.transform.Find("CloseButton").gameObject.GetComponent<Button>();
        EventTrigger closeEV = close.gameObject.AddComponent<EventTrigger>();                                                              //Add EvenTrigger component
        EventTrigger.Entry closeClick = new EventTrigger.Entry();                                                                           //Create trigger
        closeClick.eventID = EventTriggerType.PointerClick;                                                                                 //Define trigger type   (Pointer click)
        closeClick.callback.AddListener((data) => { resetVariables(); });                                                 //Add listener to call function/ do something(changes text)
        closeEV.triggers.Add(closeClick);                                                                                                  //Add to Event Trigger

        //if (trapPrefab.Length > 0)
        //{
        //    GameObject newTrap = Instantiate(trapPrefab[0]);
        //    newTrap.transform.position = theGridSystem.grid[1].transform.position;
        //    newTrap.transform.SetParent(GameObject.Find("GameCanvas").transform);
        //    myTraps.Add(newTrap);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if(myTraps.Count==1)
        //{
        //    if (!myTraps[0].GetComponent<Trap>().isactive)
        //        myTraps.Clear();
        //}
        //else
        //{
        //    myTraps.Sort(new SortTrapActiveFirst());
        //    myTraps.
        //}
        switch (myState)
        {
            case executionState.GRID_CHOOSING:
                if (Input.GetMouseButtonDown(0))
                {
                    //Instantiate(trapPrefab[0]);
                    trapPos = CheckClickedPosition(Input.mousePosition);    //Get position for trap to be placed
                    if (trapPos != new Vector3(0, 0, 0))                    //If trap position is (0,0,0), do nothing
                    {
                        myState = executionState.TRAP_CHOOSING;             //Change state
                        trapSelectionPanel.SetActive(true);                 //activate panel to choose trap
                    }
                }
                break;
            case executionState.TRAP_CHOOSING:
                if (trapToBePlaced)
                {
                    GameObject trap = Instantiate(trapToBePlaced);
                    trap.transform.position = trapPos;
                    trap.transform.SetParent(gameCanvas.transform);
                    trap.GetComponent<Trap>().team = 1;
                    myTraps.Add(trap);
                    resetVariables();
                }
                break;
        }
        //cleanUpTraps();
    }

    Vector3 CheckClickedPosition(Vector3 _mousePos)
    {
        if (theGridSystem)                                                                                          //If theGridSystem is not null
        {
            for(int i=0; i<theGridSystem.grid.Length; ++i)                                                          //For each grid(images)
            {
                Image element = theGridSystem.grid[i];
                if(Mathf.Abs((_mousePos - element.transform.position).magnitude) < 50 && !theGridSystem.taken[i])   //If the click position is within the grid
                {
                    return element.transform.position;                                                              //return the position of the grid
                }
            }
        }
        return new Vector3(0, 0, 0);
    }

    void resetVariables()
    {
        trapPos = new Vector3(0, 0, 0);
        myState = executionState.GRID_CHOOSING;
        trapSelectionPanel.SetActive(false);
        trapToBePlaced = null;
    }

    public void SetTrapToBePlaced(GameObject _theTrap)
    {
        trapToBePlaced = _theTrap;
    }

    public void cleanUpTraps()
    {
        if (myTraps.Count == 0)
            return;
        myTraps.Sort(new SortTrapNotActiveFirst());
        while(!myTraps[0].GetComponent<Trap>().isactive)
        {
            if(myTraps.Count ==1)
            {
                Destroy(myTraps[0]);
                myTraps.Clear();
                break;
            }
            Destroy(myTraps[0]);
            myTraps.RemoveAt(0);
        }
    }
    private class SortTrapNotActiveFirst : IComparer<GameObject>
    {
        int IComparer<GameObject>.Compare(GameObject a, GameObject b) //implement Compare
        {
            bool aActive = a.GetComponent<Trap>().isactive;
            bool bActive = b.GetComponent<Trap>().isactive;
            if (aActive && !bActive)
                return -1;
            else if (!aActive && bActive)
                return 1;
            else
                return 0; // equal
        }
    }
}
