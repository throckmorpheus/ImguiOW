# Dear ImGui
Basic support for the immediate mode GUI library [Dear ImGui](https://github.com/ocornut/imgui) using [ImGui.NET](https://github.com/ImGuiNET/ImGui.NET), adapted from the [UImGui](https://github.com/psydack/uimgui) Unity implementation.

## Usage
Add 'Throckmorpheus.ImGuiOW' as a dependency in your mod manifest, and add the nuget package [ImGuiOuterWildsAPI](https://www.nuget.org/packages/ImGuiOuterWildsAPI/) as a dependency in your csproj file. This will give you both the mod API and the correct ImGui dependencies.

The API exposes a Layout event, which fires every time ImGui redraws. During this time, calls to the ImGui methods will work correctly.

If you are planning to use this as an optional dependency, do note that the mod template defaults to not copying across the dlls of dependencies (to avoid copying all the OWML stuff into the folder of every mod), so when the ImGuiOW mod is not present referencing the API or ImGui will cause an exception as it tries to call into a non-existent dll.
To fix this, you can set `CopyLocalLockFileAssemblies` to `true` in your project file to make your mod include its dependencies. If doing this, you can avoid copying all the OMWL and OuterWildsGameLibs dlls by adding `<IncludeAssets>compile</IncludeAssets>` to their package reference delcarations, eg:
```
<PackageReference Include="OWML" Version="2.13.0">
    <IncludeAssets>compile</IncludeAssets>
</PackageReference>
<PackageReference Include="OuterWildsGameLibs" Version="1.1.15.1018">
    <IncludeAssets>compile</IncludeAssets>
</PackageReference>
```

## Limitations
- When ImGui is rebuilding its meshes while a lot of other processing is going on, it causes weird flickering in the parts that are changing. This notably means it happens whenever in the Solar System scene. While UImGui has a [fix for this](https://github.com/psydack/uimgui/issues/17), it only works in the 2020 version of Unity, and thus cannot be applied here.
- ImGui is rendered on top of the active camera, which due to how canvases work in Unity means it renders under GUI such as the pause menu.
- The mod is not currently stopping input events from passing through to the game when they should be being consumed by ImGui. This isn't a fundamental limitation like the first two, I just need to get around to it.
- Does not currently expose ImGuizmo, ImPlot or ImNodes like UImGui does. Again I just need to get around to this.