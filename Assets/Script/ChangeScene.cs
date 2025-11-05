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
    
    // ゲーム画面へ
    public void GotoGamePlay()
    {
        string gameplay = "GamePlay";
        Initiate.Fade(gameplay, black, 1.0f);
    }
}
