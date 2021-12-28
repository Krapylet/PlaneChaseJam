using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.UI;
using UnityEngine.UI;

public class Cardseletor : MonoBehaviour
{
    public Card CoruntCard;
    public List<Card> aktivekort;
    public Deck deck;
    public Cards AllCards;
    public GameObject newCard;
    public int indexafaktiv;

    public void Awake()
    {
        deck = new Deck();
        deck.Facedown = new List<Card>(AllCards.cardList);
    }


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

    public void updatebilledetilnæste()
    {
        indexafaktiv++;
        try
        {
            gameObject.GetComponent<Image>().sprite = aktivekort[indexafaktiv].img;
        }
        catch
        {

        }

    }




    public void updatebillede()
    {
            gameObject.GetComponent<Image>().sprite = CoruntCard.img;


    }

    public void PlanesWalk()
    {

        if(deck.Facedown.Count == 0)
        {
            deck.Facedown = deck.FaceUp;
            FaceDownShoffel();
        }

  
        
        CoruntCard = deck.Facedown.First();
        updatebillede();
        aktivekort.Add(CoruntCard);
        deck.Facedown.Remove(deck.Facedown.First());

    }
    public Card trakkort()
    {

        deck.Facedown.Remove(deck.Facedown.First());

        return deck.Facedown.First();
    }

    public void planeWalkAway()
    {
        deck.FaceUp.AddRange(aktivekort);
        aktivekort.Clear();
    }

    public void FaceDownShoffel()
    {
       
        List<Card> midListe = new List<Card>();

       System.Random ran = new System.Random();
         midListe =  deck.Facedown.OrderBy(a => ran.Next()).ToList();
        deck.Facedown = midListe;
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


 

        for(int i = 0; (i <= deck.Facedown.Count) && (antal < skalfindens); i++){
            Card nextCard = trakkort();
            Debug.Log(nextCard.name);

            if (nextCard.cardType == Cardtype.Phonenom)
            {

                Debug.Log("PH");
                midListe.Add(nextCard);
            }
            else
            {
                Debug.Log("plane");
                resolt.Add(nextCard);
                antal++;
                Debug.Log(antal);
                Debug.Log(skalfindens);
            }
        }

        deck.Facedown.AddRange(midListe);

        Debug.Log(antal);
        return resolt;
    }

    



}
