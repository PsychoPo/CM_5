using System;

namespace lab5VAR29

{
	class Program
	{
		const double a = 0.0, b = 2.0, eps = 0.001;
		static double f(double x)
		{
			return x * Math.Pow(2, -x);
		}
		static double F(double x)
		{
			return -((Math.Log(2) * x + 1) / (Math.Pow(Math.Log(2), 2) * Math.Pow(2, x))); // -ln(2)*x+1/ln^2(2)*2^x
		}
		static double f_Newton_Leibniz()
		{
			return F(b) - F(a);
		}
		static double f_Trapezium(out double N)
		{
			double res_prev = 0.0, res_now = 0.0;
			N = 2.0;

			do
			{
				double tau = (b - a) / N, temp = 0.0;
				for (double i = 0; i <= 2; i += tau)
				{
					if (i == 0.0 || i == 2.0)
						temp += (tau / 2) * (f(Math.Round(i, 5)));
					else
						temp += (tau / 2) * (2 * f(Math.Round(i, 5)));
				}
				res_prev = res_now;
				res_now = temp;
				N++;
				Console.WriteLine($"Количестов узлов N = {N}, Значения абсолютных погрешностей Функции Трапеций = {Math.Round(Math.Abs(res_now - f_Newton_Leibniz()), 4)}");
			} while (Math.Abs(res_now - res_prev) > eps);

			return res_now;
		}
		static double f_Simpson(out double N)
		{
			double res_prev = 0.0, res_now = 0.0;
			N = 2.0;

			do
			{
				double tau = (b - a) / N, temp = 0.0;
				int k = 0;
				for (double i = 0.0; i <= 2.0; i += tau)
				{
					if (i == 0.0 || i == 2.0)
						temp += (tau / 3) * (f(Math.Round(i, 5)));
					else if ((k % 2) == 0 && i != 2.0 && i != 0.0)
						temp += (tau / 3) * (2 * f(Math.Round(i, 5)));
					else if ((k % 2) != 0 && i != 2.0 && i != 0.0)
						temp += (tau / 3) * (4 * f(Math.Round(i, 5)));
					k++;
				}
				res_prev = res_now;
				res_now = temp;
				N += 2;
				Console.WriteLine($"Количестов узлов N = {N}, Значения абсолютных погрешностей Функции Симпсона = {Math.Round(Math.Abs(res_now - f_Newton_Leibniz()), 5)}");
			} while (Math.Abs(res_now - res_prev) > eps);

			return res_now;
		}
		static void Main(string[] args)
		{
			double Ntr, Nsim;
			Console.Clear();
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Console.WriteLine($"Функция Ньютона-Лейбница = {Math.Round(f_Newton_Leibniz(), 5)}");
			f_Trapezium(out Ntr);
			Console.WriteLine();
			f_Simpson(out Nsim);
			Console.WriteLine($"Количество узлов Функции Трапеций = {Ntr + 1}\nКоличество узлов Функции Симпсона = {Nsim + 1}\n{Ntr + 1}>{Nsim + 1}");
		}
	}
}
