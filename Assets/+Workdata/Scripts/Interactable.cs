using System;
using UnityEngine;

namespace _Workdata.Scripts
{
    public class Interactable : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (this.CompareTag("Food") || this.CompareTag("Poison"))
            {
               Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
               rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
    }
}
