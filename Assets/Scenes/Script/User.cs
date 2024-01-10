using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class User : Singleton<User>
{
    public List<Playerable> MyCharacters => myCharacters;
    [SerializeField] List<Playerable> myCharacters;

    [SerializeField] List<GameObject> myCharacterObjects;

    public List<Playerable> Deck => deck;
    [SerializeField] List<Playerable> deck;

    const int deckCount = 3;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI shopGoldText;

    public int Gold
    {
        get => gold;
        set
        {
            gold = value;
            goldText.text = gold.ToString();
            shopGoldText.text = gold.ToString();
        }
    }
    int gold;

    private new void Awake()
    {
        base.Awake();
        myCharacterObjects = new List<GameObject>();
        myCharacters = new List<Playerable>();
        deck = new List<Playerable>();

        Gold = 1000;
    }

    public void InitCharacter(Playerable p)
    {
        AddCharacterObject(p);
    }

    void AddCharacterObject(Playerable p)
    {
        GameObject addObject = Instantiate(p.gameObject, transform);
        p = addObject.GetComponent<Playerable>();
        p.Init();

        myCharacters.Add(p);
        myCharacterObjects.Add(addObject);
        GameManager.instance.curPlayerableList.Add(p);
        addObject.SetActive(false);

        p.OnDie += () => { deck.Remove(p); };
    }

    public void AddCharacter(Playerable p)
    {
        foreach (Playerable myP in myCharacters)
        {
            if(myP.player_type == p.player_type)
            {
                myP.LevelUp();
                return;
            }
        }
        AddCharacterObject(p);
    }

    public void SetDeck(int index)
    {
        if(index >= myCharacters.Count)
            return;
        if (deck.Contains(myCharacters[index]))
            deck.Remove(myCharacters[index]);
        else
        {
            if (deck.Count < deckCount)
                deck.Add(myCharacters[index]);
            else
                Debug.Log("µ¦ÀÌ ²Ë Ã¡½À´Ï´Ù.");
        }
    }
}
