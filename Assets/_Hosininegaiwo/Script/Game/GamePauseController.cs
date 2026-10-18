using Naninovel;
using UnityEngine;

public class GamePauseController : MonoBehaviour
{
    private void Update()
    {
        bool paused = Engine.Initialized &&
            Engine.GetServiceOrErr<IInputManager>().IsMuted("StarClick");

        MySceneManager.Instance.SetGamePause(paused);
    }

    private void OnDisable()
    {
        if (MySceneManager.Instance != null)
        {
            MySceneManager.Instance.SetGamePause(false);
        }
    }
}
