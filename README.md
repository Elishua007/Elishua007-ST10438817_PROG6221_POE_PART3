**BRIEF DESCRIPTION**


JARVIS is a GUI-based cybersecurity awareness chatbot built in C# using Windows Forms (WinForms) and XAML. JARVIS is a friendly, 
interactive, conversational chatbot, designed to educate users on cybersecurity topics like password safety, phishing, and privacy 
settings. To make this bot feel user-friendly to interact with, I have added personalization, console styling, and even voice interaction. 
JARVIS with new GUI components, a cybersecurity quiz, task assistant, and basic natural language processing (NLP) capabilities.



**FEATURES**


•	Conversational GUI with stylized bot and user dialogues.
•	Personalizes interaction using user’s name.
•	Voice greeting using .wav audio playback (via NAudio).
•	Sentiment detection, chatbot can adjust its responses based on the user’s sentiment to create an empathetic interaction.
•	Chatbot can recognise keywords and generate random responses for each keyword.
•	Memory recall for favourite topics and past interactions.
•	Code is modular consisting of 6 core classes. 
•	GUI built using XAML and WPF controls.
•	Answers main topic questions about:
o	Password Security
o	Phishing
o	Privacy Settings
•	ASCII art logo on startup and exit.
•	Graphical User Interface (GUI) built using Windows Forms and XAML.
•	Add, delete, and complete cybersecurity tasks.
•	Set reminders with time intervals.
•	GUI panel for viewing, managing, and tracking tasks.
•	Cybersecurity Mini-Game (Quiz) with 15 questions (including 5 multiple choice and long question), score tracking, and feedback.
•	Chatbot uses FuzzySharp package to check the user’s answer against the correct answer and then provides feedback to the user. 
The answer must meet a certain percentage compared to the correct/actual answer.
•	Logs and displays key events like tasks added, reminders set, and quiz attempts.
•	GUI button to view the latest activity log entries.
•	Summarized display for recent chatbot-user interactions.
•	Natural Language Processing (NLP) using basic keyword matching and string manipulation.



**HOW TO SETUP PROJECT**


1.	Prerequisites
•	Visual Studio 2022
•	C# (.NET Framework + WPF(XAML))
•	NAudio (NuGet Package)
•	FuzzySharp (NuGet Package)

2.	Cloning the Project
Clone from GitHub or download and unzip the folder.

3.	Project Structure
Ensure the following files are present:
o	ChatBot_Activity_Log.cs
o	UserDetails.cs
o	ChatBot_Dialogue.cs
o	DataDictionary.cs
o	ChatBot_Characteristics.cs
o	ChatBot_Logo.cs
o	ChatBot_Quiz.cs
o	ChatBot_Task.cs
o	MainWindow.xaml & MainWindow.xaml.cs
o	App.xaml & App.xaml.cs
o	JARVIS.wav audio file
o	ST10438817_POE_PART3_CHATBOT.csproj

4.	Install NuGet Packages
Open the NuGet Package Manager Console and install Package NAudio. Then install FuzzySharp. Chatbot uses
FuzzySharp package to check the user’s answer against the correct answer and then provides feedback to
the user. The answer must meet a certain percentage compared to the correct/actual answer.


6.	Run the Program
Build and run the project from Visual Studio. Interact with the chatbot through the GUI.



**FLOW OF ACTIVITIES**


1.	Startup Sequence
•	The application launches with an ASCII logo display.
•	A voice greeting plays (e.g., “Hello! Welcome to the Cybersecurity Awareness Bot.”).
•	The chatbot asks for your name to personalize the session.

2.	Main Menu Options (GUI Interface)
•	You are presented with buttons or selectable options in the GUI:
o	Start Conversing with Chatbot
o	Cybersecurity Quiz
o	Task Assistant
o	View Activity Log
o	Exit (say “goodbye”)
•	These activities can be done in any order. User must navigate correctly between Chatbot, Task Assistant, Quiz and Activity Log.

3.	Chatbot Interaction
•	You can type a message or question in the chatbot input box.
•	The chatbot uses keyword recognition and basic NLP to determine the context.
•	It responds with stylized and personalised messages.
•	If your input shows emotion, it uses sentiment detection to adapt its tone and response.
•	Examples of accepted topics:
o	“Tell me about phishing.”
o	“How do I make a strong password?”
o	“What is your purpose?”

4.	Task Assistant (GUI)
•	Add a new task (“Enable two-factor authentication”).
•	Specify a reminder timeframe (“2025-06-31 19:00”).
•	View the list of pending tasks.
•	Shows the status of a task.
•	The ability to update task details.
•	Mark tasks as completed or delete them.


5.	Cybersecurity Quiz
•	Launch the quiz with 15 questions.
•	Questions include multiple choice and long questions.
•	Receive instant feedback for each answer.
•	At the end, view your score and performance summary.
•	View other quiz score attempts.

6.	View Activity Log
•	Click on “Activity Log” to see:
o	Tasks added, updated or deleted
o	Reminders set
o	Quiz attempts
•	Shows 5 most recent actions in a clear list format.

7.	Exit
Upon the user saying “goodbye” (or anything like “goodbye”) to the chatbot, a final ASCII art logo is shown.



**REQUIRED PACKAGES**
Install the NuGet Package, from the NuGet Packages.  NAudio is used to play audio file within the chatbot. 
This is how voice integration is incorporated into the project. Install FuzzySharp, from the NuGet Packages. 
Chatbot uses FuzzySharp package to check the user’s answer against the correct answer. The answer must meet 
a certain percentage compared to the correct/actual answer.



**DEVELOPER INFORMATION**
DEVELOPER NAME: Elishua Emmanuel Naidoo
DEVELOPER STUDENT NUMBER: ST10438817
INSTITUTION: The IIE Varsity College, Westville
COURSE: PROG6221 – Programming 2A
IDE: Visual Studio 2022
LANGUAGE: C# (.NET Framework)
UI FRAMEWORK: WPF (XAML)
YOUTUBE LINK: https://youtu.be/CDPFyJvXAmU
