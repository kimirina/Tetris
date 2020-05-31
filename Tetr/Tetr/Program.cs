using System;
using System.Linq;
using System.Text;
using System.Threading;
namespace Tetris
{
    class MainClass
    {
        static Random rand = new Random();
        // level = 1;
        static bool fix = true, gameover = false;
        static int figure_number, tmp;
        static string fig = "";
        static char[,] gameground = new char[18, 12];
        static int x1, x2, x3, x4, y1, y2, y3, y4, droptime = 500, rotate = 0, full_itr = 0;
        static ConsoleKeyInfo key_info = new ConsoleKeyInfo();
        static Thread backgroundGame = new Thread(backgroundTetris);

        static void fig_selects()
        {
            rotate = 0;
            figure_number = rand.Next(1, 8);
            if (figure_number == 1) fig = "T";
            else if (figure_number == 2) fig = "J";
            else if (figure_number == 3) fig = "L";
            else if (figure_number == 4) fig = "Q";
            else if (figure_number == 5) fig = "Z";
            else if (figure_number == 6) fig = "nZ";
            else if (figure_number == 7) fig = "I";
        }

        static void figure_inst()
        {
            if (fig == "I")
            {
                y1 = 0; x1 = 5;
                y2 = 1; x2 = 5;
                y3 = 2; x3 = 5;
                y4 = 3; x4 = 5;
            }
            else
            if (fig == "Q")
            {
                y1 = 0; x1 = 5;
                y2 = 0; x2 = 6;
                y3 = 1; x3 = 5;
                y4 = 1; x4 = 6;
            }
            else
            if (fig == "T")
            {
                y1 = 0; x1 = 4;
                y2 = 0; x2 = 5;
                y3 = 0; x3 = 6;
                y4 = 1; x4 = 5;
            }
            else
            if (fig == "J")
            {
                y1 = 0; x1 = 6;
                y2 = 1; x2 = 6;
                y3 = 2; x3 = 6;
                y4 = 2; x4 = 5;
            }
            else
            if (fig == "L")
            {
                y1 = 0; x1 = 5;
                y2 = 1; x2 = 5;
                y3 = 2; x3 = 5;
                y4 = 2; x4 = 6;
            }
            else
            if (fig == "Z")
            {
                y1 = 0; x1 = 5;
                y2 = 1; x2 = 5;
                y3 = 1; x3 = 6;
                y4 = 2; x4 = 6;
            }
            else
            if (fig == "nZ")
            {
                y1 = 0; x1 = 6;
                y2 = 1; x2 = 6;
                y3 = 1; x3 = 5;
                y4 = 2; x4 = 5;
            }

            gameground[y1, x1] = '#';
            gameground[y2, x2] = '#';
            gameground[y3, x3] = '#';
            gameground[y4, x4] = '#';
        }

