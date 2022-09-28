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

	//プレイヤーが死んだフラグを取得する
	public static bool IsPlayerDeath() => instance.playerManager.deathFlag;

	//ゲームオーバーになったか判定
	public static bool IsGameOver() => instance.playerManager.GetDeathFlag();

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

	//ハイスコアを更新する
	public static void UpdateHighScore() => instance.scoreManager.UpdateHighScore();

	//ハイスコアを取得する
	public static int GetHighScore() => instance.scoreManager.highScore;

	//経過時間を取得する
	public static string GetTimeText() => instance.timeManager.GetText();

	//時止めを発動する
	public static void OnTimeStop() => instance.playerManager.OnTimeStop();

	//時止めを発動できるか
	public static bool CanTimeStop() => instance.playerManager.CanTimeStop();

	//時止めゲージを増加させる
	public static void AddTimeStopGauge(float value) => instance.playerManager.gauge.AddValue(value);

	//敵キャラを停止する用
	public static float TimeRate => 1.0f - instance.playerManager.timeStop.range;

	//決定ボタンを押した
	public static bool GetSubmitButtonDown() => GetTimeStopButtonDown();

	//上ボタンを押した
	public static bool GetUpButtonDown() => (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0.0f) || ( Input.GetAxis("L_Vertical") < 0.0f );

	//下ボタンを押した
	public static bool GetDownButtonDown() => (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0.0f) || (Input.GetAxis("L_Vertical") > 0.0f);

	//時止めボタンを押した
	public static bool GetTimeStopButtonDown() => Input.anyKeyDown;//Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space);

	//ゲーム開始時の初期化処理
	public static void OnGameStart() => instance.OnGameStartCore();

	//時止め中か判定
	public static bool IsTimeStop() => instance.playerManager.stopFlag;

	//BGMを再生する
	public static void PlayBGM(AudioClip clip) => instance._PlayBGM(clip);

	//SEを再生する
	public static void PlaySE(AudioClip clip) => instance._PlaySE(clip);

	//エフェクトを再生する
	public static GameObject PlayEffect(GameObject prefab, Vector3 position) => instance._PlayEffect(prefab, position, Quaternion.identity);
	public static GameObject PlayEffect(GameObject prefab, Vector3 position, Quaternion rotation) => instance._PlayEffect(prefab, position, rotation);

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

		foreach( var bullet in GameObject.FindObjectsOfType<Bullet>() )
        {
			GameObject.Destroy(bullet.gameObject);
        }
	}

	//更新処理
	private void DoUpdateCore( float deltaTime )
	{
		playerManager.DoUpdate(deltaTime);
		timeManager.DoUpdate(deltaTime);

		//時止めが可能か判定
		if (CanTimeStop())
        {
			//時止めボタンを押したとき
			if (GetTimeStopButtonDown())
			{
				//時止め発動
				OnTimeStop();
			}
		}
	}

	[SerializeField] private AudioSource sound;

	public static GaugeObject GaugeObject => instance.gauge;
	[SerializeField] private GaugeObject gauge;//ゲージ


	//BGMを再生する
	private void _PlayBGM( AudioClip clip )
    {
		sound.clip = clip;
		sound.Play();
    }

	//SEを再生する
	private void _PlaySE( AudioClip clip )
    {
		sound.PlayOneShot(clip);
    }

	//エフェクトを生成する
	private GameObject _PlayEffect( GameObject prefab, Vector3 position, Quaternion rotation )
    {
		return GameObject.Instantiate(prefab, position, rotation);
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
			if (minute > 99)
			{
				minute = 99;
				seconds = 59.0f;
			}
			else
            {
				seconds = seconds - 60.0f;
			}
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
	[SerializeField] private GameObject deathEffectPrefab;  //死亡時のエフェクト

	private GameObject effect;

	//現在のHP
	public int hpValue;

	//死亡したフラグ
	public bool deathFlag;

	//時止めフラグ
	public bool stopFlag;

	public Timer timeStopTimer;

	//時止めが使えるか判定
	public bool CanTimeStop()
	{
		if (deathFlag) return false;    //死亡後は使えない
		if (stopFlag ) return false;      //時止め中は使えない
		if (gauge.GetRate() < 1.0f) return false;	//ゲージがたまってないと使えない
		return true;
	}

	//効果音指定
	[SerializeField] private AudioClip timeStopSE1;
	[SerializeField] private AudioClip timeStopSE2;
	[SerializeField] private AudioClip damageSE;

	//時止め発動
	public void OnTimeStop()
	{
		//効果音鳴らす
		GameManager.PlaySE(timeStopSE1);
		GameManager.PlaySE(timeStopSE2);

		GameManager.GaugeObject.Release();

		stopFlag = true;
		timeStop.SetFlag(true);
		timeStopTimer.Reset();
	}

	//プレイヤーがダメージを受けたとき
	public void OnDamage( int value )
	{
		if (deathFlag) return;

		hpValue -= value;
		if (hpValue < 0) hpValue = 0;
		if( hpValue <= 0 )
		{
			GameManager.PlaySE(damageSE);
			effect = GameManager.PlayEffect(deathEffectPrefab, new Vector3(0,0, -3));
			deathFlag = true;
		}
	}

	public bool GetDeathFlag()
    {
		return
			deathFlag && effect == null;
    }

	//更新処理
	public void DoUpdate( float deltaTime )
	{
		timeStop.DoUpdate(deltaTime);

		if( stopFlag )
		{
			timeStopTimer.DoUpdate(deltaTime);
			gauge.SetValue( (int)( (1.0f - timeStopTimer.GetRate()) * GaugeManager.GAUGE_MAX) );
			if ( timeStopTimer.IsEnd() )
            {
				stopFlag = false;
				timeStop.SetFlag(false);
            }

			//gauge.AddValue(-1);
			//if( gauge.GetRate() <= 0.0f )
			//{
			//	stopFlag = false;
			//	timeStop.SetFlag(false);
			//}
		}
	}

	//時止めゲージ
	public GaugeManager gauge;

	//時止め演出用
	public TimeStop timeStop;

	//初期化処理
	public void Reset()
	{
		timeStopTimer = new Timer(3.0f);

		//時止め演出リセット
		timeStop.Reset();

		//HPをリセット
		hpValue = 1;

		//時止めフラグをOFF
		stopFlag = false;

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
	//ゲージMAX時の効果音
	[SerializeField] private AudioClip gaugeMaxSE;

	//現在の値
	public float value;

	//ゲージの最大値
	public const float GAUGE_MAX = 70;

	//ゲージを加算する
	public void AddValue( float value )
	{
		//効果音鳴らす
		if (this.value < GAUGE_MAX)
        {
			if( this.value + value >= GAUGE_MAX )
            {
				GameManager.PlaySE(gaugeMaxSE);
				GameManager.GaugeObject.ReachGauge();
            }
        }

		SetValue(this.value + value);
	}

	//ゲージの数値を設定する
	public void SetValue( float value )
	{
		if (value < 0) value = 0;
		else if (value > GAUGE_MAX) value = GAUGE_MAX;
		this.value = value;
	}

	//ゲージの割合を取得する
	public float GetRate()
	{
		var t = (float)value / GAUGE_MAX;
		if (t > 1.0f) t = 1.0f;
		return t;
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
	public const int SCORE_MAX = 99999999;

	public void UpdateHighScore()
    {
		if( highScore < score )
        {
			highScore = score;
        }
    }

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


