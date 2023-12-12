using CodeBase._GAME.Common;
using CodeBase._GAME.Components;
using CodeBase._GAME.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeBase._GAME.Effects
{
    public class SpawnBloodBallSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<EnemyRefsComponent, HitTag, BloodKeeper>> _damagedEnemieswithBlood;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _damagedEnemieswithBlood.Value)
            {
                ref var refsComponent = ref systems.GetWorld().GetComponent<EnemyRefsComponent>(entity);
                ref var hitTag = ref systems.GetWorld().GetComponent<HitTag>(entity);

                int count = Random.Range(2, refsComponent.BloodBalls.Length);

                for (int i = 0; i < count; i++)
                {
                    var bloodBall = refsComponent.BloodBalls[Random.Range(0, refsComponent.BloodBalls.Length)];
                    InstatiateBloodBall(hitTag, bloodBall);
                }
            }
        }

        private static void InstatiateBloodBall(in HitTag hitTag, Rigidbody bloodBall)
        {
            var newBall = Object.Instantiate(bloodBall);
            newBall.transform.position = bloodBall.transform.position;
            newBall.transform.rotation = bloodBall.transform.rotation;
            newBall.gameObject.SetActive(true);
            newBall.velocity = newBall.transform.forward * 3f;
        }
    }
}
