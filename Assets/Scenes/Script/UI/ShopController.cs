using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<Playerable> playerables;
    public Playerable curSelectCharacter;

    BtnPressGrow btnPressGrow;

    public BtnImageViewController deckImage;
    public BtnImageViewController infoImage;

    public GameObject moneyEffect;

    public TextMeshProUGUI[] priceTexts;
    public TextMeshProUGUI curUserGold;

    public void Start()
    {
        UIManager.instance.shopController = this;
        btnPressGrow = GetComponent<BtnPressGrow>();
    }

    public void SetBtnImage()
    {
        deckImage.SetImage();
        infoImage.SetImage();
    }

    public void CharacterSelect(int index)
    {
        if(curSelectCharacter != null
          && curSelectCharacter.player_type == playerables[index].player_type)
            curSelectCharacter = null;
        else
            curSelectCharacter = playerables[index];

        btnPressGrow.SetBtnGrow(index);
    }

    public void Buy()
    {
        if (curSelectCharacter == null)
            return;

        int curPrice = 100;

        foreach (Playerable p in User.instance.MyCharacters)
        {
            if(curSelectCharacter.player_type == p.player_type)
            {
                curPrice = p.Level * 100;
                Debug.Log(curPrice);
            }
        }



        if (User.instance.Gold < curPrice)
        {
            Debug.Log("µ· ¾ø¾î");
            return;
        }

        

        User.instance.Gold -= curPrice;
        moneyEffect.SetActive(true);
        User.instance.AddCharacter(curSelectCharacter);
        UIManager.instance.SetBtn();

        SetBtnImage();
        SetPriceText();
    }

    public void SetPriceText()
    {
        for(int i = 0; i < User.instance.MyCharacters.Count; i++)
        {
            priceTexts[i].text = (User.instance.MyCharacters[i].Level * 100).ToString();
        }
    }
}
