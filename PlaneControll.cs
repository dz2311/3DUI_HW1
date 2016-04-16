using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlaneControll : MonoBehaviour {
	private bool select_flag;
	private float planespeed;
	private float default_speed;
	private float x_, prevx;
	private float y_, prevy;
	private float z_, prevz;
	private GameObject Planepanel;
	private Vector3 plane_vec;
	private Vector3 rotate_vec;
	private Vector3 originPoint;
	private Camera mycam;
	private GameObject report_text;
	public BaseControll base_;
	public PlatformRotation platform_;
	public TeacupControll teacup_;
	public HorseControll horse_;
	//private Text txt;
	// Use this for initialization
	void Start () {
		originPoint = transform.localPosition;
		//originPoint = new Vector3(0,0,0);
		select_flag = false;
		default_speed = 5*Time.deltaTime;
		planespeed = default_speed;
		Planepanel = GameObject.FindWithTag ("PlanePanel");
		Planepanel.SetActive (false);
		x_ = -1;
		prevx = x_;
		y_ = 0;
		prevy = y_;
		z_ = 0;
		prevz = z_;
		rotate_vec = new Vector3 (0, 0, 0);
		select_flag = false;
		mycam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		//txt = gameObject.GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
			
		plane_vec = gameObject.transform.localPosition;
			clickselect ();
			deselect ();
		if (!Planepanel.activeSelf) {
			if(!select_flag)
				transform.Translate (x_ * default_speed, y_ * default_speed, z_ * default_speed);
			} else {
				transform.Translate (x_ * planespeed, y_ * planespeed, z_ * planespeed);
			rotate_vec = new Vector3 (x_, y_, z_);
			transform.rotation = Quaternion.FromToRotation (new Vector3 (prevx, prevy, prevz), rotate_vec);
			}

			ArriveBoundary ();
	
	}
	public void PlaneDirX(string s)
	{
		Debug.Log ("The x direction is " + s);
		prevx = x_;
		x_ = float.Parse (s);
	}
	public void PlaneDirY(string s)
	{
		Debug.Log ("The y direction is " + s);
		prevy = y_;
		y_ = float.Parse (s);
	}
	public void PlaneDirZ(string s)
	{
		Debug.Log ("The z direction is " + s);
		prevz = z_;
		z_ = float.Parse (s);

	}
	public void PlaneSpeed(string s)
	{
		Debug.Log ("The speed is " + s);
		planespeed = float.Parse (s)*Time.deltaTime;
	}
	void clickselect()
	{
		if (Input.GetMouseButtonDown (0)&&select_flag==false) {
			Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.tag == "Plane") {
					if (!base_.getFlag () && !horse_.getFlag () && !teacup_.getFlag () && !platform_.getFlag ()) {
						select_flag = true;
						GetComponent<AudioSource> ().Play ();
						Planepanel.SetActive (true);
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
				if (hit.transform.gameObject.tag == "Plane")
				{
					select_flag = false;
					Planepanel.SetActive (false);
				} 
				Debug.Log ("Plane is deselected");
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
	private void ArriveBoundary()
	{
		if (plane_vec.x < -5||plane_vec.x > 5) {
			//transform.position = new Vector3(1f, 2f, 1f);
			transform.localPosition = originPoint;
			//txt.enabled = true;
			//txt.text = "The plane exceeds boundary x";
			Debug.Log ("boundary x out");
		}
		if (plane_vec.z > 5 || plane_vec.z < -5) {
			//transform.position = new Vector3(1f, 2f, 1f);
			transform.localPosition = originPoint;
			//txt.enabled = true;
			//txt.text = "The plane exceeds boundary z";
			Debug.Log ("boundary z out");
		}
		if (plane_vec.y > 6.7 || plane_vec.y < 1.5) {
			//transform.position = new Vector3(1f, 2f, 1f);
			transform.localPosition = originPoint;
			//txt.enabled = true;
			//txt.text = "The plane exceeds boundary y";
			Debug.Log ("boundary y out");
		}
			
	}
}