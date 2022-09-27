using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	//シーンを変更する処理
	public static void OnChangeScene(SceneID sceneID) => instance.OnChangeSceneCore(sceneID);

	//ゲームオーバーになったか判定
	public static bool IsGameOver() => Input.GetKeyDown(KeyCode.R); //仮

	//プレイヤーがダメージを受けた時
	public static void OnPlayerDamage(int value) => instance.OnPlayerDamageCore(value);

	//ゲージの割合を取得する
	public static float GetGaugeRate() => instance.GetGaugeRateCore();

	//スコアを取得する
	public static int GetScore() => instance.score;

	//スコアを加算する
	public static void AddScore(int value) => instance.AddScoreCore(value);

	//スコアを設定する
	public static void SetScore(int value) => instance.SetScoreCore(value);

	//時止めを発動する
	public static void OnSpecial() => instance.OnSpecialCore();

	//時止めを発動できるか
	public static bool CanSpecial() => instance.CanSpecialCore();

	//時止めゲージを増加させる
	public static void AddSpecialGauge(int value) => instance.AddSpecialGaugeCore(value);

	//敵キャラを停止する用
	public static float TimeRate => instance.stopFlag.GetRate();

	//決定ボタンを押した
	public static bool GetSubmitButtonDown() => Input.GetButtonDown("Submit");

	//上ボタンを押した
	public static bool GetUpButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f;

	//下ボタンを押した
	public static bool GetDownButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f;

	//シングルトン
	private static GameManager _instance;
	private static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}

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


	//シーンの切り替え処理
	private void OnChangeSceneCore( SceneID sceneID )
	{
		this.sceneID = sceneID;
		if (currentScene != null)
		{
			//シーンの終了処理
			currentScene.OnExit();
		}

		//シーンを設定する
		switch (sceneID)
		{
			case SceneID.Title: currentScene = titleScene; break;
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
    public float GetGaugeRateCore()
    {
        return (float)specialValue / SPECIAL_MAX;
    }


	//プレイヤーがダメージを受けた時
	public void OnPlayerDamageCore(int value)
	{
		OnChangeScene(SceneID.Result);
	}

	//スコアを加算する
	private void AddScoreCore( int value )
    {
        score += value;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }

    //スコアを設定する
    public void SetScoreCore( int value )
    {
        score = value;
        if (score < 0) score = 0;
        if (score > SCORE_MAX) score = SCORE_MAX;
    }

	//時止めを発動する
	private void OnSpecialCore()
	{
		stopFlag.SetFlag(true);
	}

	//時止めを発動できるか
	public bool CanSpecialCore()
    {
        if (stopFlag.GetFlag()) return false;//時止め中は発動できない
        if (specialValue < SPECIAL_MAX) return false;　//ゲージが溜まってないときは発動不可
        return true;
    }

	//時止めゲージを増加させる
	private void AddSpecialGaugeCore( int value )
	{
		specialValue += value;
		if (specialValue > SPECIAL_MAX) specialValue = SPECIAL_MAX;
	}

}
