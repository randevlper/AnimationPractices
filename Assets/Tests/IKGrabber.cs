using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKGrabber : MonoBehaviour {

	[SerializeField] Animator animController;
	public bool ikActive;
	public float distanceToActivate;
	public Transform lookObj;
	public Transform rightHandObj;

	// Update is called once per frame
	void Update () {

	}

	private void OnAnimatorIK (int layerIndex) {
		if (animController) {

			if (ikActive) {
				float distToRHObj = Vector3.Distance (animController.GetBoneTransform (HumanBodyBones.Head).position, rightHandObj.position);
				if (distToRHObj < distanceToActivate) {
					if (rightHandObj != null) {
						animController.SetIKPositionWeight (AvatarIKGoal.RightHand, distanceToActivate - distToRHObj / distanceToActivate);
						animController.SetIKRotationWeight (AvatarIKGoal.RightHand, distanceToActivate - distToRHObj / distanceToActivate);
						animController.SetIKPosition (AvatarIKGoal.RightHand, rightHandObj.position);
						animController.SetIKRotation (AvatarIKGoal.RightHand, rightHandObj.rotation);
					}
				}
				if (lookObj != null) {
					animController.SetLookAtWeight (1);
					animController.SetLookAtPosition (lookObj.position);
				}
			}
		}

		//if the IK is not active, set the position and rotation of the hand and head back to the original position
		else {
			animController.SetIKPositionWeight (AvatarIKGoal.RightHand, 0);
			animController.SetIKRotationWeight (AvatarIKGoal.RightHand, 0);
			animController.SetLookAtWeight (0);
		}
	}
}