using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �l�R�̃v���n�u
    public Cat catPrefab;
    // �l�R�̏o���ʒu
    private Transform catSpawnPos;
    // ���͂�����炤�l�R
    private Cat currentCat;

    // Start is called before the first frame update
    void Start()
    {
        RandomCatSpawn();
    }

    // �l�R�������_���ɌĂт����֐�
    void RandomCatSpawn()
    {
        /*
        // Cat_DataBase ���烉���_����1�C�擾
        int randomIndex = Random.Range(0, Cat_DataBase.Instance.GetCatCount());
        CatData data = Cat_DataBase.Instance.GetCatDataByIndex(randomIndex);

        // �l�R�𐶐�
        currentCat = Instantiate(catPrefab, catSpawnPos.position, Quaternion.identity);

        // �l�R�Ƀf�[�^���Z�b�g
        currentCat.SetCatData(data.name, data.foodAmount);

        Debug.Log($"�o�Ă����l�R�F{data.name}�i���z�� {data.foodAmount}g�j");
        */
    }
}
