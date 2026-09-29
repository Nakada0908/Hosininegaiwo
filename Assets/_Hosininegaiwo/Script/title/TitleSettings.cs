using System;
using Naninovel;
using UnityEngine;

public class TitleSettings : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private string nextSceneName;
    private bool startingGame;

    private void Start()
    {
        if (bgm != null)
        {
            SoundManager.Instance.PlayBGM(bgm);
        }
    }

    public async void OnClickStartButton()
    {
        if (startingGame) return;
        startingGame = true;

        try
        {
            await RuntimeInitializer.Initialize();
            if (!this || !isActiveAndEnabled) return;

            await Engine.GetServiceOrErr<IStateManager>().ResetState();
            if (!this || !isActiveAndEnabled) return;

            NovelGameStarter.nextScript = null;
            GameRoundCounter.StartFirstGame();
            MySceneManager.Instance.ChangeScene(nextSceneName);
        }
        catch (Exception exception)
        {
            startingGame = false;
            Debug.LogException(exception);
        }
    }
}
