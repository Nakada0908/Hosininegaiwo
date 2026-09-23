using UnityEngine;
using UnityEngine.InputSystem;

public class ESCGame : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            QuitGame();
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
