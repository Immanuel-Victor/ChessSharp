using ChessSharp.Tabuleiro;

namespace ChessSharp;

class Program
{
    static void Main(string[] args)
    {

        Posicao p = new Posicao(3, 4);
        
        Console.WriteLine($"Posição: {p}");
        
        Tela.ImprimirTabuleiro(new TabuleiroJogo(8, 8));
    }
}