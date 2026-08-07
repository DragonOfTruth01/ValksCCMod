using Nickel;

namespace DragonOfTruth01.ValksCCMod;

internal interface IValksCCModCard
{
    static abstract void Register(IModHelper helper);
}

internal interface IValksCCModArtifact
{
    static abstract void Register(IModHelper helper);
}
