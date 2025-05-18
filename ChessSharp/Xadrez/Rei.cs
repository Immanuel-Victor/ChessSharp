namespace ChessSharp.Xadrez;
using Tabuleiro;

public class Rei : Peca
{
    public Rei(TabuleiroJogo tabuleiroJogo, Cor cor) : base(tabuleiroJogo, cor)
    {
    }

    public override string ToString()
    {
        return "R";
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] matrizMov = new bool[TabuleiroJogo.Linhas, TabuleiroJogo.Colunas];

        Posicao current = new Posicao(Posicao.Linha, Posicao.Coluna);
        //CHECK POSITION
         //NORTH
         current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //NE
         current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //EAST
         current.DefinirValores(Posicao.Linha , Posicao.Coluna + 1);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //SE
         current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //SOUTH
         current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //SW
         current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //WEST
         current.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
         //NW
         current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
         if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
         {
             matrizMov[current.Linha, current.Coluna] = true;
         }
        
        return matrizMov;
    }
}