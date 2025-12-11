using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private StageSelectManager ssm;

    // フェードの色を黒色に
    private Color black = Color.black;

    private void Start()
    {
        // リザルトしーんに存在しないのが原因
        ssm = GameObject.FindObjectOfType<StageSelectManager>();
    }

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
            int s_Num = StageSelectManager.SelectStageNum;
            s_Num++;
            ssm.UnlockFlagList[s_Num] = true;
        }
        string stage = "StageSelect";
        Initiate.Fade(stage, black, 1.0f);
    }
    
    // もう一度プレイする
    public void GotoGamePlay()
    {
        // 前のシーンがゲームシーンorリザルトシーンのとき
        if (SceneManager.GetActiveScene().name == "GamePlay" ||
            SceneManager.GetActiveScene().name == "Result")
        {
            int Replay = GameManager.GetCurrentStageForReplay();
        }
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
