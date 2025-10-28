using System.Collections.Generic; // List��Dictionary���g�p���邽��
using System.IO; // (csv�Ȃǂ�)�t�@�C����ǂݏ������邽��
using UnityEngine;

// --- �˂��f�[�^�N���X ---
public class CatData
{
    public string name;      // �l�R�̖��O
    public int foodAmount;   // ���͂�ʁig�j

    // �˂��P�C���̃f�[�^�i���O�E���͂�ʁj
    public CatData(string name, int foodAmount)
    {
        this.name = name;
        this.foodAmount = foodAmount;
    }
}

public class Cat_DataBase : MonoBehaviour
{
    public static Cat_DataBase Instance;   // �V���O���g���i�ǂ�����ł��Ăяo����j
    public TextAsset csvFile;              // cats_data.csv �������Ƀh���b�O
    public List<CatData> catList = new List<CatData>();

    void Awake()
    {
        // �V���O���g���ݒ�
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

    // CSV�t�@�C���̓ǂݍ���
    void LoadCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("cats_data.csv ���w�肳��Ă��܂���I");
            return;
        }

        // CSV�t�@�C����ǂݍ���
        using (StringReader reader = new StringReader(csvFile.text))
        {
            // �w�b�_�[���ǂ����𔻕�
            bool isHeader = true;
            while (true)
            {
                // �P�s�ǂ�
                string line = reader.ReadLine();
                // �ǂލs���Ȃ��Ȃ�����A�~�߂�
                if (line == null) break;
                // 1�s�ڃX�L�b�v
                if (isHeader) { isHeader = false; continue; }

                // ","�i�J���}�j�ŋ�؂�
                string[] values = line.Split(',');
                // �u���O�v�Ɓu���їʁv�̂Q�������ĂȂ�������A����
                if (values.Length < 2) continue;

                // �O��̋󔒕������폜
                string name = values[0].Trim();
                // ������𐔒l�ɕϊ�����
                if (int.TryParse(values[1].Trim(), out int amount))
                {
                    // ���l�ɕϊ��o������A�u���O�v�Ɓu���їʁv��List�ɓ����
                    catList.Add(new CatData(name, amount));
                }
            }
        }

        Debug.Log($"Cat_DataBase�F{catList.Count}�C�̃l�R�f�[�^��ǂݍ��݂܂����B");
    }

    // �l�R�̖��O���炲�͂�ʂ��擾
    public int GetFoodAmount(string catName)
    {
        // �n�����l�R�̖��O��List����T���o��
        CatData cat = catList.Find(c => c.name == catName);

        if (cat != null)
        {
            // ����������A���̃l�R�̖��O����u���їʁv�̒l��Ԃ�
            return cat.foodAmount;
        }
        else
        {
            // ������Ȃ�������A-1��Ԃ��ăG���[�ɂȂ�
            Debug.LogWarning($"�l�R�u{catName}�v�̃f�[�^��������܂���B");
            return -1;
        }
    }
}