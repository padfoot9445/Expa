TOKENTYPE_FILE = "TokenTypes.cs"
CHARS_FILE = "Chars.cs"
LITERALS_FILE = "Literals.cs"
KW_STR_CONSTS_FILE = "KwStrConsts.cs"
KW_STR_TO_TT_FILE = "KwStrToTT.cs"
from enum import Enum
from dataclasses import dataclass

class MyEnums(Enum):
    SWITCH = 1
    VT = 2
    NMSPCT = 3
    COM = 4
    PARAM = 5
    CTRL = 6
    FUNC = 7
    ATTR = 8
    C_OP = 9
@dataclass
class RS:
    literal: str
    TTT: list
mainList = [RS()]