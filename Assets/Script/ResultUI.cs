using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] GameObject NextStageBTN;

    void Start()
    {
        if(GameManager.instance.isFinalStage())
        {
            NextStageBTN.SetActive(false);
        }
    }
}