using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//タイトルシーン
public class TitleScene : MonoBehaviour, IScene
{
    //ゲームマネージャ
    [SerializeField] private GameManager game;

    //タイトルシーンの初期化
    public void OnEnter()
    {
        gameObject.SetActive(true);
    }

    //タイトルシーンの更新
    public void DoUpdate()
    {
        //ボタンを押したとき
        if( game.GetSubmitButtonDown() )
        {
            //ゲームシーンに遷移する
            game.OnChangeScene(SceneID.Game);
        }
    }

    //タイトルシーンの終了処理
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
