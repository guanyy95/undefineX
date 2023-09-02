---
--- Modules定义类
--- Created by 
--- DateTime: 2023/9/1 10:58
---

local ModuleTypeEnum = {
    UTL = "Utility",
    CTL = "Controller",
    MDL = "Model",
}
local Modules = {}
local Utilities = {}
local Controllers = {}
local Models = {}

local UtilityList = {
    "String"
}

local PriorityBootList = {
    "Login",
}

local ModuleList = {
    "Mail",
}

local function GetModuleName(name, moduleTypeName)
    if moduleTypeName == "UTL"
end

local function RegisterUtility(name)
    local result, utl = pcall(require, )
end

local function RegisterGlobalVar()
    def("UTL", Utilities)
    def("CTL", Controllers)
    def("MDL", Models)
end

function Modules.Preboot()
    
end 

function Modules.Initialize()
    RegisterGlobalVar()
    for _, v in ipairs(Utilities) do
        
    end
end

-- 注册Module\注册Controller
local function InitModule(name)
    local result, controller = pcall(require, AssemModuleName(name, ModuleNameEnum.Ctrl))
    if result and controller then
        Ctrl[name] = controller
    end
    local result, module = pcall(require, AssemModuleName(name, ModuleNameEnum.Mod))
    if result and module then
        Mod[name] = module
    end
end