using Parser;

string file = File.ReadAllText("D:/School/S4/Low Code/Parser/Code.txt");
Tokenizer tokenizer = new();
//Parse parser = new();
List<Token> list = tokenizer.ConvertTokens(file);


File.WriteAllText("D:/School/S4/Low Code/Parser/Export.txt", Parse.MakeJSON(list));
//File.WriteAllText("D:/School/S4/Low Code/Parser/Export.txt", Parse.MakeParse(list));
//Parse.MakeParse(list);
//parser.MakeParse(list);
//Console.WriteLine(parser.MakeParse(list)); ;
