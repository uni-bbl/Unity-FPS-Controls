using UnityEngine;
using System.Collections;

/**
 * Rotates an avatar (and child camera if any) in response to AddVector3Input().
 * 
 * Notes:
 * - can accumulate several input sources (for example, mouse and keyboard) 
 * within the same frame.
 * - alignWithHorizontalPlane is useful if you want to restore horizontality, 
 * for example after disabling game controls.
 */
public class RotationController : Vector3Input {

	public GameObject target;
	public float scale = 1f;
	public float minPitch = -60;
	public float maxPitch = 60;
	
	[Header("For Debugging")]
	public Vector3 input = Vector3.zero;
	public Vector3 lastInput = Vector3.zero;
	public float pitchValue = 0f;

	[Header("Rotate back to horizontal")]
	public bool alignWithHorizontalPlane = false;
	public float maxAutoRotationPerSecond = 10f;

	// <Vector3Input> -----------------------------------------

	override public void AddVector3Input(Vector3 value){
		
		input += value;

	}

	// Life-cycle ---------------------------------------------

	// Update pitch (only for displaying pitch value in inspector)
	void Update(){
		
		if (target == null)return;
		pitchValue = pitch;
		
	}

	void FixedUpdate () {

		if (target == null)return;
		Vector3 O = target.transform.position;
		target.transform.RotateAround (O, Vector3.up, scale * lastInput.x);
		target.transform.RotateAround (O, target.transform.right, scale * lastInput.y);
		if (pitch > maxPitch) pitch = maxPitch;
		if (pitch < minPitch) pitch = minPitch;
		if (alignWithHorizontalPlane) {
			float maxAngle = maxAutoRotationPerSecond*Time.deltaTime;
			Vector3 forward = target.transform.forward;
			Vector3 planar = forward; planar.y=0f; planar.Normalize();
			target.transform.forward = Vector3.RotateTowards(forward,planar,maxAngle*Mathf.Deg2Rad,1f);
		}

	}

	// Clear accumulated input otherwise it would 'leak' into the next frame.
	void LateUpdate(){

		if (target == null)return;
		lastInput = input;
		input = Vector3.zero;

	}

	// Implementation -----------------------------------------------------------

	float pitch{
		get{
			Vector3 forward = target.transform.forward;
			Vector3 planar = forward; planar.y=0f; planar.Normalize();
			float angle =Vector3.Angle(forward,planar);
			return forward.y>0f ? angle : -angle;
		}
		set{
			Vector3 O = target.transform.position;
			Vector3 forward = target.transform.forward;
			Vector3 planar = forward; planar.y=0f; planar.Normalize();
			float delta = pitch-value;
			target.transform.RotateAround (O, target.transform.right, delta);
		}
	}

}
