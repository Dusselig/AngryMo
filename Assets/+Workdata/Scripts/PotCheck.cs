using Unity.VisualScripting;
using UnityEngine;

namespace _Workdata.Scripts
{
    public class PotCheck : MonoBehaviour
    {
        public static PotCheck Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else 
            {
                Destroy(this);
            }
        }
        [SerializeField] private SpriteRenderer waterSprite;
        public int poisonCounter = 0;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Food") || other.CompareTag("Player"))
            {
                if (poisonCounter >= 0)
                {
                    poisonCounter--;
                }

                if (other.CompareTag("Food"))
                {
                    Destroy(other.gameObject);
                }
            }
            else if (other.CompareTag("Poison"))
            {
                poisonCounter++;
                Destroy(other.gameObject);
            }

            if (poisonCounter < 2)
            {
                waterSprite.color = Color.white;
            }
            else if  (poisonCounter == 2)
            {
                waterSprite.color = Color.mediumSeaGreen;
            }
            else if (poisonCounter <= 3)
            {
                waterSprite.color = Color.blueViolet;
            }
        }
    }
}
