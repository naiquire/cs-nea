using client_app.menus.games;
using System;
using System.Threading.Tasks;

namespace client_app.games
{
	public class accuracy : abstractGame, IPlayable
	{
		public accuracy(main main) : base(main, "accuracy", 1)
		{

		}
	}
}
