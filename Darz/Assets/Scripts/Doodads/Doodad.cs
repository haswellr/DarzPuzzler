using UnityEngine;
using System.Collections;
using Tiles;

namespace Doodads
{
    public abstract class Doodad : MonoBehaviour
    {

        public abstract void OnEnter(Tile origin, Character character);
        public abstract void OnExit(Tile destination, Character character);

        public virtual bool IsTilePassable(Tile moveOrigin, GameObject movingObject)
        {
            return true;
        }

    }
}
