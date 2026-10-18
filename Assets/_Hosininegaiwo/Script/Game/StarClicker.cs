using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarClicker : MonoBehaviour
{
    private Camera gameCamera;

    private void Start()
    {
        gameCamera = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        //設定画面の表示中は星のクリック判定を行わない。
        if (Engine.Initialized)
        {
            var uiManager = Engine.GetServiceOrErr<IUIManager>();
            var settingsUI = uiManager.GetUI<ISettingsUI>();

            if (settingsUI != null && settingsUI.Visible)
            {
                return;
            }
        }

        Vector2 mouse = Mouse.current.position.ReadValue();

        //画面上のマウス位置を、ゲーム内のXY座標へ変換
        Vector2 world = gameCamera.ScreenToWorldPoint(
            new Vector3(mouse.x, mouse.y, 0f));

        //マウスの位置にコライダーがあるか判定
        Physics2D.SyncTransforms();
        Collider2D hit = Physics2D.OverlapPoint(world);

        //コライダーがあれば、親のStarMoveスクリプトを取得してHit()を呼び出す
        if (hit != null)
        { 
            hit.GetComponentInParent<StarMove>()?.Hit();
        }
    }
}