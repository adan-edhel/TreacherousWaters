namespace TreacherousWaters
{
    /// <summary>
    /// Delivers a float health or damage value between scripts.
    /// </summary>
    public interface ITakeDamage
    {
        /// <summary>
        /// Adds or subtracts integrity value.
        /// </summary>
        /// <param name="value"></param>
        void TakeDamage(float value);
    }
}

