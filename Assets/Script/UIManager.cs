using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("ボタンUI")]
    public GameObject Menu;
    public GameObject Credit;
    public GameObject DeleteData;
    public GameObject EndGame;

    [Header("パネルUI")]
    public GameObject MenuPanal;


    void Start()
    {
        // ゲーム開始時、非表示に
        MenuPanal.SetActive(false);
    }

    // メニューボタンをクリック
    public void click_MenuButton()
    {
        Menu.SetActive(true);
    }
}
