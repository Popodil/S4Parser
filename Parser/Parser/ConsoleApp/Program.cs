using Parser;

string file = File.ReadAllText("D:/School/S4/Low Code/Parser/Code.txt");
Tokenizer tokenizer = new();
Parse parser = new();
List<Token> list = tokenizer.ConvertTokens(file).ToList();


File.WriteAllText("D:/School/S4/Low Code/Parser/Export.txt", parser.MakeJSON(list));
Console.WriteLine(list.Count);
