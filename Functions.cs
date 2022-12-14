using System.Globalization;
using Math = System.Math;

namespace MES_Csharp;

public static class Functions
{
    //  Probably garbage
    
    public static double function11(double x)
    {
        return 2 * (Math.Pow(x, 2)) + 3 * x - 8;
    }
    
    public static double function21(double x, double y)
    {
        return (-2 * Math.Pow(x, 2) * y) + 2 * x * y + 4;
    }
    
    static double Integrate1d2(Func<double, double> function)
    {
        double[] nodes = new double[] { 0, -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) };
        
        double result = 0;
        for (int i = 1; i < 3; i++)
        {
            result += function(nodes[i]);
        }
        
        return result;
    }

    static double Integrate1d3(Func<double, double> function)
    {
        double[] nodes = { 0,-Math.Sqrt(3.0 / 5.0), 0, Math.Sqrt(3.0 / 5.0) };
        double[] coefficients = { 0, 5.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0 };
        double result = 0;

        for (int i = 1; i <= 3; i++)
        {
            result += function(nodes[i]) * coefficients[i];
        }
        return result;
    }

    static double Integrate2d2(Func<double, double, double> function)
    {
        double[] nodes = new double[] { 0, -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) };
        double[] coefficients = new double[] { 0, 1, 1 };
        double result = 0;

        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                result += function(nodes[j], nodes[i]) * coefficients[i] * coefficients[j];
            }
        }

        return result;
    }

    static double Integrate2d3(Func<double, double, double> function)
    {
        double[] nodes = new double[] { 0, -Math.Sqrt(3.0 / 5.0), 0, Math.Sqrt(3.0 / 5.0) };
        double[] coefficients = new double[] { 0, 5.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0 };
        double result = 0;

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                result += function(nodes[i], nodes[j]) * coefficients[i] * coefficients[j];
            }
        }

        return result;
    }

    //  Shape functions
    
    public static double N1(double ξ, double η)
    {
        return (1 - ξ) * (1 - η) / 4;
    }
    public static double N2(double ξ, double η)
    {
        return  (1 + ξ) * (1 - η) / 4;
    }
    public static double N3(double ξ, double η)
    {
        return (1 + ξ) * (1 + η) / 4;
    }
    public static double N4(double ξ, double η)
    {
        return (1 - ξ) * (1 + η) / 4;
    }
    
    //  Derivatives of Shape Functions 
    
    public static double N1dξ(double? ξ, double η)
    {
        return - (1 - η) / 4;
    }
    public static double N2dξ(double? ξ, double η)
    {
        return  (1 - η) / 4;
    }
    public static double N3dξ(double? ξ, double η)
    {
        return (1 + η) / 4;
    }
    public static double N4dξ(double? ξ, double η)
    {
        return - (1 + η) / 4;
    }
    
    
    public static double N1dη(double ξ, double? η = 0)
    {
        return (1 - ξ) * - 1 / 4;
    }
    public static double N2dη(double ξ, double? η = 0)
    {
        return (1 + ξ) * - 1 / 4;
    }
    public static double N3dη(double ξ, double? η = 0)
    {
        return (1 + ξ) / 4;
    }
    public static double N4dη(double ξ, double? η = 0)
    {
        return (1 - ξ) / 4;
    }

    public static double FindFromIntegralPoint(double value1, double value2, double ksi)
    {
        double wage1 = Math.Abs(ksi - 1) / 2;
        double wage2 = Math.Abs(ksi + 1) / 2;
        
        return value1 * wage1 + value2 * wage2;
    }
    
    //  Mathematical functions

    public static double[,] MatrixInversion(this double[,] matrix)
    {
        double inverseDeterminant = 1 / MatrixDeterminant(matrix);

        return new double[,]
        {
            { matrix[1, 1] * inverseDeterminant, -matrix[0, 1] * inverseDeterminant },
            { -matrix[1, 0] * inverseDeterminant, matrix[0, 0] * inverseDeterminant }
        };
    }

    public static double MatrixDeterminant(this double[,] matrix)
    {
        return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
    }

    public static double[,] VectorsMultiplication(double[] column, double[] row)
    {
        double[,] matrixToReturn = new double[column.Length, column.Length];

        for (int i = 0; i < column.Length; i++)
        {
            for (int j = 0; j < column.Length; j++)
            {
                matrixToReturn[i, j] = column[i] * row[j];
            }
        }

        return matrixToReturn;
    }

    public static double[,] MatrixSummation(double[,] matrixA, double[,] matrixB, int size)
    {
        double[,] matrixToReturn = new double[size, size];
    
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrixToReturn[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }
    
        return matrixToReturn;
    }
    
    public static double[,] MatrixSummation(double[,] matrixA, double[,] matrixB)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrixA.Length));
        double[,] matrixToReturn = new double[lenght, lenght];

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrixToReturn[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }

        return matrixToReturn;
    }
    
    public static void AddMatrix(this double[,] matrixA, double[,] matrixB)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrixA.Length));

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrixA[i, j] += matrixB[i, j];
            }
        }
    }
    
    public static double[,] MatrixSummation(double[,] matrixA, double[,] matrixB, double[,] matrixC)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrixA.Length));
        double[,] matrixToReturn = new double[lenght, lenght];

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrixToReturn[i, j] = matrixA[i, j] + matrixB[i, j] + matrixC[i, j] ;
            }
        }

        return matrixToReturn;
    }
    
    public static double[,] MultiplyMatrix(this double[,] matrix, double multiplier)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrix.Length));
        double[,] matrixToReturn = new double[lenght, lenght];

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrixToReturn[i, j] = matrix[i, j] * multiplier;
            }
        }

        return matrixToReturn;
    }
    
    public static void PrintMatrix(this double[,] matrix, int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write($"{matrix[i, j].ToString("F2", CultureInfo.InvariantCulture)}\t");
            }   Console.WriteLine();
        }   Console.WriteLine();
    }
    
    public static void PrintMatrix(this double[,] matrix)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrix.Length));
        
        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                Console.Write($"{matrix[i, j].ToString("F2", CultureInfo.InvariantCulture)}\t");
            }   Console.WriteLine();
        }   Console.WriteLine();
    }

    public static double[] VectorSummation(double[] vectorA, double[] vectorB)
    {
        double[] vectorToReturn = new double [vectorA.Length];
        for (int i = 0; i < vectorA.Length; i++)
        {
            vectorToReturn[i] = vectorA[i] + vectorB[i];
        }

        return vectorToReturn;
    }

    public static double[] MultiplyVector(double[] vector, double multiplier)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] *= multiplier;
        }

        return vector;
    }
    
    public static double GetDistance(Node node1, Node node2)
    {
        return Math.Sqrt(Math.Pow(node2.X - node1.X, 2) + Math.Pow(node2.Y - node1.Y, 2));
    }

    public static double[,] CopyMatrix(this double[,] matrix)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrix.Length));
        double[,] matrixToReturn = new double[lenght, lenght];

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrixToReturn[i, j] = matrix[i, j];
            }
        }

        return matrixToReturn;
    }
    
    public static void CopyMatrix(this double[,] matrix, double[,] copiedMatrix)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(copiedMatrix.Length));

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrix[i, j] = copiedMatrix[i, j];
            }
        }

        //matrix = matrixToReturn;
    }

    public static void Clear(this double[,] matrix)
    {
        int lenght = Convert.ToInt32(Math.Sqrt(matrix.Length));

        for (int i = 0; i < lenght; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                matrix[i, j] = 0;
            }
        }
    }
    
    public static void Clear(this double[] vector)
    {
        int lenght = vector.Length;

        for (int i = 0; i < lenght; i++)
        {
            vector[i] = 0;
        }
    }

    public static double[] MultiplyMatrixByVector(double[,] matrix, double[] vector)
    {
        int length = vector.Length;
        double[] vectorToReturn = new double[length];

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                vectorToReturn[i] += matrix[i, j] * vector[j];
            }
        }

        return vectorToReturn;
    }
}