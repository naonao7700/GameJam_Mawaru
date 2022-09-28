using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeObject : MonoBehaviour
{
    NewGaugeScript nGS;

    [SerializeField] RectTransform bar;


    //���Ԃ̃X�s�[�h
    [SerializeField] float normalSpeed = 0.4f;   //�����Ă��鎞�̑��x
    [SerializeField] float maxSpeed = 1.0f;      //�������Ă��鎞�̑��x

    void Awake()
    {
        nGS = GetComponent<NewGaugeScript>();
    }


    #region �ĂԊ֐�
    //����������
    public void Initialize()
    {
        nGS.CloseAnim();
        NormalSpeed();
        Amount(0);
    }

    //�Q�[�W�𓮂����֐�
    //��������Q�[�W�̃X�P�[����ݒ�B
    public void Amount(float _gaugeAmount)
    {
        float x = bar.localScale.x;
        float y = _gaugeAmount;
        if (y > 1) y = 1;
        if (y < 0) y = 0;


        bar.localScale = new Vector2(x, y);
    }


    //�X�e�B�b�N������Ă���Ƃ��Ă�(���t���[���Ă�ł��o�O��Ȃ�)
    public void NormalSpeed()
    {
        ChangeSpeed(normalSpeed);
    }

    //�X�e�B�b�N�������Ă���Ƃ��Ă�(���t���[���Ă�ł��o�O��Ȃ�)
    public void MaxSpeed()
    {
        ChangeSpeed(maxSpeed);
    }


    //���܂������̊֐�
    //�Q�[�W�����܂������̃A�j���[�V�������Đ��B
    public void ReachGauge()
    {
        nGS.FillAnim();
    }

    //������̊֐�
    //�Q�[�W������A�j���[�V�������Đ��B
    public void Release()
    {
        nGS.CloseAnim();
    }
    #endregion

    #region Private
    void ChangeSpeed(float _gearSpeed)
    {
        nGS.ChangeSpeed(_gearSpeed);
    }

    #endregion


}
