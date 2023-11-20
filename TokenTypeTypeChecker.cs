namespace Tokens{
    internal static class TokenTypeTypeChecker{
        public static bool IsSwitch(this Token input){
            switch(input.Type){
                case TokenType.INFORMATION: 
                case TokenType.ALL:
                case TokenType.FROM:
                case TokenType.IN:
                case TokenType.AS:
                case TokenType.BREAK:
                case TokenType.CASE:
                case TokenType.ELSE:
                    return true;
                default: return false;
            }
        }
        public static bool IsValueType(this Token input){
            switch(input.Type){
                case TokenType.GLOBAL:
                case TokenType.TIME:
                case TokenType.NATION:
                case TokenType.AREA:
                case TokenType.SHIPYARD:
                case TokenType.BERTHS:
                case TokenType.SHIPCLASS:
                case TokenType.TEMPLATE:
                case TokenType.FUNCTION:
                case TokenType.INT:
                case TokenType.STRING:
                case TokenType.COMPONENT:
                case TokenType.VOID:
                case TokenType.NAMESPACE:
                case TokenType.BUNDLE:
                    return true;
                default: return false;
            }
        }
        public static bool IsNameSpaceType(this Token input){
            switch(input.Type){
                case TokenType.GLOBAL:
                case TokenType.NATION:
                case TokenType.AREA:
                case TokenType.TEMPLATE:
                case TokenType.FUNCTION:
                    return true;
                default: return false;
            }
        }
        public static bool IsCommand(this Token input){
            switch(input.Type){
                case TokenType.NEW:
                case TokenType.VIEW:
                case TokenType.USING:
                case TokenType.MODIFY:
                case TokenType.FOR:
                case TokenType.FOREACH:
                case TokenType.PERMANENT:
                case TokenType.WHILE:
                    return true;
                default: return false;
            }
        }
        public static bool IsParameter(this Token input){
            switch(input.Type){
                case TokenType.MAXQUEUE:
                case TokenType.MINQUEUE:
                case TokenType.MAXSIZE:
                case TokenType.MINSIZE:
                case TokenType.SPEED:
                case TokenType.DISPLAY:
                case TokenType.EQUALIZE:
                case TokenType.COMMENT:
                case TokenType.DEPENDANCY:
                    return true;
                default: return false;
            }
        }
        public static bool IsCtrl(this Token input){
            switch(input.Type){
                case TokenType.SWITCH:
                case TokenType.IF:
                    return true;
                default: return false;
            }
        }
        public static bool IsFunction(this Token input){
            switch(input.Type){
                case TokenType.ADD:
                case TokenType.REMOVE:
                case TokenType.RELEASE:
                case TokenType.HOLDQUEUE:
                case TokenType.MIN:
                case TokenType.MAX:
                case TokenType.SHIFT:
                case TokenType.UNSHIFT:
                case TokenType.ROUND:
                case TokenType.GET:
                case TokenType.LENGTH:
                    return true;
                default: return false;
            }
        }
        public static bool IsAttribute(this Token input){
            switch(input.Type){
                case TokenType.QUEUE:
                case TokenType.QUEUELENGTH:
                case TokenType.TYPE:
                    return true;
                default: return false;
            }
        }
        public static bool IsCtrlOperator(this Token input){
            switch(input.Type){
                case TokenType.NOT:
                case TokenType.AND:
                case TokenType.OR:
                    return true;
                default: return false;
            }
        }
    }
}