using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private StageData StageData;
    private StageSelectManager StageSelectManager;
    private int StageNum = 0;

    // スクロールビュー（ステージアイコン）
    private Scrollbar scrollbar;

    void Start()
    {
        scrollbar = GameObject.Find("Scrollbar Horizontal").GetComponent<Scrollbar>();

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
            MoveScrollview();
            StageSelectManager.SetStageUI(StageNum);
            StageSelectManager.UpdateStageIcons(StageNum);
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
            MoveScrollview();
            StageSelectManager.SetStageUI(StageNum);
            StageSelectManager.UpdateStageIcons(StageNum);
        }
    }

    public void MoveScrollview()
    {
        StageNum = StageData.StageNum;

        switch ((StageNum + 1))
        {
            case 1: // ステージ１のとき
                scrollbar.value = 0.0f;
                break;
            case 2: // ステージ２のとき
                scrollbar.value = 0.25f;
                break;
            case 3: // ステージ３のとき
                scrollbar.value = 0.5f;
                break;
            case 4: // ステージ４のとき
                scrollbar.value = 0.75f;
                break;
            case 5: // ステージ５のとき
                scrollbar.value = 1.0f;
                break;
        }
    }

    //-------------------------------------------------------------------------
}
