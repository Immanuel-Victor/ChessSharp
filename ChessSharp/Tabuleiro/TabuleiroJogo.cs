using ChessSharp.Tabuleiro.Exeptions;

namespace ChessSharp.Tabuleiro;

public class TabuleiroJogo
{
    public int Linhas { get; set; }
    public int Colunas { get; set; }
    private Peca?[,] _pecas;

    public TabuleiroJogo(int linhas, int colunas)
    {
        Linhas = linhas;
        Colunas = colunas;
        _pecas = new Peca[linhas, colunas];
    }

    public Peca? ReturnPeca(Posicao pos)
    {
        return _pecas[pos.Linha, pos.Coluna];
    }

    public bool ExistePeca(Posicao pos)
    {
        ValidarPosicão(pos);
        return _pecas[pos.Linha, pos.Coluna] != null;
    }

    public void ColocarPeca(Peca p, Posicao pos)
    {
        if (ExistePeca(pos))
        {
            throw new TabuleiroException("Já existe uma peça nessa posição");
        }
        _pecas[pos.Linha, pos.Coluna] = p;
        p.Posicao = pos;
    }

    public Peca? RetirarPeca(Posicao pos)
    {
        Peca? aux = ReturnPeca(pos);
        if (aux == null)
        {
            return null;
        }

        aux.Posicao = null;
        _pecas[pos.Linha, pos.Coluna] = null;
        return aux;
    }

    public bool PosicaoValida(Posicao pos)
    {
        if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
        {
            return false;
        }

        return true;
    }

    public void ValidarPosicão(Posicao pos)
    {
        var validPosition = PosicaoValida(pos);

        if (!validPosition)
        {
            throw new TabuleiroException("Posição inválida!");
        }
    }
}