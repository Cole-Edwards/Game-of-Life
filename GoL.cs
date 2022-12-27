using System;
using System.Threading.Tasks;

namespace Yeet{
    class GoL{

        //-------------------------Global Variables----------------
        //Debug Mode
        public static bool DEBUG = false;

        //default size of the 2D array
        // 10 arrays
        public static int Rows = 10;
        // 10 spots in each array
        public static int positions = 10;

        //default number of generations you want to watch
        public static int maxGenerations = 10;

        //-------------------------Game Functions------------------
        public static void StartGame(){
            Console.WriteLine("Starting Game...");
            //clear the screen
            Console.Clear();

            //create the array
            int[,] board = new int[Rows, positions];

            //populate the board
            board = populateBoard(board);

            //print the initial board
            Console.WriteLine("Generation 0:");
            printBoard(board);
            Console.WriteLine();

            //start iterating through the desired amount of generations
            for(int i = 0; i <= maxGenerations; i++){
                //Send Board for changes
                board = getNeighbors(board);

                //print the new board
                Console.WriteLine("Generation " + (i+1) +":");
                printBoard(board);
                Console.WriteLine();
            }
        }

        //decide how the initial board will look like
        public static int[,] populateBoard(int[,] board){
            //set up the random class
            var rand = new Random();

            //loop through the rows and put a new value (0 or 1) in each position
            for(int i = 0; i < Rows; i++){
                for(int j = 0; j < positions; j++){
                    board.SetValue(rand.Next(0,2), i, j);
                }
            }

            //send the board back
            return board;
        }

        //Helper Function: Print the current board in a readable fasion
        public static void printBoard(int[,] board){
            for(int i = 0; i < Rows; i++){
                for(int j = 0; j < positions; j++){
                    Console.Write("| " + board.GetValue(i, j) + " ");
                }
                Console.WriteLine("|");
            }
        }

        //get the cur position of the board, and get the number of neighbors
        public static int[,] getNeighbors(int[,] board){
            int curitem = -1;
            int neighbortotal;
            int updatedItem;
            int[,] newBoard = new int[Rows,positions];
            //get cur position on the board
            for(int i = 0; i < Rows; i++){
                for(int j = 0; j < positions; j++){
                    neighbortotal = 0;
                    updatedItem = -1;
                    curitem = (int)board.GetValue(i,j);
                    if(DEBUG){
                        Console.WriteLine("curitem check");
                    }

                    //check all neighbor positions around curitem
                    // if neighbor is 1, add to neighbor counter
                    // else, pass
                    if(DEBUG){
                        Console.WriteLine("current Coord: (" + i + ", " + + j + ")");
                    }
                    for(int x = i-1; x < i+2; x++){
                        if((x < 0) || (x >= Rows)){
                            continue;
                        }
                        if(DEBUG){
                            Console.WriteLine("x check");
                        }
                        for(int y = j-1; y < j+2; y++){
                            if((y < 0) || (y >= positions)){
                                continue;
                            }
                            if((x == i) && (y == j)){
                                continue;
                            }
                            if(DEBUG){
                                Console.WriteLine("y check");
                            }
                            if((int)board.GetValue(x,y) == 1){
                                neighbortotal += 1;
                            }
                        }
                    }
                    
                    //send the number of neighbors to decide if it should be updated
                    updatedItem = updateCurPos(curitem, neighbortotal);
                    
                    if(DEBUG){
                        Console.WriteLine("Back in getNeighbors() from updateCurPos");
                        Console.WriteLine("updatedItem == " + updatedItem);
                        Console.WriteLine("current value = " + newBoard.GetValue(i,j));
                    }

                    //update the new value on a new board
                    newBoard.SetValue(updatedItem, i, j);
                    
                    if(DEBUG){
                        Console.WriteLine("new current value = " + newBoard.GetValue(i,j));
                    }
                }
            }

            // return the new board
            return newBoard;
        }

