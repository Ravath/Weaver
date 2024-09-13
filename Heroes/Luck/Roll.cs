using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Weaver.Heroes.Luck {
	public static class Roll
	{
		#region Static generators
			private static Random _generator = new Random();//the numbers generator.
	
			private static Parser.Parser? _parser;
			/// <summary>
			/// Singleton access to the parser.
			/// </summary>
			private static Parser.Parser Parser
			{
				get
				{
					if(_parser == null)
					{
						_parser = new Parser.Parser();
					}
					return _parser;
				}
			}
		#endregion


		/// <summary>
		/// Throws a dice with the given number of face.
		/// </summary>
		/// <returns>A random value.</returns>
		public static int RollD(int faces)
		{
			return _generator.Next(1, faces + 1);
		}
		
		/// <summary>
		/// Get a random value in the given interval.
		/// </summary>
		/// <param name="min">The minimum possible value.</param>
		/// <param name="max">The maximum possible value.</param>
		/// <returns>A random value.</returns>

		public static int Interval(int min, int max)
		{
			return _generator.Next(min, max + 1);
		}
		/// <summary>
		/// Throws a 1d20.
		/// </summary>
		/// <returns>A random value.</returns>
		public static int D20()
		{
			return RollD(20);
		}

		/// <summary>
		/// Rolls dice and returns the sum.
		/// </summary>
		/// <param name="dice">Dice pool to throw.</param>
		/// <returns>The throw result.</returns>
		public static int RollD( int nbr, int face ) {
			//Contract.Requires<ArgumentNullException>(dice != null);
			int res = 0;
			for (int i = 0; i < nbr; i++)
			{
				res += RollD(face);
			}
			return res;
		}

		/// <summary>
		/// Parse the given dice macro and return the equivalent model object.
		/// </summary>
		/// <param name="macro"></param>
		/// <returns>Null if couldn't parse.</returns>
		public static IRoll? Parse(string macro)
		{
			TextReader tr = new StringReader(macro);

			//Parse string
			Parser.Scanner = new Parser.Parser.Lexer(tr);
			
			if (Parser.Parse())
			{
				return Parser.result;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Look for any occurence of [[macro]] and replace with result of the found macro.
		/// Replace with "0" if could not parse.
		/// </summary>
		/// <param name="value">A raw text were dice macros can found.</param>
		/// <returns>A parsed string.</returns>
		public static string ParseFromText(string value)
		{
			Regex r = new(@"\[\[[^\]]*\]\]");

			// Find first
			Match m = r.Match(value);
			string macro = m.Value;

			// If something found
			while (!string.IsNullOrWhiteSpace(macro))
			{
				string toParse = macro.Substring(2, macro.Length - 4);
				IRoll? roll = Parse(toParse);
				int replacementResult;
				if (roll == null)
				{
					replacementResult = 0;
				}
				else
				{
					roll.Roll();
					replacementResult = roll.NetResult;
				}
				value = value.Replace(macro, ""+replacementResult);

				// Try find next
				m = r.Match(value);
				macro = m.Value;
			}
			return value;
		}
	}
}
