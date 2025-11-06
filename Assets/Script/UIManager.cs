using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*
    [Header("ボタンUI")]
    public GameObject Menu;
    public GameObject Credit;
    public GameObject DeleteData;
    public GameObject EndGame_;

    [Header("パネルUI")]
    public GameObject MenuPanal;
    */

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
