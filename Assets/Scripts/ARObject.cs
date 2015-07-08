using System;
using UnityEngine;

public class ARObject {
	public string name { get; set; }
	public GameObject gameObject { get; set; }

	public ARObject (GameObject obj, string name) {
		this.gameObject = obj;
		this.name = name;
	}
}
