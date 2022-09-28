using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
	Standard,	//�ʏ�
	Clocks,		//���v
};

public class GameManager : MonoBehaviour
{
	//�j�̉�]���@
	public static GameMode Mode;

	public static void DoUpdate(float deltaTime) => instance.DoUpdateCore(deltaTime);

	//�V�[����ύX���鏈��
	public static void OnChangeScene(SceneID sceneID) => instance.sceneManager.OnChangeScene(sceneID);

	//�G�L������S�ď���
	public static void OnDeleteAllEnemy() => instance.OnDeleteAllEnemyCore();

	//�v���C���[�����񂾃t���O���擾����
	public static bool IsPlayerDeath() => instance.playerManager.deathFlag;

	//�Q�[���I�[�o�[�ɂȂ���������
	public static bool IsGameOver() => instance.playerManager.GetDeathFlag();

	//�v���C���[���_���[�W���󂯂���
	public static void OnPlayerDamage(int value) => instance.playerManager.OnDamage(value);

	//�Q�[�W�̊������擾����
	public static float GetGaugeRate() => instance.playerManager.gauge.GetRate();

	//�X�R�A���擾����
	public static int GetScore() => instance.scoreManager.score;

	//�X�R�A�����Z����
	public static void AddScore(int value) => instance.scoreManager.AddScore(value);

	//�X�R�A��ݒ肷��
	public static void SetScore(int value) => instance.scoreManager.SetScore(value);

	//�n�C�X�R�A���X�V����
	public static void UpdateHighScore() => instance.scoreManager.UpdateHighScore();

	//�n�C�X�R�A���擾����
	public static int GetHighScore() => instance.scoreManager.highScore;

	//�o�ߎ��Ԃ��擾����
	public static string GetTimeText() => instance.timeManager.GetText();

	//���~�߂𔭓�����
	public static void OnTimeStop() => instance.playerManager.OnTimeStop();

	//���~�߂𔭓��ł��邩
	public static bool CanTimeStop() => instance.playerManager.CanTimeStop();

	//���~�߃Q�[�W�𑝉�������
	public static void AddTimeStopGauge(float value) => instance.playerManager.gauge.AddValue(value);

	//�G�L�������~����p
	public static float TimeRate => 1.0f - instance.playerManager.timeStop.range;

	//����{�^����������
	public static bool GetSubmitButtonDown() => GetTimeStopButtonDown();

	//��{�^����������
	public static bool GetUpButtonDown() => (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0.0f) || ( Input.GetAxis("L_Horizontal") < 0.0f );

	//���{�^����������
	public static bool GetDownButtonDown() => (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0.0f) || (Input.GetAxis("L_Horizontal") > 0.0f);

	//���~�߃{�^����������
	public static bool GetTimeStopButtonDown() => Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space);
	//public static bool GetTimeStopButtonDown() => Input.anyKeyDown && !Input.GetButtonDown("Horizontal");//Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space);

	//�Q�[���J�n���̏���������
	public static void OnGameStart() => instance.OnGameStartCore();

	//���~�ߒ�������
	public static bool IsTimeStop() => instance.playerManager.stopFlag;

	//BGM���Đ�����
	public static void PlayBGM(AudioClip clip) => instance._PlayBGM(clip);

	//SE���Đ�����
	public static void PlaySE(AudioClip clip, float volume = 1.0f ) => instance._PlaySE(clip, volume );

	//�G�t�F�N�g���Đ�����
	public static GameObject PlayEffect(GameObject prefab, Vector3 position) => instance._PlayEffect(prefab, position, Quaternion.identity);
	public static GameObject PlayEffect(GameObject prefab, Vector3 position, Quaternion rotation) => instance._PlayEffect(prefab, position, rotation);

	//�V���O���g��
	private static GameManager _instance;
	private static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}

	//�V�[���Ǘ�
	public SceneManager sceneManager;

	//�v���C���[�Ǘ�
	public PlayerManager playerManager;

	//�X�R�A�Ǘ�
	public ScoreManager scoreManager;

	//�o�ߎ���
	public TimeManager timeManager;

	//�Q�[���J�n���̏���������
	private void OnGameStartCore()
	{
		//�X�R�A�����Z�b�g
		scoreManager.SetScore(0);

		//�o�ߎ��Ԃ����Z�b�g
		timeManager.Reset();

		//�v���C���[��HP�����Z�b�g
		playerManager.Reset();

		//�G�L������S�ď���
		OnDeleteAllEnemyCore();
	}

	//�G�L������S�ď���
	private void OnDeleteAllEnemyCore()
	{
		//�������Ă���G�L������S�ď���
		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			GameObject.Destroy(enemy.gameObject);
		}

		foreach( var bullet in GameObject.FindObjectsOfType<Bullet>() )
        {
			GameObject.Destroy(bullet.gameObject);
        }

		var player = GameObject.FindGameObjectWithTag("Player");
		if( player != null )
		{
			GameObject.Destroy(player);
		}

		var enemyManager = GameObject.FindObjectOfType<EnemyManeger>();
		if( enemyManager != null )
		{
			GameObject.Destroy(enemyManager.gameObject);
		}
	}

	//�X�V����
	private void DoUpdateCore( float deltaTime )
	{
		playerManager.DoUpdate(deltaTime);
		timeManager.DoUpdate(deltaTime);

		sound.volume = Mathf.Lerp(0.1f, 1.0f, TimeRate);

		//���~�߂��\������
		if (CanTimeStop())
        {
			//���~�߃{�^�����������Ƃ�
			if (GetTimeStopButtonDown())
			{
				//���~�ߔ���
				OnTimeStop();
			}
		}
	}

	[SerializeField] private AudioSource sound;
	[SerializeField] private AudioSource seSound;

	public static GaugeObject GaugeObject => instance.gauge;
	[SerializeField] private GaugeObject gauge;//�Q�[�W


	//BGM���Đ�����
	private void _PlayBGM( AudioClip clip )
    {
		sound.clip = clip;
		sound.Play();
    }

	//SE���Đ�����
	private void _PlaySE( AudioClip clip, float volume = 1.0f )
    {
		seSound.PlayOneShot(clip, volume );
    }

	//�G�t�F�N�g�𐶐�����
	private GameObject _PlayEffect( GameObject prefab, Vector3 position, Quaternion rotation )
    {
		return GameObject.Instantiate(prefab, position, rotation);
    }

}

