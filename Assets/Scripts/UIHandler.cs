using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIHandler : MonoBehaviour {
	public Shader SelectedObjectShader;
	public Shader UnselectedObjectShader;
	public GameObject BottomMenu;

	private string mSelectedName = "";
	private List<ARObject> mObjects = new List<ARObject>();
	private Text mPlanetLabel;

	private Animator mMenuAnimator;
	private bool showMenu = false;

	public void Start () {
		mObjects.Add (new ARObject(GameObject.Find ("Earth"), "Earth"));
		mObjects.Add (new ARObject(GameObject.Find ("Moon"), "Moon"));
		mObjects.Add (new ARObject(GameObject.Find ("Mars"), "Mars"));
		mObjects.Add (new ARObject(GameObject.Find ("Jupiter"), "Jupiter"));

		foreach (ARObject obj in mObjects) {
			Debug.Log (obj.name);
		}

		mPlanetLabel = BottomMenu.transform.Find ("PlanetLabel").GetComponent<Text> ();
		mMenuAnimator = BottomMenu.GetComponent<Animator>();
		mMenuAnimator.enabled = false;
	}
	
	public void Update () {
		handleTouches ();
	}

	private void handleTouches() {
		if (Input.touchCount == 0 || Input.touches[0].phase != TouchPhase.Began)
			return;
	
		Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			GameObject selected = hit.collider.gameObject;
			Debug.Log ("touched: " + selected.name);

			mSelectedName = selected.name;
		} else {
			mSelectedName = "";
		}
		Debug.Log ("currently selected: " + mSelectedName);
		setOutlines ();
		setPlanetNameText ();

		if (mSelectedName == "")
			HideMenu ();
		else
			ShowMenu ();
	}

	private void setOutlines() {
		foreach (ARObject obj in mObjects) {
			Renderer r = obj.gameObject.GetComponent<Renderer>();
			if(obj.name == mSelectedName) {
				r.material.shader = SelectedObjectShader;
				r.material.SetColor("_OutlineColor", Color.cyan);
			} else {
				r.material.shader = UnselectedObjectShader;
			}
		}
	}

	private void setPlanetNameText() {
		Debug.Log ("setting planet name: " + mSelectedName);
		mPlanetLabel.text = mSelectedName;
	}

	public void ShowMenu() {
		if (showMenu)
			return;
		mMenuAnimator.enabled = true;
		mMenuAnimator.Play ("BottomMenuShow");
		showMenu = true;
	}

	public void HideMenu() {
		if (!showMenu)
			return;
		showMenu = false;
		mMenuAnimator.Play ("BottomMenuHide");
	}
}
