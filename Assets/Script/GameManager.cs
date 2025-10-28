using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ネコのプレハブ
    public Cat[] catPrefabs;
    // ネコの出現位置
    public Transform catSpawnPos;
    // ごはんをもらうネコ
    private Cat currentCat;
    // ネコデータベース
    private Cat_DataBase catDB;
    

    // Start is called before the first frame update
    void Start()
    {
        catDB = Cat_DataBase.Instance;
        RandomCatSpawn();
    }

    // ネコをランダムに呼びだす関数
    void RandomCatSpawn()
    {
        if (catPrefabs.Length == 0)
        {
            Debug.LogError("ネコのプレハブが設定されていません！");
            return;
        }

        // プレハブをランダムに選択
        int index = Random.Range(0, catPrefabs.Length);
        Cat chosenCat = catPrefabs[index];

        // CSVからランダムで名前とごはん量を取得
        string name = catDB.GetRandomCatName();
        int meal = catDB.GetFoodAmount(name);

        // ネコを出現！
        currentCat = Instantiate(chosenCat, catSpawnPos.position, Quaternion.identity);
        currentCat.SetCatData(name, meal);

        Debug.Log($"{name} が登場！（理想のごはん量：{meal}g）");
    }

    // Plate から判定結果を伝える関数
    public void DecideCatMeal(float diff, float margin)
    {
        if (currentCat != null)
        {
            currentCat.ReactToMeal(diff, margin);
        }
    }

    public Cat GetCurrentCat()
    {
        return currentCat;
    }
}
