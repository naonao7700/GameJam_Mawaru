using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//�V�[����ύX���鏈��
	public static void OnChangeScene(SceneID sceneID) => instance.OnChangeSceneCore(sceneID);

	//�Q�[���I�[�o�[�ɂȂ���������
	public static bool IsGameOver() => Input.GetKeyDown(KeyCode.R); //��

	//�v���C���[���_���[�W���󂯂���
	public static void OnPlayerDamage(int value) => instance.OnPlayerDamageCore(value);

	//�Q�[�W�̊������擾����
	public static float GetGaugeRate() => instance.GetGaugeRateCore();

	//�X�R�A���擾����
	public static int GetScore() => instance.score;

	//�X�R�A�����Z����
	public static void AddScore(int value) => instance.AddScoreCore(value);

	//�X�R�A��ݒ肷��
	public static void SetScore(int value) => instance.SetScoreCore(value);

	//���~�߂𔭓�����
	public static void OnSpecial() => instance.OnSpecialCore();

	//���~�߂𔭓��ł��邩
	public static bool CanSpecial() => instance.CanSpecialCore();

	//���~�߃Q�[�W�𑝉�������
	public static void AddSpecialGauge(int value) => instance.AddSpecialGaugeCore(value);

	//�G�L�������~����p
	public static float TimeRate => instance.stopFlag.GetRate();

	//����{�^����������
	public static bool GetSubmitButtonDown() => Input.GetButtonDown("Submit");

	//��{�^����������
	public static bool GetUpButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f;

	//���{�^����������
	public static bool GetDownButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f;

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

	//�V�[���̎Q�Ɛݒ�(Inspecter�Őݒ�)
	[SerializeField] private TitleScene titleScene;
	[SerializeField] private GameScene gameScene;
	[SerializeField] private ResultScene resultScene;

    private IScene currentScene;    //���݂̃V�[��
    public SceneID sceneID;    //���݂̃V�[��ID

    //����������
    private void Start()
    {
        titleScene.OnExit();
        gameScene.OnExit();
        resultScene.OnExit();

        //�^�C�g���V�[�����ŏ��Ɏ��s����
        OnChangeScene(SceneID.Title);
    }

    //�X�V����
    private void Update()
    {
        //���݂̃V�[�����X�V����
        currentScene.DoUpdate();
    }


	//�V�[���̐؂�ւ�����
	private void OnChangeSceneCore( SceneID sceneID )
	{
		this.sceneID = sceneID;
		if (currentScene != null)
		{
			//�V�[���̏I������
			currentScene.OnExit();
		}

		//�V�[����ݒ肷��
		switch (sceneID)
		{
			case SceneID.Title: currentScene = titleScene; break;
			case SceneID.Game: currentScene = gameScene; break;
			case SceneID.Result: currentScene = resultScene; break;
		}

		//�V�[���̏���������
		currentScene.OnEnter();
	}


    //�Q�[���̃X�R�A
    public int score;
    public const int SCORE_MAX = 9999;

    //���~�ߒ��t���O
    public FlagTimer stopFlag;

    //���~�߃Q�[�W
    public int specialValue;
    public const int SPECIAL_MAX = 120;
    public float GetGaugeRateCore()
    {
        return (float)specialValue / SPECIAL_MAX;
    }


	//�v���C���[���_���[�W���󂯂���
	public void OnPlayerDamageCore(int value)
	{
		OnChangeScene(SceneID.Result);
	}

	//�X�R�A�����Z����
	private void AddScoreCore( int value )
    {
        score += value;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }

    //�X�R�A��ݒ肷��
    public void SetScoreCore( int value )
    {
        score = value;
        if (score < 0) score = 0;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }

	//���~�߂𔭓�����
	private void OnSpecialCore()
	{
		stopFlag.SetFlag(true);
	}

	//���~�߂𔭓��ł��邩
	public bool CanSpecialCore()
    {
        if (stopFlag.GetFlag()) return false;//���~�ߒ��͔����ł��Ȃ�
        if (specialValue < SPECIAL_MAX) return false;�@//�Q�[�W�����܂��ĂȂ��Ƃ��͔����s��
        return true;
    }

	//���~�߃Q�[�W�𑝉�������
	private void AddSpecialGaugeCore( int value )
	{
		specialValue += value;
		if (specialValue > SPECIAL_MAX) specialValue = SPECIAL_MAX;
	}

}
