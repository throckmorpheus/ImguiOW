using OWML.Common;
using OWML.ModHelper;
using ImGuiNET;
using UImGui;
using UnityEngine;
using System.Linq;
using HarmonyLib;
using System.Reflection;
using ImGuiOW.API;
using System;

namespace ImGuiOW;

public class ImGuiOW : ModBehaviour
{
	public static ImGuiOW Instance;
	public static IModConsole Console => Instance?.ModHelper?.Console;
	
	public override object GetApi() => new ImGuiAPI();
	
	public event Action Layout;
	
	private UImGui.UImGui _uImGui = null;

	private Camera _activeCamera = null;
	public Camera ActiveCamera {
		get => _activeCamera;
		set {
			_activeCamera = value;
			if (ActiveCamera is not null) _uImGui?.SetCamera(ActiveCamera);
			_uImGui.enabled = ActiveCamera is not null;
			Console.Message($"Switch ImGui camera to {ActiveCamera switch { null => "null", _ => ActiveCamera }}");
		}
	}

	public void Awake() => Instance = this;

	public void Start() {
		Console.Success("ImGui loaded.");
		
		//new Harmony("Throckmorpheus.ImGuiOW").PatchAll(Assembly.GetExecutingAssembly());

		_uImGui = gameObject.AddComponent<UImGui.UImGui>();

		if (ModHelper.Assets.LoadBundle("assets/shaders")?.LoadAllAssets()?.SingleOrDefault() is Shader shader) {
			_uImGui?.SetShader(shader); Console.Info("Loaded UImGui shader.");
		} else Console.Error("Failed to load shaders.");

		OnCompleteSceneChange(OWScene.None, OWScene.TitleScreen);
		ModHelper.Events.Scenes.OnCompleteSceneChange += OnCompleteSceneChange;
		GlobalMessenger<OWCamera>.AddListener("SwitchActiveCamera", OnSwitchActiveCamera);
		GlobalMessenger.AddListener("WakeUp", OnPlayerWakeUp);

		_uImGui.Layout += OnUImGuiLayout;
	}

	public void OnDestroy() {
		GlobalMessenger<OWCamera>.RemoveListener("SwitchActiveCamera", OnSwitchActiveCamera);
		GlobalMessenger.RemoveListener("WakeUp", OnPlayerWakeUp);
	}

	private void OnUImGuiLayout(UImGui.UImGui uImGui) => Layout?.Invoke();

	public void SetCamera(OWCamera camera) => ActiveCamera = camera?.mainCamera ?? camera?.farCamera;
	public void SetCamera(Camera camera) => ActiveCamera = camera;

	private void OnSwitchActiveCamera(OWCamera camera) => SetCamera(camera);
	private void OnCompleteSceneChange(OWScene oldScene, OWScene newScene) => FindCamera();
	private void OnPlayerWakeUp() => FindCamera();
	
	private void FindCamera() {
		if (Locator._playerCamera is OWCamera playerCamera) SetCamera(playerCamera);
		else if (FindObjectOfType<OWCamera>() is OWCamera owCamera) SetCamera(owCamera);
		else SetCamera(FindObjectOfType<Camera>());
	}
}

