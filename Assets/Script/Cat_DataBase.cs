using System.Collections.Generic; // ListやDictionaryを使用するため
using System.IO; // (csvなどの)ファイルを読み書きするため
using UnityEngine;

// --- ねこデータクラス ---
public class CatData
{
    public string name;      // ネコの名前
    public int foodAmount;   // ごはん量（g）

    // ねこ１匹分のデータ（名前・ごはん量）
    public CatData(string name, int foodAmount)
    {
        this.name = name;
        this.foodAmount = foodAmount;
    }
}

public class Cat_DataBase : MonoBehaviour
{
    public static Cat_DataBase Instance;   // シングルトン（どこからでも呼び出せる）
    public TextAsset csvFile;              // cats_data.csv をここにドラッグ
    public List<CatData> catList = new List<CatData>();

    void Awake()
    {
        // シングルトン設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCSV();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // CSVファイルの読み込み
    void LoadCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("cats_data.csv が指定されていません！");
            return;
        }

        // CSVファイルを読み込む
        using (StringReader reader = new StringReader(csvFile.text))
        {
            // ヘッダーかどうかを判別
            bool isHeader = true;
            while (true)
            {
                // １行読む
                string line = reader.ReadLine();
                // 読む行がなくなったら、止める
                if (line == null) break;
                // 1行目スキップ
                if (isHeader) { isHeader = false; continue; }

                // ","（カンマ）で区切る
                string[] values = line.Split(',');
                // 「名前」と「ご飯量」の２個が入ってなかったら、次へ
                if (values.Length < 2) continue;

                // 前後の空白文字を削除
                string name = values[0].Trim();
                // 文字列を数値に変換する
                if (int.TryParse(values[1].Trim(), out int amount))
                {
                    // 数値に変換出来たら、「名前」と「ご飯量」をListに入れる
                    catList.Add(new CatData(name, amount));
                }
            }
        }

        Debug.Log($"Cat_DataBase：{catList.Count}匹のネコデータを読み込みました。");
    }

    // ネコの名前からごはん量を取得
    public int GetFoodAmount(string catName)
    {
        // 渡したネコの名前をListから探し出す
        CatData cat = catList.Find(c => c.name == catName);

        if (cat != null)
        {
            // 見つかったら、そのネコの名前から「ご飯量」の値を返す
            return cat.foodAmount;
        }
        else
        {
            // 見つからなかったら、-1を返してエラーになる
            Debug.LogWarning($"ネコ「{catName}」のデータが見つかりません。");
            return -1;
        }
    }
}