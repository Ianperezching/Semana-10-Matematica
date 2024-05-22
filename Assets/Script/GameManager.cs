using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText; 
    private int score = 0;

    void Awake()
    {
      
        if (instance == null)
        {
            instance = this;
        }
        else
        {
           
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        UpdateScoreText();
    }

   
    public void AddPoints(int points)
    {
      
        score += points;
      
        UpdateScoreText();
        if (score == 40)
        {
            SceneManager.LoadScene("Victoria");
        }
    }

  
    void UpdateScoreText()
    {
    
        scoreText.text = "Score: " + score;
    }
}