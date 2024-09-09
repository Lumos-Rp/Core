namespace HogWarp.Lib.Engine.Plugin
{
    public interface IPluginBase
    {
        string name { get; }
        string description { get; }
        void Initialize();
    }
}
