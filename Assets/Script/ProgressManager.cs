using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;

    [Header("アンロック情報")]
    public List<bool> UnlockFlagList = new List<bool>() { true, false, false, false, false };

    private const string UnlockKey = "UnlockStage_";

    private const string StageHighScoreKey = "StageHighScore_";

    private const string TotalWastedMealKey = "TotalWastedMeal";

    private void Awake()
    {
        // FPSの固定
        Application.targetFrameRate = 60;

        // インスタンス
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //LoadUnlockFlags();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // 開放したかどうか
    public bool IsUnlocked(int stageNum)
    {
        return UnlockFlagList[stageNum];
    }
    // 実際に開放する関数
    public void UnlockStage(int stageNum)
    {
        if (stageNum < 0 || stageNum >= UnlockFlagList.Count) return;

        if (!UnlockFlagList[stageNum])
        {
            UnlockFlagList[stageNum] = true;
            SaveUnlockFlag(stageNum);
        }
    }

    void SaveUnlockFlag(int stageNum)
    {
        PlayerPrefs.SetInt(UnlockKey + stageNum, UnlockFlagList[stageNum] ? 1 : 0);
    }

    void LoadUnlockFlags()
    {
        for (int i = 0; i < UnlockFlagList.Count; i++)
        {
            UnlockFlagList[i] = PlayerPrefs.GetInt(UnlockKey + i, i == 0 ? 1 : 0) == 1;
        }
    }


    public void SaveStageHighScore(int stageNum, int score)
    {
        // スコアのロード
        int currentHighScore = LoadStageHighScore(stageNum);

        // スコアが高い場合
        if(score > currentHighScore)
        {
            //ハイスコア更新
            PlayerPrefs.SetInt(StageHighScoreKey + stageNum, score);
            // セーブする
            SaveAll();
        }
    }
    public int LoadStageHighScore(int stageNum)
    {
        // 0 は保存されていないときの値
        return PlayerPrefs.GetInt(StageHighScoreKey + stageNum, 0);
    }

    public void SaveTotalWastedMeal(float wastedMeal)
    {
        // 無駄にしたごはん量をロード
        float current = LoadTotalWastedMeal();
        // 無駄にしたごはん量を加算していく
        PlayerPrefs.SetFloat(TotalWastedMealKey, current + wastedMeal);
        // セーブする
        SaveAll();
    }
    public float LoadTotalWastedMeal()
    {
        // 0 は保存されていないときの値
        return PlayerPrefs.GetFloat(TotalWastedMealKey, 0);
    }

    public void SaveAll()
    {
        PlayerPrefs.Save();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(UnlockKey);
        PlayerPrefs.DeleteKey(StageHighScoreKey);
        PlayerPrefs.DeleteKey(TotalWastedMealKey);
    }

}
