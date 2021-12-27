using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public enum Cardtype
    {
        Plane = 0,
        Phonenom = 1
    }

    [Serializable]
    public class Card
    {
        public string name;
        public Sprite img;
        public int conunter;
        public Cardtype cardType;

        public Card(string name, Sprite img, int conunter) {
            this.name = name;
            this.img = img;
            this.conunter = conunter;

        }

}





