using UnityEngine;
using System.Collections;
using System;

namespace Tiles
{
    public class WeakGround : Tile
    {

        public override void OnEnter(Tile origin, Character character)
        {
            
        }

        public override void OnExit(Tile destination, Character character)
        {
            Debug.Log("Oh no, this weak ground is collapsing!!");
            Destroy(this.gameObject);
        }

    }
}