using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class BaseControll : MonoBehaviour {
	private bool select_flag;
	private Camera mycam;
	public EventSystem eventSystem;
	//private LayerMask Raylayer;
	private int layerMask; 
	//private int calayer;
	//private Material mat;
	//public AudioClip clickAudio;
	// Use this for initialization
	void Start () {
		//mat = GetComponent<Renderer>().material;
		select_flag = false;
		mycam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		//Raylayer = 1<<(LayerMask.NameToLayer("touchInputMask"));
		layerMask = 1 << 11;
		layerMask = ~layerMask;
		//calayer = 1 << Raylayer;
	}
	
	// Update is called once per frame
	void Update () {
		clickselect ();
		deselect ();
	}
	public bool getFlag()
	{
		return select_flag;
	}
	void clickselect()
	{
		if (Input.GetMouseButtonDown (0) && select_flag == false) {
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			} else {
				Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100.0f, layerMask)) {
					if (hit.transform.gameObject.tag == "Base") {
						{
							if (!gameObject.GetComponentInChildren<TeacupControll> ().getFlag () && !gameObject.GetComponentInChildren<HorseControll> ().getFlag () && !gameObject.GetComponentInChildren<PlatformRotation> ().getFlag () && !gameObject.GetComponentInChildren<PlaneControll> ().getFlag ()) {
								select_flag = true;
								GetComponent<AudioSource> ().Play ();
								gameObject.GetComponentInChildren<TeacupControll> ().setFlag (true);
								gameObject.GetComponentInChildren<HorseControll> ().setFlag (true);
								gameObject.GetComponentInChildren<PlatformRotation> ().setFlag (true);
								gameObject.GetComponentInChildren<PlaneControll> ().setFlag (true);
							}
						}
					} 
				}

			}
		}

		//this.gameObject.GetComponentInChildren<PlaneControll>().setFlag(true);*/
	}
	void deselect()
	{
		if (Input.GetMouseButtonDown (1) && select_flag == true) {
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			} else {
				Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100.0f, layerMask)) {
					if (hit.transform.gameObject.tag == "Base") {
						select_flag = false;
						gameObject.GetComponentInChildren<TeacupControll> ().setFlag (false);
						gameObject.GetComponentInChildren<HorseControll> ().setFlag (false);
						gameObject.GetComponentInChildren<PlatformRotation> ().setFlag (false);
						gameObject.GetComponentInChildren<PlaneControll> ().setFlag (false);
					} 
					Debug.Log ("Base is deselected");

					//this.gameObject.GetComponentInChildren<PlaneControll>().setFlag(false);*/ 
				}

			}
		}
}
}