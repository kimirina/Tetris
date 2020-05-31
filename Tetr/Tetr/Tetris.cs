using System;
using System.Linq;
using System.Text;
using System.Threading;
namespace Tetr
{
    class MainClass
    {
        static Random random = new Random();
        // level = 1;
        static bool fix = true, game_over = false;
        static int fig_num, tmp;
        static string figure = "";
        static char[,] game_ground = new char[18, 12];
        static int x1, x2, x3, x4, y1, y2, y3, y4, drop_time = 600, rotates = 0, full_iterator = 0;
        static ConsoleKeyInfo key_info = new ConsoleKeyInfo();
        static Thread backgroundGame = new Thread(backgroundTetris);

        static void fig_select()
        {
            rotates = 0;
            fig_num = random.Next(1, 8);
            if (fig_num == 1) figure = "T";
            else if (fig_num == 2) figure = "J";
            else if (fig_num == 3) figure = "L";
            else if (fig_num == 4) figure = "Q";
            else if (fig_num == 5) figure = "Z";
            else if (fig_num == 6) figure = "nZ";
            else if (fig_num == 7) figure = "I";
        }

        static void fig_install()
        { 
            if (figure == "I")
            {
                y1 = 0; x1 = 5;
                y2 = 1; x2 = 5;
                y3 = 2; x3 = 5;
                y4 = 3; x4 = 5;
            }
            else
            if (figure == "Q")
            {
                y1 = 0; x1 = 5;
                y2 = 0; x2 = 6;
                y3 = 1; x3 = 5;
                y4 = 1; x4 = 6;
            }
            else
            if (figure == "T")
            {
                y1 = 0; x1 = 4;
                y2 = 0; x2 = 5;
                y3 = 0; x3 = 6;
                y4 = 1; x4 = 5;
            }
            else
            if (figure == "J")
            {
                y1 = 0; x1 = 6;
                y2 = 1; x2 = 6;
                y3 = 2; x3 = 6;
                y4 = 2; x4 = 5;
            }
            else
            if (figure == "L")
            {
                y1 = 0; x1 = 5;
                y2 = 1; x2 = 5;
                y3 = 2; x3 = 5;
                y4 = 2; x4 = 6;
            }
            else
            if (figure == "Z")
            {
                y1 = 0; x1 = 5;
                y2 = 1; x2 = 5;
                y3 = 1; x3 = 6;
                y4 = 2; x4 = 6;
            }
            else
            if (figure == "nZ")
            {
                y1 = 0; x1 = 6;
                y2 = 1; x2 = 6;
                y3 = 1; x3 = 5;
                y4 = 2; x4 = 5;
            }

            game_ground[y1, x1] = '#';
            game_ground[y2, x2] = '#';
            game_ground[y3, x3] = '#';
            game_ground[y4, x4] = '#';
        }

