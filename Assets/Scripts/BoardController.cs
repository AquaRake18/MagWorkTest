using System;
using System.Collections.Generic;
using UnityEngine;

public static class BoardController {
	public static void ShuffleBoard(
		SGridCoords boardSize,
		ref BoardTile[,] boardTiles,
    	ref LinkerObject[,] linkerObjects) {
		int n = linkerObjects.Length;
		while (n > 1) {
			--n;
			int k = UnityEngine.Random.Range(0, n + 1);
			SGridCoords posK = ArrayIndexToCoords(boardSize, k);
			SGridCoords posN = ArrayIndexToCoords(boardSize, n);
			LinkerObject value = linkerObjects[posK._Column, posK._Row];
			linkerObjects[posK._Column, posK._Row] = linkerObjects[posN._Column, posN._Row];
			linkerObjects[posK._Column, posK._Row].transform.parent = boardTiles[posK._Column, posK._Row].transform;
			linkerObjects[posK._Column, posK._Row].transform.position = boardTiles[posK._Column, posK._Row].transform.position;
			linkerObjects[posK._Column, posK._Row]._GridCoords = new SGridCoords(posK._Column, posK._Row);
			linkerObjects[posN._Column, posN._Row] = value;
			linkerObjects[posN._Column, posN._Row].transform.parent = boardTiles[posN._Column, posN._Row].transform;
			linkerObjects[posN._Column, posN._Row].transform.position = boardTiles[posN._Column, posN._Row].transform.position;
			linkerObjects[posN._Column, posN._Row]._GridCoords = new SGridCoords(posN._Column, posN._Row);
		}
	}

	public static bool HasAnyThreeOrMoreChains(SGridCoords boardSize, LinkerObject[,] linkerObjects) {
		List<LinkerObject> currentLinks = new List<LinkerObject>();
		for (int row = 0; row < boardSize._Row; ++row) {
			for (int column = 0; column < boardSize._Column; ++column) {
				currentLinks.Add(linkerObjects[column, row]);
				while (TryAddLink(ref currentLinks, boardSize, linkerObjects)) {}
				if (currentLinks.Count >= 3) {
					return true;
				}
				currentLinks.Clear();
			}
		}
		return false;
	}

	private static bool TryAddLink(
		ref List<LinkerObject> currentLinks,
		SGridCoords boardSize,
		LinkerObject[,] allLinkerObjects) {
		LinkerObject focusedLinker = currentLinks[currentLinks.Count - 1];
		int direction = 0;
		int directionCount = Enum.GetNames(typeof(EDirection)).Length;
		while (direction < directionCount) {
			SGridCoords otherCoords = focusedLinker._GridCoords.GetRelativeCoords((EDirection)direction);
			if (!OutOfBounds(boardSize, otherCoords)) {
				LinkerObject other = allLinkerObjects[otherCoords._Column, otherCoords._Row];
				if (!currentLinks.Contains(other)
					&& focusedLinker.gameObject.CompareTag(other.gameObject.tag)) {
					currentLinks.Add(other);
					return true;
				}
			}
			++direction;
		}
		return false;
	}

	private static bool OutOfBounds(
		SGridCoords boardSize,
		SGridCoords gridCoords) {
		if (gridCoords._Row < 0) {
			// North
			return true;
		} else if (gridCoords._Row >= boardSize._Row) {
			// South
			return true;
		} else if (gridCoords._Column < 0) {
			// West
			return true;
		} else if (gridCoords._Column >= boardSize._Column) {
			// East
			return true;
		}
		return false;
	}

	private static SGridCoords ArrayIndexToCoords(SGridCoords boardSize, int index) {
		int boardIndex = 0;
		for (int y = 0; y < boardSize._Row; ++y) {
			for (int x = 0; x < boardSize._Column; ++x) {
				if (index == boardIndex) {
					return new SGridCoords(x, y);
				}
				++boardIndex;
			}
		}
		Debug.LogError("Suggested index out of bounds: " + index + " in 0-" + (boardSize._Row * boardSize._Row - 1));
		return new SGridCoords();
	}
}
