using ChessSharp.Tabuleiro;
using ChessSharp.Xadrez;

namespace ChessSharp;

class Program
{
    static void Main(string[] args)
    {
        PartidaXadrez partida = new PartidaXadrez();

        while (!partida.Finished)
        {
            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro);

            Console.WriteLine();
            Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

            bool[,] posicoesPossiveis = partida.Tabuleiro.ReturnPeca(origem).MovimentosPossiveis();
            
            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);
            
            
            
            Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
            
            partida.ExecutarMovimento(origem, destino );
        }   
        
        Tela.ImprimirTabuleiro(partida.Tabuleiro);
    }
}