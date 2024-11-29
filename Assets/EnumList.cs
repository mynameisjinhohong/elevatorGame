public enum Character
{
    Random      = 0, //·£´ý Ä³¸¯ÅÍ

    Otaki       = 1,
    Stranger    = 2,
    Thief       = 3,
    Rich        = 4,
}

public enum CharacterAction
{
    Show = 0,
    Hide = 1,
}

public enum GameState
{
    OpenElevator,
    OutCharacter,
    ShowCharacter,
    Conversation,
    CloseElevator,
    MoveFloor
}