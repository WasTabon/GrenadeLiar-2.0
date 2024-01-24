using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] private GameObject _numberPanel;
    [SerializeField] private GameObject _trustPanel;

    public event Action<int> NumberChoosed;
    public event Action<bool> TrustChoosed;
    
    private void Awake()
    {
        SetSingleton();
    }

    public void OpenNumberPanel()
    {
        _numberPanel.SetActive(true);
        _trustPanel.SetActive(false);

    }
    public void OpenTrustPanel()
    {
        _trustPanel.SetActive(true);
        _numberPanel.SetActive(false);
    }
    
    public void SayNumber(int number)
    {
        NumberChoosed?.Invoke(number);
        Debug.Log($"Number {number}");
        _numberPanel.SetActive(false);
    }
    public void SayTrust(bool trust)
    {
        TrustChoosed?.Invoke(trust);
        _trustPanel.SetActive(false);
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
