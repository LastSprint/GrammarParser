namespace GrammarParser.Library {

    /// <summary>
    /// Объект, который обеспечивает то, что он может предоставить определенную зависимость.
    /// Но при том явно не оговариается откуда возьмется зависимость - будет создан новый объект, или переиспользован старый.
    /// </summary>
    /// <typeparam name="Output">Ваш типа дынных.</typeparam>
    public interface IInjector<Output> {
        Output Injection();
    }
}
