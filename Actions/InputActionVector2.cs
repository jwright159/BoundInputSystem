using Godot;

namespace BoundInputSystem.Actions;

[GlobalClass, Tool]
public partial class InputActionVector2 : InputAction
{
	public override InputActionEmitter InstantiateEmitter() => new InputActionVector2Emitter { InputAction = this };
}