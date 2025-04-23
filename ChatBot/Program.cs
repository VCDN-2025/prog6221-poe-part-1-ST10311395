using System;
using System.Collections.Generic;
using System.Media;
using System.IO;
using System.Threading;
using Figgle;

class Program
{
    // Dictionary: Stores user questions and corresponding CyberGuardian responses
    static Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "how are you", "I'm a bot, but I'm fully operational and ready to help you stay safe online!" },
        { "what's your purpose", "My mission is to educate and empower you with cybersecurity knowledge." },
        { "what can i ask you about", "You can ask me about password safety, phishing, safe browsing, and general cybersecurity tips." },
        { "password safety", "Use long, unique passwords for each account. Consider using a password manager." },
        { "phishing", "Be cautious of emails asking for personal info. Always verify links before clicking." },
        { "safe browsing", "Avoid clicking unknown links, use HTTPS websites, and keep your browser up to date." }
    };

    static void Main()
    {
        try
        {
            // SECTION: Introduction
            DrawBorder("CyberGuardian");
            PlayAudio(@"C:\Users\Katelyn Narain\source\repos\ChatBot\Music\ElevenLabs_2025-04-23T15_55_15_Rachel_pre_sp100_s50_sb75_se0_b_m2.wav");

            // SECTION: User Setup
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();

            ShowWelcomeMessage(userName);

            // SECTION: Chat
            StartChat(userName);

            // SECTION: Goodbye
            Console.ForegroundColor = ConsoleColor.Green;
            Divider("Session Ended");
            TypeText($"\nThank you for using CyberGuardian, {userName}!");
            TypeText("Stay safe, stay smart. Press Enter to exit.");
            Console.ResetColor();
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            DisplayError("An unexpected error occurred: " + ex.Message);
        }
    }

    // Draws ASCII art header with stylized border
    static void DrawBorder(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;

        string asciiArt = FiggleFonts.Slant.Render(title);
        int width = asciiArt.Split('\n')[0].Length;

        Console.WriteLine(new string('═', width));
        Console.WriteLine(asciiArt);
        Console.WriteLine(new string('═', width));
        Console.WriteLine("      [:: Your AI Guide to Cybersecurity ::]");
        Console.WriteLine(new string('═', width));
        Console.ResetColor();
    }

    // Plays welcome audio file
    static void PlayAudio(string soundFilePath)
    {
        try
        {
            if (!File.Exists(soundFilePath))
            {
                DisplayError("Audio file not found: " + soundFilePath);
                return;
            }

            using (SoundPlayer player = new SoundPlayer(soundFilePath))
            {
                player.Load();
                player.PlaySync();
            }
        }
        catch (Exception ex)
        {
            DisplayError("Audio could not be played: " + ex.Message);
        }
    }

    // Greets the user with a bordered message box
    static void ShowWelcomeMessage(string userName)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        string[] lines = new string[]
        {
            $"Welcome, {userName}!",
            "I'm CyberGuardian, your cybersecurity companion.",
            "Ask me anything about staying safe online."
        };

        int maxLen = 0;
        foreach (string line in lines)
            if (line.Length > maxLen)
                maxLen = line.Length;

        string border = new string('*', maxLen + 6);
        Console.WriteLine("\n" + border);
        foreach (string line in lines)
        {
            Console.Write("*  ");
            TypeText(line.PadRight(maxLen), 15);
            Console.WriteLine("  *");
        }
        Console.WriteLine(border);
        Console.ResetColor();
    }

    // Begins the Q&A session with user
    static void StartChat(string userName)
    {
        Divider("Chat Help");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("You can ask about:");
        Console.WriteLine(" - Password safety");
        Console.WriteLine(" - Phishing");
        Console.WriteLine(" - Safe browsing");
        Console.WriteLine(" - How I am");
        Console.WriteLine(" - What is my purpose");
        Console.WriteLine(" - What can I ask you about");
        Console.WriteLine("Type 'exit' to leave the chat.");
        Console.WriteLine("------------------------------\n");
        Console.ResetColor();

        while (true)
        {
            Console.WriteLine(); // Adds space before each prompt
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{userName}: ");
            string input = Console.ReadLine().Trim();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                break;

            if (string.IsNullOrWhiteSpace(input))
            {
                DisplayError("I didn't quite understand that. Could you rephrase?");
                continue;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            if (responses.TryGetValue(input.ToLower(), out string answer))
            {
                TypeText($"CyberGuardian: {answer}");
            }
            else
            {
                TypeText("CyberGuardian: I'm still learning! Try asking about cybersecurity topics like phishing or password safety.");
            }
            Console.ResetColor();
        }
    }

    // Typing effect for natural response output
    static void TypeText(string text, int delay = 30)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }

    // Error output in red
    static void DisplayError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    // Divider with title for visual structure
    static void Divider(string title)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        string line = new string('-', 40);
        Console.WriteLine($"\n{line}");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"[ {title} ]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{line}\n");
        Console.ResetColor();
    }
}
