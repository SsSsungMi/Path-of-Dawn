using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inc_Defense : BuffInfo
{
    Button btn;

    void Start()
    {
        btn = this.GetComponent<Button>();
    }

    public override void BuffSelect()
    {
        base.BuffSelect();

        for (int i = 0; i < User.instance.Deck.Count; i++)
        {
            User.instance.Deck[i].Def += 15;
            UIManager.instance.playerableUIController.playerableUISlots[i].SetDefText(User.instance.Deck[i]);
        }
        controller.buffSelectOut();
    }
}
