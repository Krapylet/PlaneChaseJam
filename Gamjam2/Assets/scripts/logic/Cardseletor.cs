using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Model;
using System;
public class Cardseletor : MonoBehaviour
{
   public Card CoruntCard;

    public Deck deck;


    public void trakkort()
    {

        deck.FaceUp.Add( deck.Facedown.First());
        CoruntCard = deck.Facedown.First();
        deck.Facedown.Remove(deck.Facedown.First());

    }


    public  void FaceDownShoffel()
    {
       
        List<Card> midListe = new List<Card>();

       System.Random ran = new System.Random();
       deck.Facedown.OrderBy(a => ran.Next()).ToList();

     }

    public void FaceUpShoffel()
    {

        List<Card> midListe = new List<Card>();

        System.Random ran = new System.Random();
        deck.FaceUp.OrderBy(a => ran.Next()).ToList();

    }



    



}
