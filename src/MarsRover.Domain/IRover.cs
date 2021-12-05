using System.Collections.Generic;

namespace MarsRover.Domain
{
    public interface IRover
    {
        DirectionState State { get; }
        IEnumerable<Command> Navigation { get; }

        void SetNavigation(string commands);
        void GoToNavigation();
        void MoveForward();
        void TurnLeft();
        void TurnRight();
    }
}
