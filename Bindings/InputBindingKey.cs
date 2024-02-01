using Godot;

namespace BoundInputSystem.Bindings;

[GlobalClass, Tool]
public partial class InputBindingKey : InputBinding
{
	[Export] public InputEventKey? Event;
	
	private bool wasPressed;
	private bool isPressed;
	
	public override void Update(InputEvent ev)
	{
		if (!IsMatch(ev)) return;
		wasPressed = isPressed;
		isPressed = ev.IsPressed();
	}
	
	public override bool IsMatch(InputEvent ev) => Event is not null && Event.IsMatch(ev, true) && Event.IsEcho() == ev.IsEcho();
	
	public override float FloatValue => isPressed ? 1 : 0;
	
	public override double DoubleValue => isPressed ? 1 : 0;
	
	public override bool BoolValue => isPressed;
	
	public override Vector2 Vector2Value => new(){ X = DoubleValue };
	
	public override Vector3 Vector3Value => new(){ X = DoubleValue };
	
	public override bool WasPressedThisFrame => isPressed && !wasPressed;
	
	public override bool WasReleasedThisFrame => !isPressed && wasPressed;
}