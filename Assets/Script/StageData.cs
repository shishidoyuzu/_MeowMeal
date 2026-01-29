using System.Collections.Generic;
using UnityEngine;

// ScriptableObject は Unity の“データ専用アセット”みたいなやつ。
// ゲーム中で使う設定やパラメータを、コードじゃなくて外部ファイルとして保存できる箱に近い。


[CreateAssetMenu(fileName = "StageData", menuName = "MyGame/StageData")]
public class StageData : ScriptableObject
{
    [Header("ステージ番号")]
    public int StageNum;

    [Header("ステージに出てくるねこのリスト")]
    public List<string> CatName;

    [Header("ごはんの誤差")]
    public float margin;

    [Header("制限時間")]
    public float TimeLimit = 10.0f;

    [Header("１ステージに出るねこの数")]
    public int CatCount = 3;

    [Header("ステージ説明")]
    [TextArea]
    public string StageExplanation;

    [Header("保存スコア値")]
    [SerializeField] private int HighScore;
}
