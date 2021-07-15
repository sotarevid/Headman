using System;
using System.Linq;
using System.Text;

namespace Headman
{
    public static class Group
    {
        private static StringBuilder _builder = new StringBuilder();
        private static Random _random = new Random();

        private static readonly string[] FullNames = new []
        {
            "lorem",
            "ipsum",
            "dolor",
            "sit",
            "amet"
        };

        private static readonly string OrderedGroup;
        public static string GetGroup() => OrderedGroup;
        
        static Group()
        {
            _builder.Clear();
            var i = 0;
            foreach (var name in FullNames.OrderBy(e => e))
            {
                _builder.Append($"{++i}. {name}\n");
            }

            OrderedGroup = _builder.ToString();
        }

        public static string GetRandomName()
        {
            return FullNames[_random.Next(FullNames.Length)];
        }

        public static string GetRandomOrder()
        {
            _builder.Clear();
            var i = 0;
            foreach (var name in FullNames.OrderBy(e => _random.Next()))
            {
                _builder.Append($"{++i}. {name}\n");
            }

            return _builder.ToString();
        }
    }
}