using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // フェードの色を黒色に
    private Color black = Color.black;


    // タイトルシーンへ
    public void GotoTitle()
    {
        string title = "Title";
        Initiate.Fade(title, black, 1.0f);
    }

    // ステージ選択シーンへ
    public void GotoSelectStage()
    {
        // 前のシーンがリザルトシーンのとき
        if(SceneManager.GetActiveScene().name == "Result")
        {
            int next = StageSelectManager.SelectStageNum + 1;
            // 次のステージを開放する
            ProgressManager.instance.UnlockStage(next);
            // そして、フォーカスする（中央へ）
            StageSelectManager.SelectStageNum = next;
        }
        string stage = "StageSelect";
        Initiate.Fade(stage, black, 1.0f);
    }
    
    // ゲームシーンへ
    public void GotoGamePlay()
    {
        string replay = "GamePlay";
        Initiate.Fade(replay, black, 1.0f);
    }

    // 次のステージへ
    public void GotoNextStage()
    {
        int Nextstage = GameManager.GetNextStage();

        string gameplay = "GamePlay";
        Initiate.Fade(gameplay, black, 1.0f);
    }

    // リザルトシーンへ
    public void GotoResult()
    {
        string result = "Result";
        Initiate.Fade(result, black, 1.0f);
    }
}
