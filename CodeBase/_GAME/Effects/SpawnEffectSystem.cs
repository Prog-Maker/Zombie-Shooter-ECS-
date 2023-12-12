using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Effects
{
    public class SpawnEffectSystem : IEcsRunSystem
    {
        private EcsCustomInject<ProjectContext> _projectContext;
        private EcsFilterInject<Inc<ShowEffectComponent>> _effects;

        public void Run(IEcsSystems systems)
        {
            foreach (var efEntity in _effects.Value)
            {
                ref var effectComp = ref systems.GetWorld().GetComponent<ShowEffectComponent>(efEntity);
                LoadEffect(effectComp);
            }
        }


        private async void LoadEffect(ShowEffectComponent showEffectComponent)
        {
            var effectAsset = _projectContext.Value.StaticDataService.GetEffect(showEffectComponent.EffectType);
            var effectPrefab = await _projectContext.Value.AssetProvider.Load<GameObject>(effectAsset);
            
            if (effectPrefab != null)
            {
                var effect = Object.Instantiate(effectPrefab);
                effect.transform.SetParent(showEffectComponent.Parent);
                effect.transform.position = showEffectComponent.Position;
                effect.transform.rotation = Quaternion.Euler(showEffectComponent.Rotation);
            }
        }
    }
}
