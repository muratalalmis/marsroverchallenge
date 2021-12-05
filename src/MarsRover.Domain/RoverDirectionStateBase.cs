namespace MarsRover.Domain
{
    internal abstract class RoverDirectionStateBase
    {
        public abstract Direction Direction { get; }
        public abstract void MoveForward(IRover rover);
        public abstract void TurnLeft(IRover rover);
        public abstract void TurnRight(IRover rover);
    }
}
