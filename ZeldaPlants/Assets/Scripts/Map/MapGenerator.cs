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

    private readonly IList<float> _pathProbabilities;

	private bool _reachedBorderX;
	private bool _reachedBorderY;

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

    public MapGenerator(int sizeX, int sizeY, int noOfSpriteColumns, int noOfSpriteRows, IList<float> pathProbabilities)
    {
        _sizeX = sizeX;
        _sizeY = sizeY;

        _noOfSpriteColumns = noOfSpriteColumns;
        _noOfSpriteRows = noOfSpriteRows;

        _pathProbabilities = pathProbabilities;

		_reachedBorderX = false;
		_reachedBorderY = false;

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

		// Set initial sprite at position 0,0
		Map[currentX, currentY] = currentSpriteY * _noOfSpriteColumns + currentSpriteX;
	    AddDirections(currentX, currentY, currentSpriteX, currentSpriteY, spriteDirectionColumns, spriteDirectionRows, Direction.East);	
    }

	private void AddDirections(int currentX, int currentY, int currentSpriteX, int currentSpriteY, SpriteDirection spriteDirectionColumns, SpriteDirection spriteDirectionRows, Direction lastDirection)
    {
		var possibleDirections = GetPossibleDirections(currentX, currentY);
		
		if (possibleDirections.Any ()) {
			var directions = GetRandomDirections(possibleDirections);

			foreach (var direction in directions)
			{
				AddSpriteNo(currentX, currentY, currentSpriteX, currentSpriteY, spriteDirectionColumns, spriteDirectionRows, lastDirection, direction);
			} 
		}              
    }

	private void AddSpriteNo(int currentX, int currentY, int currentSpriteX, int currentSpriteY, SpriteDirection spriteDirectionColumns, SpriteDirection spriteDirectionRows, Direction lastDirection, Direction newDirection)
    {
		// Calculate new SpriteX and SpriteY


		/*
		if (newDirection == Direction.West || newDirection == Direction.East)
		{
			var newSpriteX = currentSpriteX + (int) spriteDirectionColumns;			

			if (newDirection != lastDirection) {
				spriteDirectionColumns = spriteDirectionColumns == SpriteDirection.Up ? SpriteDirection.Down : SpriteDirection.Up;
			} else {
				if (newSpriteX < 0 || newSpriteX > _noOfSpriteColumns - 1)
				{
					spriteDirectionColumns = spriteDirectionColumns == SpriteDirection.Up ? SpriteDirection.Down : SpriteDirection.Up;
					newSpriteX = currentSpriteX + (int) spriteDirectionColumns;
				}
			}
			currentSpriteX = newSpriteX;
		}
		else
		{
			var newSpriteY = currentSpriteY + (int) spriteDirectionRows;		

			if (newDirection != lastDirection) {
				spriteDirectionColumns = spriteDirectionColumns == SpriteDirection.Up ? SpriteDirection.Down : SpriteDirection.Up;
			} else {
			if (newSpriteY < 0 || newSpriteY > _noOfSpriteRows - 1)
			{
				spriteDirectionColumns = spriteDirectionColumns == SpriteDirection.Up ? SpriteDirection.Down : SpriteDirection.Up;
				newSpriteY = currentSpriteY + (int) spriteDirectionRows;
			}
			}
			
			currentSpriteY = newSpriteY;
		}
		*/

		switch (newDirection)
		{
		case Direction.West:
			currentX -= 1;
			currentSpriteX -= 1;
			break;
		case Direction.East:
			currentX += 1;
			currentSpriteX += 1;
			break;
		case Direction.North:
			currentY -= 1;
			currentSpriteY -= 1;
			break;
		case Direction.South:
			currentY += 1;
			currentSpriteY += 1;
			break;
		}

		if (Map [currentX, currentY] == -1) {
			var spriteNo = (currentSpriteY % _noOfSpriteRows) * _noOfSpriteColumns + (currentSpriteX % _noOfSpriteColumns);
			Map [currentX, currentY] = spriteNo;
						AddDirections (currentX, currentY, currentSpriteX, currentSpriteY, spriteDirectionColumns, spriteDirectionRows, newDirection);	
				}
	}


    private IEnumerable<Direction> GetRandomDirections(List<Direction> possibleDirections) {

        var directions = new List<Direction>();
        var pathProbabilityIndex = 0;

        while(possibleDirections.Any()) {
            var direction = possibleDirections.ToArray()[Random.Range(0, possibleDirections.Count())];

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

		if (x == _sizeX) {
			_reachedBorderX = true;
				}
		if (y == _sizeY) {
			_reachedBorderY = true;
				}

		if (_reachedBorderX && _reachedBorderY && _pathProbabilities.Count() > 1) {
			_pathProbabilities.RemoveAt(0);
		}
    }
}
