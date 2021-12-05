namespace MarsRover.Domain.States
{
    internal class OnWestState : RoverDirectionStateBase
    {
        public override Direction Direction => Direction.W;

        public override void MoveForward(IRover rover)
        {
            rover.State.X--;
        }

        public override void TurnLeft(IRover rover)
        {
            rover.State.Direction = Direction.S;
        }

        public override void TurnRight(IRover rover)
        {
            rover.State.Direction = Direction.N;
        }
    }
}
