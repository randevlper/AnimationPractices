using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBitmapSelector : MonoBehaviour {
	MeshRenderer meshRen;
	Material mat;

	public int width;
	public int height;

	float tilingX;
	float tilingY;
	public int character;

	private void Start () {
		meshRen = GetComponent<MeshRenderer> ();
		mat = meshRen.material;
		SetUV ();
		SetOffset(1);
	}

	private void Update () {
		SetOffset(character);
	}

	void SetUV () {
		tilingX = 1.0f / width;
		tilingY = 1.0f / height;
		//Debug.Log ("x: " + tilingX + " y: " + tilingY);
		mat.SetTextureScale ("_MainTex", new Vector2 (tilingX, tilingY));
	}

	void SetOffset (int value) {
		float offsetX = (tilingX * (value % width));
		float offsetY = 1 - (tilingY * 1 + (value / height));
		//Debug.Log ("x: " + offsetX + " y: " + offsetY);
		mat.SetTextureOffset ("_MainTex", new Vector2 (offsetX, offsetY));
	}

}