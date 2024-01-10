using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inc_Speed : BuffInfo
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
            User.instance.Deck[i].Speed += 25;
            UIManager.instance.playerableUIController.playerableUISlots[i].SetSpeedText(User.instance.Deck[i]);
        }
        controller.buffSelectOut();
    }
}
