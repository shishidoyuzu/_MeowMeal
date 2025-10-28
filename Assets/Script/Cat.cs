using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public string catName; // ネコの名前
    public int targetMeal; // ネコの規定ごはん量
    public SpriteRenderer catSprite; // ネコの見た目


    private CatData catData;

    void Start()
    {
        // ランダムにネコをピックアップする
        //string Cat_Random = catData.name[Random.Range(0, catData.)];
    }

    void Update()
    {
        
    }
}
