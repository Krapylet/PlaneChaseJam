using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Model;
using System;
using Unity.UI;
using UnityEngine.UI;

public class Cardseletor : MonoBehaviour
{
    public Card CoruntCard;
    public List<Card> aktivekort;
    public Deck deck;

 
    public void LookAtthetop()
    {
        aktivekort.Add(deck.Facedown.First());
        deck.Facedown.Remove(deck.Facedown.First());
    }

    public void ligTillmagePÅToppen()
    {
        Card kortsomSKalLiggesTilmage = aktivekort[1];
        List<Card> midliste = new List<Card>();
       
        midliste.Add(kortsomSKalLiggesTilmage);

        for(int i = 0; i< deck.Facedown.Count; i++)
        {
            midliste.Add(deck.Facedown[i]);
        }


        deck.Facedown = midliste;


        aktivekort.Remove(kortsomSKalLiggesTilmage);
    }

    public void ligIBunden()
    {
        Card kortsomSKalLiggesTilmage = aktivekort[1];

        aktivekort.Remove(kortsomSKalLiggesTilmage);
        deck.Facedown.Add(kortsomSKalLiggesTilmage);
    }




    public void PlanesWalk()
    {
    
        
        CoruntCard = deck.Facedown.First();

        aktivekort.Add((Plane)CoruntCard);
        deck.Facedown.Remove(deck.Facedown.First());

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
                
                midListe.Add(nextCard);
            }
            else
            {
                resolt.Add(nextCard);
                antal++;
            }
        }

        deck.Facedown.AddRange(midListe);

        return resolt;
    }

    



}
