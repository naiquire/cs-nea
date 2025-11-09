using client_app.menus.games;
using System;
using System.Threading.Tasks;

namespace client_app.games
{
	public class Accuracy : Game, IPlayable
	{
		public Accuracy(Main main) : base(main, "accuracy", 1)
		{

		}
	}
}
