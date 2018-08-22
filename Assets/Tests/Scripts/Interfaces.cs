using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct HitData
{
	public float damage;
	public GameObject other;

	public HitData(float dam, GameObject you = null){
		damage = dam;
		other = you;
	}
}

public interface IDamageable
{
	void Damage(HitData hit);
}