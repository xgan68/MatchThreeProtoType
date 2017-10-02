using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate : System.IEquatable<Coordinate> {
	
	int x;
	int y;
	bool visited;

	public Coordinate(int x, int y) {
		this.x = x;
		this.y = y;
		visited = true;
	}

	public void setVisited(bool v) {
		visited = v;
	}

	public bool isVisited() {
		return visited;
	}

	public override int GetHashCode() {
		return ((x + y) * (x + y + 1) / 2) + y;
	}

	public override bool Equals(object obj) {
		return Equals (obj as Coordinate);
	}

	public bool Equals(Coordinate coor) {
		return coor.x == this.x && coor.y == this.y;
	}
}
