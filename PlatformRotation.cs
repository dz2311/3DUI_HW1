using UnityEngine;
using System.Collections;

public class PlatformRotation : MonoBehaviour {
	private bool select_flag;
	private Camera mycam;
	public BaseControll base_;
	public TeacupControll teacup_;
	public PlaneControll plane_;
	public HorseControll horse_;
	// Use this for initialization
	void Start () {
		select_flag = false;
		mycam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		clickselect ();
		//gameObject.GetComponentInChildren<TeacupControll>().setFlag(select_flag);
		//gameObject.GetComponentInChildren<HorseControll>().setFlag(select_flag);
		deselect ();
		if(!select_flag)
			transform.Rotate (Vector3.up * Time.deltaTime * 15, Space.World);
	}
	void clickselect()
	{
		if (Input.GetMouseButtonDown (0) && select_flag == false) {
			Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.tag == "Platform") {
					if (!base_.getFlag () && !plane_.getFlag () && !horse_.getFlag () && !teacup_.getFlag ()) {	
						Debug.Log ("Platform is -----selected");
						select_flag = true;
						gameObject.GetComponentInChildren<TeacupControll> ().setFlag (true);
						gameObject.GetComponentInChildren<HorseControll> ().setFlag (true);
						GetComponent<AudioSource> ().Play ();
					}
				}
			}
		}
	}
		
	void deselect()
	{
		if (Input.GetMouseButtonDown (1)&&select_flag==true) {
			Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.tag == "Platform")
				{
					
					select_flag = false;

					gameObject.GetComponentInChildren<TeacupControll>().setFlag(false);
					gameObject.GetComponentInChildren<HorseControll>().setFlag(false);
				} 
				Debug.Log ("Platform is deselected");

				//this.gameObject.GetComponentInChildren<PlaneControll>().setFlag(false);*/ 
			}
		}
	}
	public void setFlag(bool res)
	{
		select_flag = res;
	}
	public bool getFlag()
	{
		return select_flag;
	}

}
