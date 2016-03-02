using UnityEngine;
using System.Collections;

/**
 * A script which ensures that the up vector is always pointing up, without
 * modifying orientation.
 */
public class KeepUpright : MonoBehaviour {

	void Update () {

		Vector3 u = transform.forward;
		u.y = 0f;
		transform.forward = u;

	}
}
