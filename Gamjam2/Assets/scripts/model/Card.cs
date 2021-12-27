using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Model
{
    public enum Cardtype
    {
        Plane = 0,
        Phonenom = 1
    }

    [Serializable]
    public class Card : ScriptableObject
    {
        [SerializeField]
        public string Name { get; set; }
        [SerializeField]
        public string img { get; set; }
        [SerializeField]
        public int conunter { get; set; }

        [SerializeField]
        public Cardtype cardType { get; set; }



        public Card(string name, string img, int conunter) {
            Name = name;
            this.img = img;
            this.conunter = conunter;

        }


    }

}

