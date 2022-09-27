using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���U���g�V�[��
public class ResultScene : MonoBehaviour, IScene
{
    //�Q�[���}�l�[�W��
    [SerializeField] private GameManager game;

    //�J�[�\���̌��݈ʒu
    [SerializeField] private int cursorPos;

    //�J�[�\���摜���X�g
    [SerializeField] private GameObject[] cursorImage;

    //���U���g�V�[���̏�����
    public void OnEnter()
    {
        //���U���g��ʂ�\������
        gameObject.SetActive(true);

        //�J�[�\���ʒu������������
        SetCursorPos(0);
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
        //�J�[�\���̈ړ�����
        int cursor = cursorPos;
        if( game.GetUpButtonDown() )
        {
            //�J�[�\������֓�����
            cursor--;
            if (cursor < 0) cursor = cursorImage.Length - 1;    //���[�v�Ή�
        }
        else if( game.GetDownButtonDown() )
        {
            //�J�[�\�������֓�����
            cursor++;
            if (cursor >= cursorImage.Length ) cursor = 0;  //���[�v�Ή�
        }

        //�J�[�\���ʒu���X�V����
        if( cursor != cursorPos )
        {
            SetCursorPos(cursor);
        }

        //�{�^�����������Ƃ�
        if ( game.GetSubmitButtonDown() )
        {
            //�J�[�\���ʒu�ɂ���ĕ��򂷂�
            if( cursorPos == 0 )
            {
                //�Q�[���V�[���֑J�ڂ���
                game.OnChangeScene(SceneID.Game);
            }
            else
            {
                //�^�C�g���V�[���֑J�ڂ���
                game.OnChangeScene(SceneID.Title);
            }
        }
    }

    //���U���g�V�[���̏I������
    public void OnExit()
    {
        //���U���g��ʂ��\���ɂ���
        gameObject.SetActive(false);
    }

}
