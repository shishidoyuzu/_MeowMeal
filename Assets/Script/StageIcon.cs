using UnityEngine;

public class StageIcon : MonoBehaviour
{
    public GameObject LockObj;      // 鍵アイコン
    public CanvasGroup CanvasGroup; // 色暗くする用

    // ロックされている → 暗くする
    public void SetDark(bool isDark)
    {
        CanvasGroup.alpha = isDark ? 0.5f : 1f;
    }
}