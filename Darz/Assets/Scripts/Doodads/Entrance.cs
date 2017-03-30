using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class Entrance : Doodad
    {
        public override void OnEnter(Tile origin, Character character)
        {
            
        }

        // Use this for initialization
        void Start()
        {
            Character character = FindObjectOfType<Character>();
            Tile parent = this.transform.parent.gameObject.GetComponent<Tile>();
            character.MoveTo(parent);
        }
    }
}
