using System.Collections.Generic;

public class BorderScoreTable
{
    private static int[][] borderScores = new int[5][]{
        new int [3]{18000,25000,30000},
        new int [3]{18000,25000,30000},
        new int [3]{18000,25000,30000},
        new int [3]{18000,25000,30000},
        new int [3]{18000,25000,30000}
    };

    public static int[] GetBorderScores(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < borderScores.Length)
        {
            return borderScores[stageIndex];
        }
        
        return null;
    }
}