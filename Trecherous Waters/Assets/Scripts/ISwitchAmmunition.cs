namespace TreacherousWaters
{
    /// <summary>
    /// Handles switching of current ammunition.
    /// </summary>
    public interface ISwitchAmmunition
    {
        /// <summary>
        /// Switches ammunition with a given type.
        /// </summary>
        /// <param name="type"></param>
        public void SwitchAmmunition(AmmunitionType type);
    }
}
