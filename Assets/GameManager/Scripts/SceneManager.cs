using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーン管理クラス
public class SceneManager : MonoBehaviour
{
	//シーンの参照設定(Inspecterで設定)
	[SerializeField] private TitleScene titleScene;
	[SerializeField] private GameScene gameScene;
	[SerializeField] private ResultScene resultScene;

	private IScene currentScene;    //現在のシーン
	public SceneID sceneID;    //現在のシーンID

	private void Start()
	{
		//初期化
		titleScene.OnExit();
		gameScene.OnExit();
		resultScene.OnExit();

		//タイトルシーンを最初に実行する
		OnChangeScene(SceneID.Title);
	}

	//更新処理
	private void Update()
	{
		//現在のシーンを更新する
		currentScene.DoUpdate();

		gameScene.UIUpdate(Time.deltaTime);
	}

	//シーンの切り替え処理
	public void OnChangeScene(SceneID sceneID)
	{
		this.sceneID = sceneID;
		if (currentScene != null)
		{
			//シーンの終了処理
			currentScene.OnExit();
		}

		//シーンを設定する
		switch (sceneID)
		{
			case SceneID.Title:
				{
					currentScene = titleScene;
					gameScene.uiTimer.SetFlag(false);
					break;
				}
			case SceneID.Game: currentScene = gameScene; break;
			case SceneID.Result: currentScene = resultScene; break;
		}

		//シーンの初期化処理
		currentScene.OnEnter();
	}
}
