using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V�[���̃C���^�[�t�F�C�X
public interface IScene
{
    //�V�[���ɓ������Ƃ��̏���
    void OnEnter();

    //�V�[���̍X�V����
    void DoUpdate();

    //�V�[�����I������Ƃ��̏���
    void OnExit();
}