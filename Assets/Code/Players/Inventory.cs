using System;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
   [field: SerializeField] public Card[] _baseCardSetup { get; private set; }
   
   [field: SerializeField] public int id { get; private set; }

   [SerializeField] protected CardHandler _cardHandler;

   [SerializeField] protected float _cardPlaySpeed;
   [SerializeField] protected float _cardRotateSpeed;

   public Card[] myCards = new Card[11];
   
   public event Action<int, int> CardMovedToTable;

   protected int _tempId;
   
   protected virtual void Start()
   { 
      _cardHandler.SpawnCards();

      foreach (Card card in _cardHandler._cards)
      {
         card.CardChoosed += ChooseNumber;
      }
      
      UIManager.instance.NumberChoosed += MoveCardToTable;
      Table.instance.SayTrustTurn += ChooseTrust;
   }

   protected virtual void ChooseNumber(int number)
   {
      if (id == RoundController.instance._currentPlayerCard)
      {
         _tempId = number;
         UIManager.instance.OpenNumberPanel();
      }
   }

   protected virtual void ChooseTrust()
   {
      if (RoundController.instance.canSayTrust)
         UIManager.instance.OpenTrustPanel();
   }
   
   // В скрипте игрока оставить, а в скрипте бота сделать вместо OpenTrustPanel сразу выполнение метода SayNumber и SayTrust с рандомным значением
   
   private void MoveCardToTable(int choosedNumber)
   {
      Debug.Log(myCards[_tempId].gameObject.name);
      DOTween.Sequence()
         .Join(myCards[_tempId].transform.DOMove(Table.instance.cardPlacer.position, _cardPlaySpeed))
         .Join(myCards[_tempId].transform.DORotate(new Vector3(90f, 0f, 0f), _cardRotateSpeed))
         .AppendCallback(() =>
         {
            myCards[_tempId].gameObject.transform.parent = Table.instance.cardPlacer;
            myCards[_tempId] = null;
            CardMovedToTable?.Invoke(_tempId, choosedNumber);
         });
   }
}
