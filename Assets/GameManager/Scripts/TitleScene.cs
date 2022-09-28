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

    //�^�C�g���V�[���̏�����
    public void OnEnter()
    {
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
				//�Q�[���V�[���ɑJ�ڂ���
				GameManager.OnChangeScene(SceneID.Game);
			}
			return;
		}

		logo.rectTransform.anchoredPosition = Vector3.Lerp(logoHidePos, logoViewPos, t);
		color.a = Mathf.Lerp(0.0f, 1.0f, t);
		logo.color = color;

		button.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);


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
