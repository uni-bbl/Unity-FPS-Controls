using UnityEngine;
using System.Collections;

/**
 * Responsible for pushing the avatar in response to input, aka "walking".
 * @integrity
 * There are issues with this class.
 * - It would be better if this were attached to the avatar; as it is, confusing.
 * - Freezing velocity when idle is no good (see 1) and doesn't even belong here 
 * (should be part of 'idle' activity)
 * - (2) Explicit reference to Jumping class.
 */
public class MotionController : Vector2Input {

	public GameObject target;
	public float scale = 1f;
	public Vector2 input = Vector2.zero;
	public float groundThreshold = 0.1f;
	[Tooltip("10m/s ~ 36km/h")]
	public float maxSpeedInMetersPerSecond = 10f;

	[Header("For Debugging")]
	public Vector3 force;

	override public void AddVector2Input(Vector2 value){
		input = value;
	}

	void FixedUpdate () {

		if (target == null)return;
		Rigidbody tbody=target.GetComponentInChildren<Rigidbody> ();
		XBehaviour xb = target.GetComponentInChildren<XBehaviour> ();

		// (1) This forces the RB in place while not providing input.
		// Sometimes undesirable but easiest way to prevent sliding.
		// (2) Disable this behaviour while jumping.
		if (jumping) {
			Debug.DrawLine (position,position+up+transform.forward,Color.magenta);
		}else if (input == Vector2.zero && xb.altitude<=groundThreshold) {
			tbody.velocity = Vector3.zero;
			return;
		}

		float speed = tbody.velocity.magnitude;
		if (speed > maxSpeedInMetersPerSecond) {
			//ebug.Log ("speed clamp");
			return;
		}
		force = target.transform.forward * input.y * scale
			+ target.transform.right * input.x * scale;
		tbody.AddForce (force);

	}

	bool jumping{
		get{ return target.GetComponent<Jumping>()!=null; }
	}
}
