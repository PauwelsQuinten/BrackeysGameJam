using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    public void GoToStartScreen()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name, LoadSceneMode.Single);
    }

    public void EndGame()
    {
#if UNITY_WEBGL
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#elif UNITY_STANDALONE_WIN
        Application.Quit();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
