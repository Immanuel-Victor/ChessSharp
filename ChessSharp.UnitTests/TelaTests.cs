using ChessSharp.Tabuleiro;
using FluentAssertions;

namespace ChessSharp.UnitTests;

public class TelaTests
{
    [Theory]
    [InlineData(8,8)]
    public void Tela_ImprimirTabuleiro_ShowEmptyBoard(int linhas, int colunas)
    {
        //Arrange
        var tab = new TabuleiroJogo(linhas, colunas);

        using var sw = new StringWriter();
        Console.SetOut(sw);
        
        //Act
        
        Tela.ImprimirTabuleiro(tab);
        
        using var sw2 = new StringWriter();
        for (int i = 0; i < linhas; i++)
        {
            sw2.Write((linhas - i) + " ");    
            for (int j = 0; j < colunas; j++)
            {
                sw2.Write("- ");
            }
            sw2.WriteLine();
        }
        sw2.Write("  a b c d e f g h");
        
        //Assert
        
        var expectedOutput = sw2.ToString();

        sw.ToString().Normalize().Should().Contain(expectedOutput.Normalize());
    }
}
