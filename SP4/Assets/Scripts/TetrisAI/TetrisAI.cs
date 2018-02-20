using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

class TetrisMove: TetrisAIBase{

	TetrisCube theObject = new TetrisCube();
	Vector2 moveSpeed = new Vector2();
	public TetrisMove(string _stateID,TetrisCube _object)
	{
		stateID = _stateID;
		theObject = _object;
		moveSpeed.Set (0, 100);
	}

	// Use this for initialization
	public override void Enter () {

	}

	// Update is called once per frame
	public override void Update () {
		theObject.partOne.MovePosition(theObject.partOne.position + moveSpeed * Time.deltaTime);
		theObject.partTwo.MovePosition(theObject.partTwo.position + moveSpeed * Time.deltaTime);
		theObject.partThree.MovePosition(theObject.partThree.position + moveSpeed * Time.deltaTime);
		theObject.partFour.MovePosition(theObject.partFour.position + moveSpeed * Time.deltaTime);

	}
	public override void Exit(){

	}
}
