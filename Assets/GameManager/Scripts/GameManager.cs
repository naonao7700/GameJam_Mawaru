using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
	Standard,	//通常
	Clocks,		//時計
};

public class GameManager : MonoBehaviour
{
	//針の回転方法
	public static GameMode Mode;

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
	public static bool GetUpButtonDown() => (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0.0f) || ( Input.GetAxis("L_Horizontal") < 0.0f );

	//下ボタンを押した
	public static bool GetDownButtonDown() => (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0.0f) || (Input.GetAxis("L_Horizontal") > 0.0f);

	//時止めボタンを押した
	public static bool GetTimeStopButtonDown() => Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space);
	//public static bool GetTimeStopButtonDown() => Input.anyKeyDown && !Input.GetButtonDown("Horizontal");//Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Space);

	//ゲーム開始時の初期化処理
	public static void OnGameStart() => instance.OnGameStartCore();

	//時止め中か判定
	public static bool IsTimeStop() => instance.playerManager.stopFlag;

	//BGMを再生する
	public static void PlayBGM(AudioClip clip) => instance._PlayBGM(clip);

	//SEを再生する
	public static void PlaySE(AudioClip clip, float volume = 1.0f ) => instance._PlaySE(clip, volume );

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

		var player = GameObject.FindGameObjectWithTag("Player");
		if( player != null )
		{
			GameObject.Destroy(player);
		}

		var enemyManager = GameObject.FindObjectOfType<EnemyManeger>();
		if( enemyManager != null )
		{
			GameObject.Destroy(enemyManager.gameObject);
		}
	}

	//更新処理
	private void DoUpdateCore( float deltaTime )
	{
		playerManager.DoUpdate(deltaTime);
		timeManager.DoUpdate(deltaTime);

		sound.volume = Mathf.Lerp(0.1f, 1.0f, TimeRate);

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
	[SerializeField] private AudioSource seSound;

	public static GaugeObject GaugeObject => instance.gauge;
	[SerializeField] private GaugeObject gauge;//ゲージ


	//BGMを再生する
	private void _PlayBGM( AudioClip clip )
    {
		sound.clip = clip;
		sound.Play();
    }

	//SEを再生する
	private void _PlaySE( AudioClip clip, float volume = 1.0f )
    {
		seSound.PlayOneShot(clip, volume );
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
	[SerializeField] private CutInManager CutInManager;	//カットイン

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
		GameManager.PlaySE(timeStopSE1, 3.0f );
		GameManager.PlaySE(timeStopSE2 );

		//ゲージの演出変更
		GameManager.GaugeObject.Release();

		//カットイン制御
		CutInManager.OnStart();

		stopFlag = true;
		timeStop.SetFlag(true);
		timeStopTimer.Reset();
	}

	//プレイヤーがダメージを受けたとき
	public void OnDamage( int value )
	{
		if (stopFlag) return;
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
		return deathFlag && effect == null;
    }

	//更新処理
	public void DoUpdate( float deltaTime )
	{
#if UNITY_EDITOR
		if( Input.GetKeyDown(KeyCode.Y))
		{
			gauge.SetValue(60);
		}
		if( Input.GetKeyDown(KeyCode.I))
		{
			GameManager.OnPlayerDamage(100);
		}
#endif

		timeStop.DoUpdate(deltaTime);

		CutInManager.DoUpdate(deltaTime);

		if( stopFlag )
		{
			timeStopTimer.DoUpdate(deltaTime);
			gauge.SetRate( (1.0f - timeStopTimer.GetRate()) );
			if ( timeStopTimer.IsEnd() )
            {
				stopFlag = false;
				timeStop.SetFlag(false);
            }
		}
	}

	//時止めゲージ
	public GaugeManager gauge;

	//時止め演出用
	public TimeStop timeStop;

	//初期化処理
	public void Reset()
	{
		CutInManager.Reset();
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

	[SerializeField] private float prevValue;	//前回の値
	[SerializeField] private float threshold;	//閾値

	//現在の値
	public float value;

	//ゲージの最大値
	[SerializeField]public float GAUGE_MAX = 70;

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
		//前回の量を保持
		prevValue = this.value;

		if (value < 0) value = 0;
		else if (value > GAUGE_MAX) value = GAUGE_MAX;
		this.value = value;

		if( this.value - prevValue > threshold )
		{
			GameManager.GaugeObject.MaxSpeed();
		}
		else
		{
			GameManager.GaugeObject.NormalSpeed();
		}
	}

	public void SetRate( float rate )
	{
		this.value = rate * GAUGE_MAX;
		if (value < 0) value = 0;
		if (value > GAUGE_MAX) value = GAUGE_MAX;
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

//カットインの制御
[System.Serializable]
public class CutInManager
{
	[SerializeField] private UnityEngine.UI.Image image;
	[SerializeField] private Vector3 startPos;
	[SerializeField] private Vector3 endPos;

	[SerializeField] private Vector3 startScale;
	[SerializeField] private Vector3 endScale;

	[SerializeField] private float[] times;

	public Timer timer;
	public int step;
	public bool activeFlag;

	public void Reset()
	{
		activeFlag = false;
		image.gameObject.SetActive(false);
	}

	//カットイン演出開始
	public void OnStart()
	{
		step = 0;
		timer.Reset( times[0], 0.0f );
		image.gameObject.SetActive(true);
		image.transform.position = startPos;
		image.transform.localScale = startScale;
		image.color = Color.white;
		activeFlag = true;
	}

	//更新処理
	public void DoUpdate( float deltaTime )
	{
		if (!activeFlag) return;
		timer.DoUpdate(deltaTime);
		var t = timer.GetRate();
		if( timer.IsEnd() )
		{
			timer.Reset(times[step], 0.0f );
			step++;
			if( step >= times.Length )
			{
				image.gameObject.SetActive(false);
				activeFlag = false;
			}
		}
		switch( step )
		{
			case 0:
				{
					//スライドイン
					image.rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
					break;
				}
			case 1:
				{
					//待機
					break;
				}
			case 2:
				{
					//拡大&透明化
					image.transform.localScale = Vector3.Lerp(startScale, endScale, t);
					var color = image.color;
					color.a = Mathf.Lerp(1.0f, 0.0f, t);
					image.color = color;
					break;
				}
		}
	}

}