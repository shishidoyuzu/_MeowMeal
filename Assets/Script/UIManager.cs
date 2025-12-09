using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private StageData StageData;
    private StageSelectManager StageSelectManager;
    private int StageNum = 0;

    void Start()
    {
        StageSelectManager = FindObjectOfType<StageSelectManager>();
        StageData = StageSelectManager.AllStageData[StageSelectManager.SelectStageNum];
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
        StageNum = StageData.StageNum;

        // 選択しているステージ番号が５より下なら
        if (StageNum < StageSelectManager.AllStageData.Count - 1)
        {
            StageNum++;
            StageSelectManager.SetStageUI();
            StageSelectManager.UpdateStageIcons();
        }
    }
    // 左にある選択UI(黄色いの)を押したとき
    public void PushPREV_UI()
    {
        StageNum = StageData.StageNum;

        // 選択しているステージ番号が０より上なら
        if (StageNum > 0)
        {
            StageNum--;
            StageSelectManager.SetStageUI();
            StageSelectManager.UpdateStageIcons();
        }
    }
    //-------------------------------------------------------------------------
}
