using UnityEngine;

public class UIManager : MonoBehaviour
{
    private StageSelectManager StageSelectManager;



    void Start()
    {
        StageSelectManager = FindObjectOfType<StageSelectManager>();
    }

    // メニューで使用するゲーム終了ボタン
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    //-----------------------セレクトシーン------------------------------------
    // 右にある選択UI(黄色いの)を押したとき
    public void PushNEXT_UI()
    {
        StageSelectManager.TryMoveStage(+1);
    }
    // 左にある選択UI(黄色いの)を押したとき
    public void PushPREV_UI()
    {
        StageSelectManager.TryMoveStage(-1);
    }
    //-------------------------------------------------------------------------

    public void ClickButton(AudioClip click)
    {
        // SEの再生
        if (SoundManager.instance != null)
            SoundManager.instance.SE_audioSource.PlayOneShot(click);
        else
            Debug.LogWarning("SoundManagerが見つからないよ！");
    }
}
