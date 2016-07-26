using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using NUnit.Framework;
using TemplateEngine;

namespace TemplateTests
{
    [TestFixture]
    public class ParseTest
    {
        string testSnippet;
        List<Tuple<TokenType, string>> chunksExpected;

        [Test]
        public void AllInclusive()
        {
            testSnippet = "text0<%code0\n%>text1<%=expr\n%>\n<%code1%><%%>text5";
            chunksExpected = new List<Tuple<TokenType, string>>
            {
                new Tuple<TokenType, string>(TokenType.Text, "text0"),
                new Tuple<TokenType, string>(TokenType.Code, "code0\n"),
                new Tuple<TokenType, string>(TokenType.Text, "text1"),
                new Tuple<TokenType, string>(TokenType.Eval, "expr\n"),
                new Tuple<TokenType, string>(TokenType.Text, @"\n"),
                new Tuple<TokenType, string>(TokenType.Code, "code1"),
                new Tuple<TokenType, string>(TokenType.Text, "text5"),
            };
            Assert.AreEqual(chunksExpected, new Parser(testSnippet).Parse());
        }

        [Test]
        public void CodeOnly()
        {
            testSnippet = "<%code0%><%%><%code1%>";
            chunksExpected = new List<Tuple<TokenType, string>>
            {
                new Tuple<TokenType, string>(TokenType.Code, "code0"),
                new Tuple<TokenType, string>(TokenType.Code, "code1")
            };
            Assert.AreEqual(chunksExpected, new Parser(testSnippet).Parse());
        }

        [Test]
        public void TextOnly()
        {
            testSnippet = "text1text2";
            chunksExpected = new List<Tuple<TokenType, string>>
            {
                new Tuple<TokenType, string>(TokenType.Text, "text1text2")
            };
            Assert.AreEqual(chunksExpected, new Parser(testSnippet).Parse());
        }

        [Test]
        public void EvalOnly()
        {
            testSnippet = "<%=expr%>";
            chunksExpected = new List<Tuple<TokenType, string>>
            {
                new Tuple<TokenType, string>(TokenType.Eval, "expr")
            };
            Assert.AreEqual(chunksExpected, new Parser(testSnippet).Parse());
        }

        [Test]
        public void CodeEmpty()
        {
            testSnippet = "<%%>";

            Assert.That(() => new Parser(testSnippet).Parse(), Throws.TypeOf<TemplateFormatException>());

        }

        [Test]
        public void EvalEmpty()
        {
            testSnippet = "<%=%>";
            Assert.That(() => new Parser(testSnippet).Parse(), Throws.TypeOf<TemplateFormatException>());

        }

        [Test]
        public void Nothing()
        {
            testSnippet = "";
            Assert.That(() => new Parser(testSnippet).Parse(), Throws.TypeOf<TemplateFormatException>());

        }

        [Test]
        public void UnopenedBracket()
        {
            testSnippet = @"for (int i = 0%><%; i < 9; i++) %>6";
            Assert.That(() => new Parser(testSnippet).Parse(), Throws.TypeOf<TemplateFormatException>());

        }

        [Test]
        public void UnclosedBracket()
        {
            testSnippet = @"<%for (int i = 0%><%; i < 9; i++) 6";
            Assert.That(() => new Parser(testSnippet).Parse(), Throws.TypeOf<TemplateFormatException>());

        }

        [Test]
        public void NestedBrackets()
        {
            testSnippet = @"<%for <%(int i%> = 0%><%; i < 9; i++)%> 6";
            Assert.That(() => new Parser(testSnippet).Parse(), Throws.TypeOf<TemplateFormatException>());

        }

        [Test]
        public void SpecialCharacters()
        {
            testSnippet = "\n<%%>\t<%%>\"<%%>\'<%\0%>";
            chunksExpected = new List<Tuple<TokenType, string>>
            {
                new Tuple<TokenType, string>(TokenType.Text, @"\n"),
                new Tuple<TokenType, string>(TokenType.Text, @"\t"),
                new Tuple<TokenType, string>(TokenType.Text, @"\"""),
                new Tuple<TokenType, string>(TokenType.Text, @"\'"),
                new Tuple<TokenType, string>(TokenType.Code, "\0"),
            };
            Assert.AreEqual(chunksExpected, new Parser(testSnippet).Parse());
        }
    }

    [TestFixture]
    public class CodeComposerTest
    {
        string testSnippet;
        string expectedCode;
        List<Tuple<TokenType, string>> chunks;
        string actualCode;

        [Test]
        public void CSharpComposer()
        {
            testSnippet = "<%=5+5%>lol<%int (i = 0; i < 5; i++) %>*";
            expectedCode = "using System;\n\n" + "using System.IO;\n\n" +
                "class CSharpTemplate\n{\n" +
                "public static TextWriter method(string json)\n{\n" +
                "StringWriter output = new StringWriter();\n" +
                "output.Write((5+5).ToString());\n" +
                "output.Write(\"lol\");\n" +
                "int (i = 0; i < 5; i++) output.Write(\"*\");\n" +
                "return output;\n}\n}";
            chunks = new Parser(testSnippet).Parse();
            actualCode = new CSharp().ComposeCode(chunks);
            Assert.AreEqual(expectedCode, actualCode);
        }

