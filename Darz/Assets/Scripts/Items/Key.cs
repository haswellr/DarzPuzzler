using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Key
    {
        public string Color
        {
            get
            {
                return color;
            }
        }
        private string color;

        public Key(string color)
        {
            this.color = color;
        }
    }
}
