using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] board = new char[3, 3]; // 創建遊戲棋盤
            InitializeBoard(board); // 初始化棋盤

            char currentPlayer = 'X'; // 當前玩家，初始為 'X'
            bool gameWon = false; // 是否有玩家獲勝

            while (!gameWon)
            {
                Console.Clear();
                Display(board); // 使用 Display 方法在控制台上顯示當前的遊戲棋盤。

                // 顯示消息提示當前玩家輸入棋子的位置，要求輸入座標，例如 "0,0" 表示第一行第一列。
                Console.WriteLine($"輪到玩家 {currentPlayer} 下棋，請輸入座標（例如：0,0）：");
                string input = Console.ReadLine();
                string[] coordinates = input.Split(',');//從控制台讀取用戶輸入的座標，使用 Split 方法分割成兩個部分

                // 並使用 TryParse 方法確認座標是否有效，即在範圍內且該位置沒有棋子。
                if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out int ix) || !int.TryParse(coordinates[1], out int iy))
                {
                    Console.WriteLine("輸入座標無效，請重新輸入。"); 
                    Console.ReadLine();
                    continue;
                }

               
                if (!IsValidMove(ix, iy, board))//如果位置已有棋子，顯示重新輸入
                {
                    Console.WriteLine("該位置已經被佔據，請重新輸入。");
                    Console.ReadLine();
                    continue;
                }

             
                board[ix, iy] = currentPlayer; // 如果玩家輸入的座標有效，將在該位置放置玩家的棋子。

                // 使用 Won 方法判斷當前玩家是否獲勝。如果任何一行、列或對角線有三個相同的棋子，則該玩家獲勝。
                if (Won(currentPlayer, board))
                {
                    Console.Clear();
                    Display(board); // 顯示最終的遊戲棋盤
                    Console.WriteLine($"玩家 {currentPlayer} 獲勝！");
                    gameWon = true;
                }
                else if (IsBoardFull(board)) //如果遊戲棋盤已滿且沒有玩家獲勝，則判斷為平局。
                {
                    Console.Clear();
                    Display(board); // 顯示最終的遊戲棋盤
                    Console.WriteLine("遊戲結束，平局！");
                    gameWon = true;
                }
                else
                {
                    // 切換到另一個玩家
                    currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                }
            }

            Console.ReadLine(); 
        }

        // 初始化棋盤
        static void InitializeBoard(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        // 顯示遊戲棋盤
        static void Display(char[,] board)
        {
            Console.WriteLine("  0   1   2");//座標
            Console.WriteLine();
            Console.WriteLine($"0 {board[0, 0]} | {board[0, 1]} | {board[0, 2]}");
            Console.WriteLine(" ---+---+---");
            Console.WriteLine($"1 {board[1, 0]} | {board[1, 1]} | {board[1, 2]}");
            Console.WriteLine(" ---+---+---");
            Console.WriteLine($"2 {board[2, 0]} | {board[2, 1]} | {board[2, 2]}");
            Console.WriteLine();
        }//棋盤綫條，加座標

        // 驗證移動是否有效
        static bool IsValidMove(int ix, int iy, char[,] board)
        {
            if (ix >= 0 && ix < 3 && iy >= 0 && iy < 3 && board[ix, iy] == ' ')// 檢查玩家輸入的座標是否在合法範圍內，且該位置是否已經被佔據
            {
                return true;// 若符合條件，則移動有效，返回 true
            }
            return false;//否則返回
        }

        // 判斷是否獲勝
        static bool Won(char player, char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                // 判斷水平和垂直方向是否有連線
                if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                    (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                {
                    return true; //若有連線，則返回 true，表示玩家獲勝
                }
            }

            // 判斷對角線是否有連綫
            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
            {
                return true;//若有，返回true，表示贏
            }

            return false;//若沒有,返回false，表示還沒贏
        }

        // 判斷棋盤是否已滿
        static bool IsBoardFull(char[,] board)//檢查遊戲棋盤上的每個位置是否已經被佔據
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')// 使用雙重迴圈將遊戲棋盤的每個位置設置為空格，表示未被佔據
                    {
                        return false;// 若存在空格，則返回 false，表示遊戲尚未結束
                    }
                
                }
            }
            return true;// 若所有位置都被佔據，則返回 true，表示遊戲結束且為平局
        }
    }
}