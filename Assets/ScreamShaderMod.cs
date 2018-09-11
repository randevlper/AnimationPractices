using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamShaderMod : MonoBehaviour {
	[SerializeField] MeshRenderer meshRen;
	Material mat;

	public Vector4 mod;

	// Use this for initialization
	void Start () {
		mat = meshRen.material;
	}
	
	// Update is called once per frame
	void Update () {
		float sinMod = Mathf.Sin(Time.time);
		mat.SetVector("_Point", mod * sinMod);
	}
}
