using UnityEngine;
using UnityEngine.InputSystem;

public class StarClicker : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mouse = Mouse.current.position.ReadValue();

        //画面上のマウス位置を、ゲーム内のXY座標へ変換
        Vector2 world = gameCamera.ScreenToWorldPoint(
            new Vector3(mouse.x, mouse.y, 0f));

        Physics2D.SyncTransforms();
        Collider2D hit = Physics2D.OverlapPoint(world);

        if (hit != null)
            hit.GetComponentInParent<StarMove>()?.Hit();
    }
}