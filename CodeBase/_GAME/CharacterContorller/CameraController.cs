using System.Collections;
using UnityEngine;

namespace Assets.CodeBase._GAME.CharacterContorller
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;
        public float Speed = 10;

        private void OnValidate()
        {
            transform.position = Target.position + Offset;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, Target.position + Offset, Speed * Time.deltaTime);
        }

    }
}