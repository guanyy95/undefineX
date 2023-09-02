local rawset = rawset
require("Base.define")

-- 全局函数
-- 用于声明全局变量
function def(name, value)
    rawset(_G, name, value)
end

-- 初始化模块
local function init()
    
end


-- 初始化全局定义
local function globalDefine()
    
end

-- 初始化一些参数
local function initParam()
    -- 初始化随机种子
    math.randomseed(tostring(os.time()):reverse():sub(1, 6))

    --垃圾收集器间歇率控制着收集器需要在开启新的循环前要等待多久。 增大这个值会减少收集器的积极性。
    --当这个值比 100 小的时候，收集器在开启新的循环前不会有等待。 设置这个值为 200 就会让收集器等到总内存使用量达到之前的两倍时才开始新的循环。
    collectgarbage("setpause", 99)
    --垃圾收集器步进倍率控制着收集器运作速度相对于内存分配速度的倍率。 增大这个值不仅会让收集器更加积极，还会增加每个增量步骤的长度。
    --不要把这个值设得小于 100 ， 那样的话收集器就工作的太慢了以至于永远都干不完一个循环。 默认值是 200 ，这表示收集器以内存分配的"两倍"速工作。
    collectgarbage("setstepmul", 2000)
    --重启垃圾收集器的自动运行
    collectgarbage("restart")
end

--主入口函数。从这里开始lua逻辑
function Main()
    
end

--场景切换通知
function OnLevelWasLoaded(level)
    collectgarbage("collect")
    Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end