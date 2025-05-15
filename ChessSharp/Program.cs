using ChessSharp.Tabuleiro;
using ChessSharp.Xadrez;

namespace ChessSharp;

class Program
{
    static void Main(string[] args)
    {

        Posicao p = new Posicao(3, 4);
        TabuleiroJogo tab = new TabuleiroJogo(8, 8);
        tab.ColocarPeca(new Rei(tab, Cor.Preta), p);
        
        
        
        Tela.ImprimirTabuleiro(tab);
    }
}