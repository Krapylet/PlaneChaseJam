using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Card List")]
public class Cards : ScriptableObject
{
    public List<Card> cardList;



    private void OnValidate() {

        foreach (var card in cardList) {
            card.name = card.img.name;
        }
    }
}
