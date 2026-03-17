using System;
namespace SecondOrderSystemSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            Console.WriteLine("Informe a frequência natural (wn): ");
            double wn = double.Parse(Console.ReadLine());
            Console.WriteLine("Informe a razão de amortecimento (ζ): ");
            double z = double.Parse(Console.ReadLine());
            Console.WriteLine("Informe o tempo de amostragem (Ts): ");
            double Ts = double.Parse(Console.ReadLine());
            Console.WriteLine("Informe o tempo total de simulação (T): ");
            double T = double.Parse(Console.ReadLine());

            double denominator = Denominator(wn, z, Ts);
            double[] inputCoefficients = InputCoefficients(wn, Ts, denominator);
            double[] outputCoefficients = OutputCoefficients(wn, z, Ts, denominator);

            double[] u_passado = [0.0, 0.0];
            double[] y_passado = [0.0, 0.0];

            for (double t = 0; t < T; t += Ts)
            {
                double u = 1.0;

                double y = (inputCoefficients[0] * u)
                         + (inputCoefficients[1] * u_passado[0])
                         + (inputCoefficients[2] * u_passado[1])
                         - (outputCoefficients[0] * y_passado[0])
                         - (outputCoefficients[1] * y_passado[1]);

                y_passado[1] = y_passado[0];
                y_passado[0] = y;

                u_passado[1] = u_passado[0];
                u_passado[0] = u;

                Console.WriteLine($"Tempo: {t:F2}s | Saída: {y:F4}");
            }
        }

        static double Denominator(double wn, double z, double Ts)
        {
            return 4 + (4 * z * wn * Ts) + (Math.Pow(wn, 2) * Math.Pow(Ts, 2));
        }

        static double[] InputCoefficients(double wn, double Ts, double denominator)
        {
            double[] coefficients =
            [
                (Math.Pow(wn,2) * Math.Pow(Ts,2)) / denominator,
                2 *(Math.Pow(wn,2) * Math.Pow(Ts,2)) / denominator,
                (Math.Pow(wn,2) * Math.Pow(Ts,2)) / denominator,
            ];
            return coefficients;
        }

        static double[] OutputCoefficients(double wn, double z, double Ts, double denominator)
        {
            double[] coefficients =
            [
                ((2* Math.Pow(wn,2)*Math.Pow(Ts,2))-8)/denominator,
                (4-(4*z*wn*Ts) +(Math.Pow(wn,2)*Math.Pow(Ts,2)))/denominator,
            ];
            return coefficients;
        }
    }
}