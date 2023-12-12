using CodeBase.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "WeaponStaticData", menuName = "Static Data/Weapon")]
    public class WeaponStaticData : ScriptableObject
    {
        public WeaponType WeaponType;

        public bool UpdateHandPoints = true;
        public bool UpdateRhandPoint = true;
        public bool UpdateLhandPoint = true;

        public Vector3 WeaponLocalPosition;
        public Vector3 WeaponLocalRotation;
        
        public Vector3 RHandTargetLocalPosition;
        public Vector3 RHandTargetLocalRotation;

        public Vector3 LHandTargetLocalPosition;
        public Vector3 LHandTargetLocalRotation;

        public float CurrentDamage;

        public AssetReference Asset;

#if UNITY_EDITOR
        [Button]
        public void UpdateWeaponPositionAndRotation()
        {
            //var player = FindObjectOfType<Player.Player>();
            //var weapon = player.Shooter.CurrentWeapon;
            //var rhand = player.GetComponentInChildren<RHandMarker>();
            //var lhand = player.GetComponentInChildren<LHandMarker>();

            //WeaponLocalPosition = weapon.transform.localPosition;
            //WeaponLocalRotation = weapon.transform.eulerAngles;

            //RHandTargetLocalPosition = rhand.transform.localPosition;
            //RHandTargetLocalRotation = rhand.transform.localRotation.eulerAngles;

            //LHandTargetLocalPosition = lhand.transform.localPosition;
            //LHandTargetLocalRotation = lhand.transform.localRotation.eulerAngles;

            //UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}
