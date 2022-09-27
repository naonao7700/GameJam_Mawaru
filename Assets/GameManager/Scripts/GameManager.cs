using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //シーンの参照設定(Inspecterで設定)
    [SerializeField] private TitleScene titleScene;
    [SerializeField] private GameScene gameScene;
    [SerializeField] private ResultScene resultScene;

    private IScene currentScene;    //現在のシーン
    public SceneID sceneID;    //現在のシーンID

    //初期化処理
    private void Start()
    {
        titleScene.OnExit();
        gameScene.OnExit();
        resultScene.OnExit();

        //タイトルシーンを最初に実行する
        OnChangeScene(SceneID.Title);
    }

    //更新処理
    private void Update()
    {
        //現在のシーンを更新する
        currentScene.DoUpdate();
    }

    //ゲームオーバーになったか判定
    public bool IsGameOver()
    {
        return Input.GetKeyDown(KeyCode.R);
        //return GetSubmitButtonDown();
    }

    //シーンを変更する処理
    public void OnChangeScene(SceneID sceneID)
    {
        this.sceneID = sceneID;
        if ( currentScene != null )
        {
            //シーンの終了処理
            currentScene.OnExit();
        }

        //シーンを設定する
        switch (sceneID)
        {
            case SceneID.Title:　currentScene = titleScene; break;
            case SceneID.Game: currentScene = gameScene; break;
            case SceneID.Result: currentScene = resultScene; break;
        }

        //シーンの初期化処理
        currentScene.OnEnter();
    }

    //ゲームのスコア
    public int score;
    public const int SCORE_MAX = 9999;

    //時止め中フラグ
    public FlagTimer stopFlag;

    //時止めゲージ
    public int specialValue;
    public const int SPECIAL_MAX = 120;
    public float GetGaugeRate()
    {
        return (float)specialValue / SPECIAL_MAX;
    }

    //プレイヤーがダメージを受けた時
    public void OnPlayerDamage( int value )
    {
        OnChangeScene(SceneID.Result);
    }

    //スコアを取得する
    public int GetScore()
    {
        return score;
    }

    //スコアを加算する
    public void AddScore( int value )
    {
        score += value;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }

    //スコアを設定する
    public void SetScore( int value )
    {
        score = value;
        if (score < 0) score = 0;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }


    //時止めを発動する
    public void OnSpecial()
    {
        stopFlag.SetFlag(true);
    }

    //時止めを発動できるか
    public bool CanSpecial()
    {
        if (stopFlag.GetFlag()) return false;//時止め中は発動できない
        if (specialValue < SPECIAL_MAX) return false;　//ゲージが溜まってないときは発動不可
        return true;
    }

    //時止めゲージを加算する
    public void AddSpecialGauge( int value )
    {

    }

    //敵キャラを停止する用
    public float TimeRate => stopFlag.GetRate();
    

    //決定ボタンを押した
    public bool GetSubmitButtonDown()
    {
        return Input.GetButtonDown("Submit");
    }

    //上ボタンを押した
    public bool GetUpButtonDown()
    {
        return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f;
    }

    //下ボタンを押した
    public bool GetDownButtonDown()
    {
        return Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f;
    }
}
