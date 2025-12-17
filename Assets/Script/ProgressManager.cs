using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;

    [Header("アンロック情報")]
    public List<bool> UnlockFlagList = new List<bool>() { true, false, false, false, false };

    private const string UnlockKey = "_UnlockStage";

    private void Awake()
    {
        // FPSの固定
        Application.targetFrameRate = 60;

        // インスタンス
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
}
