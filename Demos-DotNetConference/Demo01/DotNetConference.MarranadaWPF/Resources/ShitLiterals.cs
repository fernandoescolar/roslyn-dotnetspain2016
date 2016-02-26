using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetConference.MarranadaWPF.Resources
{
    public class ShitLiterals
    {
        static List<String> l = new List<string> {
            "Fix it, idiot...you haven't seen it!!!",
            "Please, wake up and correct this piece of shit",
            @"
                 _.-- ------._
               .'             '.
              /                 \
             ;                   ;
             |                   |
             |                   |
             ;                   ;
              \ (`'--,    ,--'`) /
               \ \  _ )  ( _  / /
                ) )(')/  \(')( (
               (_ `""` /\ `""` _)
                \`'-, /  \ ,-'`/
                 `\ / `""` \ /`
                  |/\/\/\/\/\|           Oohhh really!!! Please!!
                  |\        /|
                  ; |/\/\/\| ;
                   \`-`--`-`/
                    \      /
                ',__,'
                      q__p
                      q__p
                      q__p
                      q__p
            ",
            "Another time!! you're at one step to be a very bad programmer",
            "GOGOGOGOOGOGOG! I don't have all day!",
            @"                        W                            
                       WWW          
                       WWW          
                      WWWWW        
                W     WWWWW     W  
                WWW   WWWWW   WWW  
                 WWW  WWWWW  WWW    
                  WWW  WWW  WWW    Tell me the truth... you've been smoking before code this?
                   WWW WWW WWW      
                     WWWWWWW        
                  WWWW  |  WWWW    
                        |          
                        | "
        };

        public static string GetOne {
            get {
                var r = new Random();
                return l[(r.Next() % l.Count)];
            }
        }

        public static string Ok
        {
            get
            {
                return @"
                      _
                     ( )
                      H
                      H
                     _H_ 
                  .-'-.-'-.
                 /         \
                |           |
                |   .-------'._
                |  / /  '.' '. \
                |  \ \ @   @ / / 
                |   '---------'        
                |    _______|  
                |  .'-+-+-+|  
                |  '.-+-+-+|        IT WORKS!!
                |    """""" |
                '-._______.-'

                        ";
            }
        }

        public static string Start { get { return @"using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetConference.MarranadaConRoslyn
{

    class Program
    {
        static void Main()
        {
            Console.WriteLine(""Hello"");
        }
    }
}"; } }

    }
}
