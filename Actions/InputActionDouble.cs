using Godot;

namespace BoundInputSystem.Actions;

[GlobalClass, Tool]
public partial class InputActionDouble : InputAction
{
	public override InputActionEmitter InstantiateEmitter() => new InputActionDoubleEmitter { InputAction = this };
}