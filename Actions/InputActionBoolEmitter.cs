using BoundInputSystem.Bindings;
using Godot;

namespace BoundInputSystem.Actions;

[Tool]
public partial class InputActionBoolEmitter : InputActionEmitter
{
	[Signal]
	public delegate void ValueChangedEventHandler(bool value);
	
	[Signal]
	public delegate void PressedEventHandler();
	
	[Signal]
	public delegate void ReleasedEventHandler();
	
	protected override void EmitEventSignal(InputBinding binding)
	{
		EmitSignal(SignalName.ValueChanged, binding.BoolValue);
		
		if (binding.WasPressedThisFrame)
			EmitSignal(SignalName.Pressed);
		
		if (binding.WasReleasedThisFrame)
			EmitSignal(SignalName.Released);
	}
}