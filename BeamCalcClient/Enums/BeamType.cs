namespace BeamCalcClient.Enums
{
    /// <summary>
    /// Represents the type of the beam. It can either have one support or two supports.
    /// </summary>
    public enum BeamType
    {
        /// <summary>
        /// The beam has one support which doesn't allow the beam to move.
        /// </summary>
        OneSupport,
        /// <summary>
        /// The beam has two supports. One allows the beam to be rotated and the other allows the beam to be moved only in the direction of its axis (most notably for allowing temperature dilation).
        /// </summary>
        TwoSupports
    }
}
