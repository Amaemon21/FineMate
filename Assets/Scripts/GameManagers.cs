using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagers : MonoBehaviour
{
    private readonly int _maxAttempts = 3; 
    
    [SerializeField] private List<Card> _cards = new List<Card>();
    [SerializeField] private Sprite[] _cardImages;
    [SerializeField] private Slider _slider;

    [SerializeField] private PopUp _winPopup;
    [SerializeField] private PopUp _losePopup;

    private List<Card> _cardsOpen = new List<Card>();

    private Card _firstCard;
    private Card _secondCard;
    private int _currentAttempts;
    private int _matchedPairs;
    private bool _isCheckingMatch;
    private bool _isEnd;

    public bool IsCheckingMatch => _isCheckingMatch;
    public bool IsEnd => _isEnd;
    
    public void Init()
    { 
        _currentAttempts = _maxAttempts; 
        UpdateUI(_currentAttempts);
        
        List<Sprite> images = new List<Sprite>(_cardImages);
        images.AddRange(_cardImages);
        
        Shuffle(images);
        
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].Setup(this);
            _cards[i].SetupFaceSprite(images[i]);
        }
        
        _matchedPairs = 0;
    }
    
    public void CardRevealed(Card card)
    {
        if (_firstCard == null)
        {
            _firstCard = card;
        }
        else
        {
            _secondCard = card;
            StartCoroutine(CheckForMatch());
        }
    }

    private IEnumerator CheckForMatch()
    {
        _isCheckingMatch = true; 

        if (_firstCard.FaceSprite != _secondCard.FaceSprite)
        {
            yield return new WaitForSeconds(1f);
            _firstCard.Hide();
            _secondCard.Hide();
            
            if (_cardsOpen.Contains(_firstCard) || _cardsOpen.Contains(_secondCard))
            {
                _currentAttempts--;
                UpdateUI(_currentAttempts);
                Debug.Log("Fail");
            }
            
            if (_currentAttempts <= 0)
            {
                GameOver();
            }
        }
        else
        {
            Destroy(_firstCard);
            Destroy(_secondCard);
            
            _matchedPairs++; 
            
            if (_matchedPairs >= _cards.Count / 2)
            {
                WinGame(); 
            }
        }
        
        _cardsOpen.Add(_firstCard);
        _cardsOpen.Add(_secondCard);

        _firstCard = null;
        _secondCard = null;
        _isCheckingMatch = false;
    }

    private void UpdateUI(int value)
    {
        _slider.value = value;
    }

    private void Shuffle(List<Sprite> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }
    
    private void GameOver()
    {
        if (_winPopup.gameObject.activeSelf)
        return;
        
        _isEnd = true;
        _losePopup.gameObject.SetActive(true);
    }
    
    private void WinGame()
    {
        _isEnd = true;
        _winPopup.gameObject.SetActive(true);
    }
}