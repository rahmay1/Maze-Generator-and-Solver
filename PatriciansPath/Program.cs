/*
 * Date: December 7th, 2017
 * Description: A program which may read in, or generate a maze, the program then solves the maze (if its solvable) and outputs the correct path to the user. The user can choose to either generate the maze, or read in the maze from a text file.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // For the StreamWriter, basically shortens the StreamWriter summoning command lines.
using System.Collections;
using System.Threading;

namespace PatriciansPath
{
    class Program
    {
        // A series of functions which all have a string "text" parameter to change the text color based on their corrisponding function names.
        public static void WriteRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteYellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {
            // Initalized Local Variables
            char userChoice = ' '; // The variable which represents the users choice.
            Boolean seeOrNot = false; // Represents the boolean variable if the user wants to see the animated output or just the results of the maze solver.
            Boolean error = false; // Represents if an error occured when the user enters a invalid input, basically catches any exceptions.
            int heightOfMaze; // A integer variable which represents the height of the maze.
            int lengthOfMaze; // A integer variable which represents the length of the maze.
            int x = 0; // A integer variable which represents the start point in the x position, specified by the user.
            int y = 0; // A integer variable which represents the start point in the y position, specified by the user.
            char[,] maze = null; // Initializing the maze to null (for now.)


            // Do While Statement - Asks user if they would like to solve a maze, which repeats if the user enters a invalid choice.
            do
            {
                // Prompting user to enter a choice.
                WriteYellow("Hello there, this is a program which solves mazes using a recursive algorithm.\nWould you like to solve a maze today (Y - Yes, N - No): ");
                error = char.TryParse(Console.ReadLine(), out userChoice); // Using boolean error to catch exception.
                if (error == false || !(char.ToUpperInvariant(userChoice) == 'Y' || char.ToUpperInvariant(userChoice) == 'N'))
                {
                    WriteRed("\nERROR: Please enter a valid choice.\n\n"); // Outputting invalid choice if user enters an error.
                }
                else if (char.ToUpperInvariant(userChoice) == 'N')
                {
                    goto exit; // Going to "exit" reference if the user choose 'N' or 'n'.
                }
            } while (error == false || !(char.ToUpperInvariant(userChoice) == 'Y' || char.ToUpperInvariant(userChoice) == 'N'));

            // Do While Statement - If the user wants to play again, which repeats if the user plays again.
            do
            {
                // This has to always be available if the user wants to try again...
                StreamReader sr = new StreamReader("maze.txt"); // Assigning StreamReader to a variable so it can be called and pinpointing the file to be read.

                // Do While Statement - Asks user if they would like to see the maze being solved or not, which repeats if the user enters a invalid choice.
                do
                {
                    // Prompting user to enter a choice.
                    WriteYellow("\n\nWould you like to see the maze animated (somewhat slow)? (Y - Yes, N - No): ");
                    error = char.TryParse(Console.ReadLine(), out userChoice); // Using boolean error to catch exception.
                    if (error == false || !(char.ToUpperInvariant(userChoice) == 'Y' || char.ToUpperInvariant(userChoice) == 'N'))
                    {
                        WriteRed("\nERROR: Please enter a valid choice.\n\n"); // Outputting invalid choice if user enters an error.
                    }
                } while (error == false || !(char.ToUpperInvariant(userChoice) == 'Y' || char.ToUpperInvariant(userChoice) == 'N'));

                // If Statements - If the userChoice is 'Y' allow the user to see the animations for maze (seeOrNot = true).
                if (userChoice == 'Y')
                {
                    seeOrNot = true;
                }
                else // Else If - If not, allow the to not see the animations (seeOrNot = false).
                {
                    seeOrNot = false;
                }

                // Do While Statement - Asks user if they would like a autogenerated maze or a maze read from a text file, which repeats if the user enters a invalid choice.
                do
                {
                    // Prompting user to enter a choice.
                    WriteYellow("\n\nWould you like a Generated Maze or a Maze read from a Text File? (G - Generated, T - TextFile): ");
                    error = char.TryParse(Console.ReadLine(), out userChoice); // Using boolean error to catch exception.
                    if (error == false || !(char.ToUpperInvariant(userChoice) == 'G' || char.ToUpperInvariant(userChoice) == 'T'))
                    {
                        WriteRed("\nERROR: Please enter a valid choice.\n\n"); // Outputting invalid choice if user enters an error.
                    }
                } while (error == false || !(char.ToUpperInvariant(userChoice) == 'G' || char.ToUpperInvariant(userChoice) == 'T'));

                // Compounded If/Else If Statements - If the user Choice is equal to G, continue...
                if (char.ToUpperInvariant(userChoice) == 'G')
                {

                    // Do While Statement - Asks user if they would like to specify the height and length or autogenerate the height and length, which repeats if the user enters a invalid choice.
                    do
                    {
                        // Prompting user to enter a choice.
                        WriteYellow("\n\nWould you like to specify the Height and Length or Generate them? (S - Specify, A - AutoGenerate from (8 - 20)): ");
                        error = char.TryParse(Console.ReadLine(), out userChoice); // Using boolean error to catch exception.
                        if (error == false || !(char.ToUpperInvariant(userChoice) == 'S' || char.ToUpperInvariant(userChoice) == 'A'))
                        {
                            WriteRed("\nERROR: Please enter a valid choice.\n\n"); // Outputting invalid choice if user enters an error.
                        }
                    } while (error == false || !(char.ToUpperInvariant(userChoice) == 'S' || char.ToUpperInvariant(userChoice) == 'A'));

                    // Compounded If/Else If Statements - If the userChoice is equal to S (ignoring cases), continue...
                    if (char.ToUpperInvariant(userChoice) == 'S')
                    {
                        // Do While Statement - Asks the user for the height of the maze, which repeats if the user enters a invalid choice.
                        do
                        {
                            // Prompting user to enter a choice.
                            WriteYellow("\n\nPlease enter the Height of the Maze (8 - 50): ");
                            error = int.TryParse(Console.ReadLine(), out heightOfMaze); // Using boolean error to catch exception.
                            if (error == false || !(heightOfMaze > 7 && heightOfMaze < 51))
                            {
                                WriteRed("\nERROR: Please enter a valid Height of the Maze.\n\n"); // Outputting invalid choice if user enters an error.
                            }
                        } while (error == false || !(heightOfMaze > 7 && heightOfMaze < 51));

                        // Do While Statement - Asks the user for the height of the maze, which repeats if the user enters a invalid choice.
                        do
                        {
                            // Prompting user to enter a choice.
                            WriteYellow("\n\nPlease enter the Length of the Maze (8 - 50): ");
                            error = int.TryParse(Console.ReadLine(), out lengthOfMaze); // Using boolean error to catch exception.
                            if (error == false || !(lengthOfMaze > 7 && lengthOfMaze < 51))
                            {
                                WriteRed("\nERROR: Please enter a valid Length of the Maze.\n\n"); // Outputting invalid choice if user enters an error.
                            }
                        } while (error == false || !(lengthOfMaze > 7 && lengthOfMaze < 51));
                    }
                    else // Else Statement - If the userChoice is not equal to S (ignoring cases)... 
                    {
                        heightOfMaze = new Random().Next(8, 21); // ...Generate a random height for the heightOfMaze variable from 8 - 21
                        lengthOfMaze = new Random().Next(8, 21); // ...Generate a random length for the lengthOfMaze variable from 8 - 21
                    }
                    maze = new char[lengthOfMaze, heightOfMaze]; // Initalizing the string "generatedMaze" as a 2D array with a size of the obtained length as "x" and the obtained height as "y".

                    // Generating the "Walls" (or 'X') of this maze using two For-Loops representing row or x (g) and column or y (i). 
                    for (int i = 0; i < heightOfMaze; i++)
                    {
                        for (int g = 0; g < lengthOfMaze; g++)
                        {
                            maze[g, i] = 'X';
                        }
                    }
                    // Autogenerating/Carving out the rest of the maze using the MazeGen function which takes the 'X' filled maze, the starting x, and y points, a boolean if you would like a $ to represent the ending of the maze, a random value, the randomDirections value set as 0, and the rest of the values set to their default constructors.
                    MazeGen(maze, 1, 1, new List<Tuple<int, int>>(), true, new Random(), new List<int>(), new Stack<Tuple<int, int>>(), seeOrNot);

                    // If Statement - If seeOrNot is true...
                    if (seeOrNot == true)
                    {
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + heightOfMaze); // ...Resetting the cursor position to default to make sure everything is outputted properly
                    }
                }
                else // Else Statement - If the userChoice doesn't equal 'G' (ignoring cases)
                {
                    // Calling StreamReader to read the file contents to be assigned to integers and string arrays.
                    heightOfMaze = int.Parse(sr.ReadLine()); // Assigning the first line to a integer height variable to specify the "height" of the maze.
                    lengthOfMaze = int.Parse(sr.ReadLine()); // Assigning the second line to a integer length variable to specify the "length" of the maze.
                    maze = new char[lengthOfMaze, heightOfMaze]; // Assigning the string "generatedMaze" as a 2D array with a size of the obtained length as "x" and the obtained height as "y".
                }
                int p; // Represents the x value to be indicated by the array.

                // Prompting the user about outputting the maze
                WriteGreen("\n\nGenerating the maze: \n\n");

                Console.WriteLine("  Y"); // Outputting the Y to indicate the Y Columns

                Console.Write("X +  "); // Outputting the X to indicate the X Rows

                // For Loop Statement - Repeats for every X value
                for (int l = 0; l < lengthOfMaze; l++)
                {
                    // If Statement - If the l is equal to or greater than 10, output 1 less space so it has room.
                    if (l / 10 >= 1)
                    {
                        Console.Write(l); // Outputting the row index to indicate a new row.
                    }
                    else // If Not, continue...
                    {
                        Console.Write(l + " "); // Outputting the row index to indicate a new row.
                    }
                }
                Console.WriteLine(); // Outputting a space to position maze properly.

                // Outputting the unsolved maze using two for loops representing row or x (g) and column or y (i). 
                for (int i = 0; i < heightOfMaze; i++)
                {
                    // If Statement - If the i is equal to or greater than 10, output 1 less space so it has room.
                    if (i / 10 >= 1)
                    {
                        Console.Write(" {0}  ", i); // Outputting the column index to indicate a new column.
                    }
                    else // If Not, continue...
                    {
                        Console.Write("  {0}  ", i); // Outputting the column index to indicate a new column.
                    }

                    // If the userChoice is T (ignoring cases)
                    if (char.ToUpperInvariant(userChoice) == 'T')
                    {
                        p = 0; // Using the p to represent the x value of the maze.

                        foreach (var character in sr.ReadLine())
                        {
                            Console.Write(character + " "); // Output that character
                            maze[p, i] = character; // Assigning the character to the maze at the current x (p) and y (i).
                            p++; // Using the p to represent the x value of the maze.
                        }
                    }
                    else
                    { // If Not, Continue...
                        // Creating a For loop g to represent the y position in the maze.
                        for (int g = 0; g < lengthOfMaze; g++)
                        {
                            Console.Write(maze[g, i] + " "); // Output the current x and y value of maze.
                        }
                    }
                    Console.WriteLine(); // Breaking the line to indicate a new row.

                }

                // Do While Statement - Repeats if the user enters a invalid x and y position.
                do
                {
                    // Do While Statement - Asks user if they would like to specify the starting point in the x position, which repeats if the user enters a invalid choice.
                    do
                    {
                        // Prompting user to enter a choice.
                        WriteYellow("\n\nWhich starting X point would you like? (0 - " + (lengthOfMaze - 1) + "): ");
                        error = int.TryParse(Console.ReadLine(), out x); // Using boolean error to catch exception.
                        if (error == false || !(x > 0 && x < lengthOfMaze))
                        {
                            WriteRed("\nERROR: Please enter a valid choice.\n\n"); // Outputting invalid choice if user enters an error.
                        }
                    } while (error == false || !(x > 0 && x < lengthOfMaze));

                    // Do While Statement - Asks user if they would like to specify the starting point in the y position, which repeats if the user enters a invalid choice.
                    do
                    {
                        // Prompting user to enter a choice.
                        WriteYellow("\n\nWhich starting Y point would you like? (0 - " + (heightOfMaze - 1) + "): ");
                        error = int.TryParse(Console.ReadLine(), out y); // Using boolean error to catch exception.
                        if (error == false || !(y > 0 && y < heightOfMaze))
                        {
                            WriteRed("\nERROR: Please enter a valid choice.\n\n"); // Outputting invalid choice if user enters an error.
                        }
                    } while (error == false || !(y > 0 && y < heightOfMaze));

                    if (maze[x, y] == 'X')
                    {
                        WriteRed("ERROR: Please enter a valid X and Y position which does not contains a wall (X).");
                    }

                } while (maze[x, y] == 'X');
                sr.Close(); // Closing StreamReader at the end.

                // Calls the SearchEnd function to search for the end using the current maze, the starting position x and y, and every other parameter set as their constructor inputs. The SearchEnd function call returns a List of Tuples (int, int) which is assign to the variable "pathToEnd".
                List<Tuple<int, int>> pathToEnd = SearchEnd(maze, x, y, new Stack<Tuple<int, int>>(), new List<Tuple<int, int>>(), "", "", new List<Tuple<int, int>>(), seeOrNot);

                // Compounded If/Else If Statements - If the pathToEnd variable is null
                if (pathToEnd == null)
                {
                    WriteRed("\n\nCANNOT FIND A VALID PATH!\n"); // Output the valid path.
                }
                else // If the pathToEnd variable is not null...
                {
                    if (seeOrNot == true) // If Statement - If seeOrNot is true...
                    {
                        // ...Resetting the cursor position to default to make sure everything is outputted properly
                        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + heightOfMaze);

                    }

                    // Output the solution and prompt, using a double for loop to indicate x and y for the rows and column of the maze.
                    WriteGreen("\n\nThe solution to the path:\n");

                    // Outputting the solution returned from the SearchEnd Function.
                    foreach (var v in pathToEnd)
                    {
                        WriteGreen(v + " ");
                    }
                    Console.Write("\n\n");

                    if (seeOrNot == false)
                    {
                        // For Loop - Which repeats for every value in heightOfMaze
                        for (int i = 0; i < heightOfMaze; i++)
                        {
                            // For Loop - Which repeats for every value in lengthOfMaze
                            for (int c = 0; c < lengthOfMaze; c++)
                            {
                                // Compounded If/Else If Statement - If the pathToEnd contains the current x and y, change the background color to indicate the "Correct Path".
                                if (pathToEnd.Contains(new Tuple<int, int>(c, i)))
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                }
                                else // If Not, reset the color and set the text color to White to represent all other paths and walls.
                                {
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.Write(maze[c, i]); // Output the maze at the current x and y location.

                            }
                            Console.WriteLine(); // Break the line to indicate a new row.
                        }
                    }

                    // Prompting user if they would like to try again. 
                    WriteYellow("\n\nWould you like to try again? (Y - Yes, N - No): ");
                    error = char.TryParse(Console.ReadLine(), out userChoice); // Using boolean error to catch exception.
                    if (error == false || !(char.ToUpperInvariant(userChoice) == 'Y' || char.ToUpperInvariant(userChoice) == 'N')) // If the user entered a invalid choice...
                    {
                        WriteRed("\nERROR: You entered a invalid choice, exiting be default..."); // Outputting invalid choice if user enters an error.
                        goto exit; // goto the exit reference.
                    }
                    else if (char.ToUpperInvariant(userChoice) == 'N')
                    {
                        goto exit; // goto the exit reference.
                    }
                }
            } while (error == false || char.ToUpperInvariant(userChoice) == 'Y');

        exit: ; // The goto position if the user would like to exit at the beginning.

            // Prompts the user to press any key to exit, then exits.
            WriteYellow("\n\nPress any key to exit...\n");
            Console.ReadKey(true);
        }
        // MazeGen Function used to generate the mazes if the user choose to. The parameters are, the jagged maze array to be sent in, the x and y starting positions, the allPaths array which memorizes all the visited x and y coords, the mazeEnd which allows for the maze to have an end, the randomNum allowing the maze to be randomly created, the possibleDirections which memorizes the possible directions, the potentialBranches variable which memorizes the paths which allow for multiple directions to be taken.
        public static char[,] MazeGen(char[,] maze, int x, int y, List<Tuple<int, int>> allPaths, Boolean mazeEnd, Random randomNum, List<int> possibleDirections, Stack<Tuple<int, int>> potentialBranches, Boolean draw)
        {
            // If Statement - If draw is enabled, output the maze.
            if (draw == true)
            {
                // For Loop - Which repeats for every value in heightOfMaze
                for (int p = 0; p < maze.GetLength(0); p++)
                {
                    // For Loop - Which repeats for every value in  lengthOfMaze
                    for (int s = 0; s < maze.GetLength(1); s++)
                    {
                        // If Statement - If the current x (s) and y (p) value is equal to the current x and y values the function is on...
                        if (s == x && p == y)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue; // Change the background color to blue so the user can see what the maze is doing.
                        }
                        else // If Else - If Not...
                        {
                            Console.ResetColor(); // Reset the color
                        }
                        Console.Write(maze[s, p]); // Output the maze at the current x and y location.

                    }
                    Console.WriteLine(); // Break the line to indicate a new row.
                }
                Console.SetCursorPosition(0, Console.CursorTop - (maze.GetLength(0))); // Resetting the cursor position to the top so it overwrites the previous maze output, allowing for a animation of sorts.
                Thread.Sleep(50); // Allowing the thread to sleep for some time so that the user can actually see the maze generating in real time.
            }
            // Base Case If statement - Returns the completed maze if the mazeEnd is set to false and the potentialBranches array has 0 elements, or if allPaths contains atleast as much as mazeLength.
            if ((mazeEnd == false && potentialBranches.Count == 1) || allPaths.Count >= maze.Length)
            {
                return maze;
            }
            else // If not continue...
            {
                // Add the current coordinates to allPath
                allPaths.Add(new Tuple<int, int>(x, y));
                maze[x, y] = ' '; // Mark the current coordinate as a path

                // Independent If Statements - If the paths are navigable, mark them as possible paths to take. 
                if (!(allPaths.Contains(new Tuple<int, int>(x + 2, y))) && !(allPaths.Contains(new Tuple<int, int>(x + 1, y))) && !(allPaths.Contains(new Tuple<int, int>(x + 1, y - 1))) && !(allPaths.Contains(new Tuple<int, int>(x + 1, y + 1))) && x + 1 != maze.GetLength(0) - 1 && maze[x + 1, y] != '$')
                {
                    possibleDirections.Add(1);
                }
                if (!(allPaths.Contains(new Tuple<int, int>(x - 2, y))) && !(allPaths.Contains(new Tuple<int, int>(x - 1, y))) && !(allPaths.Contains(new Tuple<int, int>(x - 1, y - 1))) && !(allPaths.Contains(new Tuple<int, int>(x - 1, y + 1))) && x - 1 != 0 && maze[x - 1, y] != '$')
                {
                    possibleDirections.Add(2);
                }
                if (!(allPaths.Contains(new Tuple<int, int>(x, y + 2))) && !(allPaths.Contains(new Tuple<int, int>(x, y + 1))) && !(allPaths.Contains(new Tuple<int, int>(x + 1, y + 1))) && !(allPaths.Contains(new Tuple<int, int>(x - 1, y + 1))) && y + 1 != maze.GetLength(1) - 1 && maze[x, y + 1] != '$')
                {
                    possibleDirections.Add(3);
                }
                if (!(allPaths.Contains(new Tuple<int, int>(x, y - 2))) && !(allPaths.Contains(new Tuple<int, int>(x, y - 1))) && !(allPaths.Contains(new Tuple<int, int>(x + 1, y - 1))) && !(allPaths.Contains(new Tuple<int, int>(x - 1, y - 1))) && y - 1 != 0 && maze[x, y - 1] != '$')
                {
                    possibleDirections.Add(4);
                }
                // Independent If/Else If Statements - If the possible directions is 1 or more, continue...
                if (possibleDirections.Count > 0)
                {
                    // If Statement - If the possible directions is greater than 1, add the coordinates to the top of potentialBranches stack.
                    if (possibleDirections.Count > 1)
                    {
                        potentialBranches.Push(new Tuple<int, int>(x, y));
                    }
                    // Generate a random variable depending on the number of possible paths to take.
                    int randomDirection = randomNum.Next(0, possibleDirections.Count);

                    // Compounded If/Else If Statements - Go one of the branches at random.
                    if (possibleDirections[randomDirection] == 1)
                    {
                        possibleDirections.Clear();
                        return MazeGen(maze, x + 1, y, allPaths, mazeEnd, randomNum, possibleDirections, potentialBranches, draw);
                    }
                    else if (possibleDirections[randomDirection] == 2)
                    {
                        possibleDirections.Clear();
                        return MazeGen(maze, x - 1, y, allPaths, mazeEnd, randomNum, possibleDirections, potentialBranches, draw);
                    }
                    else if (possibleDirections[randomDirection] == 3)
                    {
                        possibleDirections.Clear();
                        return MazeGen(maze, x, y + 1, allPaths, mazeEnd, randomNum, possibleDirections, potentialBranches, draw);
                    }
                    else
                    {
                        possibleDirections.Clear();
                        return MazeGen(maze, x, y - 1, allPaths, mazeEnd, randomNum, possibleDirections, potentialBranches, draw);
                    }
                }
                // If Statement - If not, and the elements inside potentialBranches is atleast greater than 0, continue...
                if (potentialBranches.Count > 0)
                {
                    // If mazeEnd is true, set the current coordinate to the end '$'and set mazeEnd to false.
                    if (mazeEnd == true)
                    {
                        maze[x, y] = '$';
                        mazeEnd = false;
                    }
                    // Create a temp variable which stores the x and y which is taken from the top of potentialBranches without throwing it out.
                    var temp = potentialBranches.Peek();
                    x = temp.Item1; // Assign the potentialBranches previous x coord to the new x coord.
                    y = temp.Item2; // Assign the potentialBranches previous y coord to the new y coord.

                    int temp2 = 0; // Initializing a temp2 to be used for saving temporary variables to determine if the potentialBranches element at the top of the stack should be removed.

                    // Independent If Statements - If there is a valid path to take in a certain direction, add 1 to temp2 variable.
                    if (allPaths.Contains(new Tuple<int, int>(x + 1, y)) || maze[x + 1, y] != '$' && x + 1 != maze.GetLength(0) - 1)
                    {
                        temp2 = temp2 + 1;
                    }
                    if (allPaths.Contains(new Tuple<int, int>(x - 1, y)) || maze[x - 1, y] != '$' && x - 1 != 0)
                    {
                        temp2 = temp2 + 1;
                    }
                    if (allPaths.Contains(new Tuple<int, int>(x, y + 1)) || maze[x, y + 1] != '$' && y + 1 != maze.GetLength(1) - 1)
                    {
                        temp2 = temp2 + 1;
                    }
                    if (allPaths.Contains(new Tuple<int, int>(x, y - 1)) || maze[x, y - 1] != '$' && y - 1 != 0)
                    {
                        temp2 = temp2 + 1;
                    }
                    // If Statement - If the temporary variable reaches 3 or greater, than remove the top of the stack of potentialBranches.
                    if (temp2 >= 3)
                    {
                        potentialBranches.Pop();
                    }
                    // Go to the previous x and y coordinates saved in the top of potentialBranches.
                    return MazeGen(maze, x, y, allPaths, mazeEnd, randomNum, possibleDirections, potentialBranches, draw);
                }
                else // If not, return maze (Base Case #2)
                {
                    return maze;
                }
            }

        }
        // SearchEnd function which searches for the end of the maze. The parameters are, the jagged array to represent the maze, the x and y starting points, a stack tuple (int, int) to represent the multiPaths you can take, List tuple (int, int) which represents the current path you are taking, the string which represents the turns that can be made, another string to represent the importantTurns that are made.
        static public List<Tuple<int, int>> SearchEnd(char[,] maze, int x, int y, Stack<Tuple<int, int>> multiPath, List<Tuple<int, int>> currentPath, string turns, string importantTurn, List<Tuple<int, int>> deadEnds, Boolean draw)
        {
            if (draw == true)
            {
                // For Loop - Which repeats for every value in heightOfMaze
                for (int p = 0; p < maze.GetLength(1); p++)
                {
                    // For Loop - Which repeats for every value in lengthOfMaze
                    for (int s = 0; s < maze.GetLength(0); s++)
                    {
                        // If Statement - If the pathToEnd contains the current x and y, change the background color to indicate the "Correct Path".
                        if (currentPath.Contains(new Tuple<int, int>(s, p)))
                        {
                            // If Statement - If the current x and y value is the end ("$"), change the background color to dark green.
                            if (maze[x, y] == '$')
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                            }
                            else // Else If - If not, change the background color to dark red.
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                            }
                        }
                        else // Else If - If Not, reset the color and set the text color to White to represent all other paths and walls.
                        {
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(maze[s, p]); // Output the maze at the current x and y location.

                    }
                    Console.WriteLine(); // Break the line to indicate a new row.
                }
                Console.SetCursorPosition(0, Console.CursorTop - (maze.GetLength(1))); // Resetting the cursor position to the top so it overwrites the previous maze output, allowing for a animation of sorts.
                Thread.Sleep(150); // Allowing the thread to sleep for some time so that the user can actually see the maze generating in real time.
            }
            // Add the current x and y coordinates to currentPath.
            currentPath.Add(new Tuple<int, int>(x, y));

            // Base Case: If Statement - If the maze at the current coords (x and y) is $, return the currentPath.
            if (maze[x, y] == '$')
            {
                return currentPath;
            }
            else // If not, continue...
            {
                // Independent If Statements - If the current path is navigable, save it as a possible turn.
                if (maze[x + 1, y] != 'X' && !(currentPath.Contains(new Tuple<int, int>(x + 1, y))) && !(deadEnds.Contains(new Tuple<int, int>(x + 1, y))) && x + 1 != maze.GetLength(0) - 1)
                {
                    turns = turns + "R";
                }
                if (maze[x - 1, y] != 'X' && !(currentPath.Contains(new Tuple<int, int>(x - 1, y))) && !(deadEnds.Contains(new Tuple<int, int>(x - 1, y))) && x - 1 != 0)
                {
                    turns = turns + "L";
                }
                if (maze[x, y + 1] != 'X' && !(currentPath.Contains(new Tuple<int, int>(x, y + 1))) && !(deadEnds.Contains(new Tuple<int, int>(x, y + 1))) && y + 1 != maze.GetLength(1) - 1)
                {
                    turns = turns + "D";
                }
                if (maze[x, y - 1] != 'X' && !(currentPath.Contains(new Tuple<int, int>(x, y - 1))) && !(deadEnds.Contains(new Tuple<int, int>(x, y - 1))) && y - 1 != 0)
                {
                    turns = turns + "U";
                }

                // Compounded If Statements - If there is only one possible turn, continue through it.
                if (turns.Length == 1)
                {
                    if (turns.Contains("R"))
                    {
                        return SearchEnd(maze, x + 1, y, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                    else if (turns.Contains("L"))
                    {
                        return SearchEnd(maze, x - 1, y, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                    else if (turns.Contains("D"))
                    {
                        return SearchEnd(maze, x, y + 1, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                    else
                    {
                        return SearchEnd(maze, x, y - 1, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }

                }
                else if (turns.Length > 1) // Else If Statement - If there is multiple possible turns, choose one in a unimportant order and save the turn as a important turn. 
                {
                    // Add push the current x and y coords to multiPath to refer to them, if the continued path is a dead end.
                    multiPath.Push(new Tuple<int, int>(x, y));

                    // Compounded If/Else If Statements - If the "turns" variable represents a certain direction, save the "importantTurn" variable to that specific diraction and go to that specific x and y coordinate (recursion).
                    if (turns.Contains("R"))
                    {
                        importantTurn = "R";
                        return SearchEnd(maze, x + 1, y, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                    else if (turns.Contains("L"))
                    {
                        importantTurn = "L";
                        return SearchEnd(maze, x - 1, y, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                    else if (turns.Contains("D"))
                    {
                        importantTurn = "D";
                        return SearchEnd(maze, x, y + 1, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                    else
                    {
                        importantTurn = "U";
                        return SearchEnd(maze, x, y - 1, multiPath, currentPath, "", importantTurn, deadEnds, draw);
                    }
                }
                else if (turns.Length == 0 || multiPath.Count > 0) // Else If Statements - If there is no possible turns to take, continue...
                {
                    // Take the top stack off multiPath and assign it to a temp variable
                    var temp = multiPath.Peek();
                    x = temp.Item1; // Take the temp variables x coord and assign it to current x position
                    y = temp.Item2; // Take the temp variables y coord and assign it to current y position
                    int temp2 = 0; // Initalize integer variable "temp2" to represent the index of temp in currentPath.

                    // For Loop - Statement which repeats for every element in currentPath, checking if currentPath at a specific index is equal to temp, if it is, break and save the temp2 as that index.
                    for (int i = 0; i < currentPath.Count; i++)
                    {
                        if (currentPath[i].Equals(temp))
                        {
                            temp2 = i;
                            break;
                        }
                    }

                    // Remove the contents of currentPath from the temp2 index to last variable in index.
                    currentPath.RemoveRange(temp2, (currentPath.Count) - temp2);

                    int temp3 = 0; // Initializing a temp3 to be used for saving temporary variables to determine if the multiPath element at the top of the stack should be removed.

                    // Independent If Statements - If there is a valid path to take in a certain direction, add 1 to temp3 variable.
                    if ((deadEnds.Contains(new Tuple<int, int>(x + 1, y)) || maze[x + 1, y] == 'X') || (currentPath.Contains(new Tuple<int, int>(x + 1, y))))
                    {
                        temp3 = temp3 + 1;
                    }
                    if ((deadEnds.Contains(new Tuple<int, int>(x - 1, y)) || maze[x - 1, y] == 'X') || (currentPath.Contains(new Tuple<int, int>(x - 1, y))))
                    {
                        temp3 = temp3 + 1;
                    }
                    if ((deadEnds.Contains(new Tuple<int, int>(x, y + 1)) || maze[x, y + 1] == 'X') || (currentPath.Contains(new Tuple<int, int>(x, y + 1))))
                    {
                        temp3 = temp3 + 1;
                    }
                    if ((deadEnds.Contains(new Tuple<int, int>(x, y - 1)) || maze[x, y - 1] == 'X') || (currentPath.Contains(new Tuple<int, int>(x, y - 1))))
                    {
                        temp3 = temp3 + 1;
                    }

                    // If Statement - If the temporary variable reaches 3 or greater, than remove the top of the stack of multiPath.
                    if (temp3 >= 3)
                    {
                        multiPath.Pop();
                    }
                    // Compunded If Statements - If the important turn is assigned a specific turn, then change the x and y accordingly to that turn
                    if (importantTurn == "R")
                    {
                        x = x + 1;
                    }
                    else if (importantTurn == "L")
                    {
                        x = x - 1;
                    }
                    else if (importantTurn == "D")
                    {
                        y = y + 1;
                    }
                    else if (importantTurn == "U")
                    {
                        y = y - 1;
                    }
                    deadEnds.Add(new Tuple<int, int>(x, y)); // Add that point to deadEnds list of tuples (int, int)
                    x = temp.Item1; // Change the x variable back to the inital x value at the top of stack "multiPath"
                    y = temp.Item2; // Change the y variable back to the inital y value at the top of stack "multiPath"

                    // Call itself back at the previous temp position which was at the top of mulitPath.
                    return SearchEnd(maze, x, y, multiPath, currentPath, "", "", deadEnds, draw);
                }
                else // If no other choices are available, return null, as the end is unreachable (Base Case #2).
                {
                    return null;
                }
            }
        }



    }
}
