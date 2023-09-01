---
--- Lua工具类
--- Created by GYY
--- DateTime: 2023/9/1 11:02
---

function string.split(input, sepStr, maxSplit)
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