using System;
using System.Collections.Generic;
using UnityEngine;

/*

ScriptableObject は Unity の“データ専用アセット”みたいなやつ。
ゲーム中で使う設定やパラメータを、コードじゃなくて外部ファイルとして保存できる箱に近い。

 */

enum CatPersonality {
    Moody, // 気分屋
    Nervous, // 神経質



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
        →   神経質な猫   　：誤差1粒（±2.5g）
        →   大雑把な猫   　：±10gでもOK
        →　ゆっくり食べる猫：判定までの待ち時間が長い
        →   気まぐれ猫   　：真顔のまま帰る（評価は内部だけ）
        →   食いしん坊  　 ：常に目標量が多い
    ・制限時間
    　　→制限時間の短縮
        →「時間」ではなく、ねこの機嫌などの「ゲージ」


            など
     */

    [Header("ステージ番号")]
    public int StageNum;

    [Header("ステージに出てくるねこのリスト")]
    public List<string> CatName;

    [Header("ごはんの誤差")]
    public float margin;

    [Header("ねこの性格")]
    public bool Include



}
