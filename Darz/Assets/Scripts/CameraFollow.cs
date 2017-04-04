using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Vector3 CAMERA_OFFSET;

    private Character character;
    private Camera mainCamera;

	// Use this for initialization
	void Start () {
        character = this.gameObject.GetComponent<Character>();
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newCamPosition = mainCamera.transform.position;
        newCamPosition.x = character.transform.position.x + CAMERA_OFFSET.x;
        newCamPosition.z = character.transform.position.z + CAMERA_OFFSET.z;
        mainCamera.transform.position = newCamPosition;
	}
}
