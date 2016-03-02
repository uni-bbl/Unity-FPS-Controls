using UnityEngine;
using System.Collections;

/**
 * While enabled, produces an input vector in response to moving the mouse.
 * Note: the mouse cursor is locked, hidden while this component is enabled. 
 * Disabling the component restores default cursor settings.
 * Add this component to any object to start reading delta values.
 * Read the 'output' value or assign a Vector3Input 'consumer'.
 */
public class MouseDelta : MonoBehaviour {

	const string X = "Mouse X", Y = "Mouse Y";

	[Header("Parameters")]
	public float scale = 0.5f;
	public float inertia = 0.8f;
	public bool invertY = true;

	[Header("Output")]
	public Vector3Input consumer;
	public Vector3 output;

	private Vector3 delta;

	void Update () {

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		delta.x = Input.GetAxis (X); 
		delta.y = Input.GetAxis (Y) * (invertY ? -1f : 1f);
		output = output * inertia + delta * scale * (1f-inertia);
		consumer.AddVector3Input (output);

	}

	void OnDisable(){

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

	}

}