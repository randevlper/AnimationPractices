using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmmiterController : MonoBehaviour {
	public BulletEmmiter[] emmiters;

	public void ShootAll () {
		foreach (var item in emmiters) {
			item.Emit ();
		}
	}
}