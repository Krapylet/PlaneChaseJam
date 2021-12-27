using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using System;

[Serializable]
public class Phonenom : Card
{

    public List<Card> Cards;
    public Phonenom(string Name, string img, int conunter) : base(Name, img, conunter)
    {

    }

}
