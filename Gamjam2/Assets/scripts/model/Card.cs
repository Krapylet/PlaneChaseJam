using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Model
{


    [Serializable]
    public class Card
    {
        [SerializeField]
        public string Name { get; set; }
        [SerializeField]
        public string img { get; set; }
        [SerializeField]
        public int conunter { get; set; }

        public enum Tybe
        {
            Plane = 0,
            Phonenom = 1

        }

        public Card()
        {

        }

        public Card(string name, string img, int conunter)
        {
            Name = name;
            this.img = img;
            this.conunter = conunter;
        }



        // Start is called before the first frame update

    }

}

