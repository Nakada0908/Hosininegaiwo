using Naninovel;
using UnityEngine;

public class NovelGameStarter : MonoBehaviour
{
    [SerializeField] private string scriptPath = "Entry";
    [SerializeField] private string endingSceneName = "Ending";

    private IScriptPlayer scriptPlayer;
    private bool isTransitioning;

    private async void Start()
    {
        //Naninovelの初期化を待つ
        await RuntimeInitializer.Initialize();

        scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();

        //前回の立ち絵や背景などを消して、新しいらしいゲーム状態にする
        IStateManager stateManager=Engine.GetServiceOrErr<IStateManager>();
        await stateManager.ResetState();

        if (this == null || !isActiveAndEnabled)
        {
            return;
        }

        //シナリオ終了を受け取る
        scriptPlayer.OnStop += OnScriptStopped;

        //Assets/Scenario/Entry.naniを再生
        await scriptPlayer.MainTrack.LoadAndPlay(scriptPath);
    }

    private void OnScriptStopped(IScriptTrack track)
    {
        if(isTransitioning)
        {
            return;
        }

        if(scriptPlayer==null||track!=scriptPlayer.MainTrack)
        {
            return;
        }

        if(track.PlayedScript==null||
            track.PlayedScript.Path!=scriptPath)
        {
            return;
        }

        //@choiceも入力待ちに入るとOnStopを発火する
        //最後のコマンドを実行し終えた場合だけエンディングに進む
        if (track.Playlist == null || track.PlayedIndex != track.Playlist.Count - 1)
        {
            return;
        }

        isTransitioning = true;
        MySceneManager.Instance.ChangeScene(endingSceneName);
    }

    private void OnDestroy()
    {
        if(scriptPlayer!=null)
        {
            scriptPlayer.OnStop -= OnScriptStopped;
        }
    }
}
