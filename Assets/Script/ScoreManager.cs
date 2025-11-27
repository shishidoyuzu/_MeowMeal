using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("スコア")]
    // ねこの反応スコア
    int reactionScore = 0;
    // 袋の残ごはんペナルティ
    int mealRemainPenalty = 0;
    // コンボボーナス
    int comboBonus = 0;
    // 合計スコア
    int totalScore = 0;

    // 猫の反応履歴を持たせたい場合（コンボ判定用）
    public List<CatReaction> reactionHistory = new List<CatReaction>();

    public static ScoreManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void CalculateTotal()
    {
        totalScore = reactionScore + comboBonus - mealRemainPenalty;
    }
}
