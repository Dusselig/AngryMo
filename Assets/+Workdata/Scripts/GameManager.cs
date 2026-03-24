using UnityEngine;

namespace _Workdata.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private bool _gameIsOver;
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
            _gameIsOver = true;
            Debug.Log("Game Over");
        }
    
    }
}
