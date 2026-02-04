using System.Collections;
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
    // コンボボーナス
    int comboBonus = 0;
    // 無駄にしたごはん量ペナルティ
    int wastedPenalty = 0;
    // 無駄にしたごはん量
    float wastedGram = 0f;
    // 各ねこの目標量との差
    //List<int> mealDiffList = new List<int>();
    // 合計スコア
    int totalScore = 0;
    // 前回のハイスコア
    int previousScore = 0;

    [Header("コンボ判定用")]
    // 直前のねこの反応1つだけ覚える
    private CatReaction lastReaction = CatReaction.WITHIN_MARGIN;
    // コンボした回数
    private int comboCount = 0;

    [Header("リザルトUI")]
    public TextMeshProUGUI reactionScore_text;
    public TextMeshProUGUI wastedPenalty_text;
    public TextMeshProUGUI wastedGram_text;
    public TextMeshProUGUI comboBonus_text;
    public TextMeshProUGUI totalScore_text;

    //TextMeshProUGUI[] mealDiffTexts;

    [Header("ハイスコア更新画像")]
    [SerializeField] private GameObject highScoreEffect;

    // ペナルティスコアにかかる倍率
    private const int penaltyRate = 5;

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // リザルトシーンに来たらUIを探す
        FindResultUI();
        //SetResultUI();
        ClearResultUI();
        StartCoroutine(ResultSequence());
    }

    // リザルトUIを探す
    private void FindResultUI()
    {
        reactionScore_text = GameObject.Find("Cat_reaction")?.GetComponent<TextMeshProUGUI>();
        wastedPenalty_text = GameObject.Find("WastedPenalty")?.GetComponent<TextMeshProUGUI>();
        wastedGram_text = GameObject.Find("WastedGram")?.GetComponent<TextMeshProUGUI>();
        comboBonus_text = GameObject.Find("ComboBonus")?.GetComponent<TextMeshProUGUI>();
        totalScore_text = GameObject.Find("TotalScore")?.GetComponent<TextMeshProUGUI>();
        //mealDiffTexts = new TextMeshProUGUI[3];
        //mealDiffTexts[0] = GameObject.Find("MealDiff_1")?.GetComponent<TextMeshProUGUI>();
        //mealDiffTexts[1] = GameObject.Find("MealDiff_2")?.GetComponent<TextMeshProUGUI>();
        //mealDiffTexts[2] = GameObject.Find("MealDiff_3")?.GetComponent<TextMeshProUGUI>();
    }

    // スコアテキストを空白に
    private void ClearResultUI()
    {
        if (!reactionScore_text) return;

        reactionScore_text.text = "";
        comboBonus_text.text    = "";
        wastedPenalty_text.text = "";
        //wastedGram_text.text    = "";
        totalScore_text.text    = "";
    }

    // リザルト表示待機
    IEnumerator ResultSequence()
    {
        if (!reactionScore_text) yield break;

        // 反応スコアを表示
        reactionScore_text.text = $"{reactionScore}";
        // SEを流す
        PlayResultSE();
        // 少しの間コルーチンを待機させる
        yield return new WaitForSeconds(0.5f);

        // ボーナススコアを表示
        comboBonus_text.text = $"{comboBonus}";
        // SEを流す
        PlayResultSE();
        // 少しの間コルーチンを待機させる
        yield return new WaitForSeconds(0.5f);

        // 無駄にしたごはん量を表示
        //wastedGram_text.text = ($"{wastedGram}g ×{penaltyRate} ");
        // 目標量との差を表示
        //for (int i = 0; i < mealDiffList.Count; i++)
        //{
        //    mealDiffTexts[i].text = $"{mealDiffList[i]}g";
        //}
        // SEを流す
        //PlayResultSE();
        // 少しの間コルーチンを待機させる
        //yield return new WaitForSeconds(0.5f);

        // ペナルティスコアを表示
        wastedPenalty_text.text = ($"{wastedPenalty}");
        // SEを流す
        PlayResultSE();
        // 少しの間コルーチンを待機させる
        yield return new WaitForSeconds(0.7f);


        // 合計スコアを表示
        totalScore_text.text = ($"合計スコア：{totalScore}");
        PlayResultSE();

        if (totalScore > previousScore)
            ShowHighscoreEffect();

        yield return null;
    }

    /*  // UI更新
    void SetResultUI()
    {
        if (!reactionScore_text) return;

        reactionScore_text.text = $"{reactionScore}";
        comboBonus_text.text = $"{comboBonus}";
        wastedPenalty_text.text = ($"{wastedPenalty}");
        wastedGram_text.text = ($"{wastedGram}g");
        totalScore_text.text = ($"合計スコア：{totalScore}");
    }
    */

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

            //Debug.Log($"{comboCount}");

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
        // 「無駄にしたごはん量」の値を切り上げる
        int wastedInt = Mathf.CeilToInt(wasted);

        // 「無駄にしたごはん量」を記録
        wastedGram += wastedInt;

        wastedPenalty -= wastedInt * penaltyRate;

        Debug.Log($"今回:{wastedInt}g / 累計:{wastedGram}g");
    }

    public void ApplyMealDiffPenalty(float wasted)
    {
        // 「目標量からの差」の値を切り上げる
        int wastedInt = Mathf.CeilToInt(wasted);

        // ペナルティを算出する
        int penalty = wastedInt switch
        {
            <= 20 => 0,
            <= 50 => -30,
            <= 100 => -50,
            <= 150 => -100,
            _ => -200
        };
        wastedPenalty += penalty;

        Debug.Log($"無駄にしたごはん量：{wastedInt}");
    }

    // 合計スコア更新
    public void CalculateTotal()
    {
        // 合計スコアの計算
        totalScore = reactionScore + comboBonus + wastedPenalty;

        // 今回のスコアを次回用に保存しておく
        previousScore = totalScore;
    }

    public void AddMealDiff(float diff)
    {
        int diffInt = Mathf.CeilToInt(diff);

        // 表示用に保存
        //mealDiffList.Add(diffInt);

        // ペナルティ計算
        ApplyMealDiffPenalty(diffInt);
    }

    // ハイスコア更新時の演出
    private void ShowHighscoreEffect()
    {
        if (highScoreEffect == null) return;
        
        // 画像を表示する
        highScoreEffect.SetActive(true);
        // ふわふわアニメーションをつける
        StartCoroutine(PlayRecordFloat());
    }

    // ふわふわアニメーション
    IEnumerator PlayRecordFloat()
    {
        Vector3 startPos = highScoreEffect.transform.localPosition;

        float height = 10.0f;
        float speed = 1.5f;

        while (true)
        {
            float y = Mathf.Sin(Time.time *  speed) * height;
            highScoreEffect.transform.localPosition = startPos + Vector3.up * y;
            yield return null;
        }
    }

    // スコアリセット
    public void ResetScore()
    {
        reactionScore = 0;
        comboBonus = 0;
        wastedPenalty = 0;
        wastedGram = 0;
        totalScore = 0;

        previousScore = 0;

        lastReaction = CatReaction.WITHIN_MARGIN;
        comboCount = 0;

        //mealDiffList.Clear();
    }

    public int GetTotalScore()
    {
        // 合計スコアをそのまま返す
        return totalScore;
    }

    public float GetTotalWastedMeal()
    {
        // 無駄にしたごはん量をそのまま返す
        return wastedGram;
    }

    public void PlayResultSE()
    {
        if (SoundManager.instance == null)
        {
            Debug.LogWarning("SoundManager.instance が null");
            return;
        }

        if (SoundManager.instance.SE_audioSource == null)
        {
            Debug.LogWarning("SE_audioSource が null");
            return;
        }

        // スコアSEを一回流す
        SoundManager.instance.SE_audioSource.PlayOneShot(SoundManager.instance.SE_score);
    }
}