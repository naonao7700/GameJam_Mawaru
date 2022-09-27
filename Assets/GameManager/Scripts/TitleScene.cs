using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�^�C�g���V�[��
public class TitleScene : MonoBehaviour, IScene
{
    //�Q�[���}�l�[�W��
    [SerializeField] private GameManager game;

    //�^�C�g���V�[���̏�����
    public void OnEnter()
    {
        gameObject.SetActive(true);
    }

    //�^�C�g���V�[���̍X�V
    public void DoUpdate()
    {
        //�{�^�����������Ƃ�
        if( game.GetSubmitButtonDown() )
        {
            //�Q�[���V�[���ɑJ�ڂ���
            game.OnChangeScene(SceneID.Game);
        }
    }

    //�^�C�g���V�[���̏I������
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
