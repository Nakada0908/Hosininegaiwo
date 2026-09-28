using System;
using Naninovel;
using UnityEngine;

[Serializable, Alias("firstRound")]
public class RoundCommand : Command
{
    public override Awaitable Execute(ExecutionContext ctx)
    {
        GameRoundCounter.StartFirstGame();
        MySceneManager.Instance.ChangeScene("Game");
        return Async.Completed;
    }
}
