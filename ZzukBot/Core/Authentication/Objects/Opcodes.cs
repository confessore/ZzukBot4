namespace ZzukBot.Core.Authentication.Objects
{
    internal enum Opcodes : uint
    {
        Ping = 0,
        Version = 1,
        Update = 2,
        User = 3,
        Warden = 4,
        Success = 5,
        Failure = 6,
        Full = 7,
        Trial = 8,
        Profile = 9
    }
}
