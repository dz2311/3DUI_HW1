using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeacupControll : MonoBehaviour {
	//public GameObject tcControll; 
	//private TCControll tccontroll;
	//public  GameObject otherobj;
	private Camera mycam;
	private bool select_flag = false;
	private InputField tccontroll;
	public float tcspeed;
	public Vector3 tcdirection;
	public float x_,y_,z_;
	private float defaultx, defaulty, defaultz, defaultspeed;
	public float tcoffset;
	private GameObject TCpanel;
	public BaseControll base_;
	public PlatformRotation platform_;
	public PlaneControll plane_;
	public HorseControll horse_;
	// Use this for initialization
	void Start () {
		TCpanel = GameObject.FindWithTag("TCPanel");
		tcspeed = 0;
		defaultspeed = 50;
		tcdirection = new Vector3 (0,1,0);
		tcoffset = 0.0f;
		x_ = 0.0f;
		defaultx = x_;
		y_ = 1.0f;
		defaulty = y_;
		z_ = 0.0f;
		defaultz = z_;
		select_flag = false;
		TCpanel.SetActive (false);
		mycam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}

	public void TCInputSpeed(string s)
	{
		Debug.Log ("The speed is " + s);
		tcspeed = float.Parse (s);
	}
	public void TCInputDirX(string s)
	{
		Debug.Log ("The vector.x is " + s);
		x_ = float.Parse (s);
	}
	public void TCInputDirY(string s)
	{
		Debug.Log ("The vector.y is " + s);
		y_ = float.Parse (s);
	}
	public void TCInputDirZ(string s)
	{
		Debug.Log ("The vector.z is " + s);
		z_ = float.Parse (s);
	}
	public void TCInputOffset(string s)
	{
		Debug.Log ("The offset is " + s);
		tcoffset = float.Parse (s);
	}


	// Update is called once per frame
	void Update () {
		//speed = tcControll.SendMessage ("getTCSpeed");
		//tccontroll = GetComponent<TCControll>();
		tcdirection = new Vector3 (x_+tcoffset, y_, z_+tcoffset);
		clickselect ();
		deselect ();
		if(!select_flag&&!TCpanel.activeSelf)
			transform.Rotate (new Vector3(defaultx, defaulty, defaultz) * Time.deltaTime * defaultspeed, Space.Self);
		
		if(TCpanel.activeSelf)
			transform.Rotate (tcdirection * Time.deltaTime * tcspeed, Space.Self);
		

	}
	void clickselect()
	{
		if (Input.GetMouseButtonDown (0)&&select_flag==false) {
			Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.tag == "Teacup") {
					if (!base_.getFlag () && !plane_.getFlag () && !horse_.getFlag () && !platform_.getFlag ()) {
						select_flag = true;
						GetComponent<AudioSource> ().Play ();
						TCpanel.SetActive (true);
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
				if (hit.transform.gameObject.tag == "Teacup")
				{
					select_flag = false;
					TCpanel.SetActive (false);
					x_ = 0.0f;
					z_ = 0.0f;
					y_ = 1.0f;

					tcoffset = 0.0f;
				} 
				Debug.Log ("teacup is deselected");
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