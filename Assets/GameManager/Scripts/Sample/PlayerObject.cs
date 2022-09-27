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

	private void Update()
	{
		mainShot.localEulerAngles = new Vector3(0, 0, mainRot);
		subShot.localEulerAngles = new Vector3(0, 0, subRot);
		character.angle = mainRot;
	}
}
