using ChessSharp.Tabuleiro;

namespace ChessSharp.Xadrez;

public class Bispo : Peca
{
    public Bispo(TabuleiroJogo tabuleiroJogo, Cor cor) : base(tabuleiroJogo, cor)
    {
    }

    public override string ToString()
    {
        return "B";
    }
    
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] matrizMov = new bool[TabuleiroJogo.Linhas, TabuleiroJogo.Colunas];

        Posicao current = new Posicao(Posicao.Linha, Posicao.Coluna);
        
        //Movement Logic
        //NE
        current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        while (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
            if (TabuleiroJogo.ReturnPeca(current) != null && TabuleiroJogo.ReturnPeca(current).Cor != Cor)
            {
                break;
            }

            current.Linha -= 1;
            current.Coluna += 1;
        }
        
        //SE
        current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        while (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
            if (TabuleiroJogo.ReturnPeca(current) != null && TabuleiroJogo.ReturnPeca(current).Cor != Cor)
            {
                break;
            }

            current.Linha += 1;
            current.Coluna += 1;
        }
        
        //SW
        current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        while (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
            if (TabuleiroJogo.ReturnPeca(current) != null && TabuleiroJogo.ReturnPeca(current).Cor != Cor)
            {
                break;
            }

            current.Linha += 1;
            current.Coluna -= 1;
        }
        
        //NW
        current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        while (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
            if (TabuleiroJogo.ReturnPeca(current) != null && TabuleiroJogo.ReturnPeca(current).Cor != Cor)
            {
                break;
            }

            current.Linha -= 1;
            current.Coluna -= 1;
        }

        
        return matrizMov;
    }
}