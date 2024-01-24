using System;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private Transform _cardsSpawnPoint;
    [SerializeField] private Inventory _inventory;
    
    [field: SerializeField] public List<Card> _cards { get; private set; }
    

    public void SpawnCards()
    {
        _cards = new List<Card>();
        
        Vector3 spawnPos = _cardsSpawnPoint.position;

        int i = 1;


        foreach (Card card in _inventory._baseCardSetup)
        {
            if (card != null)
            {
                Card createdCard = Instantiate(card, spawnPos, _cardsSpawnPoint.localRotation, _cardsSpawnPoint);
                spawnPos.x += _offset;
                spawnPos.x += 0.1f;
                _cards.Add(createdCard);
                _inventory.myCards[i] = createdCard;
                i++;
            }
        }
    }
}