        //choose what to do based on number of neighbors
        public static int updateCurPos(int curItem, int totalN){
            if(DEBUG){
                Console.WriteLine("current item: " + curItem + " | total neighbors:" + totalN);
            }

            //if dead cell and has 3 neighbors, then 1
            if((curItem == 0) && (totalN == 3)){
                return 1;
            }
            //if alive cell and has less then 2 neighbors, then 0
            else if((curItem == 1) && (totalN < 2)){
                return 0;
            }
            //if alive cell and has 2 or 3 neighbors, then 1
            else if((curItem == 1) && ((totalN == 2) || (totalN == 3))){
                return 1;
            }
            //if alive cell and has more than 3 cells, then 0
            else if((curItem == 1) && (totalN > 3)){
                return 0;
            }
            //shouldn't get here, but if does, kill the cell
            else{
                return 0;
            }
        }

        //-------------------------Options-------------------------
        public static void Options(){
            Console.Clear();
            //list the options 
            Console.WriteLine("Starting Options Menu...");
            Console.WriteLine("What would you like to change:\n 1. Size of board \n 2. activate DEBUG mode \n 3. Number of Generations \n 4. Back to Main Menu");
            //get the user's input
            int UserChoice = Convert.ToInt32(Console.ReadLine());
            int newSize;

            //do something with the user's input
            switch (UserChoice){
                //change the size of the 2d array
                case 1:
                    //show the current size
                    Console.WriteLine("Current Size: " + Rows + "x" + positions);
                    //prompt the user for new length and width then set them to the globals
                    Console.WriteLine("New Length (integer value only): ");
                    newSize = Convert.ToInt32(Console.ReadLine());
                    Rows = newSize;
                    Console.WriteLine("New Width (integer value only): ");
                    newSize = Convert.ToInt32(Console.ReadLine());
                    positions = newSize;

                    //display the new size
                    Console.WriteLine("New Size: " + Rows + "x" + positions);
                    break;
                //activate debug mode
                case 2:
                    //update the global
                    DEBUG = true;
                    break;
                //change how many generations are made
                case 3:
                    //display current Gen Cap
                    Console.WriteLine("Current Generation Cap: " + maxGenerations);
                    //prompt the user for a new cap
                    Console.WriteLine("New Generation Cap (integer value only): ");
                    //get the user's input and update the global
                    newSize = Convert.ToInt32(Console.ReadLine());
                    maxGenerations = newSize;

                    //display the new cap
                    Console.WriteLine("New Generation Cap: " + maxGenerations);
                    break;
                //Go back to the main menu
                case 4:
                    UserDecision();
                    break;
                //bad input
                default:
                    Console.WriteLine("Please choose a number that is listed");
                    Options();
                    break;
            }

            Console.WriteLine("Are you finished? (1 = y and 0 = n)");
            UserChoice = Convert.ToInt32(Console.ReadLine());
            if(UserChoice == 1){
                UserDecision();
            }
            else{
                Options();
            }
        }

        //-------------------------Main Menu Functions-------------
        //Do something based off the user's choice
        public static void UserDecision(){
            //Start the main menu and prompt the user for a choice
            Console.WriteLine("Main Menu");
            Console.WriteLine(" 1. Start \n 2. Options \n 3. Quit");
            //get the user's input
            int UserChoice = Convert.ToInt32(Console.ReadLine());

            //do something based off the user input
            switch (UserChoice){
                //start the game
                case 1:
                    StartGame();
                    break;
                //open the options menu
                case 2:
                    Options();
                    break;
                //exit the game
                case 3:
                    Console.WriteLine("Thanks for Playing!");
                    Environment.Exit(0);
                    break;
                //bad input
                default:
                    Console.WriteLine("Please choose a number that is listed");
                    UserDecision();
                    break;
            }
        }


        //Main Function
        static void Main(string[] args){
            int UserChoice = 1;
            do{
                //start by asking the user what they want to do
                UserDecision();
                //Once one round of the game has been played, ask if they want to keep playing
                Console.WriteLine("Would you like to play again? (1 = y and 0 = n)");
                UserChoice = Convert.ToInt32(Console.ReadLine());
            }
            while(UserChoice == 1);

            Console.WriteLine("\nGame Over...");
        }
    }
}