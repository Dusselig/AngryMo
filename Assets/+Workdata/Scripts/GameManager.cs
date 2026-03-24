using UnityEngine;
using UnityEngine.SceneManagement;

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

        [SerializeField]private GameObject winPanel;
        [SerializeField]private GameObject losePanel;


        public void GameIsOver()
        {
            gameIsOver = true;
            if (PotCheck.Instance.poisonCounter >= 3)
            {
                winPanel.SetActive(true);
            }
            else 
            {
                losePanel.SetActive(true);
            }
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Restart()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    
    }
}
