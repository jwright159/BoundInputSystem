using Godot;
using Godot.Collections;

namespace BoundInputSystem;

[GlobalClass, Tool]
public partial class InputSystem : Resource
{
	private Array<InputMap> inputMaps = new();
	[Export] public Array<InputMap> InputMaps
	{
		get => inputMaps;
		set
		{
			inputMaps = value;
			EmitChanged();
		}
	}
}
