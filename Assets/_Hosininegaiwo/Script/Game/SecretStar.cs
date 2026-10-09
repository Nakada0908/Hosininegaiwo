using Naninovel;
using UnityEngine;

public class SecretStar : MonoBehaviour
{
    public void TalkSecret()
    {
        var variables = Engine.GetService<ICustomVariableManager>();

        //現在の値を取得
        bool isTrueEnd = variables.GetVariableValue("talkSecret").Boolean;

        //Naninovel側の変数を変更
        variables.SetVariableValue("talkSecret", new CustomVariableValue(true));
    }
}
