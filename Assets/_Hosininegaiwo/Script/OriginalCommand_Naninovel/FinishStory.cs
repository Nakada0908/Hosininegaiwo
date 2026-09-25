using System;
using Naninovel;
using UnityEngine;

/// <summary>
/// @finishStory: 物語の終わりに自作の Ending シーンへ進む、
/// Naninovel のシナリオ上で使えるコマンド。
/// </summary>
[Serializable, Alias("finishStory")]
public sealed class FinishStory : Command
{
    public override Awaitable Execute(ExecutionContext ctx)
    {
        MySceneManager.Instance.ChangeScene("Ending");
        return Async.Completed;
    }
}
