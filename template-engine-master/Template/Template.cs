using System.IO;
using System.Collections.Generic;

namespace TemplateEngine
{
	public class Template
	{
		ILanguage language;

		public string TemplateString
		{
			get;
			private set;
		}


		public Template(string snippet, ILanguage lang, 
		                      string[] assemblies = null,
		                      string[] namespaces = null,
		                      string json = null)
		{
			language = lang;
			language.AddExtras(assemblies, namespaces, json);
			TemplateString = snippet;
		}


		public void Render(TextWriter output)
		{
			var chunks = new Parser(TemplateString).Parse();
			language.ComposeCode(chunks);
            language.CompileAndRun(output);
		}
	}
}
