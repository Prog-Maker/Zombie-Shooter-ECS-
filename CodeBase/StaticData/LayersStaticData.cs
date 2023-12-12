using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LayersStaticData", menuName = "Static Data/Layers")]
    public class LayersStaticData : ScriptableObject
    {
        public LayerMask EnemyLayer;
        public LayerMask PlayerLayer;
        public LayerMask GroundLayer;
        public LayerMask SolidBodyLayers;
        public LayerMask BulletCollisionLayers;
        public LayerMask PlayerRotateLayers;
    }
}