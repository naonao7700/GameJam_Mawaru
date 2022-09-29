using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//リザルトシーン
public class ResultScene : MonoBehaviour, IScene
{
    //リザルトBGM
    [SerializeField] private AudioClip bgm;

    //効果音
    [SerializeField] private AudioClip cursorSE;    //カーソル移動
    [SerializeField] private AudioClip submitSE;    //決定音

    //カーソルの現在位置
    [SerializeField] private int cursorPos;

    //カーソル画像リスト
    [SerializeField] private GameObject[] cursorImage;

	//スコアテキスト
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

	//遷移演出タイマー
	[SerializeField] private Timer startTimer;
	[SerializeField] private AnimationCurve curve;

	[SerializeField] private RectTransform buttons;	//ボタン
	[SerializeField] private Image backImage;   //背景の黒色

	[SerializeField] private Vector3 buttonHidePos;
	[SerializeField] private Vector3 buttonViewPos;

    //遷移フラグ
    public bool endFlag;
    public Timer endTimer;
    [SerializeField] private FadeImage fade;

    //リザルトシーンの初期化
    public void OnEnter()
    {
        endFlag = false;

		startTimer.Reset();

        //ハイスコアを更新する
        GameManager.UpdateHighScore();

        scoreText.text = "スコア：" + GameManager.GetScore();
        highScoreText.text = "ハイスコア：" + GameManager.GetHighScore();

		//遷移前の座標へ移動
		var color = Color.white;
		color.a = 0.0f;
		scoreText.color = color;
		highScoreText.color = color;
		color = backImage.color;
		color.a = 0.0f;
		backImage.color = color;
		buttons.anchoredPosition = buttonHidePos;

        //リザルト画面を表示する
        gameObject.SetActive(true);

        //カーソル位置を初期化する
        SetCursorPos(0);

        //BGMを再生する
        GameManager.PlayBGM(bgm);
    }

    //カーソル位置を設定する
    void SetCursorPos( int index )
    {
        //変数を代入
        cursorPos = index;

        //画像の表示を更新する
        for (int i = 0; i < cursorImage.Length; ++i)
        {
            cursorImage[i].SetActive(i == cursorPos);
        }
    }

    //リザルトシーンの更新
    public void DoUpdate()
    {
        if( endFlag )
        {
            endTimer.DoUpdate(Time.deltaTime);
            var t = curve.Evaluate( endTimer.GetRate());
            t = 1.0f - t;
            var color = backImage.color;
            color.a = Mathf.Lerp(0.0f, 0.35f, t);
            backImage.color = color;

            color = scoreText.color;
            color.a = Mathf.Lerp(0.0f, 1.0f, t);
            scoreText.color = color;
            highScoreText.color = color;

            buttons.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

            if ( endTimer.IsEnd() )
            {
                fade.SetFade(false);

                GameManager.OnDeleteAllEnemy();

                //カーソル位置によって分岐する
                if (cursorPos == 0)
                {
                    //ゲームシーンへ遷移する
                    GameManager.OnChangeScene(SceneID.Game);
                }
                else
                {
                    //タイトルシーンへ遷移する
                    GameManager.OnChangeScene(SceneID.Title);
                }
            }
            return;
        }

		if( !startTimer.IsEnd() )
		{
			startTimer.DoUpdate(Time.deltaTime);
			var t = curve.Evaluate(startTimer.GetRate());
			var color = backImage.color;
			color.a = Mathf.Lerp(0.0f, 0.35f, t);
			backImage.color = color;

			color = scoreText.color;
			color.a = Mathf.Lerp(0.0f, 1.0f, t);
			scoreText.color = color;
			highScoreText.color = color;

			buttons.anchoredPosition = Vector3.Lerp(buttonHidePos, buttonViewPos, t);

			return;
		}

        //カーソルの移動処理
        int cursor = cursorPos;
        if( GameManager.GetUpButtonDown() )
        {
            //カーソルを上へ動かす
            cursor--;
            if (cursor < 0) cursor = 0;
            //if (cursor < 0) cursor = cursorImage.Length - 1;    //ループ対応
        }
        else if( GameManager.GetDownButtonDown() )
        {
            //カーソルを下へ動かす
            cursor++;
            if (cursor >= cursorImage.Length) cursor = cursorImage.Length - 1;
            //if (cursor >= cursorImage.Length ) cursor = 0;  //ループ対応
        }

        //カーソル位置を更新する
        if( cursor != cursorPos )
        {
            GameManager.PlaySE(cursorSE);
            SetCursorPos(cursor);
        }

        //ボタンを押したとき
        if ( GameManager.GetSubmitButtonDown() )
        {
            GameManager.PlaySE(submitSE);
            endFlag = true;
            endTimer.Reset();
            fade.SetFade(true);

        }
    }

    //リザルトシーンの終了処理
    public void OnExit()
    {
        //リザルト画面を非表示にする
        gameObject.SetActive(false);
    }

}
