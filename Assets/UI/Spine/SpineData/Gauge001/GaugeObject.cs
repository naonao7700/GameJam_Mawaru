using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeObject : MonoBehaviour
{
    NewGaugeScript nGS;

    [SerializeField] RectTransform bar;


    //歯車のスピード
    [SerializeField] float normalSpeed = 0.4f;   //放している時の速度
    [SerializeField] float maxSpeed = 1.0f;      //動かしている時の速度

    void Awake()
    {
        nGS = GetComponent<NewGaugeScript>();
    }


    #region 呼ぶ関数
    //初期化処理
    public void Initialize()
    {
        nGS.CloseAnim();
        NormalSpeed();
        Amount(0);
    }

    //ゲージを動かす関数
    //増減するゲージのスケールを設定。
    public void Amount(float _gaugeAmount)
    {
        float x = bar.localScale.x;
        float y = _gaugeAmount;
        if (y > 1) y = 1;
        if (y < 0) y = 0;


        bar.localScale = new Vector2(x, y);
    }


    //スティックを放しているとき呼ぶ(毎フレーム呼んでもバグらない)
    public void NormalSpeed()
    {
        ChangeSpeed(normalSpeed);
    }

    //スティックが動いているとき呼ぶ(毎フレーム呼んでもバグらない)
    public void MaxSpeed()
    {
        ChangeSpeed(maxSpeed);
    }


    //溜まった時の関数
    //ゲージが溜まった時のアニメーションが再生。
    public void ReachGauge()
    {
        nGS.FillAnim();
    }

    //解放時の関数
    //ゲージが閉じるアニメーションが再生。
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