        [Test]
        public void CSharpComposerExtraNamespace()
        {
            testSnippet = "<%=5+5%>lol<%for (int i = 0; i < 5; i++) %>*" +
                "<%var blah = new List<int>();%>";
            string[] namespaces = { "System.Collections.Generic" };
            expectedCode = "using System;\n\n" + "using System.IO;\n\n" +
                "using System.Collections.Generic;\n" +
                "class CSharpTemplate\n{\n" +
                "public static TextWriter method(string json)\n{\n" +
                "StringWriter output = new StringWriter();\n" +
                "output.Write((5+5).ToString());\n" +
                "output.Write(\"lol\");\n" +
                "for (int i = 0; i < 5; i++) output.Write(\"*\");\n" +
                "var blah = new List<int>();" +
                "return output;\n}\n}";
            chunks = new Parser(testSnippet).Parse();
            var lang = new CSharp();
            lang.AddExtras(null, namespaces, null);
            actualCode = lang.ComposeCode(chunks);
            Assert.AreEqual(expectedCode, actualCode);
        }

        [Test]
        public void RubyComposer()
        {
            testSnippet = "<%5.times { %>lol<% }%>";
            expectedCode = "5.times { print \"lol\"; }";
            chunks = new Parser(testSnippet).Parse();
            actualCode = new Ruby().ComposeCode(chunks);
            Assert.AreEqual(expectedCode, actualCode);
        }

        [Test]
        public void JavaComposer()
        {
            testSnippet = "<%for (int i = 0%><%; i < 9; i++) %>6\"";
            expectedCode = "class JavaTemplate\n{\n" +
                "public static void main(String[] args)\n{\n" +
                "for (int i = 0; i < 9; i++) " +
                "System.out.print(\"6\\\"\");" +
                "\n}\n}";
            chunks = new Parser(testSnippet).Parse();
            actualCode = new Java7().ComposeCode(chunks);
            Assert.AreEqual(expectedCode, actualCode);
        }


    }

    [TestFixture]
    public class IdeoneTest
    {
        IdeoneJob testJob;
        string code;
        string expectedOutput;

        [SetUp]
        public void IdeoneInit()
        {
            IdeoneJob.Authorize("mikeroll", "lucky_starfish");
        }

        [Test]
        public void ServiceAccessTest()
        {
            testJob = new IdeoneJob(null, 0);
            Assert.AreEqual("OK", testJob.TestAccess());
        }

        [Test]
        public void ExecuteRuby()
        {
            code = @"print 'chunky bacon!'";
            testJob = new IdeoneJob(code, 17);
            testJob.Execute();
            expectedOutput = "chunky bacon!";
            Assert.AreEqual(expectedOutput, testJob.Output);
        }


        [Test]
        public void ExecuteJava7()
        {
            code = "class Example\n{\n\t" +
                "public static void main(String[] args)\n\t{\n\t\t" +
                "System.out.println(\"Looooool\");\n\t}\n}\n";
            testJob = new IdeoneJob(code, 55);
            testJob.Execute();
            expectedOutput = "Looooool\n";
            Assert.AreEqual(expectedOutput, testJob.Output);
        }

        [Test]
        public void BadCode()
        {
            code = "(siht_cOde]";
            testJob = new IdeoneJob(code, 55);
            Assert.That(() => testJob.Execute(), Throws.TypeOf<BadCodeException>());
        }
    }

    [TestFixture]
    public class AcceptanceTests
    {
        string testSnippet;
        string expectedOutput;
        const string username = "mikeroll";
        const string apiPassword = "lucky_starfish";
        TextWriter output;

        [SetUp]
        public void IdeoneInit()
        {
            IdeoneJob.Authorize(username, apiPassword);
            output = new StringWriter();
        }

        [Test]
        public void CSharpAllInclusive()
        {
            testSnippet = "<%=-15+5%>lol<%for (int i = 0; i < 5; i++) %>*" +
                "<%new List<string>(); output.Write(42); %>";
            string[] assemblies = { "Newtonsoft.Json.dll" };
            string[] namespaces = { "System.Collections.Generic", "Newtonsoft.Json" };
            string json = null;
            expectedOutput = "-10lol*****42";
            var tw = new StringWriter();
            new Template(testSnippet, new CSharp(), assemblies, namespaces, json).Render(tw);
            Assert.AreEqual(expectedOutput, tw.ToString());
        }

        [Test]
        public void JavaAllInclusive()
        {
            testSnippet = @"""lol""<%for (int i = 0%><%; i < 9; i++) %>6<%=666*2%>";
            expectedOutput = @"""lol""6666666661332";
            new Template(testSnippet, new Java7()).Render(output);
            Assert.AreEqual(expectedOutput, output.ToString());
        }

        [Test]
        public void RubyAllInclusive()
        {
            testSnippet =
                @"<html>
                  <% 2.times {%> chunky bacon! <%} %>
                </html>";
            expectedOutput =
                @"<html>
                   chunky bacon!  chunky bacon! 
                </html>";
            new Template(testSnippet, new Ruby()).Render(output);
            Assert.AreEqual(expectedOutput, output.ToString());
        }


    }

    [TestFixture]
    public class RubyOperandsTests
    {
        string testSnippet;
        string expectedOutput;
        const string username = "mikeroll";
        const string apiPassword = "lucky_starfish";
        TextWriter output;

        [SetUp]
        public void IdeoneInit()
        {
            IdeoneJob.Authorize(username, apiPassword);
            output = new StringWriter();
        }


        [Test]
        public void RubyOperands()
        {
            testSnippet =
                @"<html>
                  <% 2.times {%> chunky bacon! <%} %>
                </html>";
            expectedOutput =
                @"<html>
                   chunky bacon!  chunky bacon! 
                </html>";
            new Template(testSnippet, new Ruby()).Render(output);
            Assert.AreEqual(expectedOutput, output.ToString());
        }

    }

}
