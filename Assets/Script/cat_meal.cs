using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_meal : MonoBehaviour
{
    // ���͂�̓����蔻��

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ���M�ɓ��������炲�т�������
        if (collision.gameObject.tag == "Plate")
        {
            Destroy(this.gameObject);
        }
    }
}
