using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{



    public class Card : MonoBehaviour
    {

        public string Name { get; set; }

        public string img { get; set; }

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

