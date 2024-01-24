using System;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public static RoundController instance { get; private set; }
    
    [SerializeField] private Inventory[] _players;
    public int _currentPlayerCard { get; private set; }
    public int _currentPlayerGrenade { get; private set; }
    
    public bool canSayTrust { get; private set; }

    public event Action RotateCardZone;
    public event Action RotateGrenadeZone;
    
    public int _lastCardId { get; private set; }
    public int _lastChoosedCardId { get; private set; }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        foreach (Inventory player in _players)
        {
            player.CardMovedToTable += GetPlayedCard;
        }

        UIManager.instance.TrustChoosed += CheckTrustCard;
    }

    private void GetPlayedCard(int id, int choosedCard)
    {
        _lastCardId = id;
        _lastChoosedCardId = choosedCard;

        canSayTrust = true;
        
        if (_currentPlayerCard == _currentPlayerGrenade)
        {
            _currentPlayerGrenade++;
            CurrentPlayerController();
            RotateGrenadeZone?.Invoke();
        }
        
        _currentPlayerCard++;
        CurrentPlayerController();
        RotateCardZone?.Invoke();
    }

    private void CheckTrustCard(bool trust)
    {
        Table.instance.DeleteAllCards();
        _lastCardId = 0;
        _lastChoosedCardId = 0;

        canSayTrust = false;
        
        if (!trust)
        {
            if (_lastCardId == _lastChoosedCardId)
            {
                _currentPlayerGrenade = _currentPlayerCard - 1;
                CurrentPlayerController();
                RotateGrenadeZone?.Invoke();
                RotateCardZone?.Invoke();
            }
            else if (_lastCardId != _lastChoosedCardId)
            {
                _currentPlayerCard++;
                CurrentPlayerController();
                RotateCardZone?.Invoke();
            }
        }
        else
        {
            if (_lastCardId != _lastChoosedCardId)
            {
                _currentPlayerGrenade = _currentPlayerCard - 1;
                CurrentPlayerController();
                RotateGrenadeZone?.Invoke();
                RotateCardZone?.Invoke();
            }
            else if (_lastCardId == _lastChoosedCardId)
            {
                _currentPlayerCard++;
                CurrentPlayerController();
                RotateCardZone?.Invoke();
            }
        }
    }

    private void CurrentPlayerController()
    {
        if (_currentPlayerCard >= _players.Length)
            _currentPlayerCard = 0;
        if (_currentPlayerGrenade >= _players.Length)
            _currentPlayerGrenade = 0;
    }
    
    private void SetSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        
        Destroy(gameObject);
    }
}
