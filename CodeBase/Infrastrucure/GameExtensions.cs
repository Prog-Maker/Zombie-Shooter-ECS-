using UnityEngine;

namespace CodeBase.Infrastrucure
{
    public static class GameExtensions
    {
        public static bool IsLayer(this Collider collider, LayerMask layerMask) => IsLayer(collider.gameObject, layerMask);

        public static bool IsLayer(this GameObject gameObject, LayerMask layerMask) =>
            ((1 << gameObject.layer) & layerMask) != 0;

        public static float ClampAngle(float angle, float min, float max)
        {
            float start = (min + max) * 0.5f - 180;
            float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
            min += floor;
            max += floor;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
