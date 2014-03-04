using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator 
{

    private readonly int _sizeX;
    private readonly int _sizeY;

    private readonly int _noOfSpriteRows;
    private readonly int _noOfSpriteColumns;

    private readonly IList<double> _pathProbabilities;

    private enum Direction
    {
        North,
        West,
        South,
        East
    };

    private enum SpriteDirection
    {
        Up = 1,
        Down = -1
    };

    public int[,] Map { get; private set; }

    public MapGenerator(int sizeX, int sizeY, int noOfSpriteColumns, int noOfSpriteRows, IList<double> pathProbabilities)
    {
        _sizeX = sizeX;
        _sizeY = sizeY;

        _noOfSpriteColumns = noOfSpriteColumns;
        _noOfSpriteRows = noOfSpriteRows;

        _pathProbabilities = pathProbabilities;

        Map = new int[0, 0];
    }
	
    public void Generate() {
        Map = new int [_sizeX, _sizeY];

        for (int x = 0; x < _sizeX; x++)
        {
            for (var y = 0; y < _sizeY; y++)
            {
                Map[x, y] = -1;
            }
        }

        var currentX = 0;
        var currentY = 0;
        var currentSpriteX = 0;
        var currentSpriteY = 0;
        var spriteDirectionColumns = SpriteDirection.Up;
        var spriteDirectionRows = SpriteDirection.Up;

		Debug.Log ("Retrieving initial possible directions");
		var possibleDirections = GetPossibleDirections(currentX, currentY);

        while (possibleDirections.Any())
        {
            var directions = GetRandomDirections(possibleDirections);
            AddSpriteNoToMap(ref currentX, ref currentY, ref currentSpriteX, ref currentSpriteY, directions, ref spriteDirectionColumns, ref spriteDirectionRows);

            Debug.Log("Retrieving possible directions");
            possibleDirections = GetPossibleDirections(currentX, currentY);
        }
    }

    private void AddSpriteNoToMap(ref int currentX, ref int currentY, ref int currentSpriteX, ref int currentSpriteY, IEnumerable<Direction> directions, ref SpriteDirection spriteDirectionColumns, ref SpriteDirection spriteDirectionRows)
    {
        foreach (var direction in directions)
        {
            CheckSpriteDirections(ref currentSpriteX, ref currentSpriteY, ref spriteDirectionColumns, ref spriteDirectionRows, direction);

            switch (direction)
            {
                case Direction.West:
                    currentX -= 1;
                    break;
                case Direction.East:
                    currentX += 1;
                    break;
                case Direction.North:
                    currentY -= 1;
                    break;
                case Direction.South:
                    currentX += 1;
                    break;
            }

            Map[currentX, currentY] = currentSpriteY * _noOfSpriteColumns + currentSpriteX;
        }                
    }

    private void CheckSpriteDirections(ref int currentSpriteX, ref int currentSpriteY, ref SpriteDirection spriteDirectionColumns, ref SpriteDirection spriteDirectionRows, Direction direction)
    {
        if (direction == Direction.West || direction == Direction.East)
        {
            var newSpriteX = currentSpriteX + (int) spriteDirectionColumns;

            if (newSpriteX < 0 || newSpriteX > _noOfSpriteColumns - 1)
            {
                spriteDirectionColumns = spriteDirectionColumns == SpriteDirection.Up
                    ? SpriteDirection.Down
                    : SpriteDirection.Up;
                newSpriteX = currentSpriteX + (int) spriteDirectionColumns;
            }

            currentSpriteX = newSpriteX;
        }
        else
        {
            var newSpriteY = currentSpriteY + (int) spriteDirectionRows;

            if (newSpriteY < 0 || newSpriteY > _noOfSpriteRows - 1)
            {
                spriteDirectionColumns = spriteDirectionColumns == SpriteDirection.Up
                    ? SpriteDirection.Down
                    : SpriteDirection.Up;
                newSpriteY = currentSpriteY + (int) spriteDirectionRows;
            }

            currentSpriteY = newSpriteY;
        }
    }


    private IEnumerable<Direction> GetRandomDirections(List<Direction> possibleDirections) {

        var directions = new List<Direction>();
        var pathProbabilityIndex = 0;

        while(possibleDirections.Any()) {
            var direction = possibleDirections.ToArray()[Random.Range(0, possibleDirections.Count() - 1)];

            var randomNo = Random.Range(0f, 1f);
            if (randomNo <= _pathProbabilities[pathProbabilityIndex]) {
                directions.Add(direction);

                if (pathProbabilityIndex < _pathProbabilities.Count() - 1) {
                    pathProbabilityIndex++;
                }
            }
            
            possibleDirections.Remove(direction);
        }

        return directions;
    }

    private List<Direction> GetPossibleDirections(int currentX, int currentY)
    {
        var directions = new List<Direction>();

        CheckDirection(directions, currentX - 1, currentY, Direction.West);
        CheckDirection(directions, currentX + 1, currentY, Direction.East);
        CheckDirection(directions, currentX, currentY - 1, Direction.North);
        CheckDirection(directions, currentX, currentY + 1, Direction.South);

        return directions;
    }

    private void CheckDirection(ICollection<Direction> directions, int x, int y, Direction direction)
    {
        if (x >= 0 && y >= 0 && x < _sizeX && y < _sizeY && Map[x, y] == -1)
        {
            directions.Add(direction);
        }            
    }
}
