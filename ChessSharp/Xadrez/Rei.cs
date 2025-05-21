namespace ChessSharp.Xadrez;
using Tabuleiro;

public class Rei : Peca
{
    private PartidaXadrez _partida;
    public Rei(TabuleiroJogo tabuleiroJogo, Cor cor, PartidaXadrez partida) : base(tabuleiroJogo, cor)
    {
        _partida = partida;
    }

    public override string ToString()
    {
        return "R";
    }

    private bool TesteTorreRoque(Posicao pos)
    {
        Peca p = TabuleiroJogo.ReturnPeca(pos);
        return p != null && p.Cor != Cor && p is Torre && p.Cor == Cor && p.MovesMade == 0;
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
        
         //Roque Pequeno
         if (MovesMade == 0 && !_partida.Xeque)
         {
             Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
             if (TesteTorreRoque(posT1))
             {
                 Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                 Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                 if (TabuleiroJogo.ReturnPeca(p1) == null && TabuleiroJogo.ReturnPeca(p2) == null)
                 {
                     matrizMov[Posicao.Linha, Posicao.Coluna + 2] = true;
                 }
             }
         }
         
         //Roque Grande
         if (MovesMade != 0 || _partida.Xeque) return matrizMov;
         {
             Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
             if (TesteTorreRoque(posT2))
             {
                 Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                 Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                 Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                 if (TabuleiroJogo.ReturnPeca(p1) == null && TabuleiroJogo.ReturnPeca(p2) == null && TabuleiroJogo.ReturnPeca(p3) == null) 
                 {
                     matrizMov[Posicao.Linha, Posicao.Coluna + 2] = true;
                 }
             }
         }

         return matrizMov;
    }
}