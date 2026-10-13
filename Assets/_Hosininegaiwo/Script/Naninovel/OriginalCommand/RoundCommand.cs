using System;
using Cysharp.Threading.Tasks;
using Naninovel;
using UnityEngine;

[Serializable, Alias("firstRound")]
public class RoundCommand : Command
{
    public override Awaitable Execute(ExecutionContext ctx)
    {
        GameRoundCounter.StartFirstGame();
        MySceneManager.Instance.ChangeScene("Game").Forget();
        return Async.Completed;
    }
}

[Serializable, Alias("secondRound")]
public class SecondRoundCommand : Command
{
    public override Awaitable Execute(ExecutionContext ctx)
    {
        GameRoundCounter.StartSecondGame();
        MySceneManager.Instance.ChangeScene("Game").Forget();
        return Async.Completed;
    }
}
