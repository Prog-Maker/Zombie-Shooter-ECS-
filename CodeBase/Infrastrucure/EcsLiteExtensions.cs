namespace Leopotam.EcsLite
{
    public static class EcsLiteExtensions
    {
        public static void Delete<T>(this EcsPool<T> ecsPool, int entity) where T : struct
        {
            if(ecsPool.Has(entity))
            {
                ecsPool.Del(entity);
            }
        }

        public static ref T AddComponent<T>(this EcsPool<T> ecsPool, int entity) where T : struct
        {
            if (!ecsPool.Has(entity))
            {
               return ref ecsPool.Add(entity);
            }

            return ref ecsPool.Get(entity);
        }

        public static ref T AddComponent<T>(this EcsWorld world, int entity) where T : struct
        {
            EcsPool<T> ecsPool = world.GetPool<T>();
            
            if (!ecsPool.Has(entity))
            {
                return ref ecsPool.Add(entity);
            }

            return ref ecsPool.Get(entity);
        }

        public static ref T GetComponent<T>(this EcsWorld world, int entity) where T : struct
        {
            EcsPool<T> ecsPool = world.GetPool<T>();

            if (ecsPool.Has(entity))
            {
                return ref ecsPool.Get(entity);
            }

            return ref ecsPool.Add(entity);
        }

        public static void DelComponent<T>(this EcsWorld world, int entity) where T : struct
        {
            EcsPool<T> ecsPool = world.GetPool<T>();

            if (ecsPool.Has(entity))
            {
                ecsPool.Del(entity);
            }
        }
    }
}
