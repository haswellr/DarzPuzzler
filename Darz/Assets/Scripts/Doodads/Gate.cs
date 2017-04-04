using UnityEngine;
using System.Collections;
using System;
using Tiles;

namespace Doodads
{
    public class Gate : Doodad
    {
        public string color;

        /// <summary>
        /// Returns the first index of a key in the specified character's possession that matches the color of this gate.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private int GetIndexOfMatchingKey(Character character)
        {
            for (int i = 0; i < character.keys.Count; i++)
            {
                if (character.keys[i].Color == this.color)
                {
                    return i;
                }
            }
            return -1;
        }

        public override void OnEnter(Tile origin, Character character)
        {
            int keyIndex = GetIndexOfMatchingKey(character);
            if(keyIndex != -1)
            {
                character.keys.RemoveAt(keyIndex);
                Destroy(this.gameObject);
            }
        }

        public override bool IsTilePassable(Tile moveOrigin, GameObject movingObject)
        {
            Character character = movingObject.GetComponent<Character>();
            return (character != null && GetIndexOfMatchingKey(character) != -1);
        }

        public override void OnExit(Tile destination, Character character)
        {

        }
    }
}
