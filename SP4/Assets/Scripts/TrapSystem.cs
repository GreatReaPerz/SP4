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
            mouseEnter.eventID = EventTriggerType.PointerDown;                                                                              //Define trigger type   (Pointer down)
            mouseEnter.callback.AddListener((data)=> { newTrapBut.transform.Find("Text").GetComponent<Text>().text = "My cost"; });         //Add listener to call function/ do something(changes text)
            buttonEV.triggers.Add(mouseEnter);                                                                                              //Add to Event Trigger

            EventTrigger.Entry mouseClick = new EventTrigger.Entry();                                                                       //Create trigger
            mouseClick.eventID = EventTriggerType.PointerClick;                                                                             //Define trigger type   (Pointer click)
            mouseClick.callback.AddListener((data) => { SetTrapToBePlaced(trap.trapPrefab); });                                             //Add listener to call function/ do something(changes text)
            buttonEV.triggers.Add(mouseClick);                                                                                              //Add to Event Trigger

            EventTrigger.Entry mouseExit = new EventTrigger.Entry();                                                                        //Create trigger
            mouseExit.eventID = EventTriggerType.PointerUp;                                                                                 //Define trigger type   (Pointer up)
            mouseExit.callback.AddListener((data) => { newTrapBut.transform.Find("Text").GetComponent<Text>().text = theTrap.getName(); }); //Add listener to call function/ do something(changes text)
            buttonEV.triggers.Add(mouseExit);                                                                                               //Add to Event Trigger

            newTrapBut.transform.position = trapSelectionPanel.transform.position + displacement;                                           //Sets position (+ displacement)
            newTrapBut.transform.SetParent(trapSelectionPanel.transform);                                                                   //Parent to panel
            displacement.x += newTrapBut.GetComponent<RectTransform>().rect.width;                                                          //Increment displacement every iteration
        }
        Button close = trapSelectionPanel.transform.Find("CloseButton").gameObject.GetComponent<Button>();
        EventTrigger closeEV = close.gameObject.AddComponent<EventTrigger>();                                                               //Add EvenTrigger component
        EventTrigger.Entry closeClick = new EventTrigger.Entry();                                                                           //Create trigger
        closeClick.eventID = EventTriggerType.PointerClick;                                                                                 //Define trigger type   (Pointer click)
        closeClick.callback.AddListener((data) => { resetVariables(); });                                                                   //Add listener to call function/ do something(changes text)
        closeEV.triggers.Add(closeClick);                                                                                                   //Add to Event Trigger
    }

    // Update is called once per frame
    void Update()
    {
        switch (myState)
        {
            case executionState.GRID_CHOOSING:
                Vector3 pos = new Vector3(0, 0, 0);
                if (Input.GetMouseButtonDown(0) )                           //if mouse input
                {
                    pos = Input.mousePosition;
                }
                    if( Input.touchCount>0)                                 //if phone input
                {
                    pos = Input.GetTouch(0).position;
                }
                if(pos != new Vector3(0,0,0)){
                    //Instantiate(trapPrefab[0]);
                    trapPos = CheckClickedPosition(pos);                    //Get position for trap to be placed
                    if (trapPos != new Vector3(0, 0, 0))                    //If trap position is (0,0,0), do nothing
                    {
                        myState = executionState.TRAP_CHOOSING;             //Change state
                        trapSelectionPanel.SetActive(true);                 //activate panel to choose trap
                    }
                }
                break;
            case executionState.TRAP_CHOOSING:
                if (trapToBePlaced)                                         //if trapToBePlaced is assigned
                {
                    GameObject trap = Instantiate(trapToBePlaced);          //Creates trap 
                    trap.transform.position = trapPos;                      //sets trap pos to grid pos(where the player clicked)
                    //trap.transform.SetParent(gameCanvas.transform);         
                    trap.GetComponent<Trap>().team = 1;                     //Set which team the trap belongs to to prevent own troop activating it
                    myTraps.Add(trap);                                      //Add to list of existing traps
                    trap.transform.SetParent(this.gameObject.transform);    //Parent to this.gameobject, so there will be no overlay issue
                    resetVariables();                                       //Reset variable to prepare for next trap placement
                }
                break;
        }
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
        myTraps.Sort(new SortTrapNotActiveFirst());             //sorts not active trap first
        while(!myTraps[0].GetComponent<Trap>().isactive)        //while the first element is not active
        {
            if(myTraps.Count ==1)                               //if only 1 trap in list
            {
                Destroy(myTraps[0]);
                myTraps.Clear();
                break;
            }
            Destroy(myTraps[0]);                                //Destroys first trap
            myTraps.RemoveAt(0);                                //Removes the first element
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
