using UnityEngine;

namespace CodeBase.StaticData.Effects
{
    [CreateAssetMenu(fileName = "EffectsStaticData", menuName = "Static Data/Effects")]
    public class EffectsStaticData : ScriptableObject
    {
        public EffectsData[] Effects;
    }
}