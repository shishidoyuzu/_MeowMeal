using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    [Header("スコア")]
    // ねこの反応スコア
    int reactionScore = 0;
    // 袋の残ごはんペナルティ
    int mealRemainPenalty = 0;
    // コンボボーナス
    int comboBonus = 0;
    // 合計スコア
    int totalScore = 0;

    [Header("コンボ判定用")]
    // 全てのねこの反応を記録しておくメモ帳
    //public List<CatReaction> reactionHistory = new List<CatReaction>();
    // 直前のねこの反応1つだけ覚える
    private CatReaction lastReaction = CatReaction.WITHIN_MARGIN;
    // コンボした回数
    private int comboCount = 0;

    [Header("リザルトUI")]
    public TextMeshProUGUI reactionScore_text;
    public TextMeshProUGUI mealRemainPenalty_text;
    public TextMeshProUGUI comboBonus_text;
    public TextMeshProUGUI totalScore_text;

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
        mealRemainPenalty_text = GameObject.Find("mealRemainPenalty")?.GetComponent <TextMeshProUGUI>();
        comboBonus_text = GameObject.Find("ComboBonus")?.GetComponent<TextMeshProUGUI>();
        totalScore_text = GameObject.Find("TotalScore")?.GetComponent<TextMeshProUGUI>();
    }

    // UI更新
    void SetResultUI()
    {
        if (!reactionScore_text) return;

        reactionScore_text.text = $"{reactionScore}";
        comboBonus_text.text = $"{comboBonus}";
        mealRemainPenalty_text.text = ($"-{mealRemainPenalty}");
        totalScore_text.text = ($"合計スコア：{totalScore}");
    }

    // ねこの反応スコア
    public void AddReactionScore(CatReaction reaction)
    {
        int score = reaction switch
        {
            CatReaction.PERFECT         => 500,
            CatReaction.WITHIN_MARGIN   => 400,
            CatReaction.FEW_MANY        => 250,
            CatReaction.LESS_MORE       => 100,
            _ => 0 // 上記以外の反応はスコア０
        };

        reactionScore += score;

    }

    // コンボボーナス
    public void AddCombo(CatReaction reaction)
    {
        // 要検討箇所

        // ねこの反応が「ぴったり」or「誤差以内」だったら
        if (reaction == CatReaction.PERFECT ||
           reaction == CatReaction.WITHIN_MARGIN)
        {
            comboCount++;

            Debug.Log($"{comboCount}");

            // コンボが２回のとき
            if (comboCount == 3)
                comboBonus = 300;  // ボーナススコア＋300
            // コンボが３回のとき
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

    // 残ごはんペナルティ
    public void AddMealRemainPenalty(float remain)
    {
        // 要検討箇所

        // 例：残ったg × 5点 マイナス
        int penalty = Mathf.RoundToInt(remain * 5f);

        mealRemainPenalty += penalty;
    }

    // 合計スコア更新
    public void CalculateTotal()
    {
        totalScore = reactionScore + comboBonus - mealRemainPenalty;
    }

    // スコアリセット
    public void ResetScore()
    {
        reactionScore = 0;
        comboBonus = 0;
        mealRemainPenalty = 0;
        totalScore = 0;

        lastReaction = CatReaction.WITHIN_MARGIN;
        comboCount = 0;
    }
}
