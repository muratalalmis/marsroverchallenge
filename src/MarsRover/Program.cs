using MarsRover.Core;
using MarsRover.Core.Services;
using MarsRover.Domain;
using System;
using System.Linq;

namespace MarsRover
{
    internal class Program
    {
        private static ILogger Logger;

        static Program()
        {
            ConfigureServices();
        }

        static void Main(string[] args)
        {
            try
            {
                if (TryInitializePlateau(out var plateau))
                {
                    void AddRecursive()
                    {
                        TryAddRover(plateau);

                        if (TryGetValue("Would you want to add another? (Y/N)", out var value))
                        {
                            if (value == "Y")
                            {
                                AddRecursive();
                            }
                        }
                    }

                    AddRecursive();

                    foreach (var rover in plateau.Rovers)
                    {
                        rover.GoToNavigation();

                        Console.WriteLine("{0} {1} {2}", rover.State.X, rover.State.Y, rover.State.Direction);
                    }
                }
                else
                {
                    throw new Exception("Plateau could not be initalized.");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                Console.WriteLine("An error occured. Application will be shutdown.");
                Console.ReadLine();
            }
        }

        static bool TryInitializePlateau(out Plateau plateau)
        {
            plateau = default;

            Plateau plateauInternal = default;
            TryAgain(() =>
            {
                plateauInternal = new Plateau(Logger);
                if (TryGetValues("Type text to plateau dimentions (e.g : 5 5)", out var limitStringArray))
                {
                    var limits = limitStringArray.Select(o => int.Parse(o)).ToArray();
                    if (limits.Length != 2)
                    {
                        throw new Exception("You must enter 2 limit, that should be x and y coordinates");
                    }

                    plateauInternal.Initialize(limits[0], limits[1]);
                }
            });

            if (plateauInternal == null)
            {
                return false;
            }

            plateau = plateauInternal;

            return true;
        }

        static bool TryAddRover(Plateau plateau)
        {
            bool response = default;

            TryAgain(() =>
            {
                if (TryGetValues("Type text to rover position (e.g : 1 2 N)", out var coordinates))
                {
                    if (coordinates.Length != 3)
                    {
                        throw new Exception("You must enter x, y and the direction.");
                    }

                    if (!int.TryParse(coordinates[0], out var x))
                    {
                        throw new Exception("Position x must be integer");
                    }

                    if (!int.TryParse(coordinates[1], out var y))
                    {
                        throw new Exception("Position y must be integer");
                    }

                    if (!Enum.TryParse(typeof(Direction), coordinates[2], out var direction))
                    {
                        throw new Exception("Direction must be N,S,E or W");
                    }

                    if (!plateau.TryLocateRover(x, y, (Direction)direction, out var rover))
                    {
                        throw new Exception("Rover could not be located.");
                    }

                    if (TryGetValue("Type text to rover commands (L,R or M)", out var commands))
                    {
                        rover.SetNavigation(commands);
                    }

                    response = true;
                }
            });

            return response;
        }

        static bool TryAgain(Action action)
        {
            try
            {
                action.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (TryGetValue("Would you want to try again? (Y/N)", out var tryAgain) && tryAgain == "Y")
                {
                    action();
                }

                return false;
            }
        }

        static bool TryGetValues(string message, out string[] values)
        {
            values = default;
            if (TryGetValue(message, out var value))
            {
                values = value.Split(' ');

                return true;
            }

            return false;
        }

        static bool TryGetValue(string message, out string value)
        {
            value = default;
            Console.WriteLine(message);
            try
            {
                value = Console.ReadLine();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        static void ConfigureServices()
        {
            Logger = new ConsoleLogger();
        }
    }
}
