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

    public Peca? ReturnPeca(int linha, int coluna)
    {
        return _pecas[linha, coluna];
    }
}