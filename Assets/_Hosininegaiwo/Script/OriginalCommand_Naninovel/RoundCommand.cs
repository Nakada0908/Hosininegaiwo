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

[Serializable, Alias("secondRound")]
public class SecondRoundCommand : Command
{
    public override Awaitable Execute(ExecutionContext ctx)
    {
        GameRoundCounter.StartSecondGame();
        MySceneManager.Instance.ChangeScene("Game");
        return Async.Completed;
    }
}
