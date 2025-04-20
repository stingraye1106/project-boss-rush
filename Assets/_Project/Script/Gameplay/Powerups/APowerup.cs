using System.Collections;
using UnityEngine;

namespace NF.Main.Gameplay.Powerups
{
    public abstract class APowerup : MonoBehaviour
    {
        [SerializeField] private float _duration = 5f;

        public abstract void ApplyPowerup(GameObject picker);
        public abstract void RemovePowerup(GameObject picker);

        protected void StartRemoveTimer(GameObject picker)
        {
            picker.GetComponent<MonoBehaviour>().StartCoroutine(IERemovePowerupAfterDuration(picker));
        }

        protected IEnumerator IERemovePowerupAfterDuration(GameObject picker)
        {
            yield return new WaitForSeconds(_duration);
            RemovePowerup(picker);
        }
    }
}

