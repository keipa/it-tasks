using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TemplateEngine
{
    public enum TokenType
    {
        Code, Eval, Text
    }

	public class Parser
	{
		public string TemplateString
		{ 
			get; 
			private set; 
		}

		public string RegexString
		{ 
			get; 
			private set; 
		}

		public List<Tuple<TokenType, string>> Chunks
		{
			get;
			private set;
		}

		public Parser(string snippet)
		{
			TemplateString = snippet;
			RegexString = GetRegexString();
		}


		string EscapeString(string input)
		{
			var output = input
                .Replace("\\", @"\\")
                    .Replace("\'", @"\'")
                    .Replace("\"", @"\""")
                    .Replace("\n", @"\n")
                    .Replace("\t", @"\t")
                    .Replace("\r", @"\r")
                    .Replace("\b", @"\b")
                    .Replace("\f", @"\f")
                    .Replace("\a", @"\a")
                    .Replace("\v", @"\v")
                    .Replace("\0", @"\0");
			return output;
		}

		string GetRegexString()
		{
			string regexBadUnopened = @"(?<error>((?!<%).)*%>)";
			string regexText = @"(?<text>((?!<%).)+)";
			string regexNoCode = @"(?<nocode><%=?%>)";
			string regexCode = @"<%(?<code>[^=]((?!<%|%>).)*)%>";
			string regexEval = @"<%=(?<eval>((?!<%|%>).)*)%>";
			string regexBadUnclosed = @"(?<error><%.*)";
			string regexBadEmpty = @"(?<error>^$)";

			return '(' + regexBadUnopened
				+ '|' + regexText
				+ '|' + regexNoCode
				+ '|' + regexCode
				+ '|' + regexEval
				+ '|' + regexBadUnclosed
				+ '|' + regexBadEmpty
				+ ")*";
		}


		public List<Tuple<TokenType, string>> Parse()
		{
			Regex templateRegex = new Regex(
				RegexString, 
				RegexOptions.ExplicitCapture | RegexOptions.Singleline
			);
			Match matches = templateRegex.Match(TemplateString);

			if (matches.Groups["error"].Length > 0)
			{
				throw new TemplateFormatException("Messed up brackets");
			}

			Chunks = matches.Groups["code"].Captures
                .Cast<Capture>()
                .Select(p => new { Type = TokenType.Code, p.Value, p.Index })
                .Concat(matches.Groups["text"].Captures
                .Cast<Capture>()
                .Select(p => new { Type = TokenType.Text, Value = EscapeString(p.Value), p.Index }))
                .Concat(matches.Groups["eval"].Captures
                .Cast<Capture>()
                .Select(p => new { Type = TokenType.Eval, p.Value, p.Index }))
                .OrderBy(p => p.Index)
                .Select(m => new Tuple<TokenType, string>(m.Type, m.Value))
                .ToList();

			if (Chunks.Count == 0)
			{
				throw new TemplateFormatException("Empty template");
			}
			return Chunks;
		}
	}
}

