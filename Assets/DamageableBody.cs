using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBody : MonoBehaviour, IDamageable {

	[SerializeField] bool isDebug;
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

#if UNITY_EDITOR
	private void OnDrawGizmos () {
		if (isDebug) {
			drawString (health + "/" + maxHealth, transform.position, Color.white);
		}
	}

	static public void drawString (string text, Vector3 worldPos, Color? colour = null) {
		UnityEditor.Handles.BeginGUI ();

		var restoreColor = GUI.color;

		if (colour.HasValue) GUI.color = colour.Value;
		var view = UnityEditor.SceneView.currentDrawingSceneView;
		if (view != null) {
			Vector3 screenPos = view.camera.WorldToScreenPoint (worldPos);

			if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0) {
				GUI.color = restoreColor;
				UnityEditor.Handles.EndGUI ();
				return;
			}

			Vector2 size = GUI.skin.label.CalcSize (new GUIContent (text));
			GUI.Label (new Rect (screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
			GUI.color = restoreColor;
			UnityEditor.Handles.EndGUI ();
		}

	}
#endif
}