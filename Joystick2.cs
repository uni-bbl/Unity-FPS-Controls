using UnityEngine;
using System.Collections;

public class Joystick2 : MonoBehaviour {

	public string description;
	public Vector2Input output;

	void Update () {
		Vector2 outvalue = new Vector2 ( Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical") );
		output.AddVector2Input (outvalue);
	}

}
