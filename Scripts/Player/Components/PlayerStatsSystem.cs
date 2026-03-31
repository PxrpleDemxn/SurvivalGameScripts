using System.Collections;
using UnityEngine;

namespace Player.Components
{
    public class PlayerStatsSystem : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            if (_playerController == null)
            {
                Debug.LogError("PlayerStatsSystem: PlayerController component not found on the GameObject.");
                return;
            }
            StartCoroutine(DecreaseFoodOverTime());
            StartCoroutine(DecreaseWaterOverTime());
        }

        IEnumerator DecreaseFoodOverTime()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(12);
                _playerController.playerData.hunger -= 1;
            }
        }

        IEnumerator DecreaseWaterOverTime()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(10);
                _playerController.playerData.thirst -= 1;
            }
        }

        private void OnDisable()
        {
            if (_playerController == null)
            {
                Debug.LogWarning("PlayerStatsSystem: PlayerController instance is null on OnDisable.");
            }
            StopCoroutine(DecreaseFoodOverTime());
            StopCoroutine(DecreaseWaterOverTime());
        }
    }
}