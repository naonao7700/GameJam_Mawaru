using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRotate : MonoBehaviour
{
    [SerializeField]
    PlayerObject playerObject;

    [SerializeField]
    float LongHandSpeed;
    [SerializeField]
    float ShortHandSpeed;

    [SerializeField]
    float GaugeUpValue;

    public float GaugeNum;

    float stickRH;
    float stickRV;
    float stickLH;
    float stickLV;

    float oldMRot;
    float oldSRot;

    private void Update()
    {
        RotationHands();
    }

    void RotationHands()
    {
        //���E�Ə㉺�̓��͂̍��v���P�ȏ�Ȃ�A�j���X�e�B�b�N�̕����Ɍ�����
        //�P�����Ȃ玩���Ŏ��v���ɓ�����

        // �E�X�e�B�b�N
        if (Mathf.Abs(Input.GetAxis("R_Horizontal")) + Mathf.Abs(Input.GetAxis("R_Vertical")) >= 1f)
        {
            stickRH = -Input.GetAxis("R_Horizontal");
            stickRV = -Input.GetAxis("R_Vertical");

            float rotateR = Mathf.Atan2(stickRH, stickRV) * Mathf.Rad2Deg;
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, rotateR, 0.1f);
        }

        //�L�[����
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 90, 0.1f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 180, 0.1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 270, 0.1f);
        }

        if (Mathf.Abs(Input.GetAxis("R_Horizontal")) + Mathf.Abs(Input.GetAxis("R_Vertical")) < 1f
            && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            playerObject.mainRot -= LongHandSpeed * Time.deltaTime;
        }

        // ���X�e�B�b�N
        if (Mathf.Abs(Input.GetAxis("L_Horizontal")) + Mathf.Abs(Input.GetAxis("L_Vertical")) >= 1f)
        {
            stickLH = -Input.GetAxis("L_Horizontal");
            stickLV = -Input.GetAxis("L_Vertical");

            float rotateL = Mathf.Atan2(stickLH, stickLV) * Mathf.Rad2Deg;
            playerObject.subRot = Mathf.LerpAngle(playerObject.subRot, rotateL, 0.1f);
        }

        //�L�[����
        if (Input.GetKey(KeyCode.W))
        {
            playerObject.subRot = Mathf.LerpAngle(playerObject.subRot, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerObject.subRot = Mathf.LerpAngle(playerObject.subRot, 90, 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerObject.subRot = Mathf.LerpAngle(playerObject.subRot, 180, 0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerObject.subRot = Mathf.LerpAngle(playerObject.subRot, 270, 0.1f);
        }

        if (Mathf.Abs(Input.GetAxis("L_Horizontal")) + Mathf.Abs(Input.GetAxis("L_Vertical")) < 1f
            && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A)
            && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            playerObject.subRot -= ShortHandSpeed * Time.deltaTime;
        }


        // �j���O�t���[�������i��ł�����Q�[�W++
        if (playerObject.mainRot < oldMRot && playerObject.subRot < oldSRot)
        {
            GaugeNum = ((oldMRot - playerObject.mainRot) + (oldSRot - playerObject.subRot)) / 2 * GaugeUpValue;
            if (GaugeNum > 100)
                GaugeNum = 100;
            GameManager.AddTimeStopGauge(GaugeNum);
        }

        oldMRot = playerObject.mainRot;
        oldSRot = playerObject.subRot;
    }
}
