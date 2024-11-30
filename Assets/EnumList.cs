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
    Show    = 0,
    Hide    = 1,
    GetOut  = 2,
    Spawn   = 3,
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

public enum SFX
{
    ElevatorOpen,
    ElevatorClose,
    ElevatorMove,
    ElevatorArrive,
    ElevatorCall,
    Angry,
    VeryAngry,
    ElevatorButton,
    nomalChat1 = 100,
    nomalChat2,
    nomalChat3,
    angryChat1,
    angryChat2,
    angryChat3,
    goodChat1,
    goodChat2,
    goodChat3,
}