using System;
using System.Collections.Generic;

namespace Lab_2
{
	class Geometry
	{
		public struct Point {
			public double x, y;
			public Point(double x, double y) {
				this.x = x;
				this.y = y;
			}
			public static double dist(Point a, Point b) {
				return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
			}
		}
		
		public abstract class Shape 
		{
			public abstract double Area();
			public abstract double Perimeter();
			public abstract Point CentreOfMass();
			public abstract void PrintBriefInfo();
			public void PrintInfo() {
				PrintBriefInfo();
				Console.WriteLine("        Area = {0:0.##}", Area());
				Console.WriteLine("        Perimeter = {0:0.##}", Perimeter());
				Point c = CentreOfMass();
				Console.WriteLine("        CenreOfMass = ({0:0.##}, {1:0.##})", c.x, c.y);
			}
		}
		
		public class Circle : Shape {
			private Point centre;
			private double radius;
			public Circle(Point centre, double radius) {
				if (radius <= 0) {
					throw new ArgumentException("Некорректный радиус");
				}
				this.centre = centre;
				this.radius = radius;
			}
			public override double Area() {
				return Math.PI * radius * radius;
			}
			public override double Perimeter() {
				return 2 * Math.PI * radius;
			}
			public override Point CentreOfMass() {
				return centre;
			}
			public override void PrintBriefInfo() {
				Console.WriteLine("Circle with ({0:0.##}, {1:0.##}) centre and radius = {2:0.##}", 
				                  			centre.x, centre.y, radius);
			}
		}
		
		public class Ellipse : Shape {
			private Point F1, F2;
			private double a, b, c;
			
			public Ellipse(Point F1, Point F2, double MajorAxis) {
				if (Point.dist(F1, F2) >= MajorAxis) {
					throw new ArgumentException("Некорректные параметры эллипса");
				}
				this.F1 = F1;
				this.F2 = F2;
				this.a = MajorAxis / 2;
				this.c = Point.dist(F1, F2) / 2;
				this.b = Math.Sqrt(a * a - c * c);
			}
			public override double Area() {
				return Math.PI * a * b;
			}
			public override double Perimeter() {
				return 4 * Math.PI * Math.Sqrt(a * a + b * b);
			}
			public override Point CentreOfMass() {
				return new Point((F1.x + F2.x) / 2, (F1.y + F2.y) / 2);
			}
			public override void PrintBriefInfo() {
				Console.WriteLine("Ellipse with F1({0:0.##}, {1:0.##}), F2({2:0.##}, {3:0.##}) and majorAxis = {4:0.##}",
									F1.x, F1.y, F2.x, F2.y, 2 * a);
			}
		}
		
		public class Polygon : Shape {
			private List<Point> vertexes;
			public Polygon(List<Point> vertexes) {
				if (vertexes.Count < 3) {
					throw new ArgumentException("Некорректные параметры многоугольника");
				}
				this.vertexes = vertexes;
				this.vertexes.Add(this.vertexes[0]);
			}
			public override double Area() {
				double s = 0;
				for (int i = 1; i < vertexes.Count; i++) {
					s += (vertexes[i].x - vertexes[i - 1].x) * (vertexes[i].y + vertexes[i - 1].y);
				}
				return Math.Abs(s) / 2;
			}
			public override double Perimeter() {
				double len = 0;
				for (int i = 1; i < vertexes.Count; i++) {
					len += Point.dist(vertexes[i], vertexes[i - 1]);
				}
				return len;
			}
			public override Point CentreOfMass() {
				double Xc = 0, Yc = 0, Curr = 0, S = 0;
				for (int i = 1; i < vertexes.Count; i++) {
					Curr = (vertexes[i].x * vertexes[i - 1].y - vertexes[i - 1].x * vertexes[i].y) / 2;
					Xc += Curr * (vertexes[i].x + vertexes[i - 1].x) / 3;
					Yc += Curr * (vertexes[i].y + vertexes[i - 1].y) / 3;
					S += Curr;
				}
				Xc /= S;
				Yc /= S;
				return new Point(Xc, Yc);
			}
			public override void PrintBriefInfo() {
				Console.WriteLine("Polygon with {0} vertexes", vertexes.Count - 1);
			}
		}
		
