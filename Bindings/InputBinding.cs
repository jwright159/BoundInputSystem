using System;
using Godot;

namespace BoundInputSystem.Bindings;

[GlobalClass, Tool]
public abstract partial class InputBinding : Resource
{
	private static bool IsMatch(InputEvent? bindingEv, InputEvent ev) => bindingEv is not null && bindingEv.GetType().IsInstanceOfType(ev) && ev switch
	{
		InputEventAction inputEventAction => bindingEv.IsMatch(inputEventAction),
		InputEventMagnifyGesture => true,
		InputEventPanGesture => true,
		InputEventKey inputEventKey => bindingEv.IsMatch(inputEventKey),
		InputEventMouseButton inputEventMouseButton => bindingEv.IsMatch(inputEventMouseButton),
		InputEventMouseMotion => true,
		InputEventScreenDrag => true,
		InputEventScreenTouch => true,
		InputEventJoypadButton inputEventJoypadButton => bindingEv.IsMatch(inputEventJoypadButton),
		InputEventJoypadMotion inputEventJoypadMotion => bindingEv.IsMatch(inputEventJoypadMotion),
		_ => throw new ArgumentException($"Input binding matched against an unsupported event type: {ev}")
	};
	
	public abstract void Update(InputEvent ev);
	
	public abstract bool IsMatch(InputEvent ev);
	
	public abstract float FloatValue { get; }
	public abstract double DoubleValue { get; }
	public abstract bool BoolValue { get; }
	public abstract Vector2 Vector2Value { get; }
	public abstract Vector3 Vector3Value { get; }
	
	public abstract bool WasPressedThisFrame { get; }
	public abstract bool WasReleasedThisFrame { get; }
}