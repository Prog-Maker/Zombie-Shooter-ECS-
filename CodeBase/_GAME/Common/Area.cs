using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase._GAME.Common
{
    public partial class Area : MonoBehaviour
    {
        public AreaType AreaType;

        [ShowIf(nameof(AreaType), AreaType.Box)]
        public BoxSetting BoxSetting;

        [ShowIf(nameof(AreaType), AreaType.Sphere)]
        public SphereSettings SphereSettings;

        private void OnDrawGizmosSelected()
        {
            Color color = Gizmos.color;
            Gizmos.color = Color.red;

            switch (AreaType)
            {
                case AreaType.Sphere:
                    Gizmos.DrawWireSphere(transform.position, SphereSettings.Radius);
                    break;
                case AreaType.Box:
                    Gizmos.DrawWireCube(transform.position, BoxSetting.Size);
                    break;
                case AreaType.Capsule:
                    break;
            }

            Gizmos.color = color;
        }
    }

    public enum AreaType
    {
        Sphere, Box, Capsule
    }
}