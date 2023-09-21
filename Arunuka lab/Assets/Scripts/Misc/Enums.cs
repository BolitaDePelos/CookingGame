public enum Ingredients
{
    Carrot,
    Celery,
    Garlic,
    Onion,
    Scallops,
    Pumpkin,
    Quail,
    Salt,
    Pepper,
    whiteWine,
    OliveOil,
}

public enum SceneName
{
    MainMenu,
    Scene_Level_1,
    Scene_Level_Tutorial
}

/// <summary>
/// State of the <see cref="Food"/>.
/// </summary>
public enum FoodCookState
{
    Raw,
    Cooked,
    Burn,
}

/// <summary>
/// Location of the <see cref="Food"/>.
/// </summary>
public enum FoodLocation
{
    Table,
    Pot,
    Pan,
    Plate
}