namespace TreacherousWaters
{
    /// <summary>
    /// Delivers a float health or damage value between scripts.
    /// </summary>
    public interface IAdjustIntegrity
    {
        /// <summary>
        /// Adds or subtracts integrity value.
        /// </summary>
        /// <param name="value"></param>
        public virtual void AdjustIntegrity(float value)
        {

        }
    }
}

