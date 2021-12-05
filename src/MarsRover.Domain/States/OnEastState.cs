namespace MarsRover.Domain.States
{
    internal class OnEastState : RoverDirectionStateBase
    {
        public override Direction Direction => Direction.E;

        public override void MoveForward(IRover rover)
        {
            rover.State.X++; 
        }

        public override void TurnLeft(IRover rover)
        {
            rover.State.Direction = Direction.N;
        }

        public override void TurnRight(IRover rover)
        {
            rover.State.Direction = Direction.S;
        }
    }
}
