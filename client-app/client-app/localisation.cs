using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app
{
    public static class @languages
    {
        public static List<string> supportedLanguages = new List<string>()
        {
            "en",
            "fr",
            "es",

            "jp",
        };
        public static Dictionary<string, Dictionary<string, string>> localisation = new Dictionary<string, Dictionary<string, string>>();

        public static void addWords()
        {
            localisation.Add("Friends", new Dictionary<string, string>()
            {
                {"en", "Friends" },
                {"fr", "Amis" },
                {"es", "Amigos" },

                {"jp", "友達" },
            });
        }
    }
}
