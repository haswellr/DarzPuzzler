using UnityEngine;
using System.Collections;
using Tiles;

namespace Doodads
{
    public abstract class Doodad : MonoBehaviour
    {

        public abstract void OnEnter(Tile origin, Character character);

    }
}
