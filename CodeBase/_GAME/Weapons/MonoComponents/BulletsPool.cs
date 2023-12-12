using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase._GAME.Weapons
{
    public class BulletsPool : MonoBehaviour
    {
        public float StartSize = 20;
        public bool CanExpand = true;
        public BulletBase BulletPrefab;

        [ReadOnly]
        public Transform ParentForBullets;

        public List<BulletBase> Bullets = new List<BulletBase>();

        public void Init(Transform parent)
        {
            ParentForBullets = parent;

            for (int i = 0; i < StartSize; i++)
            {
                CreateBullet();
            }
        }

        public BulletBase GetBullet()
        {
            foreach (var bullet in Bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    return bullet;
                }
            }

            if (CanExpand)
            {
                var newBullet = CreateBullet();
                return newBullet;
            }

            return null;
        }

        private BulletBase CreateBullet()
        {
            var bullet = Instantiate(BulletPrefab, ParentForBullets);
            bullet.gameObject.SetActive(false);
            Bullets.Add(bullet);
            return bullet;
        }
    }
}