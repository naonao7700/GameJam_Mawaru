using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Q�[���V�[��
public class GameScene : MonoBehaviour, IScene
{
    //�Q�[���}�l�[�W��
    [SerializeField] private GameManager game;

    //UI
    [SerializeField] private GameObject ui;
    [SerializeField] private gaugeScript gauge; //�Q�[�W

    //�C���X�^���X�����p��Prefab�Q�Ɛݒ�
    [SerializeField] private GameObject playerPrefab;    //�v���C���[��Prefab�Q��
    [SerializeField] private GameObject enemyPrefab;    //�G�L������Prefab�Q��


    //�C���X�^���X�̎Q��
    [SerializeField] private GameObject playerObject;   //�v���C���[
    [SerializeField] private GameObject enemyObject;    //�G�L�����X�|�i�[

    //�Q�[���V�[���̏�����
    public void OnEnter()
    {
        //UI��\������
        ui.SetActive(true);

        foreach( var enemy in GameObject.FindGameObjectsWithTag("Enemy") )
        {
            GameObject.Destroy(enemy.gameObject);
        }

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
        //�Q�[���I�[�o�[�ɂȂ����Ƃ�
        if( game.IsGameOver() )
        {
            //���U���g�V�[���֑J�ڂ���
            game.OnChangeScene(SceneID.Result);
        }

        //�Q�[�W�̍X�V
        gauge.Change(game.GetGaugeRate());
    }

    //�Q�[���V�[���̏I������
    public void OnExit()
    {
        //UI���\���ɂ���
        ui.SetActive(false);
    }
}
