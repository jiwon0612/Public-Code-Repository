using Code.Player;

namespace Code.Core.EventSystem
{
    public static class PlayerEvents
    {
        public static readonly DashEvent Dash = new DashEvent();
        public static readonly BoostEvent Boost = new BoostEvent();
        public static readonly GroundEvent Ground = new GroundEvent();
        public static readonly HealthChangeEvent HealthChange = new HealthChangeEvent();
    }

    public class DashEvent : GameEvent
    {
        public bool isEnable;
        public DashEvent Init(bool isEnable)
        {
            this.isEnable = isEnable;
            return this;
        }
    }

    public class GroundEvent : GameEvent
    {
        public GroundVerdict verdict;

        public GroundEvent Init(GroundVerdict verdict)
        {
            this.verdict = verdict;
            return this;
        }
    }

    public class BoostEvent : GameEvent
    {
        public float value;

        public BoostEvent Init(float value)
        {
            this.value = value;
            return this;
        }
    }

    public class HealthChangeEvent : GameEvent
    {
        public float afterHeath;
        public float currentHealth;
        public float maxHealth;

        public HealthChangeEvent Init(float afterHeath, float currentHealth, float maxHealth)
        {
            this.afterHeath = afterHeath;
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
            return this;
        }
    }
}