
using OWML.Common;

namespace ImGuiOW;

public static class ModConsoleExtensions
{
    public static void LoadedMessage<T>(this IModConsole console, T obj) =>
        console?.WriteLine(
            $"{typeof(T).Name} {(obj is not null ? "loaded" : "failed to load")}.",
            obj is not null ? MessageType.Success : MessageType.Error
        );
    
    public static void Message<T>(this IModConsole console, string message, MessageType messageType = MessageType.Message) =>
        console?.WriteLine(message, messageType, typeof(T).Name);

    public static void Message<T>(this IModConsole console, T _from, string message, MessageType messageType = MessageType.Message) =>
        console.Message<T>(message, messageType);
}