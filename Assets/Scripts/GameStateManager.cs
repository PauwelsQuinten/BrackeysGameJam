using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private HighScoreManager _highScoreManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void GameLost(Component sender, object obj)
    {
        SceneManager.LoadScene(2);
    }

    public void HighScoreChanged(Component sender, object obj)
    {
        //Add the high to the score board here
        _highScoreManager.AddScore(sender.name, (int)obj);

    }
}
