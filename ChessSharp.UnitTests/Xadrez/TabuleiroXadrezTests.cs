using ChessSharp.Tabuleiro;
using ChessSharp.Xadrez;
using FluentAssertions;

namespace ChessSharp.UnitTests.Xadrez;

public class TabuleiroXadrezTests
{
    private readonly TabuleiroJogo tabuleiroJogo;
    
    public TabuleiroXadrezTests()
    {
        tabuleiroJogo = new TabuleiroJogo(8,8);
    }
    
    [Theory]
    [InlineData('a', 1, 0, 7)]
    [InlineData('c', 7, 2, 1)]
    [InlineData('h', 8, 7, 0)]
    public void TabuleiroXadrez_ToPosicao_ShouldReturnPosicao(char coluna, int linha, int linhaPos, int colunaPos)
    {
        //Arrange
        PosicaoXadrez pos = new PosicaoXadrez(coluna, linha);
        
        //Act
        var result = pos.ToPosicao();
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Posicao>();
        result.Coluna.Should().Be(linhaPos);
        result.Linha.Should().Be(colunaPos);
    }
}