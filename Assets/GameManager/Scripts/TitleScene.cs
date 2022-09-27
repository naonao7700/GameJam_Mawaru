using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//タイトルシーン
public class TitleScene : MonoBehaviour, IScene
{
    //タイトルシーンの初期化
    public void OnEnter()
    {
        gameObject.SetActive(true);
    }

    //タイトルシーンの更新
    public void DoUpdate()
    {
        //ボタンを押したとき
        if( GameManager.GetSubmitButtonDown() )
        {
            //ゲームシーンに遷移する
            GameManager.OnChangeScene(SceneID.Game);
        }
    }

    //タイトルシーンの終了処理
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
