namespace TreacherousWaters
{
    /// <summary>
    /// Delivers input for firing trigger.
    /// </summary>
    public interface IFire
    {
        /// <summary>
        /// Fires with a given broadside.
        /// </summary>
        /// <param name="side"></param>
        public void Fire(Broadside side);
    }
}

