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
                //ゲージ
                gaugeManager.Amount(per);
                if (per > 1f)
                {
                    reach = true;
                    //溜まった用アニメーションを呼ぶ
                    gaugeManager.ReachGauge();
                }

                gs.Change(per);
                //スティックを回している間スピードを上げる。
                //(毎フレーム呼び出してもよいが、1回だけ呼んでほしい)
                gaugeManager.MaxSpeed();
            }

        }
        else
        {
            //回していない間は普通のスピードへ
            gaugeManager.NormalSpeed();
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (reach == true)
            {

                //ゲージの解放アニメーションを呼ぶ。
                gaugeManager.Release();
                //ゲージ
                gaugeManager.Amount(per);
                gs.Change(per);

                reach = false;
            }
            if (per > 0)
            {
                per -= 0.02f;
            }
            //ゲージ
            gaugeManager.Amount(per);
        }

    }
}
