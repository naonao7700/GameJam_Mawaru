using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeStopStep
{
	None,	//í èÌ
	Start,	//éûä‘í‚é~äJén
	Hold,	//éûä‘í‚é~íÜ
	Finish,	//éûä‘í‚é~èIóπ
}

//éûé~Çﬂââèo
public class TimeStop : MonoBehaviour
{
	[SerializeField] private Shader shader;
	[SerializeField] private Material material;
	[Range(0, 1)] public float range;

	[SerializeField] private float startTime;
	[SerializeField] private float finishTime;

	public TimeStopStep step;
	[SerializeField] private Timer timer;

	[SerializeField] private AnimationCurve startCurve;
	[SerializeField] private AnimationCurve finishCurve;

	public void DoUpdate( float deltaTime )
	{
		timer.DoUpdate(deltaTime);

		switch( step )
		{
			case TimeStopStep.None: break;
			case TimeStopStep.Start:
				{
					var t = timer.GetRate();
					range = startCurve.Evaluate(t);
					if( timer.IsEnd() )
					{
						step = TimeStopStep.Hold;
					}
					break;
				}
			case TimeStopStep.Hold:
				{
					range = 1.0f;
					break;
				}
			case TimeStopStep.Finish:
				{
					var t = timer.GetRate();
					range = 1.0f - finishCurve.Evaluate(t);
					if( timer.IsEnd() )
					{
						step = TimeStopStep.None;
					}
					break;
				}
		}
	}

	public void Reset()
	{
		step = TimeStopStep.None;
		timer.Reset();
	}

	public void SetFlag( bool value )
	{
		if( value )
		{
			timer.Reset( startTime, 0.0f );
			step = TimeStopStep.Start;
		}
		else
		{
			timer.Reset(finishTime, 0.0f);
			step = TimeStopStep.Finish;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Range", range);
		Graphics.Blit(source, destination, material);
	}
}
