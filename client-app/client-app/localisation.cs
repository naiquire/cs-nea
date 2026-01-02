using System.Collections.Generic;

namespace client_app
{
	public static class Languages
	{
		public static readonly List<string> supportedLanguages = new List<string>()
		{
			"English",
			"Française",
			"Español",
			"Português",
			"Deutsch",
		};
		public static readonly List<string> languageCodes = new List<string>()
		{
			"en",
			"fr",
			"es",
			"pt",
			"de",
		};
		public static readonly Dictionary<string, Dictionary<string, string>> localisation = new Dictionary<string, Dictionary<string, string>>();

		public static void loadWords()
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
			localisation.Add("Accuracy", new Dictionary<string, string>()
			{
				{"en", "Accuracy" },
				{"fr", "Précision" },
				{"es", "Precisión" },
				{"pt", "Precisão" },
				{"de", "Genauigkeit" },
			});
			localisation.Add("Versus", new Dictionary<string, string>()
			{
				{"en", "Versus" },
				{"fr", "Contre" },
				{"es", "Versus" },
				{"pt", "Versus" },
				{"de", "Gegen" },
			});
			localisation.Add("Knockout", new Dictionary<string, string>()
			{
				{"en", "Elimination" },
				{"fr", "Élimination" },
				{"es", "Eliminación" },
				{"pt", "Eliminação" },
				{"de", "Beseitigung" },
			});
			localisation.Add("Profile", new Dictionary<string, string>()
			{
				{"en", "Profile" },
				{"fr", "Profil" },
				{"es", "Perfil" },
				{"pt", "Perfil" },
				{"de", "Profil" },
			});
			localisation.Add("Edit", new Dictionary<string, string>()
			{
				{"en", "Edit" },
				{"fr", "Éditer" },
				{"es", "Editar" },
				{"pt", "Editar" },
				{"de", "Bearbeiten" },
			});
			localisation.Add("Continue", new Dictionary<string, string>()
			{
				{"en", "Continue" },
				{"fr", "Continuer" },
				{"es", "Continuar" },
				{"pt", "Continuar" },
				{"de", "Fortsetzen" },
			});
			localisation.Add("Login", new Dictionary<string, string>()
			{
				{"en", "Login" },
				{"fr", "Connexion" },
				{"es", "Iniciar sesión" },
				{"pt", "Entrar" },
				{"de", "Anmelden" },
			});
			localisation.Add("Create Account", new Dictionary<string, string>()
			{
				{"en", "Create Account" },
				{"fr", "Créer un compte" },
				{"es", "Crear cuenta" },
				{"pt", "Criar conta" },
				{"de", "Konto erstellen" },
			});
			localisation.Add("Queue", new Dictionary<string, string>()
			{
				{"en", "Queue" },
				{"fr", "File d'attente" },
				{"es", "Cola" },
				{"pt", "Fila" },
				{"de", "Warteschlange" },
			});
			localisation.Add("Accept", new Dictionary<string, string>()
			{
				{"en", "Accept" },
				{"fr", "Accepter" },
				{"es", "Aceptar" },
				{"pt", "Aceitar" },
				{"de", "Akzeptieren" },
			});
			localisation.Add("About me", new Dictionary<string, string>()
			{
				{ "en", "About me" },
				{"fr", "À propos de moi" },
				{"es", "Sobre mí" },
				{"pt", "Sobre mim" },
				{"de", "Über mich" },
			});
			localisation.Add("An error occured while connecting to the server", new Dictionary<string, string>()
			{
				{ "en", "An error occured while connecting to the server" },
				{"fr", "Une erreur s'est produite lors de la connexion au serveur" },
				{"es", "Se produjo un error al conectar con el servidor" },
				{"pt", "Ocorreu um erro ao conectar ao servidor" },
				{"de", "Beim Verbinden mit dem Server ist ein Fehler aufgetreten" },
			});
			localisation.Add("Account does not exist", new Dictionary<string, string>()
			{
				{ "en", "Account does not exist" },
				{"fr", "Le compte n'existe pas" },
				{"es", "La cuenta no existe" },
				{"pt", "A conta não existe" },
				{"de", "Konto existiert nicht" },
			});
			localisation.Add("User is currently logged in on another device", new Dictionary<string, string>()
			{
				{ "en", "User is currently logged in on another device" },
				{"fr", "L'utilisateur est actuellement connecté sur un autre appareil" },
				{"es", "El usuario ha iniciado sesión en otro dispositivo" },
				{"pt", "O usuário está atualmente conectado em outro dispositivo" },
				{"de", "Der Benutzer ist derzeit auf einem anderen Gerät angemeldet" },
			});
			localisation.Add("Unrecognised success code", new Dictionary<string, string>()
			{
				{ "en", "Unrecognised success code" },
				{"fr", "Code de succès non reconnu" },
				{"es", "Código de éxito no reconocido" },
				{"pt", "Código de sucesso não reconhecido" },
				{"de", "Nicht erkannter Erfolgscode" },
			});
			localisation.Add("An error occurred. Please wait and try again", new Dictionary<string, string>()
			{
				{ "en", "An error occurred. Please wait and try again" },
				{"fr", "Une erreur s'est produite. Veuillez patienter et réessayer" },
				{"es", "Ocurrió un error. Por favor, espere y vuelva a intentarlo" },
				{"pt", "Ocorreu um erro. Por favor, aguarde e tente novamente" },
				{"de", "Ein Fehler ist aufgetreten. Bitte warten Sie und versuchen Sie es erneut" },
			});
			localisation.Add("Username is not available", new Dictionary<string, string>()
			{
				{ "en", "Username is not available" },
				{"fr", "Le nom d'utilisateur n'est pas disponible" },
				{"es", "El nombre de usuario no está disponible" },
				{"pt", "O nome de usuário não está disponível" },
				{"de", "Benutzername ist nicht verfügbar" },
			});
			localisation.Add("Starting in", new Dictionary<string, string>()
			{
				{ "en", "Starting in" },
				{"fr", "Démarrage dans" },
				{"es", "Comenzando en" },
				{"pt", "Iniciando em" },
				{"de", "Startet in" },
			});
			localisation.Add("Next letter in", new Dictionary<string, string>()
			{
				{ "en", "Next letter in" },
				{"fr", "Prochaine lettre dans" },
				{"es", "Siguiente letra en" },
				{"pt", "Próxima letra em" },
				{"de", "Nächster Buchstabe in" },
			});
			localisation.Add("Add Friend", new Dictionary<string, string>()
			{
				{ "en", "Add Friend" },
				{"fr", "Ajouter un ami" },
				{"es", "Agregar amigo" },
				{"pt", "Adicionar amigo" },
				{"de", "Freund hinzufügen" },
			});
			localisation.Add("Remove Friend", new Dictionary<string, string>()
			{
				{ "en", "Remove Friend" },
				{"fr", "Supprimer un ami" },
				{"es", "Eliminar amigo" },
				{"pt", "Remover amigo" },
				{"de", "Freund entfernen" },
			});
			localisation.Add("by longest time elapsed", new Dictionary<string, string>()
			{
				{ "en", "by longest time elapsed" },
				{"fr", "par le temps le plus long écoulé" },
				{"es", "por el mayor tiempo transcurrido" },
				{"pt", "pelo maior tempo decorrido" },
				{"de", "durch die längste verstrichene Zeit" },
			});
			localisation.Add("by incorrect submission", new Dictionary<string, string>()
			{
				{ "en", "by incorrect submission" },
				{"fr", "par une soumission incorrecte" },
				{"es", "por una presentación incorrecta" },
				{"pt", "por submissão incorreta" },
				{"de", "falsche Einreichung eliminiert" },
			});
			localisation.Add("Eliminated", new Dictionary<string, string>()
			{
				{ "en", "Eliminated" },
				{"fr", "Éliminé" },
				{"es", "Eliminado" },
				{"pt", "Eliminado" },
				{"de", "Eliminiert" },
			});
			localisation.Add("Passed", new Dictionary<string, string>()
			{
				{ "en", "Passed" },
				{"fr", "Passé" },
				{"es", "Aprobado" },
				{"pt", "Passou" },
				{"de", "Bestanden" },
			});
			localisation.Add("players", new Dictionary<string, string>()
			{
				{ "en", "players" },
				{"fr", "joueurs" },
				{"es", "jugadores" },
				{"pt", "jogadores" },
				{"de", "Spieler" },
			});
			localisation.Add("Total", new Dictionary<string, string>()
			{
				{ "en", "Total" },
				{"fr", "Total" },
				{"es", "Total" },
				{"pt", "Total" },
				{"de", "Insgesamt" },
			});
			localisation.Add("Correct", new Dictionary<string, string>()
			{
				{"en", "Correct" },
				{"fr", "Correct" },
				{"es", "Correcto" },
				{"pt", "Correto" },
				{"de", "Korrekt" },
			});
			localisation.Add("Incorrect", new Dictionary<string, string>()
			{
				{"en", "Incorrect" },
				{"fr", "Incorrect" },
				{"es", "Incorrecto" },
				{"pt", "Incorreto" },
				{"de", "Falsch" },
			});
			localisation.Add("Delta", new Dictionary<string, string>()
			{
				{ "en", "Delta" },
				{"fr", "Delta" },
				{"es", "Delta" },
				{"pt", "Delta" },
				{"de", "Delta" },
			});
			localisation.Add("Results", new Dictionary<string, string>()
			{
				{ "en", "Results" },
				{"fr", "Résultats" },
				{"es", "Resultados" },
				{"pt", "Resultados" },
				{"de", "Ergebnisse" },
			});
			localisation.Add("Time", new Dictionary<string, string>()
			{
				{ "en", "Time" },
				{"fr", "Temps" },
				{"es", "Tiempo" },
				{"pt", "Tempo" },
				{"de", "Zeit" },
			});
			localisation.Add("Round", new Dictionary<string, string>()
			{
				{ "en", "Round" },
				{"fr", "Manche" },
				{"es", "Ronda" },
				{"pt", "Rodada" },
				{"de", "Runde" },
			});
			localisation.Add("Clear", new Dictionary<string, string>()
			{
				{ "en", "Clear" },
				{"fr", "Effacer" },
				{"es", "Borrar" },
				{"pt", "Limpar" },
				{"de", "Löschen" },
			});
			localisation.Add("Submit", new Dictionary<string, string>()
			{
				{ "en", "Submit" },
				{"fr", "Soumettre" },
				{"es", "Enviar" },
				{"pt", "Enviar" },
				{"de", "Einreichen" },
			});
			localisation.Add("Home", new Dictionary<string, string>()
			{
				{ "en", "Home" },
				{"fr", "Accueil" },
				{"es", "Inicio" },
				{"pt", "Início" },
				{"de", "Startseite" },
			});
			localisation.Add("Winner", new Dictionary<string, string>()
			{
				{ "en", "Winner" },
				{"fr", "Gagnant" },
				{"es", "Ganador" },
				{"pt", "Vencedor" },
				{"de", "Gewinner" },
			});
			// Confirm Password
			// Incorrect Password
		}
	}
}
