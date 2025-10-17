using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat_meal : MonoBehaviour
{
    // ‚²‚Í‚ñ‚Ì“–‚½‚è”»’è

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ‚¨M‚É“–‚½‚Á‚½‚ç‚²”Ñ‚ªÁ‚¦‚é
        if (collision.gameObject.tag == "Plate")
        {
            Destroy(this.gameObject);
        }
    }
}
