using Newtonsoft.Json;
using Parser;
using Parser.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class OutputGenerator
    {
        public List<Point> points = new();
        public string Generate(Tree tree)
        {
            string str = string.Empty;

            foreach (Base item in tree.bases)
            {
                Point point = new() { value = item.token.Id };
                if (item.HasFirst())
                {
                    if (item.First.First != null)
                    {
                        Point first = new() { value = item.First.token.Id, first = item.First.First.token.Id, second = item.First.Second.token.Id };
                        point.first = first;
                    }
                    else point.first = item.First.token.Id;

                    if (item.Second != null)
                    {
                        Point second = new() { value = item.Second.token.Id, first = item.Second.First.token.Id, second = item.Second.Second.token.Id };
                        point.second = second;
                    }
                    else point.second = item.Second.token.Id;
                    /*point.first = item.First.token.Id;
                    point.second = item.Second.token.Id;*/
                }
                points.Add(point);
            }

            //str = JsonConvert.SerializeObject(points, Formatting.Indented);
            str = JsonConvert.SerializeObject(tree.bases, Formatting.Indented);
            return str;
        }
    }
}
