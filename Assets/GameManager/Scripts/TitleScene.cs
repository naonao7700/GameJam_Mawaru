using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�^�C�g���V�[��
public class TitleScene : MonoBehaviour, IScene
{
    //�^�C�g���V�[���̏�����
    public void OnEnter()
    {
        gameObject.SetActive(true);
    }

    //�^�C�g���V�[���̍X�V
    public void DoUpdate()
    {
        //�{�^�����������Ƃ�
        if( GameManager.GetSubmitButtonDown() )
        {
            //�Q�[���V�[���ɑJ�ڂ���
            GameManager.OnChangeScene(SceneID.Game);
        }
    }

    //�^�C�g���V�[���̏I������
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
