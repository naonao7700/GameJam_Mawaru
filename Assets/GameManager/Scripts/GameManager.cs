using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static void DoUpdate(float deltaTime) => instance.DoUpdateCore(deltaTime);

	//�V�[����ύX���鏈��
	public static void OnChangeScene(SceneID sceneID) => instance.sceneManager.OnChangeScene(sceneID);

	//�G�L������S�ď���
	public static void OnDeleteAllEnemy() => instance.OnDeleteAllEnemyCore();

	//�Q�[���I�[�o�[�ɂȂ���������
	public static bool IsGameOver() => instance.playerManager.deathFlag;

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

	//�o�ߎ��Ԃ��擾����
	public static string GetTimeText() => instance.timeManager.GetText();

	//���~�߂𔭓�����
	public static void OnSpecial() => instance.playerManager.stopFlag.SetFlag(true);

	//���~�߂𔭓��ł��邩
	public static bool CanSpecial() => instance.playerManager.CanSpecial();

	//���~�߃Q�[�W�𑝉�������
	public static void AddSpecialGauge(int value) => instance.playerManager.gauge.AddValue(value);

	//�G�L�������~����p
	public static float TimeRate => instance.playerManager.stopFlag.GetRate();

	//����{�^����������
	public static bool GetSubmitButtonDown() => Input.GetButtonDown("Submit");

	//��{�^����������
	public static bool GetUpButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f;

	//���{�^����������
	public static bool GetDownButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f;

	//�Q�[���J�n���̏���������
	public static void OnGameStart() => instance.OnGameStartCore();

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
	}

	//�X�V����
	private void DoUpdateCore( float deltaTime )
	{
		playerManager.DoUpdate(deltaTime);
		timeManager.DoUpdate(deltaTime);
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
			seconds = seconds - 60.0f;
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
	//���݂�HP
	public int hpValue;

	//���S�����t���O
	public bool deathFlag;

	//���~�߃t���O
	public FlagTimer stopFlag;

	//���~�߂��g���邩����
	public bool CanSpecial()
	{
		if (deathFlag) return false;    //���S��͎g���Ȃ�
		if (stopFlag.GetFlag() ) return false;      //���~�ߒ��͎g���Ȃ�
		if (gauge.GetRate() < 1.0f) return false;	//�Q�[�W�����܂��ĂȂ��Ǝg���Ȃ�
		return true;
	}

	//�v���C���[���_���[�W���󂯂��Ƃ�
	public void OnDamage( int value )
	{
		hpValue -= value;
		if (hpValue < 0) hpValue = 0;
		if( hpValue <= 0 )
		{
			deathFlag = true;
		}
	}

	//�X�V����
	public void DoUpdate( float deltaTime )
	{
		stopFlag.DoUpdate(deltaTime);
	}

	//���~�߃Q�[�W
	public GaugeManager gauge;

	//����������
	public void Reset()
	{
		//HP�����Z�b�g
		hpValue = 1;

		//���~�߃t���O��OFF
		stopFlag = new FlagTimer(10);

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
	//���݂̒l
	public int value;

	//�Q�[�W�̍ő�l
	public const int GAUGE_MAX = 100;

	//�Q�[�W�����Z����
	public void AddValue( int value )
	{
		SetValue(this.value + value);
	}

	//�Q�[�W�̐��l��ݒ肷��
	public void SetValue( int value )
	{
		if (value < 0) value = 0;
		else if (value > GAUGE_MAX) value = GAUGE_MAX;
		this.value = value;
	}

	//�Q�[�W�̊������擾����
	public float GetRate()
	{
		return (float)value / GAUGE_MAX;
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
	public const int SCORE_MAX = 9999;

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


