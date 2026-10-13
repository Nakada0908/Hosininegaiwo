using System;
using Naninovel;
using UnityEngine;

public class TitleSettings : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private string nextSceneName;
    private bool startingGame;

    private async void Start()
    {
        if (bgm != null)
        {
            SoundManager.Instance.PlayBGM(bgm);
        }

        //naninovelÇÃèâä˙âªÇë“Ç¬
        try
        {
            await RuntimeInitializer.Initialize();
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    public async void OnClickStartButton()
    {
        if (MySceneManager.Instance.isLoading) return;
        if (startingGame) return;
        startingGame = true;

        try
        {
            await Engine.GetServiceOrErr<IStateManager>().ResetState();
            if (!this || !isActiveAndEnabled) return;

            NovelGameStarter.nextScript = null;
            GameRoundCounter.StartFirstGame();
            //êÿÇËë÷Ç¶èIÇÌÇËÇ‹Ç≈ë“Ç¬
            await MySceneManager.Instance.ChangeScene(nextSceneName);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
        finally
        {
            startingGame = false;
        }
    }
}
