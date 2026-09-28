using System;
using Naninovel;
using UnityEngine;

/// <summary>
/// Unity の Game シーンに入ったとき、指定されたシナリオを再生する。
/// </summary>
public class NovelGameStarter : MonoBehaviour
{
    [SerializeField, ScriptAssetRef] private string startScript;
    public static string nextScript;

    private async void Start()
    {
        try
        {
            // 自動初期化が有効でも、完了するまでは Naninovel の機能を使わない。
            await RuntimeInitializer.Initialize();
            if (!this || !isActiveAndEnabled) return;

            // 新規開始なので、前回の背景・立ち絵・再生位置などを初期状態へ戻す。
            var stateManager = Engine.GetServiceOrErr<IStateManager>();
            await stateManager.ResetState();
            if (!this || !isActiveAndEnabled) return;

            //指定されたシナリオを再生する。
            string selectedScript = string.IsNullOrEmpty(nextScript)
                ? startScript
                : nextScript;
            await Engine.GetServiceOrErr<IScriptPlayer>().MainTrack.LoadAndPlay(selectedScript);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
