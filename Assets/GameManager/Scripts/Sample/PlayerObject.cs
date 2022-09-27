using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
	[SerializeField] private Transform mainShot;
	[SerializeField] private Transform subShot;

	[SerializeField] public Transform mainShotPos;
	[SerializeField] public Transform subShotPos;

	[SerializeField] private PlayerImage character;

	[Range(0,360)] public float mainRot;
	[Range(0,360)] public float subRot;

	[SerializeField] private GameObject prefab;

	private void TestUpdate()
	{
		mainRot += Input.GetAxis("Horizontal");
		mainRot %= 360.0f;
		if( Input.GetKeyDown(KeyCode.Space) )
		{
			GameObject.Instantiate(prefab, mainShotPos.position, mainShot.localRotation);
		}
	}

	private void Update()
	{
		TestUpdate();

		mainShot.localEulerAngles = new Vector3(0, 0, mainRot);
		subShot.localEulerAngles = new Vector3(0, 0, subRot);
		character.angle = mainRot;
	}
}
