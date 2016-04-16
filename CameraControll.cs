using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CameraControll : MonoBehaviour {
	public GameObject player;
	public float pitchupper;
	public float pitchlower;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"),0,CrossPlatformInputManager.GetAxis("Vertical"));
		transform.position = transform.position + movement;
		Vector3 target = transform.eulerAngles + new Vector3 (-CrossPlatformInputManager.GetAxis ("pitch") * 2, CrossPlatformInputManager.GetAxis ("yaw") * 2, 0);
		if (target.x > 280 || target.x < 80) {
			transform.rotation = Quaternion.Euler (target);
		}
	}

}