        static void figure_view()
        {
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(gameground[i, j]);
                }
            }
        }

        static void Left_Arrow()
        {
            if (x1 != 0 && x2 != 0 && x3 != 0 && x4 != 0)
            {
                gameground[y1, x1] = ' ';
                gameground[y2, x2] = ' ';
                gameground[y3, x3] = ' ';
                gameground[y4, x4] = ' ';
                x1--; x2--; x3--; x4--;
                gameground[y1, x1] = '#';
                gameground[y2, x2] = '#';
                gameground[y3, x3] = '#';
                gameground[y4, x4] = '#';
                figure_view();
            }
        }

        static void Right_Arrow()
        {
            if (x1 != 11 && x2 != 11 && x3 != 11 && x4 != 11)
            {
                gameground[y1, x1] = ' ';
                gameground[y2, x2] = ' ';
                gameground[y3, x3] = ' ';
                gameground[y4, x4] = ' ';
                x1++; x2++; x3++; x4++;
                gameground[y1, x1] = '#';
                gameground[y2, x2] = '#';
                gameground[y3, x3] = '#';
                gameground[y4, x4] = '#';
                figure_view();
            }
        }

        static void DownArrow()
        {
            droptime = 50;
        }

        static void BarEvent() //повороты фигуры нижней клавишей
        {
            rotate++;

            gameground[y1, x1] = ' ';
            gameground[y2, x2] = ' ';
            gameground[y3, x3] = ' ';
            gameground[y4, x4] = ' ';

            if (figure_number == 1 && y2 != 0 && x2 != 0 && x2 != 11) //фигура "T";
            {
                if (rotate == 1)
                {
                    y1--; x1++;
                    y3++; x3--;
                    y4--; x4--;
                }
                else if (rotate == 2)
                {
                    y1++; x1++;
                    y3--; x3--;
                    y4--; x4++;
                }
                else if (rotate == 3)
                {
                    y1++; x1--;
                    y3--; x3++;
                    y4++; x4++;
                }
                else if (rotate == 4)
                {
                    y1--; x1--;
                    y3++; x3++;
                    y4++; x4--;

                    rotate = 0;
                }
            }
            else if (figure_number == 1 && (y2 == 0 || x2 == 0 || x2 == 11)) rotate--;
            else if (figure_number == 2 && y2 != 0 && x2 != 0 && x2 != 11) //фигура "J";
            {
                if (rotate == 1)
                {
                    y1++; x1++;
                    y3--; x3--;
                    y4 -= 2;
                }
                if (rotate == 2)
                {
                    y1++; x1--;
                    y3--; x3++;
                    x4 += 2;
                }
                if (rotate == 3)
                {
                    y1--; x1--;
                    y3++; x3++;
                    y4 += 2;
                }
                if (rotate == 4)
                {
                    y1--; x1++;
                    y3++; x3--;
                    x4 -= 2;

                    rotate = 0;
                }
            }
            else if (figure_number == 2 && (y2 == 0 || x2 == 0 || x2 == 11)) rotate--;
            else if (figure_number == 3 && y2 != 0 && x2 != 0 && x2 != 11) //фигура = "L";
            {
                if (rotate == 1)
                {
                    y1++; x1++;
                    y3--; x3--;
                    x4 -= 2;
                }
                if (rotate == 2)
                {
                    y1++; x1--;
                    y3--; x3++;
                    y4 -= 2;
                }
                if (rotate == 3)
                {
                    y1--; x1--;
                    y3++; x3++;
                    x4 += 2;
                }
                if (rotate == 4)
                {
                    y1--; x1++;
                    y3++; x3--;
                    y4 += 2;

                    rotate = 0;
                }
            }
            else if (figure_number == 3 && (y2 == 0 || x2 == 0 || x2 == 11)) rotate--;
            else if (figure_number == 5 && y2 != 0 && x2 != 0 && x2 != 11) //фигура "Z";
            {
                if (rotate == 1)
                {
                    y1++; x1++;
                    y3++; x3--;
                    x4 -= 2;
                }
                if (rotate == 2)
                {
                    y1--; x1--;
                    y3--; x3++;
                    x4 += 2;

                    rotate = 0;
                }
            }
            else if (figure_number == 5 && (y2 == 0 || x2 == 0 || x2 == 11)) rotate--;
            else if (figure_number == 6 && y2 != 0 && x2 != 0 && x2 != 11) //фигура "nZ";
            {
                if (rotate == 1)
                {
                    y1++; x1++;
                    y3--; x3++;
                    y4 -= 2;
                }
                if (rotate == 2)
                {
                    y1--; x1--;
                    y3++; x3--;
                    y4 += 2;

                    rotate = 0;
                }
            }
            else if (figure_number == 6 && (y2 == 0 || x2 == 0 || x2 == 11)) rotate--;
            else if (figure_number == 7 && y2 != 0 && x2 != 0 && x2 != 11) //фигура "I";
            {
                if (rotate == 1)
                {
                    y1++; x1--;
                    y3--; x3++;
                    y4 -= 2; x4 += 2;
                }
                if (rotate == 2)
                {
                    y1--; x1++;
                    y3++; x3--;
                    y4 += 2; x4 -= 2;

                    rotate = 0;
                }
            }
            else if (figure_number == 7 && (y2 == 0 || x2 == 0 || x2 == 11)) rotate--;

            gameground[y1, x1] = '#';
            gameground[y2, x2] = '#';
            gameground[y3, x3] = '#';
            gameground[y4, x4] = '#';
            figure_view();
        }

        static void drawfield()
        {
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(12, i);
                Console.WriteLine('|');
            }
            Console.SetCursorPosition(0, 18);
            Console.Write("------------");
        }

        static void Tetris_int()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    gameground[i, j] = ' ';
                }
            }

            Console.Clear();
            Console.WriteLine("This is tetris!");
            Thread.Sleep(2000);
            Console.Clear();



            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(500);
            Console.WriteLine("       ");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(500);
            Console.WriteLine("       ");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(500);
            Console.WriteLine("       ");
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(500);
            Console.WriteLine("       ");
            Thread.Sleep(500);
            drawfield();
        }

        static bool verticalFixed()
        {
            if (figure_number == 1)
            {
                if (rotate == 0 && (gameground[y4 + 1, x4] == '#' || gameground[y1 + 1, x1] == '#' || gameground[y3 + 1, x3] == '#')) return true;
                else if (rotate == 1 && (gameground[y3 + 1, x3] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else if (rotate == 2 && (gameground[y1 + 1, x1] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y3 + 1, x3] == '#')) return true;
                else if (rotate == 3 && (gameground[y1 + 1, x1] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (figure_number == 2)
            {
                if (rotate == 0 && (gameground[y4 + 1, x4] == '#' || gameground[y3 + 1, x3] == '#')) return true;
                else if (rotate == 1 && (gameground[y3 + 1, x3] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y1 + 1, x1] == '#')) return true;
                else if (rotate == 2 && (gameground[y1 + 1, x1] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else if (rotate == 3 && (gameground[y1 + 1, x1] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (figure_number == 3)
            {
                if (rotate == 0 && (gameground[y4 + 1, x4] == '#' || gameground[y3 + 1, x3] == '#')) return true;
                else if (rotate == 1 && (gameground[y1 + 1, x1] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else if (rotate == 2 && (gameground[y1 + 1, x1] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else if (rotate == 3 && (gameground[y1 + 1, x1] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y3 + 1, x3] == '#')) return true;
                else return false;
            }
            else
            if (figure_number == 4)
            {
                if (gameground[y3 + 1, x3] == '#' || gameground[y4 + 1, x4] == '#') return true;
                else return false;
            }
            else
            if (figure_number == 5)
            {
                if (rotate == 0 && (gameground[y4 + 1, x4] == '#' || gameground[y2 + 1, x2] == '#')) return true;
                else if (rotate == 1 && (gameground[y1 + 1, x1] == '#' || gameground[y3 + 1, x3] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (figure_number == 6)
            {
                if (rotate == 0 && (gameground[y4 + 1, x4] == '#' || gameground[y2 + 1, x2] == '#')) return true;
                else if (rotate == 1 && (gameground[y1 + 1, x1] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (figure_number == 7)
            {
                if (rotate == 0 && gameground[y4 + 1, x4] == '#') return true;
                else if (rotate == 1 && (gameground[y1 + 1, x1] == '#' || gameground[y2 + 1, x2] == '#' || gameground[y3 + 1, x3] == '#' || gameground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else return false;
        }

        static void falling()
        {
            fix = false;
            figure_view();
            Thread.Sleep(droptime);

            while (y1 != 17 && y2 != 17 && y3 != 17 && y4 != 17)
            {
                Thread.Sleep(droptime);
                if (verticalFixed() == true)
                {
                    for (int f = 0; f < 12; f++)
                    {
                        if (gameground[0, f] == '#')
                        {
                            gameover = true;
                            break;
                        }
                    }

                    break;
                }
                gameground[y1, x1] = ' ';
                gameground[y2, x2] = ' ';
                gameground[y3, x3] = ' ';
                gameground[y4, x4] = ' ';
                y1++; y2++; y3++; y4++;
                gameground[y1, x1] = '#';
                gameground[y2, x2] = '#';
                gameground[y3, x3] = '#';
                gameground[y4, x4] = '#';
                figure_view();
            }

            fix = true;
            droptime = 600;
        }

        static void gameOver()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    gameground[i, j] = ' ';
                }
            }

            figure_view();
            Console.SetCursorPosition(4, 7);
            Console.WriteLine("GAME");
            Console.SetCursorPosition(4, 9);
            Console.Write("OVER");
            Thread.Sleep(3000);
            Console.SetCursorPosition(0, 11);
            Console.Write("AGAIN? (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) gameover = false;
            else gameover = true;
        }

        static void fullCleaner()
        {
            full_itr = 0;
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (gameground[i, j] == '#') full_itr++;

                    if (full_itr == 12)
                    {
                        for (int k = 0; k < 12; k++) gameground[i, k] = ' ';
                        if (i > 0)
                        {
                            tmp = i;
                            for (int m = tmp - 1; m >= 0; m--)
                            {
                                for (int g = 0; g < 12; g++)
                                {
                                    gameground[m + 1, g] = gameground[m, g];
                                }
                            }
                        }
                    }
                    full_itr = 0;
                }
            }
        }

        private static void Main(string[] args)
        {
            Tetris_int();
            backgroundGame.Start();
            backgroundGame.IsBackground = true;
            while (!gameover)
            {
                key_info = Console.ReadKey(true);
                if (key_info.Key == ConsoleKey.RightArrow) Right_Arrow();
                else if (key_info.Key == ConsoleKey.LeftArrow) Left_Arrow();
                else if (key_info.Key == ConsoleKey.DownArrow) DownArrow();
                else if (key_info.Key == ConsoleKey.UpArrow) BarEvent();
            }
        }

        private static void backgroundTetris()
        {
            while (!gameover)
            {
                //problems с прорисовкой
                Console.Clear();
                drawfield();
                //------------------------------------------
                if (fix == true)
                {
                    fullCleaner();
                    fig_selects();
                    figure_inst();
                }
                falling();
                if (gameover) gameOver();
            }
        }
    }
}