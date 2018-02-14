using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCube : MonoBehaviour {

	[SerializeField]
	private Rigidbody2D btmLeft;
	[SerializeField]
	private Rigidbody2D btmRight;
	[SerializeField]
	private Rigidbody2D topLeft;
	[SerializeField]
	private Rigidbody2D topRight;

	private Hashtable tetrisList = new Hashtable();
	FixedJoint2D container = new FixedJoint2D();
	private Rigidbody2D bottomRight = new Rigidbody2D ();

    public bool isMoving = false;

    // Use this for initialization
    void Start () {
		container = btmLeft.GetComponent<FixedJoint2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DragbtmLeft()
	{
		btmLeft.MovePosition (Input.mousePosition);
        isMoving = true;
	}

	public void DragbtmRight()
	{
		btmRight.MovePosition (Input.mousePosition);
	}

	public void DragtopLeft()
	{
		topLeft.MovePosition(Input.mousePosition);
	}

	public void DragtopRight()
	{
		topRight.MovePosition (Input.mousePosition);
	}
		
	public void AddTetrisCube(int key, GameObject value)
	{
		tetrisList.Add (key,value);
	}

	public object GetTetrisCube(int key)
	{
		if (tetrisList.ContainsKey (key))
			return tetrisList [key];
		else
			return null;
	}

}
