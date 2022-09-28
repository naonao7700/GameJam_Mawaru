using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Sprite deathImage;

    [Range(0, 360)] public float angle;

	[SerializeField] private Timer waitTimer;
	[SerializeField] private Timer shakeTimer;
	[SerializeField] private bool shakeFlag;
	[SerializeField] private float shakeOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int GetAngleIndex( float angle )
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        if (y == 0 && x == 0) { return -1; }

        //���E��̔���̏ꍇ�A���E���㉺���D��ɂȂ�܂�
        if (y >= Mathf.Abs(x)) { return 0; } //��
        if (y <= -Mathf.Abs(x)) { return 2; } //��
        if (x < -Mathf.Abs(y)) { return 1; } //��
        if (x > Mathf.Abs(y)) { return 3; } //�E

        return -1;    //���Ȃ��͂�
    }

    // Update is called once per frame
    void Update()
    {
		if( shakeFlag )
		{
			waitTimer.DoUpdate(Time.deltaTime);
			if( waitTimer.IsEnd() )
			{
				shakeTimer.DoUpdate(Time.deltaTime);
				var t = 1.0f - shakeTimer.GetRate();
				var randX = Random.Range(-shakeOffset, shakeOffset);
				var randY = Random.Range(-shakeOffset, shakeOffset);
				var pos = transform.localPosition;
				pos.x = randX * t;
				pos.y = randY * t;
				transform.localPosition = pos;
			}
		}

        if( GameManager.IsPlayerDeath() )
        {
            render.sprite = deathImage;
        }
        else
        {
            int index = GetAngleIndex(angle);
            render.sprite = images[index];
        }
    }

	public void OnShake()
	{
		shakeFlag = true;
		waitTimer.Reset();
		shakeTimer.Reset();
	}
}
