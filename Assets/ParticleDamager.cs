using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamager : MonoBehaviour {
	public float damage;
	public ParticleSystem part;
	public List<ParticleCollisionEvent> collisionEvents;
	public GameObject prefab;

	void Start () {
		part = GetComponent<ParticleSystem> ();
		collisionEvents = new List<ParticleCollisionEvent> ();
	}

	void OnParticleCollision (GameObject other) {
		int numCollisionEvents = part.GetCollisionEvents (other, collisionEvents);

		if (prefab != null) {
			for (int i = 0; i < collisionEvents.Count; i++) {

				GameObject preObject = Instantiate (prefab, collisionEvents[i].intersection, Quaternion.identity);
				preObject.transform.forward = collisionEvents[i].normal;
				preObject.transform.parent = other.transform;
			}
		}

		Debug.Log (other);
		IDamageable damageable = other.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.Damage (new HitData (damage));
		}
	}
}