namespace ExpaObjects{
    using Tokens;
    using Scope;
    public class ExpaObject{
        public Token TokenIdentifier;
        public Dictionary<Token, ExpaNameSpace> parents = new Dictionary<Token, ExpaNameSpace>();
        public ExpaObject(ExpaNameSpace parent, Token identifier){
            TokenIdentifier = identifier;
            parents[parent.scope.TokenIdentifier] = parent;
        }
        
        public bool AddParent(ExpaNameSpace parent){
            bool rv;
            if(parents[parent.scope.TokenIdentifier] != parent){
                //raise syntax warning on caller end, but still do the assignment
                rv = false;
            } else{
                rv = true;
            }
            parents[parent.TokenIdentifier] = parent;
            if (!parent.children.ContainsKey(TokenIdentifier)){
                //if the parent does not have the current object as a child
                parents[parent.TokenIdentifier].AddChild(this);
            }
            return rv;
                
        }
    }
    
    public class ExpaNameSpace: ExpaObject{
        public Scope scope;
        public Dictionary <Token, ExpaObject> children = new Dictionary<Token, ExpaObject>();
        public ExpaNameSpace(ExpaNameSpace parent, Scope aScope):base(parent, aScope.TokenIdentifier){
            scope = aScope;
        }
        public bool AddChild(ExpaObject child){
            bool rv;
            if(children[child.TokenIdentifier] != child){
                //if the identifier is a child and the child is different
                rv = false;
            } else{
                rv = true;
            }
            children[child.TokenIdentifier] = child;
            return rv;
        }
        public bool AddChild(ExpaNameSpace child){
            bool rv;
            children[child.TokenIdentifier] = child;
            if(children[child.scope.TokenIdentifier] != child){
                rv = false;
            } else{
                rv = true;
            }
            if (!children[child.TokenIdentifier].parents.ContainsKey(scope.TokenIdentifier)){
                children[child.TokenIdentifier].AddParent(this);
            } 
            return rv;
        }
        public bool addUnparsedChild(){
            return true;
        }

    }
}