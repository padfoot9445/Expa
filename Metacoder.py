TOKENTYPE_FILE = "TokenTypes.cs"
CHARS_FILE = "Chars.cs"
LITERALS_FILE = "Literals.cs"
KW_STR_CONSTS_FILE = "KwStrConsts.cs"
KW_STR_TO_TT_FILE = "KwStrToTT.cs"
IT3_FILE = "IsT3.cs"
from enum import Enum
from dataclasses import dataclass

class MyEnums(Enum):
    SWITCH = 1
    VT = 2
    NMSPCT = 3
    COM = 4
    CTRL = 6
    C_OP = 9
    OP = 10
class Strings:
    
    st3 = ""; st3_indent = 0
    kws = ""; kws_indent = 0
    literals = ""; literals_indent = 0
    chars = ""; chars_indent = 0
    t2 = ""; t2_indent = 0
    
    switch = ""; switch_indent = 0
    vt = ""; vt_indent = 0
    nmspct = ""; nmspct_indent = 0
    com = ""; com_indent = 0
    ctrl = ""; ctrl_indent = 0
    c_op = ""; c_op_indent = 0
    op = ""; op_indent = 00
def x(i):
     return eval(f"Strings.{i}")

def main(*args: tuple[str, str, list[MyEnums]]):
    for name, str, t3 in args:
        if len(str) == 1:
                Strings.chars += f"\n{' ' * 4 * Strings.chars_indent}public const char {name.upper()} = '{str}';"
                Strings.literals += f"\n{' '* 4 * Strings.literals_indent}public const string {name.upper()} = \"{str}\";"
        elif len(str) == 2:
            Strings.kws += f"\n{' '* 4 * Strings.kws_indent}public const string {name.upper()} = \"{str}\";"
            Strings.st3 += f"\n{' '* 4 * Strings.st3_indent}case Keywords.{name.upper()}: return TokenType.{name.upper()}"
        Strings.t2 += f",\n{' '* 4 * Strings.t2_indent}{name.upper()}"
        for i in t3:
                 eval(f'''Strings.{i} += {r"""f"\n{' '* 4 * Strings.switch_indent}case TokenType.{name.upper()}: """"}''')
            
