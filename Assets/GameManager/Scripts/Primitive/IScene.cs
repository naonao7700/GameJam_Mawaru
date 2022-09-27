using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーンのインターフェイス
public interface IScene
{
    //シーンに入ったときの処理
    void OnEnter();

    //シーンの更新処理
    void DoUpdate();

    //シーンを終了するときの処理
    void OnExit();
}