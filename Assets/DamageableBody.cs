using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBody : MonoBehaviour, IDamageable {

	float health;
	public float maxHealth;
	public bool isIgnoreDamage;
	public Gold.Delegates.ActionValue<float> onHealthChange;
	public Gold.Delegates.ActionValue<HitData> onDamage;
	public Gold.Delegates.ActionValue<HitData> onDeath;
	public Gold.Delegates.ActionValue<HitData> onIgnoreDamage;

	public float Health {
		get {
			return health;
		}

		set {
			health = value;
			if (onHealthChange != null) {
				onHealthChange (value);
			}
		}
	}

	// Use this for initialization
	void Start () {
		Restore ();
	}

	public void Restore () {
		Health = maxHealth;
	}

	public void Damage (HitData hit) {
		if (!isIgnoreDamage) {
			Health -= hit.damage;
			//Debug.Log("Damaged");
			if (onDamage != null) {
				onDamage (hit);
			}
			if (health <= 0) {
				if (onDeath != null) {
					onDeath (hit);
				}
			}
		} else {
			if (onIgnoreDamage != null) {
				onIgnoreDamage (hit);
			}
		}

	}
}