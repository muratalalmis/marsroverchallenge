using MarsRover.Core;
using System;
using System.Collections.Generic;

namespace MarsRover.Domain
{
    public class Plateau
    {
        private readonly ILogger _logger;
        private readonly List<IRover> _rovers;

        public Plateau(ILogger logger)
        {
            _logger = logger;
            _rovers = new List<IRover>();
        }

        public int XLimit { get; private set; }
        public int YLimit { get; private set; }
        public bool IsInitialized { get; private set; }
        public IEnumerable<IRover> Rovers => _rovers;

        /// <summary>
        /// Set plateau dimentions
        /// </summary>
        /// <param name="x">Horizontal length of the plateau</param>
        /// <param name="y">Vertical length of the plateau</param>
        public void Initialize(int x, int y)
        {
            if (IsInitialized)
            {
                throw new Exception("Plateau is already initialized.");
            }

            if (x < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            XLimit = x;
            YLimit = y;
            IsInitialized = true;
        }

        public bool TryLocateRover(int x, int y, Direction direction, out IRover rover)
        {
            rover = default;

            try
            {
                if (!IsInitialized)
                {
                    throw new Exception("Plateau area is not configured. Plaease set size with Initialize method.");
                }

                rover = LocateInternal(x, y, direction);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                return false;
            }
        }

        IRover LocateInternal(int x, int y, Direction direction)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            var rover = RoverFactory.Create(this, x, y, direction);
            _rovers.Add(rover);

            return rover;
        }
    }
}
