using ConsoleApp;
using Newtonsoft.Json;
using Parser;

string file = File.ReadAllText("D:/School/S4/Low Code/Parser/Code.txt");
Tokenizer tokenizer = new();
Parse parser = new();
List<Token> list = tokenizer.ConvertTokens(file);

OutputGenerator outputGen = new();
var json = outputGen.Generate(parser.MakeParse(list).tree);
//var json = JsonConvert.SerializeObject(parser.MakeParse(list).tree, Formatting.Indented);
File.WriteAllText("D:/School/S4/Low Code/Parser/Export.json", json);
//Parse.MakeParse(list);
//parser.MakeParse(list);
//Console.WriteLine(parser.MakeParse(list)); ;
