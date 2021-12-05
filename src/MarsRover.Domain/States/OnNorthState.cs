namespace MarsRover.Domain.States
{
    internal class OnNorthState : RoverDirectionStateBase
    {
        public override Direction Direction => Direction.N;

        public override void MoveForward(IRover rover)
        {
            rover.State.Y++;
        }

        public override void TurnLeft(IRover rover)
        {
            rover.State.Direction = Direction.W;
        }

        public override void TurnRight(IRover rover)
        {
            rover.State.Direction = Direction.E;
        }
    }
}