//�o�ߎ��ԊǗ�
[System.Serializable]
public class TimeManager
{
	public float seconds;	//�b
	public int minute;		//��

	//������
	public void Reset()
	{
		seconds = 0.0f;
		minute = 0;
	}

	//�X�V����
	public void DoUpdate( float deltaTime )
	{
		seconds += deltaTime;
		if( seconds >= 60.0f )
		{
			minute++;
			if (minute > 99)
			{
				minute = 99;
				seconds = 59.0f;
			}
			else
            {
				seconds = seconds - 60.0f;
			}
		}
	}

	//�\�������擾����
	public string GetText()
	{
		return $"{minute.ToString("00")}:{((int)seconds).ToString("00")}";
	}
}

//�v���C���[�Ǘ�
[System.Serializable]
public class PlayerManager
{
	[SerializeField] private GameObject deathEffectPrefab;  //���S���̃G�t�F�N�g
	[SerializeField] private CutInManager CutInManager;	//�J�b�g�C��

	private GameObject effect;

	//���݂�HP
	public int hpValue;

	//���S�����t���O
	public bool deathFlag;

	//���~�߃t���O
	public bool stopFlag;

	public Timer timeStopTimer;

	//���~�߂��g���邩����
	public bool CanTimeStop()
	{
		if (deathFlag) return false;    //���S��͎g���Ȃ�
		if (stopFlag ) return false;      //���~�ߒ��͎g���Ȃ�
		if (gauge.GetRate() < 1.0f) return false;	//�Q�[�W�����܂��ĂȂ��Ǝg���Ȃ�
		return true;
	}

	//���ʉ��w��
	[SerializeField] private AudioClip timeStopSE1;
	[SerializeField] private AudioClip timeStopSE2;
	[SerializeField] private AudioClip damageSE;

	//���~�ߔ���
	public void OnTimeStop()
	{
		//���ʉ��炷
		GameManager.PlaySE(timeStopSE1, 3.0f );
		GameManager.PlaySE(timeStopSE2 );

		//�Q�[�W�̉��o�ύX
		GameManager.GaugeObject.Release();

		//�J�b�g�C������
		CutInManager.OnStart();

		stopFlag = true;
		timeStop.SetFlag(true);
		timeStopTimer.Reset();
	}

	//�v���C���[���_���[�W���󂯂��Ƃ�
	public void OnDamage( int value )
	{
		if (stopFlag) return;
		if (deathFlag) return;

		hpValue -= value;
		if (hpValue < 0) hpValue = 0;
		if( hpValue <= 0 )
		{
			GameManager.PlaySE(damageSE);
			effect = GameManager.PlayEffect(deathEffectPrefab, new Vector3(0,0, -3));
			deathFlag = true;
		}
	}

	public bool GetDeathFlag()
    {
		return deathFlag && effect == null;
    }

	//�X�V����
	public void DoUpdate( float deltaTime )
	{
#if UNITY_EDITOR
		if( Input.GetKeyDown(KeyCode.Y))
		{
			gauge.SetValue(60);
		}
		if( Input.GetKeyDown(KeyCode.I))
		{
			GameManager.OnPlayerDamage(100);
		}
#endif

		timeStop.DoUpdate(deltaTime);

		CutInManager.DoUpdate(deltaTime);

		if( stopFlag )
		{
			timeStopTimer.DoUpdate(deltaTime);
			gauge.SetRate( (1.0f - timeStopTimer.GetRate()) );
			if ( timeStopTimer.IsEnd() )
            {
				stopFlag = false;
				timeStop.SetFlag(false);
            }
		}
	}

