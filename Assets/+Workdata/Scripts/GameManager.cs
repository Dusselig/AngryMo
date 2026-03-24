using UnityEngine;

namespace _Workdata.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public bool gameIsOver;
        public static GameManager Instance;
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

        public void GameIsOver()
        {
            gameIsOver = true;
            if (PotCheck.Instance.poisonCounter >= 3)
            {
                Debug.Log("win");
            }
            else 
            {
                Debug.Log("lose");
            }
        }
    
    }
}
