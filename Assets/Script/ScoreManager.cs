using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Header("スコア")]
    // ねこの反応スコア
    int reactionScore = 0;
    // コンボボーナス
    int comboBonus = 0;
    // 無駄にしたごはん量ペナルティ
    int wastedPenalty = 0;
    // 無駄にしたごはん量
    float wastedMeal = 0f;
    // 合計スコア
    int totalScore = 0;

    [Header("コンボ判定用")]
    // 直前のねこの反応1つだけ覚える
    private CatReaction lastReaction = CatReaction.WITHIN_MARGIN;
    // コンボした回数
    private int comboCount = 0;

    [Header("リザルトUI")]
    public TextMeshProUGUI reactionScore_text;
    public TextMeshProUGUI wastedPenalty_text;
    public TextMeshProUGUI comboBonus_text;
    public TextMeshProUGUI totalScore_text;

    [Header("カウント")]
    private int reactionCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // シーンロードを監視
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        // リザルトシーンに来たらUIを探す
        FindResultUI();
        SetResultUI();
    }

    // リザルトUIを探す
    private void FindResultUI()
    {
        reactionScore_text = GameObject.Find("Cat_reaction")?.GetComponent<TextMeshProUGUI>();
        wastedPenalty_text = GameObject.Find("WastedPenalty")?.GetComponent<TextMeshProUGUI>();
        comboBonus_text = GameObject.Find("ComboBonus")?.GetComponent<TextMeshProUGUI>();
        totalScore_text = GameObject.Find("TotalScore")?.GetComponent<TextMeshProUGUI>();
    }

    // UI更新
    void SetResultUI()
    {
        if (!reactionScore_text) return;

        reactionScore_text.text = $"{reactionScore}";
        comboBonus_text.text = $"{comboBonus}";
        wastedPenalty_text.text = ($"{wastedPenalty}");
        totalScore_text.text = ($"合計スコア：{totalScore}");
    }

    // ねこの反応スコア
    public void AddReactionScore(CatReaction reaction)
    {
        int score = reaction switch
        {
            CatReaction.PERFECT => 500,
            CatReaction.WITHIN_MARGIN => 400,
            CatReaction.FEW_MANY => 250,
            CatReaction.LESS_MORE => 100,
            _ => 0 // 上記以外の反応はスコア０
        };
        reactionCount++;
        reactionScore += score;
    }

    // コンボボーナス
    public void AddCombo(CatReaction reaction)
    {
        // ねこの反応が「ぴったり」or「誤差以内」だったら
        if (reaction == CatReaction.PERFECT ||
           reaction == CatReaction.WITHIN_MARGIN)
        {
            comboCount++;

            Debug.Log($"{comboCount}");

            // コンボが３回のとき
            if (comboCount == 3)
                comboBonus = 300;  // ボーナススコア＋300
            // コンボが２回のとき
            else if (comboCount == 2)
                comboBonus = 150;  // ボーナススコア＋150
        }
        else
        {
            // ねこの反応が「多い少ない」or「多すぎ少なすぎ」だったら
            comboCount = 0;
        }

        // 今回のねこの反応を “次の猫のコンボ判定用” に保存しておく
        lastReaction = reaction;
    }

    // 無駄にしたごはん量の分だけペナルティ
    public void WastedMealPenalty(float wasted)
    {
        // 「無駄にしたごはん量」を記録
        wastedMeal += wasted;

        // ペナルティを算出する
        int penalty = wasted switch
        {
            <= 20f => 0,
            <= 50f => -30,
            <= 100f => -50,
            <= 150f => -100,
            _ => -200
        };
        wastedPenalty += penalty;

        Debug.Log($"無駄にしたごはん量：{Mathf.CeilToInt(wasted)}");
    }

    // 合計スコア更新
    public void CalculateTotal()
    {
        totalScore = reactionScore + comboBonus + wastedPenalty;
    }

    // スコアリセット
    public void ResetScore()
    {
        reactionScore = 0;
        comboBonus = 0;
        wastedPenalty = 0;
        wastedMeal = 0;
        totalScore = 0;

        lastReaction = CatReaction.WITHIN_MARGIN;
        comboCount = 0;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public float GetTotalWastedMeal()
    {
        return wastedMeal;
    }
}
