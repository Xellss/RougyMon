using System;

namespace MiniEngine
{
    public class Health : Component
    {
        public delegate void HealthEvent();
        public event HealthEvent OnHealthZero, OnHealthReduced, OnHealthAdded;

        public int CurrentHealth = 0;
        public int MaxHealth = 0;

        public void SetHealth(int health)
        {
            MaxHealth = health;
            CurrentHealth = health;
        }

        public void AddHealth(int health)
        {
            if (CurrentHealth + health < MaxHealth)
                CurrentHealth += health;
            else
                CurrentHealth = MaxHealth;

            if (OnHealthAdded != null)
                OnHealthAdded();
        }

        public void RemoveHealth(int health)
        {
            CurrentHealth -= health;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                if (OnHealthZero != null)
                    OnHealthZero();
            }

            if (OnHealthReduced != null)
                OnHealthReduced();
        }
    }
}
