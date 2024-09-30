using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private int _startTime = 60;
    [SerializeField] private GameManagers _gameManagers;
    [SerializeField] private PopUp _losePopUp;

    private int _currentTime;
    private bool _isRunning = true;

    public void Init()
    {
        _currentTime = _startTime;
        UpdateTimerText();
        StartCoroutine(CountdownTimer());
    }

    private IEnumerator CountdownTimer()
    {
        while (_currentTime > 0 && _isRunning)
        {
            yield return new WaitForSeconds(1);
            _currentTime--;
            UpdateTimerText();
        }
        
        if (_currentTime <= 0)
        {
            TimerFinished();
        }
    }

    private void UpdateTimerText()
    {
        _timerText.text = _currentTime.ToString();

        if (_gameManagers.IsEnd)
        {
            _isRunning = false;
        }
    }

    private void TimerFinished()
    {
        _isRunning = false;
        _losePopUp.gameObject.SetActive(true);
    }
}