	//���~�߃Q�[�W
	public GaugeManager gauge;

	//���~�߉��o�p
	public TimeStop timeStop;

	//����������
	public void Reset()
	{
		CutInManager.Reset();
		timeStopTimer = new Timer(3.0f);

		//���~�߉��o���Z�b�g
		timeStop.Reset();

		//HP�����Z�b�g
		hpValue = 1;

		//���~�߃t���O��OFF
		stopFlag = false;

		//���S�����t���O��OFF
		deathFlag = false;

		//�Q�[�W�̏�����
		gauge.SetValue(0);
	}

}

//�Q�[�W�Ǘ�
[System.Serializable]
public class GaugeManager
{
	//�Q�[�WMAX���̌��ʉ�
	[SerializeField] private AudioClip gaugeMaxSE;

	[SerializeField] private float prevValue;	//�O��̒l
	[SerializeField] private float threshold;	//臒l

	//���݂̒l
	public float value;

	//�Q�[�W�̍ő�l
	[SerializeField]public float GAUGE_MAX = 70;

	//�Q�[�W�����Z����
	public void AddValue( float value )
	{
		//���ʉ��炷
		if (this.value < GAUGE_MAX)
        {
			if( this.value + value >= GAUGE_MAX )
            {
				GameManager.PlaySE(gaugeMaxSE);
				GameManager.GaugeObject.ReachGauge();
            }
        }

		SetValue(this.value + value);
	}

	//�Q�[�W�̐��l��ݒ肷��
	public void SetValue( float value )
	{
		//�O��̗ʂ�ێ�
		prevValue = this.value;

		if (value < 0) value = 0;
		else if (value > GAUGE_MAX) value = GAUGE_MAX;
		this.value = value;

		if( this.value - prevValue > threshold )
		{
			GameManager.GaugeObject.MaxSpeed();
		}
		else
		{
			GameManager.GaugeObject.NormalSpeed();
		}
	}

	public void SetRate( float rate )
	{
		this.value = rate * GAUGE_MAX;
		if (value < 0) value = 0;
		if (value > GAUGE_MAX) value = GAUGE_MAX;
	}

	//�Q�[�W�̊������擾����
	public float GetRate()
	{
		var t = (float)value / GAUGE_MAX;
		if (t > 1.0f) t = 1.0f;
		return t;
	}

}

//�X�R�A�Ǘ�
[System.Serializable]
public class ScoreManager
{
	//���݂̃X�R�A
	public int score;

	//�n�C�X�R�A�L�^
	public int highScore;

	//�X�R�A�̍ő�l
	public const int SCORE_MAX = 99999999;

	public void UpdateHighScore()
    {
		if( highScore < score )
        {
			highScore = score;
        }
    }

	//�X�R�A�����Z����
	public void AddScore( int value )
	{
		SetScore(score + value);
	}

	//�X�R�A�̐��l��ݒ肷��
	public void SetScore( int value )
	{
		score = value;
		if (score < 0) score = 0;
		if (score > SCORE_MAX) score = SCORE_MAX;
	}
}

//�J�b�g�C���̐���
[System.Serializable]
public class CutInManager
{
	[SerializeField] private UnityEngine.UI.Image image;
	[SerializeField] private Vector3 startPos;
	[SerializeField] private Vector3 endPos;

	[SerializeField] private Vector3 startScale;
	[SerializeField] private Vector3 endScale;

	[SerializeField] private float[] times;

	public Timer timer;
	public int step;
	public bool activeFlag;

	public void Reset()
	{
		activeFlag = false;
		image.gameObject.SetActive(false);
	}

	//�J�b�g�C�����o�J�n
	public void OnStart()
	{
		step = 0;
		timer.Reset( times[0], 0.0f );
		image.gameObject.SetActive(true);
		image.transform.position = startPos;
		image.transform.localScale = startScale;
		image.color = Color.white;
		activeFlag = true;
	}

	//�X�V����
	public void DoUpdate( float deltaTime )
	{
		if (!activeFlag) return;
		timer.DoUpdate(deltaTime);
		var t = timer.GetRate();
		if( timer.IsEnd() )
		{
			timer.Reset(times[step], 0.0f );
			step++;
			if( step >= times.Length )
			{
				image.gameObject.SetActive(false);
				activeFlag = false;
			}
		}
		switch( step )
		{
			case 0:
				{
					//�X���C�h�C��
					image.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
					break;
				}
			case 1:
				{
					//�ҋ@
					break;
				}
			case 2:
				{
					//�g��&������
					image.transform.localScale = Vector3.Lerp(startScale, endScale, t);
					var color = image.color;
					color.a = Mathf.Lerp(1.0f, 0.0f, t);
					image.color = color;
					break;
				}
		}
	}

}