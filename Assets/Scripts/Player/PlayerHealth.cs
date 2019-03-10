namespace Dasky14.Gunslinger
{
    /// <summary>
    /// A static class for keeping track of the player's health.
    /// </summary>
    public class PlayerHealth
    {
        public static int c_iMaxHealth = 3;
        public static int c_iHealth = 3;

        /// <summary>
        /// Makes the player take 1 point of damage.
        /// </summary>
        public static void TakeDamage()
        {
            AudioManager.PlayClip("PlayerHurt", AudioManager.m_fEffectVolume);
            c_iHealth--;
            if (c_iHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Resets health to maximum.
        /// </summary>
        public static void ResetHealth()
        {
            c_iHealth = c_iMaxHealth;
        }

        /// <summary>
        /// Ends the level.
        /// </summary>
        public static void Die()
        {
            GameManager.instance.m_bLevelEnded = true;
        }
    }
}
