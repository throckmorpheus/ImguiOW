using System;

namespace ImGuiOW.API;

public class ImGuiAPI : IImGuiAPI
{
    public event Action Layout { add => ImGuiOW.Instance.Layout += value; remove => ImGuiOW.Instance.Layout -= value; }
}