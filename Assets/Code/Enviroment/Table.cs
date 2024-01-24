using System;
using UnityEngine;
using DG.Tweening;

public class Table : MonoBehaviour
{
    public static Table instance { get; private set; }

    [field: SerializeField] public Transform cardZone { get; private set; }
    [field: SerializeField] public Transform cardPlacer { get; private set; }
    [field: SerializeField] public Transform grenadeZone { get; private set; }
    [field: SerializeField] public Transform trashZone { get; private set; }


    [SerializeField] private float _cardZoneRotateSpeed;
    [SerializeField] private Ease _cardZoneRotateAnim;
    [SerializeField] private float _grenadeZoneRotateSpeed;
    [SerializeField] private Ease _grenadeZoneRotateAnim;

    public event Action SayTrustTurn;
    public event Action BotCanPlay;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        RoundController.instance.RotateCardZone += RotateCardZone;
        RoundController.instance.RotateGrenadeZone += RotateGrenadeZone;
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

    private void RotateCardZone()
    {
        int currentPlayer = RoundController.instance._currentPlayerCard;
        Vector3 rotation = Vector3.zero;

        if (currentPlayer != 0)
            rotation.y = currentPlayer * 90f;

        cardZone.DORotate(rotation, _cardZoneRotateSpeed)
            .SetEase(_cardZoneRotateAnim)
            .OnComplete(() =>
            {
                SayTrustTurn?.Invoke();
                BotCanPlay?.Invoke();
            });
    }

    private void RotateGrenadeZone()
    {
        int currentPlayer = RoundController.instance._currentPlayerGrenade;
        Debug.Log($"Current Player = {currentPlayer}");
        Vector3 rotation = Vector3.zero;

        if (currentPlayer != 0)
            rotation.y = currentPlayer * 90f;
        
        Debug.Log($"Current Rotation = {rotation}");

        grenadeZone.DORotate(rotation, _grenadeZoneRotateSpeed)
            .SetEase(_grenadeZoneRotateAnim)
            .OnComplete(() =>
            {
                
            });
    }

    public void DeleteAllCards()
    {
        Card[] cards = cardPlacer.GetComponentsInChildren<Card>();

        foreach (Card card in cards)
        {
            card.transform.DOMove(trashZone.position, 1f);
        }
    }
}
