public enum Character
{
    Random = 0, //���� ĳ����
    Sugi  = 1,

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