        static void fig_view()
        {
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(game_ground[i, j]);
                }
            }
        }

        static void leftArrowEvent()
        {
            if (x1 != 0 && x2 != 0 && x3 != 0 && x4 != 0)
            {
                game_ground[y1, x1] = ' ';
                game_ground[y2, x2] = ' ';
                game_ground[y3, x3] = ' ';
                game_ground[y4, x4] = ' ';
                x1--; x2--; x3--; x4--;
                game_ground[y1, x1] = '#';
                game_ground[y2, x2] = '#';
                game_ground[y3, x3] = '#';
                game_ground[y4, x4] = '#';
                fig_view();
            }
        }

        static void rightArrowEvent()
        {
            if (x1 != 11 && x2 != 11 && x3 != 11 && x4 != 11)
            {
                game_ground[y1, x1] = ' ';
                game_ground[y2, x2] = ' ';
                game_ground[y3, x3] = ' ';
                game_ground[y4, x4] = ' ';
                x1++; x2++; x3++; x4++;
                game_ground[y1, x1] = '#';
                game_ground[y2, x2] = '#';
                game_ground[y3, x3] = '#';
                game_ground[y4, x4] = '#';
                fig_view();
            }
        }

        static void downArrowEvent()
        {
            drop_time = 50;
        }

        static void spaceBarEvent() //rotate
        {
            rotates++;

            game_ground[y1, x1] = ' ';
            game_ground[y2, x2] = ' ';
            game_ground[y3, x3] = ' ';
            game_ground[y4, x4] = ' ';

            if (fig_num == 1 && y2 != 0 && x2 != 0 && x2 != 11) //figure = "T";
            {
                if (rotates == 1)
                {
                    y1--; x1++;
                    y3++; x3--;
                    y4--; x4--;
                }
                else if (rotates == 2)
                {
                    y1++; x1++;
                    y3--; x3--;
                    y4--; x4++;
                }
                else if (rotates == 3)
                {
                    y1++; x1--;
                    y3--; x3++;
                    y4++; x4++;
                }
                else if (rotates == 4)
                {
                    y1--; x1--;
                    y3++; x3++;
                    y4++; x4--;

                    rotates = 0;
                }
            }
            else if (fig_num == 1 && (y2 == 0 || x2 == 0 || x2 == 11)) rotates--;
            else if (fig_num == 2 && y2 != 0 && x2 != 0 && x2 != 11) //figure = "J";
            {
                if (rotates == 1)
                {
                    y1++; x1++;
                    y3--; x3--;
                    y4 -= 2;
                }
                if (rotates == 2)
                {
                    y1++; x1--;
                    y3--; x3++;
                    x4 += 2;
                }
                if (rotates == 3)
                {
                    y1--; x1--;
                    y3++; x3++;
                    y4 += 2;
                }
                if (rotates == 4)
                {
                    y1--; x1++;
                    y3++; x3--;
                    x4 -= 2;

                    rotates = 0;
                }
            }
            else if (fig_num == 2 && (y2 == 0 || x2 == 0 || x2 == 11)) rotates--;
            else if (fig_num == 3 && y2 != 0 && x2 != 0 && x2 != 11) //figure = "L";
            {
                if (rotates == 1)
                {
                    y1++; x1++;
                    y3--; x3--;
                    x4 -= 2;
                }
                if (rotates == 2)
                {
                    y1++; x1--;
                    y3--; x3++;
                    y4 -= 2;
                }
                if (rotates == 3)
                {
                    y1--; x1--;
                    y3++; x3++;
                    x4 += 2;
                }
                if (rotates == 4)
                {
                    y1--; x1++;
                    y3++; x3--;
                    y4 += 2;

                    rotates = 0;
                }
            }
            else if (fig_num == 3 && (y2 == 0 || x2 == 0 || x2 == 11)) rotates--;
            else if (fig_num == 5 && y2 != 0 && x2 != 0 && x2 != 11) //figure = "Z";
            {
                if (rotates == 1)
                {
                    y1++; x1++;
                    y3++; x3--;
                    x4 -= 2;
                }
                if (rotates == 2)
                {
                    y1--; x1--;
                    y3--; x3++;
                    x4 += 2;

                    rotates = 0;
                }
            }
            else if (fig_num == 5 && (y2 == 0 || x2 == 0 || x2 == 11)) rotates--;
            else if (fig_num == 6 && y2 != 0 && x2 != 0 && x2 != 11) //figure = "nZ";
            {
                if (rotates == 1)
                {
                    y1++; x1++;
                    y3--; x3++;
                    y4 -= 2;
                }
                if (rotates == 2)
                {
                    y1--; x1--;
                    y3++; x3--;
                    y4 += 2;

                    rotates = 0;
                }
            }
            else if (fig_num == 6 && (y2 == 0 || x2 == 0 || x2 == 11)) rotates--;
            else if (fig_num == 7 && y2 != 0 && x2 != 0 && x2 != 11) //figure = "I";
            {
                if (rotates == 1)
                {
                    y1++; x1--;
                    y3--; x3++;
                    y4 -= 2; x4 += 2;
                }
                if (rotates == 2)
                {
                    y1--; x1++;
                    y3++; x3--;
                    y4 += 2; x4 -= 2;

                    rotates = 0;
                }
            }
            else if (fig_num == 7 && (y2 == 0 || x2 == 0 || x2 == 11)) rotates--;

            game_ground[y1, x1] = '#';
            game_ground[y2, x2] = '#';
            game_ground[y3, x3] = '#';
            game_ground[y4, x4] = '#';
            fig_view();
        }

        static void draw_field()
        {
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(12, i);
                Console.WriteLine('|');
            }
            Console.SetCursorPosition(0, 18);
            Console.Write("------------");
        }

        static void tetris_init()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    game_ground[i, j] = ' ';
                }
            }

            Console.Clear();
            Console.WriteLine("Hello! This is mega-tetris!");
            Thread.Sleep(2000);
            Console.Clear();

            //falling.IsBackground = true;
            //falling.Start();

            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(600);
            Console.WriteLine("       ");
            Thread.Sleep(600);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(600);
            Console.WriteLine("       ");
            Thread.Sleep(600);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(600);
            Console.WriteLine("       ");
            Thread.Sleep(600);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Level 1");
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(600);
            Console.WriteLine("       ");
            Thread.Sleep(600);
            draw_field();
        }

        static bool is_verticalFixed()
        {
            if (fig_num == 1)
            {
                if (rotates == 0 && (game_ground[y4 + 1, x4] == '#' || game_ground[y1 + 1, x1] == '#' || game_ground[y3 + 1, x3] == '#')) return true;
                else if (rotates == 1 && (game_ground[y3 + 1, x3] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else if (rotates == 2 && (game_ground[y1 + 1, x1] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y3 + 1, x3] == '#')) return true;
                else if (rotates == 3 && (game_ground[y1 + 1, x1] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (fig_num == 2)
            {
                if (rotates == 0 && (game_ground[y4 + 1, x4] == '#' || game_ground[y3 + 1, x3] == '#')) return true;
                else if (rotates == 1 && (game_ground[y3 + 1, x3] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y1 + 1, x1] == '#')) return true;
                else if (rotates == 2 && (game_ground[y1 + 1, x1] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else if (rotates == 3 && (game_ground[y1 + 1, x1] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (fig_num == 3)
            {
                if (rotates == 0 && (game_ground[y4 + 1, x4] == '#' || game_ground[y3 + 1, x3] == '#')) return true;
                else if (rotates == 1 && (game_ground[y1 + 1, x1] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else if (rotates == 2 && (game_ground[y1 + 1, x1] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else if (rotates == 3 && (game_ground[y1 + 1, x1] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y3 + 1, x3] == '#')) return true;
                else return false;
            }
            else
            if (fig_num == 4)
            {
                if (game_ground[y3 + 1, x3] == '#' || game_ground[y4 + 1, x4] == '#') return true;
                else return false;
            }
            else
            if (fig_num == 5)
            {
                if (rotates == 0 && (game_ground[y4 + 1, x4] == '#' || game_ground[y2 + 1, x2] == '#')) return true;
                else if (rotates == 1 && (game_ground[y1 + 1, x1] == '#' || game_ground[y3 + 1, x3] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (fig_num == 6)
            {
                if (rotates == 0 && (game_ground[y4 + 1, x4] == '#' || game_ground[y2 + 1, x2] == '#')) return true;
                else if (rotates == 1 && (game_ground[y1 + 1, x1] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else
            if (fig_num == 7)
            {
                if (rotates == 0 && game_ground[y4 + 1, x4] == '#') return true;
                else if (rotates == 1 && (game_ground[y1 + 1, x1] == '#' || game_ground[y2 + 1, x2] == '#' || game_ground[y3 + 1, x3] == '#' || game_ground[y4 + 1, x4] == '#')) return true;
                else return false;
            }
            else return false;
        }

        static void falling()
        {
            fix = false;
            fig_view();
            Thread.Sleep(drop_time);

            while (y1 != 17 && y2 != 17 && y3 != 17 && y4 != 17)
            {
                Thread.Sleep(drop_time);
                if (is_verticalFixed() == true)
                {
                    for (int f = 0; f < 12; f++)
                    {
                        if (game_ground[0, f] == '#')
                        {
                            game_over = true;
                            break;
                        }
                    }

                    break;
                }
                game_ground[y1, x1] = ' ';
                game_ground[y2, x2] = ' ';
                game_ground[y3, x3] = ' ';
                game_ground[y4, x4] = ' ';
                y1++; y2++; y3++; y4++;
                game_ground[y1, x1] = '#';
                game_ground[y2, x2] = '#';
                game_ground[y3, x3] = '#';
                game_ground[y4, x4] = '#';
                fig_view();
            }

            fix = true;
            drop_time = 600;
        }

        static void gameOver()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    game_ground[i, j] = ' ';
                }
            }

            fig_view();
            Console.SetCursorPosition(4, 7);
            Console.WriteLine("GAME");
            Console.SetCursorPosition(4, 9);
            Console.Write("OVER");
            Thread.Sleep(3000);
            Console.SetCursorPosition(0, 11);
            Console.Write("AGAIN? (Y/N)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y) game_over = false;
            else game_over = true;
        }

        static void fullCleaner()
        {
            full_iterator = 0;
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (game_ground[i, j] == '#') full_iterator++;
                }
                if (full_iterator == 12)
                {
                    for (int k = 0; k < 12; k++) game_ground[i, k] = ' ';
                    if (i > 0)
                    {
                        tmp = i;
                        for (int m = tmp - 1; m >= 0; m--)
                        {
                            for (int g = 0; g < 12; g++)
                            {
                                game_ground[m + 1, g] = game_ground[m, g];
                            }
                        }
                    }
                }
                full_iterator = 0;
            }
        }

        static void backgroundTetris()
        {
            while (!game_over)
            {
                //лишние действия, из-за багов с прорисовкой
                Console.Clear();
                draw_field();
                //------------------------------------------
                if (fix == true)
                {
                    fullCleaner();
                    fig_select();
                    fig_install();
                }
                falling();
                if (game_over) gameOver();
            }
        }

        static void Main(string[] args)
        {
            tetris_init();
            backgroundGame.Start();
            backgroundGame.IsBackground = true;
            while (!game_over)
            {
                key_info = Console.ReadKey(true);
                if (key_info.Key == ConsoleKey.RightArrow) rightArrowEvent();
                else if (key_info.Key == ConsoleKey.LeftArrow) leftArrowEvent();
                else if (key_info.Key == ConsoleKey.DownArrow) downArrowEvent();
                else if (key_info.Key == ConsoleKey.UpArrow) spaceBarEvent();
            }
        }
    }
}
