using UnityEngine;

namespace Actors
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Actor actor;
        [SerializeField] private GameObject currentHealthBar;
        private Health healthBar;
        private float originalScale;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            healthBar = actor.Health;
            originalScale = currentHealthBar.transform.localScale.x;
            healthBar.OnHealthChanged += UpdateHealthBar;
        }

        private void UpdateHealthBar(float current, float max)
        {
            Vector3 currentHealthScale = currentHealthBar.transform.localScale;
            currentHealthScale.x = current / max * originalScale;
            currentHealthBar.transform.localScale = currentHealthScale;
        }
    }
}