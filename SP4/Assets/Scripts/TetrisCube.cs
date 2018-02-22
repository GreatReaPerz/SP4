using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCube {
		FixedJoint2D container = new FixedJoint2D();
	//container = btmLeft.GetComponent<FixedJoint2D> ();
	public Rigidbody2D partOne = new Rigidbody2D ();
	public Rigidbody2D partTwo = new Rigidbody2D ();
	public Rigidbody2D partThree = new Rigidbody2D ();
	public Rigidbody2D partFour = new Rigidbody2D ();

    public bool sav = false;
    public bool isMoving = false;
    public bool returning = false;
    public string troopName = "";
    public string Whatisbeingmoved = "";
    public Vector3 origin;
    public GameObject parentCube;
    public bool movable = true;

	public enum objectType
	{
		TETRIS_4X4,
		TETRIS_T,
		TETRIS_Z,
		TETRIS_L,
	}

	public objectType thisType;

	// Use this for initialization
	public TetrisCube(){

	}
		
/*	public void DragbtmLeft()
	{
		partOne.MovePosition (Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "btmLeft";
	}

	public void DragbtmRight()
	{
		partTwo.MovePosition (Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "btmRight";
    }

	public void DragtopLeft()
	{
		partThree.MovePosition(Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "topLeft";
    }

	public void DragtopRight()
	{
		partFour.MovePosition (Input.mousePosition);
        isMoving = true;
        returning = false;
        Whatisbeingmoved = "topRight";
    }*/

	public void DragObject(Rigidbody2D tetrisPart)
	{
		if (tetrisPart == partOne) {
			partOne.MovePosition (Input.mousePosition);
			isMoving = true;
			returning = false;
			Whatisbeingmoved = "partOne";
		} else if (tetrisPart ==  partTwo) {
			partTwo.MovePosition (Input.mousePosition);
			isMoving = true;
			returning = false;
			Whatisbeingmoved = "partTwo";
		} else if (tetrisPart == partFour) {
			partFour.MovePosition (Input.mousePosition);
			isMoving = true;
			returning = false;
			Whatisbeingmoved = "partThree";
		}
		else if(tetrisPart ==  partThree){
			partThree.MovePosition(Input.mousePosition);
			isMoving = true;
			returning = false;
			Whatisbeingmoved = "partFour";
		}
	}
		
	public void setTheCubes(Rigidbody2D _partOne,Rigidbody2D _partTwo,Rigidbody2D _partThree, Rigidbody2D _partFour)
	{
		partOne = _partOne;
		partTwo = _partTwo;
		partThree = _partThree;
		partFour = _partFour;
	}

	public void setTheObjectType(objectType theType)
	{
		thisType = theType;
	}
}
