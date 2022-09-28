using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//タイトルシーン
public class TitleScene : MonoBehaviour, IScene
{
    [SerializeField] private AudioClip startSE;

    //タイトルシーンの初期化
    public void OnEnter()
    {
        gameObject.SetActive(true);
		GameManager.OnDeleteAllEnemy();
    }

    //タイトルシーンの更新
    public void DoUpdate()
    {
        //ボタンを押したとき
        if( GameManager.GetSubmitButtonDown() )
        {
            //SEを再生する
            GameManager.PlaySE(startSE);

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
