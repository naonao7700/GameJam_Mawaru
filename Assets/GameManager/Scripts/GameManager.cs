using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    //�Q�[���I�[�o�[�ɂȂ���������
    public bool IsGameOver()
    {
        return Input.GetKeyDown(KeyCode.R);
        //return GetSubmitButtonDown();
    }

    //�V�[����ύX���鏈��
    public void OnChangeScene(SceneID sceneID)
    {
        this.sceneID = sceneID;
        if ( currentScene != null )
        {
            //�V�[���̏I������
            currentScene.OnExit();
        }

        //�V�[����ݒ肷��
        switch (sceneID)
        {
            case SceneID.Title:�@currentScene = titleScene; break;
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
    public float GetGaugeRate()
    {
        return (float)specialValue / SPECIAL_MAX;
    }

    //�v���C���[���_���[�W���󂯂���
    public void OnPlayerDamage( int value )
    {
        OnChangeScene(SceneID.Result);
    }

    //�X�R�A���擾����
    public int GetScore()
    {
        return score;
    }

    //�X�R�A�����Z����
    public void AddScore( int value )
    {
        score += value;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }

    //�X�R�A��ݒ肷��
    public void SetScore( int value )
    {
        score = value;
        if (score < 0) score = 0;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }


    //���~�߂𔭓�����
    public void OnSpecial()
    {
        stopFlag.SetFlag(true);
    }

    //���~�߂𔭓��ł��邩
    public bool CanSpecial()
    {
        if (stopFlag.GetFlag()) return false;//���~�ߒ��͔����ł��Ȃ�
        if (specialValue < SPECIAL_MAX) return false;�@//�Q�[�W�����܂��ĂȂ��Ƃ��͔����s��
        return true;
    }

    //���~�߃Q�[�W�����Z����
    public void AddSpecialGauge( int value )
    {

    }

    //�G�L�������~����p
    public float TimeRate => stopFlag.GetRate();
    

    //����{�^����������
    public bool GetSubmitButtonDown()
    {
        return Input.GetButtonDown("Submit");
    }

    //��{�^����������
    public bool GetUpButtonDown()
    {
        return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f;
    }

    //���{�^����������
    public bool GetDownButtonDown()
    {
        return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f;
    }
}
