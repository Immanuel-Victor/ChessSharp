using ChessSharp.Tabuleiro;

namespace ChessSharp.Xadrez;

public class Cavalo : Peca
{
    public Cavalo(TabuleiroJogo tabuleiroJogo, Cor cor) : base(tabuleiroJogo, cor)
    {
    }

    public override string ToString()
    {
        return "C";
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] matrizMov = new bool[TabuleiroJogo.Linhas, TabuleiroJogo.Colunas];

        Posicao current = new Posicao(Posicao.Linha, Posicao.Coluna);
        current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        current.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
        if (TabuleiroJogo.PosicaoValida(current) && PodeMover(current))
        {
            matrizMov[current.Linha, current.Coluna] = true;
        }

        return matrizMov;
    }
    

}