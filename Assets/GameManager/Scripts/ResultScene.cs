using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//リザルトシーン
public class ResultScene : MonoBehaviour, IScene
{
    //カーソルの現在位置
    [SerializeField] private int cursorPos;

    //カーソル画像リスト
    [SerializeField] private GameObject[] cursorImage;

    //リザルトシーンの初期化
    public void OnEnter()
    {
        //リザルト画面を表示する
        gameObject.SetActive(true);

        //カーソル位置を初期化する
        SetCursorPos(0);
    }

    //カーソル位置を設定する
    void SetCursorPos( int index )
    {
        //変数を代入
        cursorPos = index;

        //画像の表示を更新する
        for (int i = 0; i < cursorImage.Length; ++i)
        {
            cursorImage[i].SetActive(i == cursorPos);
        }
    }

    //リザルトシーンの更新
    public void DoUpdate()
    {
        //カーソルの移動処理
        int cursor = cursorPos;
        if( GameManager.GetUpButtonDown() )
        {
            //カーソルを上へ動かす
            cursor--;
            if (cursor < 0) cursor = cursorImage.Length - 1;    //ループ対応
        }
        else if( GameManager.GetDownButtonDown() )
        {
            //カーソルを下へ動かす
            cursor++;
            if (cursor >= cursorImage.Length ) cursor = 0;  //ループ対応
        }

        //カーソル位置を更新する
        if( cursor != cursorPos )
        {
            SetCursorPos(cursor);
        }

        //ボタンを押したとき
        if ( GameManager.GetSubmitButtonDown() )
        {
            //カーソル位置によって分岐する
            if( cursorPos == 0 )
            {
                //ゲームシーンへ遷移する
                GameManager.OnChangeScene(SceneID.Game);
            }
            else
            {
                //タイトルシーンへ遷移する
                GameManager.OnChangeScene(SceneID.Title);
            }
        }
    }

    //リザルトシーンの終了処理
    public void OnExit()
    {
        //リザルト画面を非表示にする
        gameObject.SetActive(false);
    }

}
