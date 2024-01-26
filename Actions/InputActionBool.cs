using Godot;

namespace BoundInputSystem.Actions;

[GlobalClass, Tool]
public partial class InputActionBool : InputAction
{
	public override InputActionEmitter InstantiateEmitter() => new InputActionBoolEmitter { InputAction = this };
}