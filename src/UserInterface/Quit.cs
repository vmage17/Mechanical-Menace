using Godot;
using System;

public class Quit : TextureButton
{
	private void onButtonUp()
	{
		GetTree().Quit();
	}
}
