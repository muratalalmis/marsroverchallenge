using System;

namespace MarsRover.Domain
{
    public class DirectionState
    {
        private int x, y;
        private readonly Plateau _plateau;

        public DirectionState(Plateau plateau)
        {
            _plateau = plateau;
        }

        public int X
        {
            get => x;
            internal set
            {
                if (value > _plateau.XLimit || value < 0)
                {
                    throw new Exception("Out of the plateau");
                }

                x = value;
            }
        }

        public int Y
        {
            get => y;
            internal set
            {
                if (value > _plateau.YLimit || value < 0)
                {
                    throw new Exception("Out of the plateau");
                }

                y = value;
            }
        }

        public Direction Direction { get; internal set; }
    }
}
