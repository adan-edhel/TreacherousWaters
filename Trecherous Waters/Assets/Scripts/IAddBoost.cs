namespace TreacherousWaters
{
    /// <summary>
    /// Delivers boost toggle.
    /// </summary>
    public interface IAddBoost
    {
        /// <summary>
        /// Adds boost to the navigation speed.
        /// </summary>
        /// <param name="toggle"></param>
        public void AddBoost(bool toggle);
    }
}
