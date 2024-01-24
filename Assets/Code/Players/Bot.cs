using UnityEngine;

public class Bot : Inventory
{
    private int randomPlay;
    
    protected override void Start()
    {
        base.Start();
        Table.instance.BotCanPlay += PlayCard;
    }

    private void PlayCard()
    {
        int randomCard;
        bool getCard = false;
        randomPlay = Random.Range(0, 100);
        if (RoundController.instance._lastCardId == 0 && RoundController.instance._lastChoosedCardId == 0)
            randomPlay = 1;
        
        while (getCard == false)
        {
            randomCard = Random.Range(1, myCards.Length);
            if (myCards[randomCard] != null)
                getCard = true;
        }
    }
    
    protected override void ChooseNumber(int number)
    {
        Debug.Log("BOT NUMBER");
        if (id == RoundController.instance._currentPlayerCard && randomPlay < 50)
        {
            Debug.Log("PLAYED BOT");
            _tempId = number;
            int randomNumber = Random.Range(1, 10);
            UIManager.instance.SayNumber(randomNumber);
        }
    }

    protected override void ChooseTrust()
    {
        if (RoundController.instance.canSayTrust && randomPlay > 50)
        {
            int random = Random.Range(1, 2);
            bool randomTrust = (random == 1) ? true : false;
            UIManager.instance.SayTrust(randomTrust);
        }
    }
}
