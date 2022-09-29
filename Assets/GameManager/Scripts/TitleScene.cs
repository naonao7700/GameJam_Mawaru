using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�^�C�g���V�[��
public class TitleScene : MonoBehaviour, IScene
{
    //�^�C�g��BGM
    [SerializeField] private AudioClip bgm;

    //�X�^�[�g���ʉ�
    [SerializeField] private AudioClip startSE;

	[SerializeField] private Image logo;
	[SerializeField] private RectTransform button;

	[SerializeField] private GameObject chara;	//�v���C���[�L����

	//�J�n���̉��o�^�C�}�[
	public Timer startTimer;

	//���S�̍��W
	public Vector3 logoHidePos;
	public Vector3 logoViewPos;

	//�{�^���̍��W
	public Vector3 buttonHidePos;
	public Vector3 buttonViewPos;

	public bool startFlag;

	//�`���[�g���A���t���O
	public bool tutoFlag;

	[SerializeField] private Image tutoImage;
	public Timer tutoTimer;

	public bool tutoEndFlag;//�`���[�g���A���I���t���O

	[SerializeField] private RectTransform spine;   //�����G
	public Vector3 spineHidePos;
	public Vector3 spineViewPos;


	//�^�C�g���V�[���̏�����
	public void OnEnter()
    {
		GameManager.OnDeleteAllEnemy();//�G��S�ď���

		spine.anchoredPosition = spineHidePos;

		tutoFlag = false;
		tutoEndFlag = false;

		chara.gameObject.SetActive(true);

		startFlag = false;

		startTimer.Reset();

        gameObject.SetActive(true);
		GameManager.OnDeleteAllEnemy();

        //BGM���Đ�����
        GameManager.PlayBGM(bgm);
    }

    //�^�C�g���V�[���̍X�V
    public void DoUpdate()
    {
		//�`���[�g���A���I������
		if( tutoEndFlag )
        {
			tutoTimer.DoUpdate(Time.deltaTime);
			var t2 = tutoTimer.GetRate();
			t2 = 1.0f - t2;

			var tutoColor = tutoImage.color;
			tutoColor.a = Mathf.Lerp(0.0f, 1.0f, t2);
			tutoImage.color = tutoColor;

			spine.anchoredPosition = Vector3.Lerp(spineHidePos, spineViewPos, t2 * t2 );

			if ( tutoTimer.IsEnd() )
            {
				//�Q�[���V�[���ɑJ�ڂ���
				GameManager.OnChangeScene(SceneID.Game);
			}
			return;
        }

		//�`���[�g���A���\������
		if( tutoFlag )
        {
			tutoTimer.DoUpdate(Time.deltaTime);
			var t2 = tutoTimer.GetRate();

			var tutoColor = tutoImage.color;
			tutoColor.a = Mathf.Lerp(0.0f, 1.0f, t2);
			tutoImage.color = tutoColor;
			
			if( tutoTimer.IsEnd() )
            {
				if (GameManager.GetSubmitButtonDown())
				{
					tutoTimer.Reset();
					GameManager.PlaySE(startSE);
					tutoEndFlag = true;
				}
			}
			return;
		}


		startTimer.DoUpdate(Time.deltaTime);
		var t = startTimer.GetRate();
		t = t * t * (3.0f - 2.0f * t);
		var color = logo.color;
		if (startTimer.IsEnd()) t = 1.0f;

		if ( startFlag )
		{
			t = 1.0f - t;
			logo.rectTransform.anchoredPosition = Vector3.Lerp(logoHidePos, logoViewPos, t);
			color.a = Mathf.Lerp(0.0f, 1.0f, t);
			logo.color = color;

			button.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

			if ( startTimer.IsEnd() )
			{
				tutoFlag = true;
				tutoTimer.Reset();

			}
			return;
		}

		logo.rectTransform.anchoredPosition = Vector3.Lerp(logoHidePos, logoViewPos, t);
		color.a = Mathf.Lerp(0.0f, 1.0f, t);
		logo.color = color;

		button.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

		spine.anchoredPosition = Vector3.Lerp(spineHidePos, spineViewPos, t);


		if (!startTimer.IsEnd()) return;
        //�{�^�����������Ƃ�
        if( GameManager.GetSubmitButtonDown() )
        {
            //SE���Đ�����
            GameManager.PlaySE(startSE);

			startFlag = true;
			startTimer.Reset();
        }
    }

    //�^�C�g���V�[���̏I������
    public void OnExit()
    {
        gameObject.SetActive(false);
		chara.SetActive(false);
    }
}
