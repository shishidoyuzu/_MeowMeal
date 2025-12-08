using UnityEngine;

public class Cat : MonoBehaviour
{
    public string catName;                      // ネコの名前
    public float targetMeal;                    // ネコの規定ごはん量
    public SpriteRenderer catSprite;            // ネコの見た目

    // 普通の表情
    public Sprite normalFace;
    // にゃーんって鳴いてる時の表情
    public Sprite meowFace;


    // GameManagerから呼ばれる：データ設定
    public void SetCatData(string name, float food)
    {
        catName = name;
        targetMeal = food;
    }
}