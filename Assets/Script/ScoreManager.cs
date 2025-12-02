using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
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
        // ねこの反応が「ぴったり」or「誤差以内」だったら
        if (reaction == CatReaction.PERFECT ||
           reaction == CatReaction.WITHIN_MARGIN)
        {
            comboCount++;
            
            // コンボが２回のとき
            if (comboCount == 2)
                // ボーナススコア＋150
                comboBonus += 150;
            // コンボが３回のとき
            else if (comboCount == 3)
                // ボーナススコア＋300
                comboBonus += 300;
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
        // 例：残ったg × 5点 減点（あとで調整可）
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
