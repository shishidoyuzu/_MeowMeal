using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ネコのプレハブ
    public Cat catPrefab;
    // ネコの出現位置
    private Transform catSpawnPos;
    // ごはんをもらうネコ
    private Cat currentCat;

    // Start is called before the first frame update
    void Start()
    {
        RandomCatSpawn();
    }

    // ネコをランダムに呼びだす関数
    void RandomCatSpawn()
    {
        /*
        // Cat_DataBase からランダムに1匹取得
        int randomIndex = Random.Range(0, Cat_DataBase.Instance.GetCatCount());
        CatData data = Cat_DataBase.Instance.GetCatDataByIndex(randomIndex);

        // ネコを生成
        currentCat = Instantiate(catPrefab, catSpawnPos.position, Quaternion.identity);

        // ネコにデータをセット
        currentCat.SetCatData(data.name, data.foodAmount);

        Debug.Log($"出てきたネコ：{data.name}（理想量 {data.foodAmount}g）");
        */
    }
}
