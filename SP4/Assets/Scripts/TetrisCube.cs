using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCube {
		FixedJoint2D container = new FixedJoint2D();
	//container = btmLeft.GetComponent<FixedJoint2D> ();
	public Rigidbody2D btmLeft = new Rigidbody2D ();
	public Rigidbody2D btmRight = new Rigidbody2D ();
	public Rigidbody2D topLeft = new Rigidbody2D ();
	public Rigidbody2D topRight = new Rigidbody2D ();

    public bool sav = false;
    public bool isMoving = false;
    public bool returning = false;
    public string Whatisbeingmoved = "";
    public Vector3 origin;
    public GameObject parentCube;

	public TetrisAIManager StateMachine = new TetrisAIManager();

	// Use this for initialization
	public TetrisCube(){
		StateMachine.Start ();
	}
		
	public void DragbtmLeft()
	{
		btmLeft.MovePosition (Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "btmLeft";
	}

	public void DragbtmRight()
	{
		btmRight.MovePosition (Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "btmRight";
    }

	public void DragtopLeft()
	{
		topLeft.MovePosition(Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "topLeft";
    }

	public void DragtopRight()
	{
		topRight.MovePosition (Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "topRight";
    }
		
	public void setTheCubes(Rigidbody2D _btmLeft,Rigidbody2D _btmRight,Rigidbody2D _topLeft, Rigidbody2D _topRight)
	{
		btmLeft = _btmLeft;
		btmRight = _btmRight;
		topLeft = _topLeft;
		topRight = _topRight;
	}

}
