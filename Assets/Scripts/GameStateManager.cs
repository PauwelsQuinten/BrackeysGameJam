using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void GameLost(Component sender, object obj)
    {
        SceneManager.LoadScene(2);
    }
}
