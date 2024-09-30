using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
