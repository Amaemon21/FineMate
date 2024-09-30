using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _face;
    [SerializeField] private SpriteRenderer _back;

    private GameManagers _gameManagers;

    public Sprite FaceSprite => _face.sprite;

    public void Setup(GameManagers gameManagers)
    {
        _gameManagers = gameManagers;
    }
    
    private void OnMouseDown()
    {
        if(_gameManagers.IsEnd)
            return;
        
        if(_gameManagers.IsCheckingMatch)
            return;
        
        transform.DORotate(new Vector2(0, transform.rotation.y + 180), 1f).OnComplete(() =>
        {
            
        });
        
        _gameManagers.CardRevealed(this);
    }

    public void SetupFaceSprite(Sprite sprite)
    {
        _face.sprite = sprite;
    }

    public void Hide()
    {
        transform.DORotate(new Vector2(0, 0), 1f).OnComplete(() =>
        {

        });
    }
}