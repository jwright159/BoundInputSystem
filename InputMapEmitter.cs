using System.Collections.Generic;
using System.Linq;
using BoundInputSystem.Actions;
using Godot;

namespace BoundInputSystem;

[Tool]
public partial class InputMapEmitter : Node
{
	private InputMap? inputMap;
	[Export] public InputMap? InputMap
	{
		get => inputMap;
		set
		{
			inputMap.DisconnectIfValid(Resource.SignalName.Changed, InputMapChangedCallable);
			inputMap = value;
			inputMap.DisconnectIfValid(Resource.SignalName.Changed, InputMapChangedCallable);
			RenderingServer.Singleton.ConnectIfValid(RenderingServer.SignalName.FramePostDraw, PostFrameSetupCallable);
		}
	}
	
	[Export] private bool RegenerateEmitters
	{
		get => false;
		set
		{
			if (!value || !Engine.IsEditorHint()) return;
			UpdateEmitters();
		}
	}
	
	private Callable? postFrameSetupCallable;
	private Callable PostFrameSetupCallable => postFrameSetupCallable ??= new Callable(this, MethodName.PostFrameSetup);
	private void PostFrameSetup()
	{
		RenderingServer.Singleton.DisconnectIfValid(RenderingServer.SignalName.FramePostDraw, PostFrameSetupCallable);
		inputMap.ConnectIfValid(Resource.SignalName.Changed, InputMapChangedCallable);
		InputMapChanged();
	}
	
	private Callable? inputMapChangedCallable;
	private Callable InputMapChangedCallable => inputMapChangedCallable ??= new Callable(this, MethodName.InputMapChanged);
	private void InputMapChanged()
	{
		UpdateName();
		UpdateEmitters();
	}
	
	public void UpdateName()
	{
		Name = inputMap?.Name ?? "Null Input Map";
	}
	
	public void UpdateEmitters()
	{
		if (InputMap is null) return;
		if (!IsInsideTree()) return;
		
		Dictionary<InputAction, InputActionEmitter> emitters = InputSystemEmitter.GetSafeChildrenPropertyDictionary<InputAction, InputActionEmitter>(this, emitter => emitter.InputAction, InputMap.InputActions, action => action.Name);
		InputMap.InputActions.UpdateContentsOf(
			emitters.Values,
			action => emitters.GetValueOrDefault(action),
			action => action.InstantiateEmitter().AddToSceneUnder(this),
			emitter => emitter.Free(),
			MoveChild);
	}
}