using System;
using System.Collections.Generic;
namespace interpreter{

    using lexer;
    using helpers;
    using System.IO;
    public class Interpreter{
        public static void Main(string[] args){
            //TODO: SEMIURGENT: Load existing objects from disk
            StreamReader sr = new("./main.expa");
            string sampleCode = "Global { \nNew time globalTime(62.2); \nNew nation Icarus( \nTime: globalTime; \nSpeed: 1; \nDisplay:; \nComment: na; \n) \n} \nIcarus{ \n	New area homeworld( \n		Time: Icarus.time; \n		Speed: 1; \n	) \n	New placeholder p1; \n	View information Icarus; \n	View program Icarus; \n	View all Icarus; \n} \nhomeworld{ \n	Using{ \n		placeholder//from immediate parent; \n		From global globalTime; \n	} \n	New shipyard orbitalDocks( \n		berths: 7; \n		maxSize: 800; \n		Minsize: -1; \n	) \n	New ship wanderer( \n		Time: 3.2; \n	) \n	New ship fighter( \n		Time: 3.1; \n	) \n	New template mainTemplate( \n		equalize: False;) \n	 \n	If(orbitalDocks.hasSpace){ \n		orbitalDocks.add(wanderer); \n	} \n	OrbitalDocks.remove(wanderer); \n	While(orbitalDocks.hasSpace){ \n		OrbitalDocs.add(mainTemplate);//this just keeps using the template \n	} \n} \nTemplate MainTemplate{ \n	//automatically imports all ships from immediate parent \n	//but you can import additional ships \n	Wanderer: 2; \n	Fighter: 200; \n	} \n \n";
            Lexer lexer = new Lexer(sampleCode);			
            Lexer lexerTwo = new Lexer("wW");
            Lexer lexerThree = new Lexer("globalTime");
            PPrinter.PPrint(new int[]{1,2,3,4,5,6,7,8});
            var tokenList = lexer.getTokens();
            Console.WriteLine(tokenList.Length);
            PPrinter.PPrint(tokenList);
            string testString = "efgd";	
        }
    }
}
