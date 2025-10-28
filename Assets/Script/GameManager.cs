using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �l�R�̃v���n�u
    public Cat[] catPrefabs;
    // �l�R�̏o���ʒu
    public Transform catSpawnPos;
    // ���͂�����炤�l�R
    private Cat currentCat;
    // �l�R�f�[�^�x�[�X
    private Cat_DataBase catDB;
    

    // Start is called before the first frame update
    void Start()
    {
        catDB = Cat_DataBase.Instance;
        RandomCatSpawn();
    }

    // �l�R�������_���ɌĂт����֐�
    void RandomCatSpawn()
    {
        if (catPrefabs.Length == 0)
        {
            Debug.LogError("�l�R�̃v���n�u���ݒ肳��Ă��܂���I");
            return;
        }

        // �v���n�u�������_���ɑI��
        int index = Random.Range(0, catPrefabs.Length);
        Cat chosenCat = catPrefabs[index];

        // CSV���烉���_���Ŗ��O�Ƃ��͂�ʂ��擾
        string name = catDB.GetRandomCatName();
        int meal = catDB.GetFoodAmount(name);

        // �l�R���o���I
        currentCat = Instantiate(chosenCat, catSpawnPos.position, Quaternion.identity);
        currentCat.SetCatData(name, meal);

        Debug.Log($"{name} ���o��I�i���z�̂��͂�ʁF{meal}g�j");
    }

    // Plate ���画�茋�ʂ�`����֐�
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
