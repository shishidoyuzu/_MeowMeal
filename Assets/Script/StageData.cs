using System.Collections.Generic;
using UnityEngine;

// ScriptableObject は Unity の“データ専用アセット”みたいなやつ。
// ゲーム中で使う設定やパラメータを、コードじゃなくて外部ファイルとして保存できる箱に近い。

public enum CatPersonality
{
    // 強制的に誤差を3g（文字の見栄え的に）にする
    Nervous,    // 神経質

    // 誤差が10gになる
    Easygoing,  // 大雑把

    // 反応するまでの時間が長くなる
    Lazy,       // 怠け者

    // 反応しなくなる（内部で評価はされる）
    Moody,      // 気まぐれ

    // 目標量が常に+10gされる
    Greedy,     // 食いしん坊

    // 判定が高確率で「ぴったり」になる
    Lucky       // ラッキー
};

[CreateAssetMenu(fileName = "StageData", menuName = "MyGame/StageData")]
public class StageData : ScriptableObject
{
    /*
    ステージごとに変える要素
    ・今のステージ数
    ・ねこの出現する種類
    ・ごはんの誤差
    ・ねこの性格 ← New!!
        → 神経質な猫 ：誤差1粒（±2.5g）
        → 大雑把な猫 ：±10gでもOK
        → のんきな猫 ：判定までの待ち時間が長い
        → 気まぐれ猫 ：真顔のまま帰る（評価は内部だけ）
        → 食いしん坊 ：常に目標量が多い
    　　→　ラッキー　：誤差の判定がランダムで甘くなる
    ・制限時間
    　　→制限時間の短縮
        →「時間」ではなく、ねこの機嫌などの「ゲージ」

            など
     */

    [Header("ステージ番号")]
    public int StageNum;

    [Header("ステージに出てくるねこのリスト")]
    public List<string> CatName;

    [Header("ねこの性格リスト")]
    public List<CatPersonality> Personalities;

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
    private int HighScore;

}
