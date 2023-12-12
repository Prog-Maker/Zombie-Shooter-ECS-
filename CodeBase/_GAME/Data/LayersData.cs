using UnityEngine;

namespace CodeBase._GAME.Data
{
    [System.Serializable]
    public class LayersData
    {
        public LayerMask GroundLayer;
        public LayerMask SolidBodyLayers;
        public LayerMask BulletCollisionLayers;
    }
}
