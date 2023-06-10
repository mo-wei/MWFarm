/// <summary>
/// 物品类型枚举
/// </summary>
public enum ItemType
{
    Seed, Commodity/*商品*/, Furniture,
    HoeTool/*锄头*/, ChopTool/*斧子*/, BreakTool, ReapTool/*镰刀*/, WaterTool, CollectionTool,
    ReapableScenery/*场景中可获得的物品*/
}

/// <summary>
/// 格子类型枚举
/// </summary>
public enum SlotType
{
    Bag,Box,Shpo
}
/// <summary>
/// 仓库类型
/// </summary>
public enum InventoryLocation
{
    Player,Box
}
/// <summary>
/// 身体各部分状态
/// </summary>
public enum PartType
{
    None,Carry,Hoe,Break
}

/// <summary>
/// 身体各部分名字
/// </summary>
public enum PartName
{
    Body,Hair,Arm,Tool
}

/// <summary>
/// 四季
/// </summary>
public enum Season
{
    春天,夏天,秋天,冬天
}