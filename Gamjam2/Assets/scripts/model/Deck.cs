
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<Card> Facedown;
    public List<Card> FaceUp;

    public Deck() {
        Facedown = new List<Card>();
        FaceUp = new List<Card>();
    }
}
