using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }



}
