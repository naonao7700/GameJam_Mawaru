using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static void DoUpdate(float deltaTime) => instance.DoUpdateCore(deltaTime);

	//シーンを変更する処理
	public static void OnChangeScene(SceneID sceneID) => instance.sceneManager.OnChangeScene(sceneID);

	//敵キャラを全て消す
	public static void OnDeleteAllEnemy() => instance.OnDeleteAllEnemyCore();

	//ゲームオーバーになったか判定
	public static bool IsGameOver() => instance.playerManager.deathFlag;

	//プレイヤーがダメージを受けた時
	public static void OnPlayerDamage(int value) => instance.playerManager.OnDamage(value);

	//ゲージの割合を取得する
	public static float GetGaugeRate() => instance.playerManager.gauge.GetRate();

	//スコアを取得する
	public static int GetScore() => instance.scoreManager.score;

	//スコアを加算する
	public static void AddScore(int value) => instance.scoreManager.AddScore(value);

	//スコアを設定する
	public static void SetScore(int value) => instance.scoreManager.SetScore(value);

	//経過時間を取得する
	public static string GetTimeText() => instance.timeManager.GetText();

	//時止めを発動する
	public static void OnSpecial() => instance.playerManager.stopFlag.SetFlag(true);

	//時止めを発動できるか
	public static bool CanSpecial() => instance.playerManager.CanSpecial();

	//時止めゲージを増加させる
	public static void AddSpecialGauge(int value) => instance.playerManager.gauge.AddValue(value);

	//敵キャラを停止する用
	public static float TimeRate => instance.playerManager.stopFlag.GetRate();

	//決定ボタンを押した
	public static bool GetSubmitButtonDown() => Input.GetButtonDown("Submit");

	//上ボタンを押した
	public static bool GetUpButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f;

	//下ボタンを押した
	public static bool GetDownButtonDown() => Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f;

	//ゲーム開始時の初期化処理
	public static void OnGameStart() => instance.OnGameStartCore();

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

	//シーン管理
	public SceneManager sceneManager;

	//プレイヤー管理
	public PlayerManager playerManager;

	//スコア管理
	public ScoreManager scoreManager;

	//経過時間
	public TimeManager timeManager;

	//ゲーム開始時の初期化処理
	private void OnGameStartCore()
	{
		//スコアをリセット
		scoreManager.SetScore(0);

		//経過時間をリセット
		timeManager.Reset();

		//プレイヤーのHPをリセット
		playerManager.Reset();

		//敵キャラを全て消す
		OnDeleteAllEnemyCore();
	}

	//敵キャラを全て消す
	private void OnDeleteAllEnemyCore()
	{
		//生存している敵キャラを全て消す
		foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			GameObject.Destroy(enemy.gameObject);
		}
	}

	//更新処理
	private void DoUpdateCore( float deltaTime )
	{
		playerManager.DoUpdate(deltaTime);
		timeManager.DoUpdate(deltaTime);
	}

}

//経過時間管理
[System.Serializable]
public class TimeManager
{
	public float seconds;	//秒
	public int minute;		//分

	//初期化
	public void Reset()
	{
		seconds = 0.0f;
		minute = 0;
	}

	//更新処理
	public void DoUpdate( float deltaTime )
	{
		seconds += deltaTime;
		if( seconds >= 60.0f )
		{
			minute++;
			seconds = seconds - 60.0f;
		}
	}

	//表示文を取得する
	public string GetText()
	{
		return $"{minute.ToString("00")}:{((int)seconds).ToString("00")}";
	}
}

//プレイヤー管理
[System.Serializable]
public class PlayerManager
{
	//現在のHP
	public int hpValue;

	//死亡したフラグ
	public bool deathFlag;

	//時止めフラグ
	public FlagTimer stopFlag;

	//時止めが使えるか判定
	public bool CanSpecial()
	{
		if (deathFlag) return false;    //死亡後は使えない
		if (stopFlag.GetFlag() ) return false;      //時止め中は使えない
		if (gauge.GetRate() < 1.0f) return false;	//ゲージがたまってないと使えない
		return true;
	}

	//プレイヤーがダメージを受けたとき
	public void OnDamage( int value )
	{
		hpValue -= value;
		if (hpValue < 0) hpValue = 0;
		if( hpValue <= 0 )
		{
			deathFlag = true;
		}
	}

	//更新処理
	public void DoUpdate( float deltaTime )
	{
		stopFlag.DoUpdate(deltaTime);
	}

	//時止めゲージ
	public GaugeManager gauge;

	//初期化処理
	public void Reset()
	{
		//HPをリセット
		hpValue = 1;

		//時止めフラグをOFF
		stopFlag = new FlagTimer(10);

		//死亡したフラグをOFF
		deathFlag = false;

		//ゲージの初期化
		gauge.SetValue(0);
	}

}

//ゲージ管理
[System.Serializable]
public class GaugeManager
{
	//現在の値
	public int value;

	//ゲージの最大値
	public const int GAUGE_MAX = 100;

	//ゲージを加算する
	public void AddValue( int value )
	{
		SetValue(this.value + value);
	}

	//ゲージの数値を設定する
	public void SetValue( int value )
	{
		if (value < 0) value = 0;
		else if (value > GAUGE_MAX) value = GAUGE_MAX;
		this.value = value;
	}

	//ゲージの割合を取得する
	public float GetRate()
	{
		return (float)value / GAUGE_MAX;
	}

}

//スコア管理
[System.Serializable]
public class ScoreManager
{
	//現在のスコア
	public int score;

	//ハイスコア記録
	public int highScore;

	//スコアの最大値
	public const int SCORE_MAX = 9999;

	//スコアを加算する
	public void AddScore( int value )
	{
		SetScore(score + value);
	}

	//スコアの数値を設定する
	public void SetScore( int value )
	{
		score = value;
		if (score < 0) score = 0;
		if (score > SCORE_MAX) score = SCORE_MAX;
	}
}


