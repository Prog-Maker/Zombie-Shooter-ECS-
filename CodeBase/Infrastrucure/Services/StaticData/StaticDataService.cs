using CodeBase._GAME.Effects;
using CodeBase.StaticData;
using CodeBase.StaticData.Effects;
using CodeBase.StaticData.Weapons;
using CodeBase.Types;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string EffectsDataPath = "StaticData/Effects/EffectsStaticData";
        private const string LayersDataPath = "StaticData/Layers/LayersStaticData";
        private const string WeponsDataPath = "StaticData/Weapons";
        //private const string MonstersDataPath = "Static Data/Monsters";
        //private const string LevelsDataPath = "Static Data/Levels";
        //private const string StaticDataWindowPath = "Static Data/UI/WindowStaticData";
        //private const string LevelCollectionPath = "Static Data/Levels/LevelCollection";
        //private const string EnvDataPath = "Static Data/Env/EnvinronmentStaticData";
        //private const string TrainsDataPath = "Static Data/Env/TrainsStaticData";
        //private const string SpawnersDataPath = "Static Data/Env/SpawnersStaticData";
        //private const string BuyEnvStaticDataPath = "Static Data/Env/BuyEnvStaticData";
        //private const string BarricadesDataPath = "Static Data/Env/BarricadesStaticData";
        //private const string WeponsStatsDataPath = "Static Data/Weapons/WeaponStats";
        //private const string PlayerStaticDataPath = "Static Data/PlayerStaticData";
        //private const string AudioStaticDataPath = "Static Data/Audio/AudioStaticData";
        //private const string HapticStaticDataPath = "Static Data/Haptic/HapticStaticData";
        //private const string ShopStaticDataPath = "Static Data/Shop/ShopStaticData";

        private Dictionary<EffectType, AssetReference> _effects;
        private Dictionary<WeaponType, AssetReference> _weaponAssets;
        private Dictionary<WeaponType, WeaponStaticData> _weaponDatas;
        private LayersStaticData _layers;


        public void Load()
        {
            var weaponStaticDatas = Resources.LoadAll<WeaponStaticData>(WeponsDataPath);
            _weaponAssets = weaponStaticDatas.ToDictionary(x => x.WeaponType, x => x.Asset);
            _weaponDatas = weaponStaticDatas.ToDictionary(x => x.WeaponType, x => x);

            var effectsData = Resources.Load<EffectsStaticData>(EffectsDataPath);
            _effects = effectsData.Effects.ToDictionary(x => x.EffectType, x => x.Asset);

            _layers = Resources.Load<LayersStaticData>(LayersDataPath);
        }

        public AssetReference GetEffect(EffectType effectType) =>
            _effects.TryGetValue(effectType, out AssetReference assetReference) ? assetReference : null;

        public LayersStaticData GetLayers() => _layers;

        public AssetReference GetWeaponAsset(WeaponType weaponType) =>
            _weaponAssets.TryGetValue(weaponType, out AssetReference asset) ? asset : null;
    }
}