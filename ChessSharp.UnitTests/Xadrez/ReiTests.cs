using ChessSharp.Tabuleiro;
using ChessSharp.Xadrez;
using FluentAssertions;

namespace ChessSharp.UnitTests.Xadrez;

public class ReiTests
{
    private readonly TabuleiroJogo tabuleiroJogo;
    
    public ReiTests()
    {
        tabuleiroJogo = new TabuleiroJogo(8,8);
    }
    [Fact]
    public void Rei_ToString_ShouldReturnR()
    {
        //Arrange
        var rei = new Rei(tabuleiroJogo, Cor.Preta);
        
        //Act

        var expected = rei.ToString();
        
        //Assert
        expected.Should().Be("R");
    }
}