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
            "English",
			"Française",
			"Español",
			"Português",
			"Deutsch",
        };
        public static List<string> languageCodes = new List<string>()
        {
            "en",
            "fr",
            "es",
            "pt",
            "de",
        };
        public static Dictionary<string, Dictionary<string, string>> localisation = new Dictionary<string, Dictionary<string, string>>();

        public static void loadWords() // convert to a text file or something idk
        {
            localisation.Add("Friends", new Dictionary<string, string>()
            {
                {"en", "Friends" },
                {"fr", "Amis" },
                {"es", "Amigos" },
                {"pt", "Amigos" },
                {"de", "Freunde" },
            });
            localisation.Add("ONLINE", new Dictionary<string, string>()
            {
                {"en", "ONLINE" },
                {"fr", "EN LIGNE" },
                {"es", "EN LÍNEA" },
                {"pt", "ON-LINE" },
                {"de", "ONLINE" },
            });
            localisation.Add("OFFLINE", new Dictionary<string, string>()
            {
                {"en", "OFFLINE" },
                {"fr", "HORS LIGNE" },
                {"es", "DESCONECTADO" },
                {"pt", "OFF-LINE" },
                {"de", "OFFLINE" },
            });
			localisation.Add("Account", new Dictionary<string, string>()
			{
				{"en", "Account" },
				{"fr", "Compte" },
				{"es", "Cuenta" },
				{"pt", "Conta" },
				{"de", "Konto" },
			});
			localisation.Add("Username", new Dictionary<string, string>()
			{
				{"en", "Username" },
				{"fr", "Nom d'utilisateur" },
				{"es", "Nombre de usuario" },
				{"pt", "Nome de usuário" },
				{"de", "Benutzername" },
			});
			localisation.Add("Password", new Dictionary<string, string>()
			{
				{"en", "Password" },
				{"fr", "Mot de passe" },
				{"es", "Contraseña" },
				{"pt", "Senha" },
				{"de", "Passwort" },
			});
		}
    }
}
