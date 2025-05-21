using ChessSharp.Tabuleiro;

namespace ChessSharp.Xadrez;

public class Peao : Peca
{
    public Peao(TabuleiroJogo tabuleiroJogo, Cor cor) : base(tabuleiroJogo, cor)
    {
    }

    public override string ToString()
    {
        return "P";
    }

    public bool ExisteInimigo(Posicao pos)
    {
        Peca p = TabuleiroJogo.ReturnPeca(pos);
        return p != null && p.Cor != Cor; 
    }

    private bool Livre(Posicao pos)
    {
        return TabuleiroJogo.ReturnPeca(pos) == null;
    }

    public override bool[,] MovimentosPossiveis()
    {
        bool[,] matrizMov = new bool[TabuleiroJogo.Linhas, TabuleiroJogo.Colunas];
        Posicao current = new Posicao(Posicao.Linha, Posicao.Coluna);
        if (Cor == Cor.Branca)
        {
            current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (TabuleiroJogo.PosicaoValida(current) && Livre(current))
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }
            current.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
            if (TabuleiroJogo.PosicaoValida(current) && Livre(current) && MovesMade == 0)
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }
            current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (TabuleiroJogo.PosicaoValida(current) && ExisteInimigo(current))
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }  
            current.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (TabuleiroJogo.PosicaoValida(current) && ExisteInimigo(current))
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }  
        }
        else
        {
            current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (TabuleiroJogo.PosicaoValida(current) && Livre(current))
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }
            current.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
            if (TabuleiroJogo.PosicaoValida(current) && Livre(current) && MovesMade == 0)
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }
            current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (TabuleiroJogo.PosicaoValida(current) && ExisteInimigo(current))
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }  
            current.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (TabuleiroJogo.PosicaoValida(current) && ExisteInimigo(current))
            {
                matrizMov[current.Linha, current.Coluna] = true;
            }  
        }

        return matrizMov;
    }
}