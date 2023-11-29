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
    GetMethodDefString = lambda name: f"""public static bool Is{name}(this TokenType input){r'{'} switch(input){r'{'}"""
    GetDefault = lambda indent: f"\n{indent * 4 * ' '}default: return false;\n{(indent - 2) * 4 * ' '}{r'}}'}"
    str_to_t2 = ""; str_to_t2_indent = 0 #string to tokentype
    kws = ""; kws_indent = 0
    literals = ""; literals_indent = 0
    chars = ""; chars_indent = 0
    t2 = ""; t2_indent = 0
    
    switch = GetMethodDefString("Switch"); switch_indent = 2
    vt = GetMethodDefString("ValueType"); vt_indent = 2
    nmspct = GetMethodDefString("NameSpaceType"); nmspct_indent = 2
    com = GetMethodDefString("Commnad"); com_indent = 2
    ctrl = GetMethodDefString("Control"); ctrl_indent = 2
    c_op = GetMethodDefString("ControlOperator"); c_op_indent = 2
    op = GetMethodDefString("Operator"); op_indent = 00
   
def x(i):
     return eval(f"Strings.{i}")

def main(*args: tuple[str, str, list[MyEnums]]):
    for name, str, t3 in args:
        if len(str) == 1:
                Strings.chars += f"\n{' ' * 4 * Strings.chars_indent}public const char {name.upper()} = '{str}';{r'}}'}"
                Strings.literals += f"\n{' '* 4 * Strings.literals_indent}public const string {name.upper()} = \"{str}\";{r'}}'}"
        else:
            Strings.kws += f"\n{' '* 4 * Strings.kws_indent}public const string {name.upper()} = \"{str}\"{r'}}'};"
            Strings.str_to_t2 += f"\n{' '* 4 * Strings.str_to_t2_indent}case Keywords.{name.upper()}: return TokenType.{name.upper()};{r'}}'}"
        Strings.t2 += f",\n{' '* 4 * Strings.t2_indent}{name.upper()}"
        for i in t3:
                if i == MyEnums.SWITCH:
                    Strings.switch += f"\n{' '* 4 * Strings.switch_indent}case TokenType.{name.upper()}:"
                elif i == MyEnums.C_OP:
                     Strings.c_op += f"\n{' '* 4 * Strings.c_op_indent}case TokenType.{name.upper()}:"
                elif i == MyEnums.CTRL:
                     Strings.ctrl += f"\n{' '* 4 * Strings.ctrl_indent}case TokenType.{name.upper()}:"
                elif i == MyEnums.VT:
                     Strings.vt += f"\n{' '* 4 * Strings.vt_indent}case TokenType.{name.upper()}:"
                elif i == MyEnums.COM:
                     Strings.com += f"\n{' '* 4 * Strings.com_indent}case TokenType.{name.upper()}:"
                elif i == MyEnums.OP:
                     Strings.op += f"\n{' '* 4 * Strings.op_indent}case TokenType.{name.upper()}:"
                elif i == MyEnums.NMSPCT:
                     Strings.nmspct += f"\n{' '* 4 * Strings.nmspct_indent}case {name.upper()}:"

        Strings.switch += Strings.GetDefault(Strings.switch_indent)
        Strings.c_op += Strings.GetDefault(Strings.c_op_indent)
        Strings.ctrl += Strings.GetDefault(Strings.ctrl_indent)
        Strings.vt += Strings.GetDefault(Strings.vt_indent)
        Strings.com += Strings.GetDefault(Strings.com_indent)
        Strings.op += Strings.GetDefault(Strings.op_indent)
        Strings.nmspct += Strings.GetDefault(Strings.nmspct_indent)
        with open("TokenTypeTypeChecker.cs", "w") as T3Ccs:
             mainstring ="""namespace Tokens.TokenTypes.Conversions;
internal static class TokenT2C{
 """
             mainstring += '\n\t' + Strings.switch + '\n' + Strings.c_op + '\n' + Strings.ctrl + Strings.vt + Strings.com + Strings.op + Strings.nmspct + '}'
             T3Ccs.write(mainstring)
        with open("KwStrConsts.cs", "w") as Kwscf:
             Kwscf.write("namespace Tokens.TokenTypes.Conversions;\ninternal static class StringToTokenType{\npublic static TokenType StringToTokenType(string input){switch(input){\n" + Strings.str_to_t2 + '}')
main(
     ("information", "information", [MyEnums.SWITCH])
)
            
