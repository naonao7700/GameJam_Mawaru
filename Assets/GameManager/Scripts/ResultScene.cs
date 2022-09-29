using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���U���g�V�[��
public class ResultScene : MonoBehaviour, IScene
{
    //���U���gBGM
    [SerializeField] private AudioClip bgm;

    //���ʉ�
    [SerializeField] private AudioClip cursorSE;    //�J�[�\���ړ�
    [SerializeField] private AudioClip submitSE;    //���艹

    //�J�[�\���̌��݈ʒu
    [SerializeField] private int cursorPos;

    //�J�[�\���摜���X�g
    [SerializeField] private GameObject[] cursorImage;

	//�X�R�A�e�L�X�g
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

	//�J�ډ��o�^�C�}�[
	[SerializeField] private Timer startTimer;
	[SerializeField] private AnimationCurve curve;

	[SerializeField] private RectTransform buttons;	//�{�^��
	[SerializeField] private Image backImage;   //�w�i�̍��F

	[SerializeField] private Vector3 buttonHidePos;
	[SerializeField] private Vector3 buttonViewPos;

    //�J�ڃt���O
    public bool endFlag;
    public Timer endTimer;
    [SerializeField] private FadeImage fade;

    //���U���g�V�[���̏�����
    public void OnEnter()
    {
        endFlag = false;

		startTimer.Reset();

        //�n�C�X�R�A���X�V����
        GameManager.UpdateHighScore();

        scoreText.text = "�X�R�A�F" + GameManager.GetScore();
        highScoreText.text = "�n�C�X�R�A�F" + GameManager.GetHighScore();

		//�J�ڑO�̍��W�ֈړ�
		var color = Color.white;
		color.a = 0.0f;
		scoreText.color = color;
		highScoreText.color = color;
		color = backImage.color;
		color.a = 0.0f;
		backImage.color = color;
		buttons.anchoredPosition = buttonHidePos;

        //���U���g��ʂ�\������
        gameObject.SetActive(true);

        //�J�[�\���ʒu������������
        SetCursorPos(0);

        //BGM���Đ�����
        GameManager.PlayBGM(bgm);
    }

    //�J�[�\���ʒu��ݒ肷��
    void SetCursorPos( int index )
    {
        //�ϐ�����
        cursorPos = index;

        //�摜�̕\�����X�V����
        for (int i = 0; i < cursorImage.Length; ++i)
        {
            cursorImage[i].SetActive(i == cursorPos);
        }
    }

    //���U���g�V�[���̍X�V
    public void DoUpdate()
    {
        if( endFlag )
        {
            endTimer.DoUpdate(Time.deltaTime);
            var t = curve.Evaluate( endTimer.GetRate());
            t = 1.0f - t;
            var color = backImage.color;
            color.a = Mathf.Lerp(0.0f, 0.35f, t);
            backImage.color = color;

            color = scoreText.color;
            color.a = Mathf.Lerp(0.0f, 1.0f, t);
            scoreText.color = color;
            highScoreText.color = color;

            buttons.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

            if ( endTimer.IsEnd() )
            {
                fade.SetFade(false);

                GameManager.OnDeleteAllEnemy();

                //�J�[�\���ʒu�ɂ���ĕ��򂷂�
                if (cursorPos == 0)
                {
                    //�Q�[���V�[���֑J�ڂ���
                    GameManager.OnChangeScene(SceneID.Game);
                }
                else
                {
                    //�^�C�g���V�[���֑J�ڂ���
                    GameManager.OnChangeScene(SceneID.Title);
                }
            }
            return;
        }

		if( !startTimer.IsEnd() )
		{
			startTimer.DoUpdate(Time.deltaTime);
			var t = curve.Evaluate(startTimer.GetRate());
			var color = backImage.color;
			color.a = Mathf.Lerp(0.0f, 0.35f, t);
			backImage.color = color;

			color = scoreText.color;
			color.a = Mathf.Lerp(0.0f, 1.0f, t);
			scoreText.color = color;
			highScoreText.color = color;

			buttons.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

			return;
		}

        //�J�[�\���̈ړ�����
        int cursor = cursorPos;
        if( GameManager.GetUpButtonDown() )
        {
            //�J�[�\������֓�����
            cursor--;
            if (cursor < 0) cursor = 0;
            //if (cursor < 0) cursor = cursorImage.Length - 1;    //���[�v�Ή�
        }
        else if( GameManager.GetDownButtonDown() )
        {
            //�J�[�\�������֓�����
            cursor++;
            if (cursor >= cursorImage.Length) cursor = cursorImage.Length - 1;
            //if (cursor >= cursorImage.Length ) cursor = 0;  //���[�v�Ή�
        }

        //�J�[�\���ʒu���X�V����
        if( cursor != cursorPos )
        {
            GameManager.PlaySE(cursorSE);
            SetCursorPos(cursor);
        }

        //�{�^�����������Ƃ�
        if ( GameManager.GetSubmitButtonDown() )
        {
            GameManager.PlaySE(submitSE);
            endFlag = true;
            endTimer.Reset();
            fade.SetFade(true);

        }
    }

    //���U���g�V�[���̏I������
    public void OnExit()
    {
        //���U���g��ʂ��\���ɂ���
        gameObject.SetActive(false);
    }

}
