using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace BoundInputSystem;

[Tool]
public partial class InputSystemEmitter : Node
{
	private InputSystem? inputSystem;
	[Export] public InputSystem? InputSystem
	{
		get => inputSystem;
		set
		{
			inputSystem.DisconnectIfValid(Resource.SignalName.Changed, InputSystemChangedCallable);
			inputSystem = value;
			inputSystem.DisconnectIfValid(Resource.SignalName.Changed, InputSystemChangedCallable);
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
		inputSystem.ConnectIfValid(Resource.SignalName.Changed, InputSystemChangedCallable);
		InputSystemChanged();
	}
	
	private Callable? inputSystemChangedCallable;
	private Callable InputSystemChangedCallable => inputSystemChangedCallable ??= new Callable(this, MethodName.InputSystemChanged);
	private void InputSystemChanged()
	{
		UpdateEmitters();
	}
	
	public void UpdateEmitters()
	{
		if (InputSystem is null) return;
		if (!IsInsideTree()) return;
		
		Dictionary<InputMap, InputMapEmitter> emitters = GetSafeChildrenPropertyDictionary<InputMap, InputMapEmitter>(this, emitter => emitter.InputMap, InputSystem.InputMaps, map => map.Name);
		InputSystem.InputMaps.UpdateContentsOf(
			emitters.Values,
			map => emitters.GetValueOrDefault(map),
			map => new InputMapEmitter { InputMap = map }.AddToSceneUnder(this),
			emitter => emitter.Free(),
			MoveChild);
		
		GetChildren().Cast<InputMapEmitter>().ForEach(emitter => emitter.UpdateEmitters());
	}
	
	internal static Dictionary<TKey, TValue> GetSafeChildrenPropertyDictionary<TKey, TValue>(Node node, Func<TValue, TKey?> keySelector, IEnumerable<TKey> keyList, Func<TKey, string> backupKeyNameSelector) where TValue : Node where TKey : notnull =>
		node.GetChildren().Cast<TValue>().PairKey(child => keySelector(child) ?? keyList.FirstOrDefault(key => backupKeyNameSelector(key) == child.Name)).KeyNotNull().ToDictionary();
}