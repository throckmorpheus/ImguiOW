using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;
using ImGuiNET;
using UnityEngine.Rendering;

namespace OWImGui;

public class Main : ModBehaviour
{
	public static Main Instance;

	public void Awake()
	{
		Instance = this;
	}

	public void Start()
	{
		ModHelper.Console.WriteLine($"Imgui loaded.", MessageType.Success);

		//new Harmony("Throckmorpheus.Imgui").PatchAll(Assembly.GetExecutingAssembly());
	}
}

