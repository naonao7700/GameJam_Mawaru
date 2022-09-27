using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームシーン
public class GameScene : MonoBehaviour, IScene
{
    //ゲームマネージャ
    [SerializeField] private GameManager game;

    //UI
    [SerializeField] private GameObject ui;
    [SerializeField] private gaugeScript gauge; //ゲージ

    //インスタンス生成用のPrefab参照設定
    [SerializeField] private GameObject playerPrefab;    //プレイヤーのPrefab参照
    [SerializeField] private GameObject enemyPrefab;    //敵キャラのPrefab参照


    //インスタンスの参照
    [SerializeField] private GameObject playerObject;   //プレイヤー
    [SerializeField] private GameObject enemyObject;    //敵キャラスポナー

    //ゲームシーンの初期化
    public void OnEnter()
    {
        //UIを表示する
        ui.SetActive(true);

        foreach( var enemy in GameObject.FindGameObjectsWithTag("Enemy") )
        {
            GameObject.Destroy(enemy.gameObject);
        }

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

    }

    //ゲームシーンの更新
    public void DoUpdate()
    {
        //ゲームオーバーになったとき
        if( game.IsGameOver() )
        {
            //リザルトシーンへ遷移する
            game.OnChangeScene(SceneID.Result);
        }

        //ゲージの更新
        gauge.Change(game.GetGaugeRate());
    }

    //ゲームシーンの終了処理
    public void OnExit()
    {
        //UIを非表示にする
        ui.SetActive(false);
    }
}
