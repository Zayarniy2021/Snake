// See https://aka.ms/new-console-template for more information

using System.Timers;

System.Timers.Timer timer=new System.Timers.Timer();

bool quit=false;
int vx;
int vy;
int headX;
int headY;
int[,] GameField;
int w = 80, h = 40;

void Init()
{
    Console.CursorVisible = false;
    Console.SetWindowSize(w+2, h+2);
    Console.SetBufferSize(w+2, h+2);
    vx = 0;
    vy = 1;
    headX = 20;
    headY = 10;
    GameField = new int[82, 42];
    GameField[headX, headY] = 1;
    //GameField[headX, headY - 1] = 2;
    //GameField[headX, headY - 2] = 3;
    //GameField[headX, headY - 3] = 4;
    Random random = new Random();
    for(int i=0;i<10;i++)
        GameField[random.Next(80), random.Next(40)] =-1 ;
    PrintGameField();
    timer.Interval = 500;
    //timer.Start();
}

void Update()
{
    //GameField[headX, headY] = 0;
    //if (GameField[headX + vx, headY + vy] < 0) Next(headX + vx, headY + vy, 1);
    //else
    {
        headX += vx;
        headY += vy;
        if (GameField[headX, headY] < 0)
        {
            GameField[headX, headY] = 1;
            Next(headX-vx, headY-vy, 1, 1);
        }
        else
            Next(headX, headY, 1);
        //if (GameField[headX, headY] > 0) quit = true;
    }
}

void Next(int tailX,int tailY, int n,int p=0)
{
    
    GameField[tailX, tailY] = n+p;

    if (GameField[tailX + 1, tailY] == n+p) Next(tailX + 1, tailY, n + 1,p);
    else
        if (GameField[tailX - 1, tailY] == n+p) Next(tailX - 1, tailY, n + 1,p);
    else
        if (GameField[tailX, tailY - 1] == n+p) Next(tailX, tailY - 1, n + 1,p);
    else
        if (GameField[tailX, tailY + 1] == n+p) Next(tailX, tailY + 1, n + 1,p);
   else 
        if (p==0) GameField[tailX, tailY] = 0;

}

void PrintGameField()
{
    for (int y = 0; y < h; y++)
        for (int x = 0; x < w; x++)
        {
            Console.SetCursorPosition(x, y);
            //System.Threading.Thread.Sleep(1);
            switch (GameField[x,y])
            {
                case 0:
                    Console.WriteLine('█');
                    break;
                case -1:
                    Console.WriteLine('&');
                    break;
                case 1:
                    Console.WriteLine('1');
                    break;
                default:
                    Console.WriteLine(GameField[x,y]);
                    break;
            }
        }
}

void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    //if (!Console.KeyAvailable) return;
    Update();
    PrintGameField();
    KeyboardUpdate();
}

void KeyboardUpdate()
{
    if (Console.KeyAvailable)
    {

        ConsoleKey key = Console.ReadKey().Key;
        System.Diagnostics.Debug.WriteLine(key);
        System.Diagnostics.Debug.WriteLine("X=" + headX + " Y=" + headY + " VX=" + vx + " VY=" + vy);
        Console.Title = DateTime.Now.ToLongTimeString();
        //if (key == ConsoleKey.Escape) return;
        switch (key)
        {

            case ConsoleKey.LeftArrow:

                vx = -1;
                vy = 0;
                break;
            case ConsoleKey.RightArrow:

                vx = 1;
                vy = 0;
                break;
            case ConsoleKey.UpArrow:

                vx = 0;
                vy = -1;
                break;
            case ConsoleKey.DownArrow:

                vx = 0;
                vy = 1;
                break;
            case ConsoleKey.Escape:
                timer.Stop();
                quit = true;
                Console.WriteLine("Bye-bye");
                break;

        }


    }

}


Init();
timer.Elapsed += Timer_Elapsed;


PrintGameField();

while (!quit)
{
    KeyboardUpdate();
    Update();
    PrintGameField();
    System.Threading.Thread.Sleep(100);
};