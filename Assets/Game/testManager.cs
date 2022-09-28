using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testManager : MonoBehaviour
{
    public float per;

    [SerializeField] gaugeScript gs;
    [SerializeField] GaugeObject gaugeManager;

    bool reach = false;

    private void Start()
    {
        per = 0;
        gaugeManager.Initialize();
    }



    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (per < 1)
            {
                per += 0.01f;
                //�Q�[�W
                gaugeManager.Amount(per);
                if (per > 1f)
                {
                    reach = true;
                    //���܂����p�A�j���[�V�������Ă�
                    gaugeManager.ReachGauge();
                }

                gs.Change(per);
                //�X�e�B�b�N���񂵂Ă���ԃX�s�[�h���グ��B
                //(���t���[���Ăяo���Ă��悢���A1�񂾂��Ă�łق���)
                gaugeManager.MaxSpeed();
            }

        }
        else
        {
            //�񂵂Ă��Ȃ��Ԃ͕��ʂ̃X�s�[�h��
            gaugeManager.NormalSpeed();
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (reach == true)
            {

                //�Q�[�W�̉���A�j���[�V�������ĂԁB
                gaugeManager.Release();
                //�Q�[�W
                gaugeManager.Amount(per);
                gs.Change(per);

                reach = false;
            }
            if (per > 0)
            {
                per -= 0.02f;
            }
            //�Q�[�W
            gaugeManager.Amount(per);
        }

    }
}
