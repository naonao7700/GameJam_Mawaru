using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Q�[���V�[��
public class GameScene : MonoBehaviour, IScene
{
    //UI
    [SerializeField] private GameObject ui;
    [SerializeField] private gaugeScript gauge; //�Q�[�W

	[SerializeField] private Text score;	//�X�R�A
	[SerializeField] private Text time;	//�o�ߎ���


    //�C���X�^���X�����p��Prefab�Q�Ɛݒ�
    [SerializeField] private GameObject playerPrefab;    //�v���C���[��Prefab�Q��
    [SerializeField] private GameObject enemyPrefab;    //�G�L������Prefab�Q��


    //�C���X�^���X�̎Q��
    [SerializeField] private GameObject playerObject;   //�v���C���[
    [SerializeField] private GameObject enemyObject;    //�G�L�����X�|�i�[

    //�Q�[���V�[���̏�����
    public void OnEnter()
    {
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
    }

    //�Q�[���V�[���̍X�V
    public void DoUpdate()
    {
		//�f�[�^�̍X�V����
		GameManager.DoUpdate(Time.deltaTime);

        //�Q�[���I�[�o�[�ɂȂ����Ƃ�
        if( GameManager.IsGameOver() )
        {
            //���U���g�V�[���֑J�ڂ���
            GameManager.OnChangeScene(SceneID.Result);
        }

		//�o�ߎ��Ԃ��X�V
		time.text = "�o�ߎ��ԁF" + GameManager.GetTimeText();

		//�X�R�A���X�V
		score.text = "Score:" + GameManager.GetScore().ToString("D4");

        //�Q�[�W�̍X�V
        gauge.Change(GameManager.GetGaugeRate());
    }

    //�Q�[���V�[���̏I������
    public void OnExit()
    {
    }
}
