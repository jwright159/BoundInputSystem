using System.Linq;
using BoundInputSystem.Bindings;
using Godot;

namespace BoundInputSystem.Actions;

[Tool]
public abstract partial class InputActionEmitter : Node
{
	private InputAction? inputAction;
	[Export] public InputAction? InputAction
	{
		get => inputAction;
		set
		{
			inputAction.DisconnectIfValid(Resource.SignalName.Changed, InputActionChangedCallable);
			inputAction = value;
			inputAction.DisconnectIfValid(Resource.SignalName.Changed, InputActionChangedCallable);
			RenderingServer.Singleton.ConnectIfValid(RenderingServer.SignalName.FramePostDraw, PostFrameSetupCallable);
		}
	}
	
	public override void _Input(InputEvent ev)
	{
		if (Engine.IsEditorHint()) return;
		if (InputAction is null) return;
		
		foreach (InputBinding binding in InputAction.InputBindings.Where(bind => bind.IsMatch(ev)))
		{
			binding.Update(ev);
			EmitEventSignal(binding);
		}
	}
	
	private Callable? postFrameSetupCallable;
	private Callable PostFrameSetupCallable => postFrameSetupCallable ??= new Callable(this, MethodName.PostFrameSetup);
	private void PostFrameSetup()
	{
		RenderingServer.Singleton.DisconnectIfValid(RenderingServer.SignalName.FramePostDraw, PostFrameSetupCallable);
		inputAction.ConnectIfValid(Resource.SignalName.Changed, InputActionChangedCallable);
		InputActionChanged();
	}
	
	private Callable? inputActionChangedCallable;
	private Callable InputActionChangedCallable => inputActionChangedCallable ??= new Callable(this, MethodName.InputActionChanged);
	private void InputActionChanged()
	{
		UpdateName();
	}

	public void UpdateName()
	{
		Name = inputAction?.Name ?? "Null Input Action";
	}

	protected abstract void EmitEventSignal(InputBinding binding);
}