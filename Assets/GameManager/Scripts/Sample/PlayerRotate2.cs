using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate2 : MonoBehaviour
{
	[SerializeField]
	PlayerObject playerObject;

	[SerializeField]
	float LongHandSpeed;
	[SerializeField]
	float ShortHandSpeed;

	[SerializeField]
	float GaugeUpValue;

	public float GaugeNum;

	float stickRH;
	float stickRV;
	float stickLH;
	float stickLV;

	float oldMRot;
	float oldSRot;

	private float prevRadian;   //前回のラジアン角

	[Range(0,4320)] public float mainValue;

	[SerializeField] private Vector2 dir = Vector2.up;

	private Vector2 GetMainDir()
	{
		var x = Mathf.Cos(mainValue * Mathf.Deg2Rad );
		var y = Mathf.Sin(mainValue * Mathf.Deg2Rad );
		return new Vector2(x, y);
	}

	private void Update()
	{
		//var v = new Vector2(Input.GetAxis("R_Horizontal"), Input.GetAxis("R_Vertical")).normalized;
		var v = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		if( !(v.x == 0.0f && v.y == 0.0f) )
		{
			float angle = Vector2.SignedAngle(dir, v);
			Debug.Log(angle);

			if( Mathf.Abs( angle ) > 5 )
            {
				if (angle < 0) angle = -5;
				else angle = 5;
				
            }
			//mainValue += angle;

			mainValue = Mathf.Lerp(mainValue, mainValue % 360, 0.1f) + (mainValue % 360);
			if( angle < 0  )
            {
				GameManager.AddTimeStopGauge(angle);
            }
		}

		//角度を変更する
		var d = GetMainDir();
		playerObject.mainRot = mainValue;
		playerObject.subRot = Mathf.Lerp(0, 360.0f, mainValue / 4320.0f);

		//RotationHands();
	}
}
