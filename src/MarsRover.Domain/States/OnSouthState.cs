namespace MarsRover.Domain.States
{
    internal class OnSouthState : RoverDirectionStateBase
    {
        public override Direction Direction => Direction.S;

        public override void MoveForward(IRover rover)
        {
            rover.State.Y--;
        }

        public override void TurnLeft(IRover rover)
        {
            rover.State.Direction = Direction.E;
        }

        public override void TurnRight(IRover rover)
        {
            rover.State.Direction = Direction.W;
        }
    }
}
