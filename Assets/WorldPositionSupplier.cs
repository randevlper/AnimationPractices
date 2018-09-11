using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionSupplier : MonoBehaviour {
	public Material mat;
	public Transform[] points;

	// Update is called once per frame
	void OnGUI () {
		if (mat == null) { return; }
		Vector4[] arr = new Vector4[4];
		for (int i = 0; i < arr.Length; i++) {
			Vector3 pos = points[i].position;
			arr[i] = new Vector4 (pos.x, pos.y, pos.z, 0);
		}
		mat.SetVectorArray("_Points", arr);
	}
}