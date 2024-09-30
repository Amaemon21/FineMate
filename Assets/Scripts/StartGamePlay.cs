using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePlay : MonoBehaviour
{
    [SerializeField] private GameManagers _gameManagers;
    [SerializeField] private Timer _timer;
    [SerializeField] private Image _infoImage;

    private void Awake()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        _infoImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        _infoImage.gameObject.SetActive(false);
        _timer.Init();
        _gameManagers.Init();
    }
}
