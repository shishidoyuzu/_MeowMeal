using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // ボタンオブジェクトにアタッチするもの
    // クリックすると、設定されているリンクへ飛ぶ

    public void OepnWebsite(string url)
    {
        Application.OpenURL(url);
    }
}
