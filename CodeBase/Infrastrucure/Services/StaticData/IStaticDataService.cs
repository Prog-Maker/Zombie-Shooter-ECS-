using CodeBase._GAME.Effects;
using CodeBase.StaticData;
using CodeBase.Types;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        AssetReference GetEffect(EffectType effectType);
        LayersStaticData GetLayers();
        AssetReference GetWeaponAsset(WeaponType weaponType);
    }
}