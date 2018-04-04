using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using GrammarParser.Library;

namespace GrammarParser.Library {

    public interface IRange<T> {
        bool Contains(T element);
    }

    /// <summary>
    /// Продеставляет промежуток символов, который задается началом и концом этого промежутка.
    /// ИСпользуеся для инкапсуляции логики проверки попадания символа в определенный промежуток.
    /// </summary>
    public class CharRange: IRange<char> {

        public readonly char? StartSymbol;
        public readonly char? EndSymbol;

        public bool IsStartEqual { get; set; }
        public bool IsEndEqual { get; set; }

        public CharRange(char? startSymbol, char? endSymbol, bool isStartEqual, bool isEndEqual) {
            this.StartSymbol = startSymbol;
            this.EndSymbol = endSymbol;
            this.IsStartEqual = isStartEqual;
            this.IsEndEqual = isEndEqual;
        }

        public CharRange(char? startSymbol, char? endSymbol) : this(startSymbol, endSymbol, false, false) { }

        public bool Contains(char symbol) {

            var isMoreThenStart = false;
            var isLessThenEnd = false;

            if (!this.StartSymbol.HasValue) {
                isMoreThenStart = true;
            }
            else {
                isMoreThenStart = this.IsStartEqual 
                    ? this.StartSymbol <= symbol 
                    : this.StartSymbol < symbol;
            }

            if (!this.EndSymbol.HasValue) {
                isLessThenEnd = true;
            }
            else {
                isLessThenEnd = this.IsEndEqual 
                    ? this.EndSymbol >= symbol 
                    : this.EndSymbol > symbol;
            }

            return isLessThenEnd && isMoreThenStart;
        }
    }

    public class CharRangeAgregator: IRange<char> {

        private IList<IRange<char>> _ranges;

        public CharRangeAgregator(IList<IRange<char>> ranges) {
            this._ranges = ranges.ToList();
        }


        public bool Contains(char element) {
            return this._ranges.All(x => x.Contains(element));
        }
    }
}

public static class CharacterSet {

    public static IRange<char> UpperCaseLetters = new CharRange('A', 'Z', true, true);

    public static IRange<char> LowerCaseLetters = new CharRange('a', 'z', true, true);

    public static IRange<char> Numbers = new CharRange('0', '9', true, true);

    public static IRange<char> AllLetters = new CharRangeAgregator(new List<IRange<char>> {
        UpperCaseLetters, LowerCaseLetters
    });

    public static IRange<char> AllLettersAndNumbers = new CharRangeAgregator(new List<IRange<char>> {
        AllLetters, Numbers
    });
}