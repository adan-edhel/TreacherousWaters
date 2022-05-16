namespace TreacherousWaters
{
    /// <summary>
    /// A container holding static delegates
    /// </summary>
    public static class Delegates
    {
        public delegate void OnGameOver();
        public delegate void OnPickedUpItem(CollectibleItem item);
    }
}