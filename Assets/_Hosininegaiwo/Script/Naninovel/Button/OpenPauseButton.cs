using Naninovel;
using Naninovel.UI;
using UnityEngine;

public class OpenPauseButton : MonoBehaviour
{
    public void OpenPause()
    {
        if (!Engine.Initialized)
        {
            return;
        }

        var uiManager = Engine.GetServiceOrErr<IUIManager>();
        var pauseUI = uiManager.GetUIOrErr<IPauseUI>();
        pauseUI.Show();
    }
}
