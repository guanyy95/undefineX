local __g = _G
local rawset = rawset
local rawget = rawget

setmetatable(__g, {
    __newindex = function(_, name, value)
        --全局环境一旦赋值后会调用这里设置的__newindex元方法，提示错误，并不执行赋值
        error(string.format("Use def(_G, %s) = value \" INSTEAD OF SET GLOBAL VARIABLE", name), 0)
    end,
    __index = function(_, name)
        return rawget(__g, name)
    end
})