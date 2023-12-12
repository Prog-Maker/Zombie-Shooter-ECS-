using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using UnityEngine;

public class ProjectContext : MonoBehaviour
{
    public static ProjectContext Container;

    public EcsWorld EcsWorld { get; private set; }
    public IStaticDataService StaticDataService { get; private set; }
    public IAssetProvider AssetProvider { get; private set; }


    private void Awake()
    {
        Container = this;
        RegisterServices();
    }

    private void RegisterServices()
    {
        AssetProvider = new AssetProvider();
        
        StaticDataService = new StaticDataService();
        StaticDataService.Load();
    }

    public void SetWorld(EcsWorld world) => EcsWorld = world;
}
