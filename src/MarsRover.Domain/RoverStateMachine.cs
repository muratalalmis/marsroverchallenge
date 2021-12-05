using MarsRover.Domain.States;
using System;
using System.Collections.Generic;

namespace MarsRover.Domain
{
    internal class RoverStateMachine
    {
        private readonly IDictionary<Direction, RoverDirectionStateBase> _directionHandlers;
        private readonly IRover _rover;

        /// <summary>
        /// TODO : there is no IoC container here, and I don't want to use reflection (Activator.CreateInstance)
        /// </summary>
        public RoverStateMachine(IRover rover)
        {
            _directionHandlers = new Dictionary<Direction, RoverDirectionStateBase>();
            _directionHandlers.Add(Direction.E, new OnEastState());
            _directionHandlers.Add(Direction.N, new OnNorthState());
            _directionHandlers.Add(Direction.S, new OnSouthState());
            _directionHandlers.Add(Direction.W, new OnWestState());
            _rover = rover;
        }

        public void Trigger(Command command)
        {
            if (!_directionHandlers.ContainsKey(_rover.State.Direction))
            {
                throw new NotImplementedException($"Direction state is not implmented ({_rover.State.Direction})");
            }

            switch (command)
            {
                case Command.L:
                    _directionHandlers[_rover.State.Direction].TurnLeft(_rover);
                    break;
                case Command.R:
                    _directionHandlers[_rover.State.Direction].TurnRight(_rover);
                    break;
                case Command.M:
                    _directionHandlers[_rover.State.Direction].MoveForward(_rover);
                    break;
            }
        }
    }
}
