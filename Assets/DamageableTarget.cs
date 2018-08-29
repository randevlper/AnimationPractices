using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTarget : MonoBehaviour {

	[SerializeField] DamageableBody dB;

	// Use this for initialization
	void Start () {
		dB.onDeath = OnDeath;
	}

	void OnDeath(HitData hit){
		gameObject.SetActive(false);
	}
}
