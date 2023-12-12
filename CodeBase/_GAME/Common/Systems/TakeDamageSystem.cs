using CodeBase._GAME.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CodeBase._GAME.Common
{
    public class TakeDamageSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HealthComponent, Damage>> _damaged;
        private EcsPoolInject<HitTag> _hitsPool;
        private EcsPoolInject<DeathTag> _deathPool;
        private EcsPoolInject<Damage> _damagePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var damagedEntity in _damaged.Value)
            {
                ref HealthComponent health = ref systems.GetWorld().GetComponent<HealthComponent>(damagedEntity);
                ref Damage damage = ref systems.GetWorld().GetComponent<Damage>(damagedEntity);

                health.Current -= damage.Value;

                if (health.Current < 0)
                {
                    _deathPool.Value.Add(damagedEntity);
                }
                else
                {
                    ref HitTag hitTag = ref _hitsPool.Value.Add(damagedEntity);
                    hitTag.Position = damage.Position;
                    hitTag.Normal = damage.Normal;
                }

                _damagePool.Value.Del(damagedEntity);
            }
        }
    }
}
