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
    float GaugeUpValue;

    public float GaugeNum;

    float stickRH;
    float stickRV;
    float stickLH;
    float stickLV;

    float oldMRot;

    private void Update()
    {
        RotationHands();
    }

    void RotationHands()
    {
        //左右と上下の入力の合計が１以上なら、針をスティックの方向に向ける
        //１未満なら自動で時計回りに動かす

        if (Mathf.Abs(Input.GetAxis("L_Horizontal")) + Mathf.Abs(Input.GetAxis("L_Vertical")) >= 1f)
        {
            stickRH = -Input.GetAxis("L_Horizontal");
            stickRV = -Input.GetAxis("L_Vertical");

            float rotateR = Mathf.Atan2(stickRH, stickRV) * Mathf.Rad2Deg;
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, rotateR, 0.1f);
        }
        if (Mathf.Abs(Input.GetAxis("R_Horizontal")) + Mathf.Abs(Input.GetAxis("R_Vertical")) >= 1f)
        {
            stickLH = -Input.GetAxis("R_Horizontal");
            stickLV = -Input.GetAxis("R_Vertical");

            float rotateL = Mathf.Atan2(stickLH, stickLV) * Mathf.Rad2Deg;
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, rotateL, 0.1f);
        }


        //キー入力
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 0, 0.1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 90, 0.1f);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 180, 0.1f);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerObject.mainRot = Mathf.LerpAngle(playerObject.mainRot, 270, 0.1f);
        }

        if (Mathf.Abs(Input.GetAxis("L_Horizontal")) + Mathf.Abs(Input.GetAxis("L_Vertical")) < 1f
            && Mathf.Abs(Input.GetAxis("R_Horizontal")) + Mathf.Abs(Input.GetAxis("R_Vertical")) < 1f
            && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            playerObject.mainRot -= LongHandSpeed * Time.deltaTime;
        }

        //長針に合わせて短針を1/12の速度で動かす
        playerObject.subRot -= (oldMRot - playerObject.mainRot) / 12;

        // 針が前フレームよりも進んでいたらゲージ++
        if (playerObject.mainRot < oldMRot)
        {
            GaugeNum = (oldMRot - playerObject.mainRot)/* + (oldSRot - playerObject.subRot)) / 2*/ * GaugeUpValue;
            if (GaugeNum > 100)
                GaugeNum = 100;
            GameManager.AddTimeStopGauge(GaugeNum);
        }

        oldMRot = playerObject.mainRot;
    }
}
