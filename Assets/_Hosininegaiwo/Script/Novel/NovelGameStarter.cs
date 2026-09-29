using System;
using Naninovel;
using UnityEngine;

/// <summary>
/// Unity の Novel シーンに入ったとき、指定されたシナリオを再生する。
/// </summary>
public class NovelGameStarter : MonoBehaviour
{
    [SerializeField, ScriptAssetRef] private string startScript;
    public static string nextScript;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetNextScript() => nextScript = null;

    private async void Start()
    {
        try
        {
            // 自動初期化が有効でも、完了するまでは Naninovel の機能を使わない。
            await RuntimeInitializer.Initialize();
            if (!this || !isActiveAndEnabled) return;

            //指定されたシナリオを再生する。
            string selectedScript = string.IsNullOrEmpty(nextScript)
                ? startScript
                : nextScript;
            nextScript = null;
            string scriptPath = ScriptAssets.GetPathOrErr(selectedScript);
            await Engine.GetServiceOrErr<IScriptPlayer>().MainTrack.LoadAndPlay(scriptPath);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
