using UnityEngine;

public class Hacker : MonoBehaviour {
    // Use this for initialization

    enum Screen {  MainMenu, easy, medium, hard, WinScreen};
    Screen currentScreen;
    string directory;
    string currentPassword;

	void Start () {
        showMainMenu();
	}

    void showMainMenu()
    {
        Terminal.ClearScreen();
        directory = "";
        currentScreen = Screen.MainMenu;
        terminalLine();
    }
    void terminalLine()
    {
        Terminal.WriteLine("hackerman@hackerlaptop:~" + directory + "$");
    }

    void OnUserInput(string input)
    {
        if (input == "help")
        {
            Terminal.WriteLine("ls                  -list directories");
            Terminal.WriteLine("cd <directory name> -change directory");
            Terminal.WriteLine("cd                  -return to home");    
            Terminal.WriteLine("clr                 -clear terminal");
            Terminal.WriteLine("q                   -quit");
            terminalLine();
        }
        else if (input == "cd")
        {
            showMainMenu();
        }
        else if (input == "clr")
        {
            Terminal.ClearScreen();
            terminalLine();
        }
        else if (input == "q")
        {
            Terminal.WriteLine("goodbye");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.easy || currentScreen == Screen.medium || currentScreen == Screen.hard)
        {
            checkPassword(input);
        }
        else if (currentScreen == Screen.WinScreen && input == "ls")
        {
            Terminal.WriteLine("\n");
            terminalLine();
        }
    }

    private void RunMainMenu(string input)
    {
        if (input == "ls")
        {
            Terminal.WriteLine("hack_dmv");
            Terminal.WriteLine("hack_bank");
            Terminal.WriteLine("hack_cia");
            terminalLine();
        }
        else if (input == "cd hack_dmv")
        {
            directory = "hack_dmv";
            currentScreen = Screen.easy;
            StartGame();
        }
        else if (input == "cd hack_bank")
        {
            directory = "hack_bank";
            currentScreen = Screen.medium;
            StartGame();
        }
        else if (input == "cd hack_cia")
        {
            directory = "hack_cia";
            currentScreen = Screen.hard;
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Command not recognized.");
            Terminal.WriteLine("Enter help for valid commands.");
            terminalLine();
        }
    }

    void StartGame()
    {
        if(currentScreen == Screen.easy)
        {
            string[] passwords = new string[] { "drive", "line", "clerk", "plates", "title" };
            currentPassword = passwords[Random.Range(0, 5)];
            Terminal.WriteLine("Initializing DMV hacking...");
            Terminal.WriteLine("DMV hacking initialized...");
            Terminal.WriteLine("Please unscramble the password:");
            Terminal.WriteLine(currentPassword.Anagram());
            terminalLine();
        }
        else if(currentScreen == Screen.medium)
        {
            string[] passwords = new string[] { "deposit", "withdraw", "transaction", "savings", "bankrupt" };
            currentPassword = passwords[Random.Range(0, 5)];
            Terminal.WriteLine("Initializing bank hacking...");
            Terminal.WriteLine("bank hacking initialized...");
            Terminal.WriteLine("Please unscramble the password:");
            Terminal.WriteLine(currentPassword.Anagram());
            terminalLine();
        }
        else if(currentScreen == Screen.hard)
        {
            string[] passwords = new string[] { "government", "surveillance", "clandestine", "analysis", "terrorism" };
            currentPassword = passwords[Random.Range(0, 5)];
            Terminal.WriteLine("Initializing CIA hacking...");
            Terminal.WriteLine("CIA hacking initialized...");
            Terminal.WriteLine("Please unscramble the password:");
            Terminal.WriteLine(currentPassword.Anagram());
            terminalLine();
        }
    }

    void checkPassword(string input)
    {
        if(input == "ls")
        {
            Terminal.WriteLine("\n");
            terminalLine();
        }
        else if(currentScreen == Screen.easy && input == currentPassword)
        {
            Terminal.WriteLine("Successfully hacked into the DMV!");
            Terminal.WriteLine("Saving stolen data to fake_licenses.txt...");
            currentScreen = Screen.WinScreen;
            terminalLine();
        }
        else if (currentScreen == Screen.medium && input == currentPassword)
        {
            Terminal.WriteLine("Successfully hacked into the Bank!");
            Terminal.WriteLine("Saving stolen data to accounts.txt...");
            currentScreen = Screen.WinScreen;
            terminalLine();
        }
        else if (currentScreen == Screen.hard && input == currentPassword)
        {

            Terminal.WriteLine("Successfully hacked into the CIA!");
            Terminal.WriteLine("Saving stolen data to aliens.txt...");
            currentScreen = Screen.WinScreen;
            terminalLine();
        }
        else
        {
            Terminal.WriteLine("Access denied...");
            Terminal.WriteLine("Resetting hack...");
            StartGame();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
