using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Model;
using System;
public class Cardseletor : MonoBehaviour
{
   public Card CoruntCard;
    public List<Card> aktivekort;
    public Deck deck;


    public Card PlanesWalk()
    {
    
        
        CoruntCard = deck.Facedown.First();

        aktivekort.Add((Plane)CoruntCard);
        deck.Facedown.Remove(deck.Facedown.First());

        return CoruntCard;
    }
    public Card trakkort()
    {

        deck.Facedown.Remove(deck.Facedown.First());

        return CoruntCard;
    }

    public void planeWalkAway()
    {
        deck.FaceUp.AddRange(aktivekort);
    }

    public void FaceDownShoffel()
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

    public void ToPlanePåsammetid()
    {

        aktivekort = findNumberforPlanes(2);
        
    
    }
    public void femPlanePåsammetid()
    {

        aktivekort = findNumberforPlanes(5);


    }

    public List<Card> findNumberforPlanes(int skalfindens)
    {
        int antal = 0;
        List<Card> resolt = new List<Card>();
        List<Card> midListe = new List<Card>();


        int planesFound = 0;

        for(int i = 0; i <= deck.Facedown.Count || antal == skalfindens; i++){
            Card nextCard = trakkort();
            if (nextCard.GetType().ToString() == "Phonenom")
            {
                
                midListe.Add((Phonenom)nextCard);
            }
            else
            {
                resolt.Add((Plane)nextCard);
                antal++;
            }
        }

        deck.Facedown.AddRange(midListe);

        return resolt;
    }

    



}
