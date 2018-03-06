using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

	[SerializeField] float walkMoveStopRadius = 0.1f;

	ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
	CameraRaycaster cameraRaycaster;
	Vector3 currentClickTarget;

	bool isInDirectMode = false; // TODO make static later

	private void Start()
	{
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
		m_Character = GetComponent<ThirdPersonCharacter>();
		currentClickTarget = transform.position;
	}

	//Fix issue with click to mover and WSAD conflicting and increasing speed

	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		if (Input.GetKeyDown (KeyCode.G)) // TODO allow player to switch to gamepad mode
		{ 
			isInDirectMode = !isInDirectMode; //TODO add to menu later
		}

		if (isInDirectMode)
		{
			ProcessDirectMovement();
		}
		else
		{
		ProcessMouseMovement(); //processes mouse movement
		}
	}

	private void ProcessDirectMovement();
	{
	
	}



	private void ProcessMouseMovement()
	{
		if (Input.GetMouseButton(0))
		{
			print("Cursor raycast hit" + cameraRaycaster.layerHit);
			switch (cameraRaycaster.layerHit) 
			{
			case Layer.Walkable:
				currentClickTarget = cameraRaycaster.hit.point;
				break;
			case Layer.Enemy:
				print ("Not moving to enemy");
				break;
			default:
				print ("Unexpected layer found bro!");
				return;
			}
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if (playerToClickPoint.magnitude >= walkMoveStopRadius) 
		{
			m_Character.Move (currentClickTarget - transform.position, false, false);
		} 
		else
		{
			m_Character.Move(Vector3.zero, false, false);
		}
	}
}
