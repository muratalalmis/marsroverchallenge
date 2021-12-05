using System;
using System.Collections.Generic;

namespace MarsRover.Domain
{
    public class RoverFactory
    {
        public static IRover Create(Plateau plateau, int x, int y, Direction direction)
        {
            return new Rover(plateau, x, y, direction);
        }

        class Rover : IRover
        {
            private readonly List<Command> _navigation;
            private readonly RoverStateMachine _roverStateMachine;

            public Rover(Plateau plateau, int x, int y, Direction direction)
            {
                State = new DirectionState(plateau)
                {
                    X = x,
                    Y = y,
                    Direction = direction
                };
                _navigation = new List<Command>();
                _roverStateMachine = new RoverStateMachine(this);
            }

            public DirectionState State { get; }
            public IEnumerable<Command> Navigation => _navigation;

            public void SetNavigation(string commands)
            {
                _navigation.Clear();

                foreach (var item in commands)
                {
                    if (!Enum.TryParse(typeof(Command), item.ToString(), out var command))
                    {
                        throw new Exception($"Unsupported command detected ({command}).");
                    }

                    _navigation.Add((Command)command);
                }
            }

            public void GoToNavigation()
            {
                foreach (var command in Navigation)
                {
                    _roverStateMachine.Trigger(command);
                }
            }

            public void MoveForward()
            {
                _roverStateMachine.Trigger(Command.M);
            }

            public void TurnLeft()
            {
                _roverStateMachine.Trigger(Command.L);
            }

            public void TurnRight()
            {
                _roverStateMachine.Trigger(Command.R);
            }
        }
    }
}
