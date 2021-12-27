using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{



    public class Card
    {

        public string Name { get; set; }

        public string img { get; set; }

        public int conunter { get; set; }

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

