using LuaInterface;

public class LuaManager
{
    private static LuaManager instance;
    private LuaState luaState;

    public static LuaManager GetInstance() {
        if (instance == null)
            instance = new LuaManager();
        return instance;
    }

    public LuaState LuaState
    {
        get { return luaState; }
    }

    public void Init()
    {
        luaState = new LuaState();
        luaState.Start();
        DelegateFactory.Init();
    }

    public void DoString(string str, string chunkName = "LuaManager.cs")
    {
        luaState.DoString(str, chunkName);
    }

    public void Require(string fileName)
    {
        luaState.Require(fileName);
    }

    public void Dispose()
    {
        if (luaState == null)
            return;
        luaState.CheckTop();
        luaState.Dispose();
        luaState = null;
    }
}