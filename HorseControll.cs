using UnityEngine;
using System.Collections;
using UnityEngine;

public class HorseControll : MonoBehaviour {
	private Camera mycam;
	private Vector3 origin_point;
	private float radian = 0;
	private float de_radian = 0;
	private float de_dy = 0;
	private float per_ra = 0.03f;
	private float radius = 0.2f;
	public float gallopingSpeed;
	public float gallopingRange; 
	private bool select_flag;
	public GameObject Gallopingpanel;
	public BaseControll base_;
	public PlatformRotation platform_;
	public TeacupControll teacup_;
	public PlaneControll plane_;
	// Use this for initialization
	void Start () {
		mycam = GameObject.FindWithTag("MainCamera").GetComponent <Camera> ();
		Gallopingpanel = GameObject.FindWithTag ("GallopingPanel");
		origin_point = transform.localPosition;
		gallopingRange = 0f;
		gallopingSpeed = 0f;
		select_flag = false;
		Gallopingpanel.SetActive (false);

	}
	public void GallopingSpeed(string s)
	{
		Debug.Log ("The speed is " + s);
		gallopingSpeed = float.Parse (s);
	}
	public void GallopingRange(string s)
	{
		Debug.Log ("The speed is" + s);
		gallopingRange = float.Parse (s);
	}
	// Update is called once per frame
	void Update () {
		clickselect ();
		deselect ();
		if (!select_flag&&!Gallopingpanel.activeSelf) {
			de_radian += per_ra;
			float de_dy = Mathf.Cos (de_radian) * radius;
			de_dy += radius;
			transform.localPosition = origin_point + new Vector3 (0, de_dy, 0);
		}
		if (Gallopingpanel.activeSelf) {
			radian += gallopingSpeed;
			/*
		float dy = Mathf.Cos (radian) * radius;
		dy += 0.2f;*/
			float dy = Mathf.Cos (radian) * gallopingRange;
			dy += gallopingRange;
			transform.localPosition = origin_point + new Vector3 (0, dy, 0);
		}
	}
	void clickselect()
	{
		if (Input.GetMouseButtonDown (0)&&select_flag==false) {
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			} else {
				Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform.gameObject.tag == "CarouselHorse") {
						if(!base_.getFlag()&&!plane_.getFlag()&&!teacup_.getFlag()&&!platform_.getFlag()){
						select_flag = true;
						GetComponent<AudioSource> ().Play ();
						Gallopingpanel.SetActive (true);
						Debug.Log ("CarouselHorse is selected");
						}
					} 
					
				}
			}
		}
	}
	void deselect()
	{
		if (Input.GetMouseButtonDown (1)&&select_flag==true) {
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			} else {
				Ray ray = mycam.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.transform.gameObject.tag == "CarouselHorse") {
						select_flag = false;
						Gallopingpanel.SetActive (false);
					} 
					Debug.Log ("Horse and boy is deselected");
				}
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