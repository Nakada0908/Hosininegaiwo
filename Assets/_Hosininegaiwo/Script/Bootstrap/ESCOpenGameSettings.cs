using UnityEngine;
using UnityEngine.InputSystem;
using Naninovel;
using Naninovel.UI;

public class ESCOpenGameSettings : MonoBehaviour
{
    private bool isSettingsOpen = false;

    private void Update()
    {
        //naninovel‚ª‰Šú‰»‚³‚ê‚Ä‚¢‚È‚¢ê‡‚Íˆ—‚µ‚È‚¢
        if (!Engine.Initialized)
        {
            return;
        }

        //naninovel‚ªESC‚Åİ’è‰æ–Ê‚È‚Ç‚ğ•Â‚¶‚é‚©‚çA‚Q‰ñ‚É•ª‚¯‚é‚±‚Æ‚Å‘Îˆ
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
        }
    }
}
