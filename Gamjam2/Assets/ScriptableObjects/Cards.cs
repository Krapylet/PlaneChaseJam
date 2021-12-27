using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

[CreateAssetMenu(menuName = "Scriptable Objects/Card List")]
public class Cards : ScriptableObject
{
    [SerializeField]
    public List<Card> cardList;
}
