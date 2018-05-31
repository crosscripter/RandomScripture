using System;
using System.IO;

class Program {
	static void Main() {
		string[] lines = File.ReadAllLines("KJV-PCE.txt");
		Random rand = new Random();

		while (true) {
			string line = lines[rand.Next(lines.Length)];
			Console.WriteLine(line);
			Console.ReadLine();
			Console.Clear();
		}
	}
}
