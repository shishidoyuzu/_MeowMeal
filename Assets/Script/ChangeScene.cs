using UnityEngine;

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
        string stage = "StageSelect";
        Initiate.Fade(stage, black, 1.0f);
    }
    
    // もう一度プレイする
    public void Game_Re_Play()
    {
        int Replay = GameManager.GetCurrentStageForReplay();

        string replay = "GamePlay";
        Initiate.Fade(replay, black, 1.0f);
    }

    // 次のステージへ
    public void GotoGamePlay()
    {
        int Nextstage = GameManager.GetNextStage();

        string gameplay = "GamePlay";
        Initiate.Fade(gameplay, black, 1.0f);
    }

    // 次のステージへ
    public void GotoGamePlayforSelect()
    {
        string gameplay = "GamePlay";
        Initiate.Fade(gameplay, black, 1.0f);
    }


    // リザルト画面へ
    public void GotoResult()
    {
        string result = "Result";
        Initiate.Fade(result, black, 1.0f);
    }
}
