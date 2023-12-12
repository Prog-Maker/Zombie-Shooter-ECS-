using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        Task<GameObject> Instantiate(string path, Vector3 at);
        Task<GameObject> Instantiate(string path);
        Task<GameObject> Instantiate(string path, Transform parent);
        Task<T> Load<T>(AssetReference prefabReference) where T : class;
        void Cleanup();
        Task<T> Load<T>(string address) where T : class;
        void Initialize();
    }
}