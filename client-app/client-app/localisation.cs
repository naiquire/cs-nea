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
        };
        public static Dictionary<string, Dictionary<string, string>> localisation = new Dictionary<string, Dictionary<string, string>>();

        public static void loadWords() // convert to a text file or something idk
        {
            localisation.Add("Friends", new Dictionary<string, string>()
            {
                {"en", "Friends" },
                {"fr", "Amis" },
                {"es", "Amigos" },
            });
        }
    }
}
