using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//タイトルシーン
public class TitleScene : MonoBehaviour, IScene
{
    //タイトルBGM
    [SerializeField] private AudioClip bgm;

    //スタート効果音
    [SerializeField] private AudioClip startSE;

	[SerializeField] private Image logo;
	[SerializeField] private RectTransform button;

	[SerializeField] private GameObject chara;	//プレイヤーキャラ

	//開始時の演出タイマー
	public Timer startTimer;

	//ロゴの座標
	public Vector3 logoHidePos;
	public Vector3 logoViewPos;

	//ボタンの座標
	public Vector3 buttonHidePos;
	public Vector3 buttonViewPos;

	public bool startFlag;

    //タイトルシーンの初期化
    public void OnEnter()
    {
		chara.gameObject.SetActive(true);

		startFlag = false;

		startTimer.Reset();

        gameObject.SetActive(true);
		GameManager.OnDeleteAllEnemy();

        //BGMを再生する
        GameManager.PlayBGM(bgm);
    }

    //タイトルシーンの更新
    public void DoUpdate()
    {
		startTimer.DoUpdate(Time.deltaTime);
		var t = startTimer.GetRate();
		t = t * t * (3.0f - 2.0f * t);
		var color = logo.color;
		if (startTimer.IsEnd()) t = 1.0f;

		if ( startFlag )
		{
			t = 1.0f - t;
			logo.rectTransform.anchoredPosition = Vector3.Lerp(logoHidePos, logoViewPos, t);
			color.a = Mathf.Lerp(0.0f, 1.0f, t);
			logo.color = color;

			button.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

			if ( startTimer.IsEnd() )
			{
				//ゲームシーンに遷移する
				GameManager.OnChangeScene(SceneID.Game);
			}
			return;
		}

		logo.rectTransform.anchoredPosition = Vector3.Lerp(logoHidePos, logoViewPos, t);
		color.a = Mathf.Lerp(0.0f, 1.0f, t);
		logo.color = color;

		button.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);


		if (!startTimer.IsEnd()) return;
        //ボタンを押したとき
        if( GameManager.GetSubmitButtonDown() )
        {
            //SEを再生する
            GameManager.PlaySE(startSE);

			startFlag = true;
			startTimer.Reset();
        }
    }

    //タイトルシーンの終了処理
    public void OnExit()
    {
        gameObject.SetActive(false);
		chara.SetActive(false);
    }
}
