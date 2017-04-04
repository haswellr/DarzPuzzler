using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class Key : Doodad
    {
        public string color;

        public override void OnEnter(Tile origin, Character character)
        {
            Items.Key keyItem = new Items.Key(this.color);
            character.keys.Add(keyItem);
            Destroy(this.gameObject);
        }

        public override void OnExit(Tile destination, Character character)
        {

        }
    }
}
