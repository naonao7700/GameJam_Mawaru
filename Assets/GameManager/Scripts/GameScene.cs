using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Q�[���V�[��
public class GameScene : MonoBehaviour, IScene
{
    //UI
    [SerializeField] private GameObject ui;

	[SerializeField] private Text score;	//�X�R�A
	[SerializeField] private Text time;	//�o�ߎ���

    //�C���X�^���X�����p��Prefab�Q�Ɛݒ�
    [SerializeField] private GameObject playerPrefab;    //�v���C���[��Prefab�Q��
    [SerializeField] private GameObject enemyPrefab;    //�G�L������Prefab�Q��


    //�C���X�^���X�̎Q��
    [SerializeField] private GameObject playerObject;   //�v���C���[
    [SerializeField] private GameObject enemyObject;    //�G�L�����X�|�i�[

    [SerializeField] private AudioClip[] bgmList;

	//UI�̑J�ڏ���
	public FlagTimer uiTimer;

	//�X�R�A�̍��W
	[SerializeField] private Vector3 scoreHidePos;
	[SerializeField] private Vector3 scoreViewPos;

	//�Q�[�W�̍��W
	[SerializeField] private Vector3 gaugeHidePos;
	[SerializeField] private Vector3 gaugeViewPos;

	//�o�ߎ��Ԃ̍��W
	[SerializeField] private Vector3 timeHidePos;
	[SerializeField] private Vector3 timeViewPos;

	//UI�I�u�W�F�N�g
	[SerializeField] private RectTransform gauge;
	[SerializeField] private RectTransform scoreImage;
	[SerializeField] private RectTransform timeImage;
	[SerializeField] private AnimationCurve curve;

    //�Q�[���V�[���̏�����
    public void OnEnter()
    {
		//�\�����o�J�n
		uiTimer.SetFlag(true);

        //BGM�������_���ōĐ�����
        int rand = Random.Range(0, bgmList.Length);
        GameManager.PlayBGM(bgmList[rand]);

		//�Q�[���J�n���̏���������
		GameManager.OnGameStart();

		//UI��\������
		ui.SetActive(true);

        //�v���C���[�����ɐ������Ă���Ȃ����
        if( playerObject != null )
        {
            GameObject.Destroy(playerObject);
        }
        //�v���C���[�̐���
        playerObject = GameObject.Instantiate(playerPrefab);

        //�G�L���������ɐ������Ă���Ȃ����
        if (enemyObject != null)
        {
            GameObject.Destroy(enemyObject);
        }

        //�G�L�����̐���
        enemyObject = GameObject.Instantiate(enemyPrefab);

        //�Q�[�W�̏���������
        GameManager.GaugeObject.Initialize();

		//�|�ꂽ�t���O(���S���o�p�̃t���O���Z�b�g)
		deathFlag = false;
    }

	//�v���C���[���|�ꂽ�t���O
	public bool deathFlag;

    //�Q�[���V�[���̍X�V
    public void DoUpdate()
    {
		//�v���C���[�|�ꂽ���̏���
		if( GameManager.IsPlayerDeath() && !deathFlag )
		{
			deathFlag = true;
			playerObject.GetComponentInChildren<PlayerImage>().OnShake();
		}

		if( deathFlag )
		{
			//�Q�[���I�[�o�[�ɂȂ����Ƃ�
			if (GameManager.IsGameOver())
			{
				//���U���g�V�[���֑J�ڂ���
				GameManager.OnChangeScene(SceneID.Result);
			}
			return;
		}
		else
		{
			//�f�[�^�̍X�V����
			GameManager.DoUpdate(Time.deltaTime);
		}

		//�o�ߎ��Ԃ��X�V
		time.text = GameManager.GetTimeText();

		//�X�R�A���X�V
		score.text = "Score:" + GameManager.GetScore().ToString("D8");

        //�Q�[�W�̍X�V
        GameManager.GaugeObject.Amount(GameManager.GetGaugeRate());
    }

    //�Q�[���V�[���̏I������
    public void OnExit()
    {
    }

	//UI�̑J�ډ��o
	public void UIUpdate( float deltaTime )
	{
		//UI�̑J�ډ��o
		uiTimer.DoUpdate(Time.deltaTime);
		var t = uiTimer.GetRate();
		t = curve.Evaluate(t);
		scoreImage.anchoredPosition = Vector3.Lerp(scoreHidePos, scoreViewPos, t);
		gauge.anchoredPosition = Vector3.Lerp(gaugeHidePos, gaugeViewPos, t);
		timeImage.anchoredPosition = Vector3.Lerp(timeHidePos, timeViewPos, t);
	}
}
