using Godot;
using System;

public class Back : TextureButton
{
	private void onButtonUp()
	{
		GetTree().ChangeScene("res://src/Screens/MainScreen.tscn");
	}
}
