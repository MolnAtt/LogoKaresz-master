using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LogoKaresz
{
	public partial class Form1 : Form
	{
		void Színezzél(Point p, Color mit, Color mire)
		{
			if (rajzlap.GetPixel(p.X, p.Y) == mit)
            {
				rajzlap.SetPixel(p.X, p.Y, mire);
				Színezzél(new Point(p.X, p.Y - 1), mit, mire);
				Színezzél(new Point(p.X-1, p.Y), mit, mire);
				Színezzél(new Point(p.X, p.Y + 1), mit, mire);
				Színezzél(new Point(p.X+1, p.Y), mit, mire);
			}
		}

		void Színezzél_Jobban(Point p, Color mit, Color mire)
		{
			Verem<Point> tennivalók = new Verem<Point>();
			if (rajzlap.GetPixel(p.X, p.Y) == mit) // amin állunk, azt színezni kell-e?
				tennivalók.Push(p);

            while (!tennivalók.Empty()) // addig dolgozunk, amíg van tennivaló
            {
				Point tennivaló = tennivalók.Pop();
				rajzlap.SetPixel(tennivaló.X, tennivaló.Y, mire);

				// itt végeztünk a legfölső tennivalóval.
				// Regisztráljuk az új tennivalókat!

				// először körbenézünk
				List<Point> szomszédok = new List<Point> {
					new Point(tennivaló.X, tennivaló.Y - 1),
					new Point(tennivaló.X - 1, tennivaló.Y),
					new Point(tennivaló.X, tennivaló.Y + 1),
					new Point(tennivaló.X + 1, tennivaló.Y) 
				};

                foreach (Point szomszéd in szomszédok)
                    if (rajzlap.GetPixel(szomszéd.X, szomszéd.Y) == mit)
						tennivalók.Push(szomszéd); // nem ugrunk rá egyből, hanem csak besoroljuk a tennivalók közé.
			}
		}

		void Színezzél_Szebben(Point p, Color mit, Color mire)
		{
			Sor<Point> tennivalók = new Sor<Point>();
			if (rajzlap.GetPixel(p.X, p.Y) == mit) // amin állunk, azt színezni kell-e?
				tennivalók.Enqueue(p);

			while (!tennivalók.Empty()) // addig dolgozunk, amíg van tennivaló
			{
				Point tennivaló = tennivalók.Dequeue();
				rajzlap.SetPixel(tennivaló.X, tennivaló.Y, mire);

				// itt végeztünk a legfölső tennivalóval.
				// Regisztráljuk az új tennivalókat!

				// először körbenézünk
				List<Point> szomszédok = new List<Point> {
					new Point(tennivaló.X, tennivaló.Y - 1),
					new Point(tennivaló.X - 1, tennivaló.Y),
					new Point(tennivaló.X, tennivaló.Y + 1),
					new Point(tennivaló.X + 1, tennivaló.Y)
				};

				foreach (Point szomszéd in szomszédok)
					if (rajzlap.GetPixel(szomszéd.X, szomszéd.Y) == mit)
						tennivalók.Enqueue(szomszéd); // nem ugrunk rá egyből, hanem csak besoroljuk a tennivalók közé.
			}
		}


		void FELADAT()
		{
            for (int i = 0; i < 5; i++)
            {
				Előre(200);
				Jobbra(72);
				//MessageBox.Show($"({defaultkaresz.Hely.X};{defaultkaresz.Hely.Y})");
            }

            using (new Rajzol(defaultkaresz, false))
            {
				Jobbra(54);
				Előre(20);
				Point kareszhelye = defaultkaresz.Hely.ToPoint();
				Színezzél_Szebben(kareszhelye, rajzlap.GetPixel(kareszhelye.X, kareszhelye.Y), Color.Red);
				Hátra(20);
				Balra(54);
			}
		}
	}
}