		public static Shape create(char choice) {
			Shape tmp = null;
			string [] values;
			switch (choice) {
				case 'c' :
					Console.WriteLine("Вы хотите создать круг. " +
					                  "Введите через пробел координаты центра и радиус");
					values = Console.ReadLine().Split();
					double xc, yc, r;
					xc = double.Parse(values[0]);
				    yc = double.Parse(values[1]);
				    r = double.Parse(values[2]);
					
					tmp = new Circle(new Point(xc, yc), r);
					Console.WriteLine("Круг успешно создан!");
					break;
					
				case 'e' :
					Console.WriteLine("Вы хотите создать эллипс. " +
					                  "Введите через пробел координаты первого фокуса, " +
					                  "координаты второго фокуса и длину большой оси");
					values = Console.ReadLine().Split();
					double majorAxis;
					Point F1, F2;
					F1.x = double.Parse(values[0]);
				    F1.y = double.Parse(values[1]);
				    F2.x = double.Parse(values[2]);
				    F2.y = double.Parse(values[3]);
				    majorAxis = double.Parse(values[4]);
					tmp = new Ellipse(F1, F2, majorAxis);
					Console.WriteLine("Эллипс успешно создан!");
					break;
					
				case 'p' :
					Console.WriteLine("Вы хотите создать многоугольник. " +
					                  "Введите количество вершин");
					int n = int.Parse(Console.ReadLine());
					List<Point> pgn = new List<Point>();
					Point p;
					for (int i = 0; i < n; i++) {
						Console.WriteLine("Введите координаты {0} вершины", i+1);
						values = Console.ReadLine().Split();
						p.x = double.Parse(values[0]);
						p.y = double.Parse(values[1]);
						pgn.Add(p);
					}
					tmp = new Polygon(pgn);
					Console.WriteLine("Многоугольник успешно создан");
					break;
					
				case 'q' :
					Console.WriteLine("Отмена");
					break;
					
				default :
					Console.WriteLine("Неверно введена клавиша");
					break;
			}
			return tmp;
		}
		
		public static void Main(string[] args)
		{
			List<Shape> list = new List<Shape>();
			List<Point> pgn = new List<Point>();
			pgn.Add(new Point(0, 0));
			pgn.Add(new Point(6, 0));
			pgn.Add(new Point(0, 8));
			
			list.Add(new Circle(new Point(0, 0), 6));
			list.Add(new Circle(new Point(3, -2), 4));
			list.Add(new Polygon(pgn));
			list.Add(new Ellipse(new Point(-4, 0), new Point(4, 0), 10));
			char key;
			do {
				Console.WriteLine("[Нажмите клавишу]");
				key = char.ToLower(Console.ReadKey(true).KeyChar);
				switch (key) {
						
					case 'c' :
						char choice;
						Shape tmp = null;
						do {
							Console.WriteLine("[Создать фигуру] :");
							Console.WriteLine("    C(c) - круг\n" +
											  "    E(e) - эллипс\n" +
							                  "    P(p) - многоугольник\n" +
							                  "    Q(q) - Отена");
							choice = char.ToLower(Console.ReadKey(true).KeyChar);
							try {
								tmp = create(choice);
							}
							catch (Exception ex) {
								Console.WriteLine("Ошибка. Неверно введены данные:\n    " + ex.Message);
							}
							if (tmp != null) {
								list.Add(tmp);
								break;
							}
						} while (choice != 'q');
						break;
						
					case 'd' :
						Console.WriteLine("[Удалить фигуру]");
						if (list.Count == 0) {
							Console.WriteLine("Нет фигур для удаления");
							break;
						}
						Console.WriteLine("Текущие фигуры :");
						for (int i = 0; i < list.Count; i++) {
							Console.Write("    {0}    ", i + 1);
							list[i].PrintBriefInfo();
						}
						Console.Write("Введите номер фигуры :    ");
						int index;
						if (int.TryParse(Console.ReadLine(),out index) && index > 0 && index <= list.Count) {
							list.RemoveAt(index - 1);
						}
						else {
							Console.WriteLine("Ошибка. Введен неверный индекс");
						}
						break;
						
					case 'l' :
						Console.WriteLine("[Список фигур]: ");
						for (int i = 0; i < list.Count; i++) {
							Console.WriteLine("    {0}", i + 1);
							list[i].PrintInfo();
							Console.WriteLine();
						}
						break;
						
					case 'h' :
						Console.WriteLine("Команды меню:\n" +
						                  "C(c) - Создать фигуру\n" +
						                  "D(d) - Удалить фигуру\n" +
						                  "L(l) - Список фигур\n" +
						                  "H(h) - Список команд\n" +
						                  "Q(q) - Выход");
						break;
						
					case 'q' :
						Console.WriteLine("Выход");
						break;
						
					default: 
						Console.WriteLine("Нажмите H(h) чтобы увидеть меню");
						break;
				}
			} while (key != 'q');
			Console.Write("Press any key..");
			Console.ReadKey(true);
		}
	}
}