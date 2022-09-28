using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームシーン
public class GameScene : MonoBehaviour, IScene
{
    //UI
    [SerializeField] private GameObject ui;

	[SerializeField] private Text score;	//スコア
	[SerializeField] private Text time;	//経過時間


    //インスタンス生成用のPrefab参照設定
    [SerializeField] private GameObject playerPrefab;    //プレイヤーのPrefab参照
    [SerializeField] private GameObject enemyPrefab;    //敵キャラのPrefab参照


    //インスタンスの参照
    [SerializeField] private GameObject playerObject;   //プレイヤー
    [SerializeField] private GameObject enemyObject;    //敵キャラスポナー

    [SerializeField] private AudioClip[] bgmList;

    //ゲームシーンの初期化
    public void OnEnter()
    {
        //BGMをランダムで再生する
        int rand = Random.Range(0, bgmList.Length);
        GameManager.PlayBGM(bgmList[rand]);

		//ゲーム開始時の初期化処理
		GameManager.OnGameStart();

		//UIを表示する
		ui.SetActive(true);

        //プレイヤーが既に生成してあるなら消す
        if( playerObject != null )
        {
            GameObject.Destroy(playerObject);
        }
        //プレイヤーの生成
        playerObject = GameObject.Instantiate(playerPrefab);

        //敵キャラが既に生成してあるなら消す
        if (enemyObject != null)
        {
            GameObject.Destroy(enemyObject);
        }

        //敵キャラの生成
        enemyObject = GameObject.Instantiate(enemyPrefab);

        //ゲージの初期化処理
        GameManager.GaugeObject.Initialize();
    }

    //ゲームシーンの更新
    public void DoUpdate()
    {
        if( !GameManager.IsPlayerDeath() )
        {
            //データの更新処理
            GameManager.DoUpdate(Time.deltaTime);
        }

        //ゲームオーバーになったとき
        if ( GameManager.IsGameOver() )
        {
            //リザルトシーンへ遷移する
            GameManager.OnChangeScene(SceneID.Result);
        }

		//経過時間を更新
		time.text = GameManager.GetTimeText();

		//スコアを更新
		score.text = "Score:" + GameManager.GetScore().ToString("D8");

        //ゲージの更新
        GameManager.GaugeObject.Amount(GameManager.GetGaugeRate());
    }

    //ゲームシーンの終了処理
    public void OnExit()
    {
    }
}
