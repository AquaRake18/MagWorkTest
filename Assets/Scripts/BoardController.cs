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
			int k = Random.Range(0, n + 1);
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

	public static int GetAvailableLinks(SGridCoords boardSize, LinkerObject[,] linkerObjects) {
		List<LinkerObject> list = new List<LinkerObject>();
		for (int row = 0; row < boardSize._Row; ++row) {
			for (int column = 0; column < boardSize._Column; ++column) {
			}
		}
		return 1;
	}

	public static SGridCoords[] GetHint(LinkerObject[,] linkerObjects) {
		return new SGridCoords[0];
	}

	public static SGridCoords[] GetBestHint(LinkerObject[,] linkerObjects) {
		return new SGridCoords[0];
	}

	private static bool HasLinks(
		SGridCoords boardSize,
		SGridCoords gridPosition,
		LinkerObject[,] linkerObjects) {
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
