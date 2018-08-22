using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraSwitch : MonoBehaviour {
	
	public CinemachineVirtualCameraBase[] cameras;
	int active = 0;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			for (int i = 0; i < cameras.Length; i++)
			{
				cameras[i].gameObject.SetActive(false);
			}
			cameras[active].gameObject.SetActive(true);
			active++;
		}
	}
}
