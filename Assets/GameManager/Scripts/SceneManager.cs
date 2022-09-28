using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V�[���Ǘ��N���X
public class SceneManager : MonoBehaviour
{
	//�V�[���̎Q�Ɛݒ�(Inspecter�Őݒ�)
	[SerializeField] private TitleScene titleScene;
	[SerializeField] private GameScene gameScene;
	[SerializeField] private ResultScene resultScene;

	private IScene currentScene;    //���݂̃V�[��
	public SceneID sceneID;    //���݂̃V�[��ID

	private void Start()
	{
		//������
		titleScene.OnExit();
		gameScene.OnExit();
		resultScene.OnExit();

		//�^�C�g���V�[�����ŏ��Ɏ��s����
		OnChangeScene(SceneID.Title);
	}

	//�X�V����
	private void Update()
	{
		//���݂̃V�[�����X�V����
		currentScene.DoUpdate();

		gameScene.UIUpdate(Time.deltaTime);
	}

	//�V�[���̐؂�ւ�����
	public void OnChangeScene(SceneID sceneID)
	{
		this.sceneID = sceneID;
		if (currentScene != null)
		{
			//�V�[���̏I������
			currentScene.OnExit();
		}

		//�V�[����ݒ肷��
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

		//�V�[���̏���������
		currentScene.OnEnter();
	}
}
