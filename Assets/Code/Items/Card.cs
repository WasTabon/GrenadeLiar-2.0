using System;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [field: SerializeField] public int id { get; private set; }
    [SerializeField] private bool _iClickable;
    [SerializeField] private float _dragSpeed;

    public event Action<int> CardChoosed;
    
    private float _startPosY;

    private void Start()
    {
        _startPosY = transform.position.y;
    }

    private void OnMouseEnter()
    {
        if (_iClickable)
            DragCard(0.2f);
    }

    private void OnMouseDown()
    {
        if (_iClickable)
            CardChoosed?.Invoke(id);
    }

    private void OnMouseExit()
    {
        if (_iClickable)
            DragCard(0f);
    }

    private void DragCard(float position)
    {
        float moveTo = _startPosY + position;

        transform.DOMoveY(moveTo, _dragSpeed);
    }

    public void PlayCard()
    {
        if (!_iClickable)
            CardChoosed?.Invoke(id);
    }
}
