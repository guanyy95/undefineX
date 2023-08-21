require("Test")
--主入口函数。从这里开始lua逻辑
function Main()
    print("Logic Start")
end

--场景切换通知
function OnLevelWasLoaded(level)
    collectgarbage("collect")
    Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end

local function Split(input, sepStr, maxSplit)
    if string.len(line) == 0 then
        return {}
    end
    sepStr = sepStr or " "
    maxSplit = maxSplit or 0
    local retval = {}
    local pos = 1
    local step = 0
    while true do
        local from, to = string.find(input, sepStr, pos, true)
        step = step + 1
        if (maxSplit ~= 0 and step > maxSplit) or not from then
            local item = string.sub(input, pos)
            table.insert(retval, item)
            break
        else
            local item = string.sub(input, pos, from - 1)
            table.insert(retval, item)
            pos = to + 1
        end
    end
    return retval
end
