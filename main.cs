using System;
using System.Collections.Generic;
namespace interpreter{

    using lexer;
    using helpers;
    public class Interpreter{
        public static void Main(string[] args){
        
            string sampleCode = "Global { \nNew time globalTime(62.2); \nNew nation Icarus( \nTime: globalTime; \nSpeed: 1; \nDisplay:; \nComment: na; \n) \n} \nIcarus{ \n	New area homeworld( \n		Time: Icarus.time; \n		Speed: 1; \n	) \n	New placeholder p1; \n	View information Icarus; \n	View program Icarus; \n	View all Icarus; \n} \nhomeworld{ \n	Using{ \n		placeholder//from immediate parent; \n		From global globalTime; \n	} \n	New shipyard orbitalDocks( \n		berths: 7; \n		maxSize: 800; \n		Minsize: -1; \n	) \n	New ship wanderer( \n		Time: 3.2; \n	) \n	New ship fighter( \n		Time: 3.1; \n	) \n	New template mainTemplate( \n		equalize: False;) \n	 \n	If(orbitalDocks.hasSpace){ \n		orbitalDocks.add(wanderer); \n	} \n	OrbitalDocks.remove(wanderer); \n	While(orbitalDocks.hasSpace){ \n		OrbitalDocs.add(mainTemplate);//this just keeps using the template \n	} \n} \nTemplate MainTemplate{ \n	//automatically imports all ships from immediate parent \n	//but you can import additional ships \n	Wanderer: 2; \n	Fighter: 200; \n	} \n \n";
            PPrinter pprinter = new PPrinter();
            Lexer lexer = new Lexer(sampleCode);			
            Lexer lexerTwo = new Lexer("wW");
            pprinter.PPrint(lexerTwo.getTokens());
            string testString = "efg";	
        }
    }
}
