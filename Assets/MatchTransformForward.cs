using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransformForward : MonoBehaviour {

	public Transform other;
	// Use this for initialization
	void Start () {
		other = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = other.forward;
	}
}
