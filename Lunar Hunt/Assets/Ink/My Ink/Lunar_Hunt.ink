VAR current_chapter = -> Cutscene_Welcome_to_Sanctuary
VAR current_speaker = ""
VAR current_conversation = ""
VAR current_location = -> Cutscene_Welcome_to_Sanctuary
 
VAR CurrentItem = ()
VAR CountList = 0
VAR MaxList = 0
 
LIST ItemList = Original_Newspaper
 
LIST ClueList = News_about_Moving_Out_People, Visitors_do_not_Checkout, Merchant_Sells_the_Newspaper, CS_Order_to_Forge_The_News, CS_Sells_the_potion, This_Potion_Improve_Health, The_Potion_is_just_Colored_Water, Cassandra_Prophesied_The_Plague, No_Record_of_Recent_Plague, CS_letters

LIST EvidenceList = CS_Faked_The_News
 
LIST KeyList = Empty
 
//Location CONST list
CONST Menu = 0
CONST Forest = 1
CONST Sanctuary = 2
CONST Meeting_Hall = 3
CONST Nursery = 4
CONST Inside_Nursery = 5 

VAR Player_Location = Menu
VAR LocationName = ""
 
//Main Menu
-> Main_Menu
 
// -- functions list --
	== function StartListHave(ref list) ==
		~ CurrentItem = LIST_MIN(list)
		~ CountList = 1
		~ MaxList = LIST_COUNT(list)
		~ ListHave(list)
	   
	== function ListHave(ref list) ==
		{ CountList <= MaxList:
			~ Listing(list)
		- else:
			~ return
		}
	   
	== function Listing(ref list) ==
		{ list ? CurrentItem:
			- {CurrentItem}
		}
		~ CurrentItem++
		~ CountList++
		~ ListHave(list)
	   
	== function Speaker(Name) ==
		~ current_speaker = Name
		~ temp text = ""
		{ Name == "Narration":
			~ text = "NARRATION"
		- else:
			~ text = current_speaker
		}
		- {text}: <>
		   
	== function Conversation(Name) ==
		~ current_conversation = Name
		- \-- {Name} --
 
	== function ChangeLocation(Name) ==
		~ Player_Location = Name
 
	== Location ==
		{ Player_Location:
			- Forest:
				~ LocationName = "The Forest"
				<- Location_Forest
			- Sanctuary:
				~ LocationName = "The Sanctuary"
				<- Location_The_Sanctuary
			- Meeting_Hall:
				~ LocationName = "The Meeting Hall"
				<- Location_Meeting_Hall
			- Nursery:
				~ LocationName = "The Nursery"
				<- Location_Nursery
			- Inside_Nursery:
				~ LocationName = "Inside Nursery"
				<- Location_Inside_Nursery
			- else:
				<- Main_Menu
		}
		- -> DONE
	== function Location_Name ==
		{ Player_Location:
			- Forest:
				~ LocationName = "The Forest"
			- Sanctuary:
				~ LocationName = "The Sanctuary"
			- Meeting_Hall:
				~ LocationName = "The Meeting Hall"
			- Nursery:
				~ LocationName = "The Nursery"
			- Inside_Nursery:
				~ LocationName = "Inside Nursery"
			- else:
				~ LocationName = ""
		}
		- LOCATION: {LocationName}
 
	== function ChapterProgress(Name) ==
		~ current_chapter = Name
 
	== function EndCon ==
		~ current_conversation = ""
		~ current_speaker = ""
	   
	== function GetClue(clue) ==
		~ ClueList += clue
		- \- Collected Clue: {clue} -
	== function GetItem(item) ==
		~ ItemList += item
		- \- Collected Item: {item} -
	== function GetEvidence(evidence) ==
		~ EvidenceList += evidence
		- \- Collected Evidence: {evidence} -
	   
// --- Main Menu Stuff ---
	==== Main_Menu ====
		\--- Lunar Hunt ---
			+ [Start Game] -> Progress_Setup.Fresh_Start -> current_chapter
			+ [Bookmark] -> Bookmark ->
			+ [About] -> About ->
			- -> Main_Menu
 
	== About ==
		\-- About --
			Welcome to the Lunar Hunt's script writing prototype.
			This is made by using Inky editor which helps with writing stories of choices while also helps with providing information for the game development side.
			I intend to use this as a way to test out my stories before using it in my game made in Unity Engine.
		- ->->
 
	== Bookmark ==
		\-- Bookmark --
		Click to jump into different part of story progress.
			+ [Start Chapter 1] -> Progress_Setup.Fresh_Start -> Cutscene_Welcome_to_Sanctuary
			+ [Cutscene Meeting in the Meeting Hall] -> Progress_Setup.Fresh_Start -> Cutscene_Meeting_in_the_Meeting_Hall
			+ [Investigate Meeting Hall] -> Progress_Setup.Fresh_Start -> Investigate
			+ [Cutscene The Witch Nursery] -> Progress_Setup.After_Meeting_Hall -> Cutscene_The_Witch_Nursery
			+ [Investigate The Sanctuary] -> Progress_Setup.After_Meeting_Hall -> Cutscene_The_Witch_Nursery.skip
			+ [Start Chapter 2] -> Progress_Setup.Start_Chapter_2 -> Cutscene_CS_Potion
			+ [Cutscene Cassandra Return] -> Cutscene_Cassandra_Return
			+ [Start Chapter 3] -> The_Reveal
			+ [Back] ->->
		   
	== Progress_Setup ==
		= Fresh_Start
			~ ItemList -= ItemList
			~ ClueList -= ClueList
			~ EvidenceList -= EvidenceList
			~ KeyList -= KeyList
			~ ChangeLocation(Meeting_Hall)
			- ->->
		= After_Meeting_Hall
			<- Fresh_Start
			~ ClueList += News_about_Moving_Out_People
			# clue.News_about_Moving_Out_People
			- -> DONE
		= Start_Chapter_2
			<- After_Meeting_Hall
			~ ClueList += (Visitors_do_not_Checkout, Merchant_Sells_the_Newspaper, CS_Order_to_Forge_The_News)
			~ EvidenceList += CS_Faked_The_News
			# clue.Visitors_do_not_Checkout
			# clue.Merchant_Sells_the_Newspaper
			# clue.CS_Order_to_Forge_The_News
			# evidence.CS_Faked_The_News
			- -> DONE
// ---- Investigation Control ----
	==== Investigate ====
		<- Notification
		\--- investigate ---
		~ Location_Name()
		<- Ingame_Menu_Choice
		<- Location
		- -> DONE
 
	== Notification ==
		// { LIST_COUNT(ClueList) == LIST_COUNT(LIST_ALL(ClueList)):
		// 	\!! Notification !!
		// 	Every clues are collected for this prototype. There maybe no other scenario left to read
		// }
		// { LIST_COUNT(ClueList) == LIST_COUNT(LIST_ALL(ClueList)):
		{ LIST_COUNT(ClueList) == 4:
			{ EvidenceList !? CS_Faked_The_News:
			\-- All clues --
				~ Speaker("Sebastian")
			I think that's all the clues I can find for now.
			Let's see if I can make use of them
				~ Speaker("Narration")
			At this point, player must select the correct clues and merge them.
			This prototype can possibly implement it but it would take too long and won't be useful for the real prototype.
			Here's the evidence for now.
				# evidence.CS_Faked_The_News
				~ GetEvidence(CS_Faked_The_News)
			-> Cutscene_CS_Potion
			}
		}
		- ->DONE
 
	== Ingame_Menu_Choice ==
		+ [In-Game Menu] -> Ingame_Menu -> Investigate
		- ->DONE
	== Ingame_Menu ==
		\-- In-game Menu --
		// + [Check Inventory] -> Check_Inventory -> Ingame_Menu
		+ [Check Inventory] -> Key_Item -> Ingame_Menu
		+ [Form Evidence] -> Form_Evidence -> Ingame_Menu
		+ [Back to Main Menu]
			Are you sure?
			++ [Yes] -> Main_Menu
			++ [No] -> Ingame_Menu
		+ [Close Menu] ->->
	   
	== Check_Inventory ==
		\-- Check Inventory --
			~ Speaker("Narration")
		Checking Inventory
		+ [Key Item List] -> Key_Item -> Check_Inventory
		+ [Close Inventory] ->->
 
	== Key_Item ==
		//\-- Key Item --
		\-- Check Inventory --
		\- Items -
		~ StartListHave(ItemList)
		\- Clues -
		~ StartListHave(ClueList)
		//{LIST_ALL(ClueList)}
		\- Evidences -
		~ StartListHave(EvidenceList)
		- ->->
	   
	== Form_Evidence ==
		\-- Form Evidence --
		\- Clues -
		~ StartListHave(ClueList)
		// + {EvidenceList ? CS_Faked_The_News}
		// 	[Use all 4 clues] -> 
		+ [Back to Inventory] ->->
 
// --- Cutscenes list ---
	=== Cutscene_Welcome_to_Sanctuary ===
		\--- Welcome to Sanctuary ---
			~ Speaker("Narration")
		While entering the entrance of the Sanctuary town. Sebastian was wondering for himself.
			~ Speaker("Sebastian")
		(Now that I got here, I should ask if someone knows about missing people.)
		(That way, I should be able to find a lead about my father.)
			~ Speaker("Narration")
		Athena, who was re-painting the town sign, noticed Sebastian.
			~ Speaker("Athena")
		Hello there, you seem to be new here, do you need any help?
			~ Speaker("Sebastian")
		Well, yes, I'm looking for a place to know more about something.
			~ Speaker("Athena")
		What happened?
			~ Speaker("Sebastian")
		I was traveling with my father but he got kidnapped by someone.
			~ Speaker("Athena")
		Oh dear… I'm so sorry to hear that…
			~ Speaker("Sebastian")
		No, don't be, father is a strong willed man. I believe he's still out there somewhere.
			~ Speaker("Athena")
		Well, that's good to know.
		I'll help you however I can. I'll make sure you can find your father.
		If you're looking for leads, the town's meeting hall can help you. There are people who like to tell stories, town board, and also some rooms for visitors to rest.
			~ Speaker("Sebastian")
		Thank you, I'll keep that in mind. I'll be sure to go ther-
			~ Speaker("Athena")
		We can go there right now let's go!
			~ Speaker("Sebastian")
		(She's so eager to help)
		(But I guess it's the first place to go anyway)
			~ Speaker("Narration")
		Athena leads the way to the town meeting hall and Sebastian follows behind her.
		They arrived at the front of the town's meeting hall.
			~ Speaker("Athena")
		Here it is! The meeting hall!
			~ Speaker("Sebastian")
		Thank you, I hope I can find my father soon.
			~ Speaker("Athena")
		I'm sure you can!
			~ Speaker("Narration")
		Athena walked away to the east.
		Sebastian walks up to the door and go inside the town's meeting hall.
		-> Cutscene_Meeting_in_the_Meeting_Hall
	   
	=== Cutscene_Meeting_in_the_Meeting_Hall ===
		\--- Meeting in the Meeting Hall ---
			~ Speaker("Narration")
		Sebastian walked in from the south door.
		//part 1
			~ Speaker("Sebastian")
		(So this is the town's meeting hall. It is well tended here)
			~ Speaker("Cassandra")
		Here's the correct Headline
			~ Speaker("Hall Staff")
		Yes ma'am, it will be done.
			~ Speaker("Narration")
		Cassandra walks away from the staff to the door in the south.
		Cassandra noticed Sebastian.
		//part 2
			~ Speaker("Cassandra")
		Oh! Welcome! You look new here.
			~ Speaker("Sebastian")
		Yes, I'm new here.
			~ Speaker("Cassandra")
		You also looked young, are you traveling alone?
			~ Speaker("Sebastian")
		No, I'm here to find my missing father.
		//This scene may need more dialogues or a simpler, shorter talk.
			~ Speaker("Cassandra")
		Well, feel free to look around here. If you need a room you can ask the staff here.
			~ Speaker("Sebastian")
		I'll keep that in mind
			~ Speaker("Narration")
		Cassandra exited the hall.
		//part 3
			~ Speaker("Sebastian")
		(Alright, it's time to look around for some leads here)
		(I should ask the Staff here)
		//  -> Investigate_Meeting_Hall
		~ ChangeLocation(Meeting_Hall)
		-> Investigate
 
	=== Cutscene_The_Witch_Nursery ===
	    \--- Cutscene The Witch Nursery ---
		//part 1
			~ Speaker("Sebastian")
		(I should keep going now)
			~ Speaker("Athena")
		Oh! Hi again! Did you find something helpful?
			~ Speaker("Sebastian")
		Oh, hi. I did find something useful. Thank you.
			~ Speaker("Athena")
		I'm just glad to help.
		Oh, I don't think we know each other's names yet. I'm Athena. What's your name?
			~ Speaker("Sebastian")
		Sebastian.
			~ Speaker("Athena")
		That's a nice name!
			~ Speaker("Sebastian")
		Thanks
			~ Speaker("Athena")
		Alright! Sebastian, If you need any other help from me you can find me at the Witch's nursery.
			~ Speaker("Sebastian")
		(Witch?)
			~ Speaker("Athena")
		Don't worry. She's a good witch! She raised me when I was gravely ill.
		And she also runs this town too! Everyone trusts her and we all do our best to help her!
			~ Speaker("Sebastian")
		I see, I'll be sure to visit the nursery later-
			~ Speaker("Athena")
		Oh wait, I didn't show you the place yet. It's over here!
			~ Speaker("Sebastian")
		Wait, it's fine I don't need to see it now- hey!
			~ Speaker("Narration")
		Athena dragged Sebastian to the Witch's Nursery.
		//part 2
			~ Speaker("Athena")
		Here it is! The Witch's Nursery!
		We also call it the Nursery for short.
			~ Speaker("Sebastian")
		There are children here, they seem to be enjoying themselves.
			~ Speaker("Athena")
		Yeah, I helped her take care of them too.
		Just like you, their parents were also missing so we took them in.
			~ Speaker("Sebastian")
		I see… Is it possible that I can ask them about it?
			~ Speaker("Athena")
		Hmm… It's not an easy thing to ask. But I'll  ask them for you when they're ready.
			~ Speaker("Sebastian")
		… Thanks, sorry if it's too much.
		I just wanted to find him.
			~ Speaker("Athena")
		It's fine! I'm sure they'll understand.
			~ Speaker("Sebastian")
		Anyway, thanks for showing me around, now I should get going and find my father.
			~ Speaker("Athena")
		Oh! Right! Sorry if I bothered you too much.
		Now I should help Ms. Cassandra too. And I hope you can find your father soon!
			~ Speaker("Sebastian")
		Thank you.
			~ Speaker("Narration")
		Athena walked into the Witch's Nursery
		//part 3
			~ Speaker("Sebastian")
		(Now I can really start searching for clues)
		~ ChangeLocation(Sanctuary)
		-> Investigate
	   
		= skip
		~ ChangeLocation(Sanctuary)
		- -> Investigate
	 
	=== Cutscene_CS_Potion ===
		\--- Cutscene C.S. Potion ---
		//part 1
		    ~ Speaker("Sebastian")
	    (Now I need to find this someone known as C.S.)
		(I should go back to the town again)
			~ Speaker("Narration")
		Sebastian went back inside the town and as he was walking by the shopping stalls...
		He saw a senile old man putting coins inside his mailbox.
		//part 2
			~ Speaker("Sebastian")
		(Did he just put some coins inside his mailbox?)
		(Is this some kind of this town's culture custom? No, I think this might be a scheme.)
			~ Speaker("Senile old man")
		Oh, young lad, I just put some coins in there to buy some potion.
			~ Speaker("Sebastian")
		Really? How did it all started?
			~ Speaker("Senile old man")
		Oh, one day, there was a mail delivered to me.
		It said that they know my health is poor and I could use some healthy potion.
		The seller name goes by C.S. and they had their workers to help check the mailbox and deliver potion to me.
			~ Speaker("Sebastian")
		I see, thanks for the information.
		{ ClueList !? CS_Sells_the_potion:
			# clue.CS_Sells_the_potion
			~ GetClue(CS_Sells_the_potion)
		}
		{ ClueList !? This_Potion_Improve_Health:
			# clue.This_Potion_Improve_Health
			~ GetClue(This_Potion_Improve_Health)
		}
		//part 3
			~ Speaker("Senile old man")
		Oh, Athena can help. There was a time when she brew a soothing potion for me. But she's so busy now. What a hardworking lass.
		I'm too old and it gets too tiring to walk even around my house. I did thought I would ask Athena to check my potion. 
		But when I heard she's so busy with work I thought I would test the potion myself.
			~ Speaker("Sebastian")
		... And did it gets better?
			~ Speaker("Senile old man")
		Well, no. But my body could be getting worse too you know? Even so I can't be sure.
			~ Speaker("Sebastian")
		Alright, I'll find someone to check this potion for you.
			~ Speaker("Senile old man")
		Not just anyone, I want it to be Athena or Cassandra. They know better when it comes to alchemy.
			~ Speaker("Sebastian")
		Ok I will.
		(Well, I don't know who else to go for)
		(Now I should find Athena for help)
		~ ChangeLocation(Sanctuary)
		-> Investigate
	//This cutscene is unused for now
	=== Cutscene_Saleman ===
		\--- Cutscene C.S. Potion ---
		    ~ Speaker("Sebastian")
	    (Now I need to find this someone known as C.S.)
		(I should go back to the town again)
			~ Speaker("Narration")
		Sebastian went back inside the town and as he was walking by the shopping stalls...
			~ Speaker("Potion Seller")
		Come! Buy! Buy! Buy! This potion will solve all your problems!
			~ Speaker("Sebastian")
		// (A potion seller? But isn't solving all problem a bit too farfecthed?)
			~ Speaker("Potion Seller")
		This is the only BEST! and THE BEST! POTION! you can buy!
		Don't believe me? This potion is approved by our trustworthy scholar C.S. who has Ph.D. on potion alchemy!
			~ Speaker("The Crowd")
		We know! Please let us buy already!
			~ Speaker("Potion Seller")
		Ate a weird mushroom?
			~ Speaker("The Crowd")
		Drink the potion!!!
			~ Speaker("Potion Seller")
		Weird ghost lurking in your room?
			~ Speaker("The Crowd")
		Drink the potion!!!
			~ Speaker("Potion Seller")
		Found a spider while you're gardening?
			~ Speaker("The Crowd")
		Drink the potion!!!
			~ Speaker("Potion Seller")
		Marriage problem?
			~ Speaker("The Crowd")
		Drink the potion!!!
			~ Speaker("Potion Seller")
		That's right everyone! We all agree this is the BEST! and the BEST! POTION!
		Normally we sell one potion for 10,000 coins
			~ Speaker("Sebastian") 
		(... You can buy 10 houses with that.)
			~ Speaker("Potion Seller")
		Now I will sell this potion in...
		DISCOUNTED PRICE OF 9,999 COINS
			~ Speaker("One of the crowd")
		Wow! that saves me a meal!
			~ Speaker("Potion Seller")
		So what are you waiting for? Get in line and GET YOUR POTION NOW!!!
			~ Speaker("The Crowd")
		YEAH!!!!!!!!!!
			~ Speaker("Sebastian")
		(...)
		(I can't believe this is real)
		(But the seller mention the name C.S. so I should look more into it)
		// (He mentioned C.S. as a scholar. I wonder if that's also true.)
	    ~ ChangeLocation(Sanctuary)
	    -> Investigate
	=== Cutscene_Cassandra_Return ===
		/--- Cutscene Cassandra Return ---
			~ Speaker("Narration")
		The door opened and it was Cassandra who came into the room.
			~ Speaker("Cassandra")
		What are you doing in my room?
			~ Speaker("Sebastian")
		So you're the one who goes by the name C.S. ?
		Why are you doing this to your own people?
			~ Speaker("Cassandra")
		Yes, I'm C.S. . Now that you know about it what will you do?
		They will only listen to me, as they always have been.
		Nobody in this town will believe a stranger like you.
			~ Speaker("Sebastian")
		Well, yes I can. Because I got the evidences.
			~ Speaker("Cassandra")
		Evidences? What evidences?
			~ Speaker("Sebastian")
		If you really did nothing wrong then I challenge you to gather everyone at the town plaza
			~ Speaker("Cassandra")
		Really? Fine. I'll let you do your little show performance. See if they will take it seriously.
		~ ChangeLocation(Sanctuary)
		- -> The_Reveal
// --- Location List ---
	=== Location_Meeting_Hall ===
			{ ClueList !? News_about_Moving_Out_People:
					~ Speaker("Narration")
				Sebastian is searching inside the meeting hall
			}
		+ [Talk to Staff] -> Talk_to_Staff ->
		+ [Look at town board] -> Town_Board ->
		+ [Leave the meeting hall]
			{
			- ClueList ? News_about_Moving_Out_People && not Cutscene_The_Witch_Nursery:
				-> Cutscene_The_Witch_Nursery
			- ClueList ? News_about_Moving_Out_People:
				~ ChangeLocation(Sanctuary)
				-> Investigate
			}
				~ Speaker("NARRATION")
			Sebastian refuses to exit the building.
				~ Speaker("Sebastian")
			I just got here, I should look around first.
		- -> Investigate
 
		== Talk_to_Staff ==
			~ Conversation("Talk to Staff")
				~ Speaker("Staff")
			Welcome, may I help you?
			+ [Do you know about missing people?] -> Do_you_know_about_missing_people ->
			+ { ClueList ? News_about_Moving_Out_People } {ClueList !? Visitors_do_not_Checkout}
				[I read the news but...] -> I_read_the_news_but ->
			+ { ClueList ? Cassandra_Prophesied_The_Plague } {ClueList !? No_Record_of_Recent_Plague}
				[Do you know about the plague refugee?] -> Do_you_know_about_the_plague_refugee ->
			+ [(Actually, I don't think I need help this time)] {EndCon()} -> END # END
			- -> Talk_to_Staff

			= Do_you_know_about_missing_people
					~ Speaker("Staff")
				Oh, I think the newspaper talks about it. You should go take a look.
				The latest edition should be pinned at the town board over there.
				- ->->

			= I_read_the_news_but
					~ Speaker("Sebastian")
				I read the news but they didn't talk about "missing people" just "moving out people".
					~ Speaker("Staff")
				Really? Well, maybe they really do move out.
					~ Speaker("Sebastian")
				But do you have visitors other than me resting here?
					~ Speaker("Staff")
				Right now you're the only one but we used to have several other visitors before today.
					~ Speaker("Sebastian")
				I see
					~ Speaker("Staff")
				And like the news said, they didn't make a checkout so they won't get tracked!
					~ Speaker("Sebastian")
				... sorry?
				Nevermind, thanks for your help.
					~ Speaker("Staff")
				I'm just glad to be of service.
				{ ClueList !? Visitors_do_not_Checkout:
					# clue.Visitors_do_not_Checkout
					~ GetClue(Visitors_do_not_Checkout)
				}
					~ Speaker("Sebastian")
				But If I may ask, where did you get these newspaper?
					~ Speaker("Staff")
				Oh, we bought these newspaper from the merchant who comes here every morning.
				All I know is he's a human with tan colored skin and wear red clothes with turban.
					~ Speaker("Sebastian")
				I see, thanks again.
				{ ClueList !? Merchant_Sells_the_Newspaper:
					# clue.Merchant_Sells_the_Newspaper
					~ GetClue(Merchant_Sells_the_Newspaper)
				}
				- ->->

			= Do_you_know_about_the_plague_refugee
					~ Speaker("Sebastian")
				Do you know about plague refugee outside the town?
					~ Speaker("Staff")
				Hmm? There's a plague going on?
				//Oh dear god
					~ Speaker("Sebastian")
				Yes, I heard from the caravan that Cassandra was the one who told them to flee and stay in the Sanctuary.
					~ Speaker("Staff")
				Suddenly there's an unknown plague appeared out of nowhere? Isn't that unreasonable.
					~ Speaker("Sebastian")
				Well, I'm curious about how Cassandra make her prophency.
				Is it possible that I could meet her?
					~ Speaker("Staff")
				I'm sorry, I don't know where she is. But you can try to visit the Witch's Nursery.
				{ ClueList !? No_Record_of_Recent_Plague:
					#clue.No_Record_of_Recent_Plague
					~ GetClue(No_Record_of_Recent_Plague)
				}
				- ->->
		== Town_Board ==
			~ Conversation("Town Board")
				~ Speaker("Narration")
			Sebastian approach the Town Board
				~ Speaker("Sebastian")
			(Let's see what the board got)
				+ [Newspaper] -> Newspaper ->
				+ [Leave the board] {EndCon()} ->->
			- -> Town_Board
 
		== Newspaper ==
			~ Conversation("Newspaper")
				~ Speaker("Sebastian")
			(There's a newspaper here)
			(There are some headlines I can read about it)
			-> Headlines
 
			= Headlines
				~ Conversation("Headlines")
					~ Speaker("Narration")
				Pick a headline to read.
				+ [Moving Out People] -> Moving_Out_People
				+ [The Strongest Potion] -> The_Strongest_Potion
				+ [Fishy Fortune] -> Fishy_Fortune
				+ [The Most Beautiful of Them All] -> The_Most_Beautiful_of_Them_All
				+ [Stop Reading] ->->
 
			= Moving_Out_People
					~ Speaker("Narration")
				The content read "In the last few months, families have been searching for the location of their own relatives who went to the Sanctuary. But those families didn't consider one of the reason for the travelers to travel is to get away from their own families, therefore, it's normal to keep their location  hidden"
					~ Speaker("Sebastian")
				(This could be the closest headline to help me so far)
				//(But what is this shrewd logic? "get away from their own families?" "normal to keep their location hidden?")
				{ ClueList !? News_about_Moving_Out_People:
					# clue.News_about_Moving_Out_People  
					~ GetClue(News_about_Moving_Out_People)
				}
				- ->->
			= The_Strongest_Potion
					~ Speaker("Narration")
				The content read "Do not ask for more potion. The potion seller only makes potions that can kills a dragon"
					//~ Speaker("Sebastian")
				//(Doesn't that sounds... too much?)
				- ->->
			= Fishy_Fortune
					~ Speaker("Narration")
				The content read "This season is advised to catch only forest tuna. Other fish are not delicious and won't sell well"
					//~ Speaker("Sebastian")
				//(I don't know much about fishing)
				- ->->
			= The_Most_Beautiful_of_Them_All
					~ Speaker("Narration")
				The content read "This year's winner of Miss Galaxy is rumored to be the one and only, Ms. Cassandra!"
					~ Speaker("Sebastian")
				(... Really?)
				- ->->
 
	=== Location_The_Sanctuary ===
		+ [Go to the Meeting Hall]
			~ ChangeLocation(Meeting_Hall)
		+ [Go to the Nursery]
			~ ChangeLocation(Nursery)
		+ [Go to the Forest]
			~ ChangeLocation(Forest)
		- -> Investigate
	   
	=== Location_Nursery ===
		+ [Talk to Athena] -> Talk_to_Athena ->
		+ [Leave Nursery]
			~ ChangeLocation(Sanctuary)
		- -> Investigate

		= Talk_to_Athena
			~ Conversation("Talk to Athena")
				~ Speaker("Athena")
			Hello Sebastian! Are you lost? Do you need any help?
			+ { ClueList ? CS_Sells_the_potion } {ClueList !? The_Potion_is_just_Colored_Water}
				[Can you help me test this potion?] -> Can_you_help_me_test_this_potion ->
			+ { ClueList ? No_Record_of_Recent_Plague } {ClueList !? CS_letters}
				[Do you know where Cassandra is?] -> Do_you_know_where_Cassandra_is
			+ [(Nevermind)] {EndCon()} ->->
				- -> Talk_to_Athena
			
			= Can_you_help_me_test_this_potion
					~ Speaker("Sebastian")
				So, you know about alchemy?
					~ Speaker("Athena")
				Oh yes I do! I'm not as great as Cassandra but I can still help you with alchemy.
					~ Speaker("Sebastian")
				Then can you help me with this potion? I want to make sure it's not poisonous.
					~ Speaker("Athena")
				Sure, give me a moment.
				    ~ Speaker("Narration")
				Athena doing her alchemist thing. 
				She took the potion from Sebastian's hand, put it on the table nearby with some dust and mixer tools.
				Turns out the potion is just colored water.
				It does nothing good or bad.
				{ ClueList !? The_Potion_is_just_Colored_Water:
					# clue.The_Potion_is_just_Colored_Water
					~ GetClue(The_Potion_is_just_Colored_Water)
				}
					~ Speaker("Athena")
				Wait this is a scam. Why would anyone do this?!
				Where did you get this potion from again?
					~ Speaker("Sebastian")
				Well, this senile old man bought this potion from C.S.
				And the way they deliver the potion is suspicious.
					~ Speaker("Athena")
				C.S. ? That name sounds familiar...
				I wish I know who they are. If you know something please tell me about it.
					~ Speaker("Sebastian")
				Sure, I'm still working on finding C.S. anyway.
				(Why would C.S. do this... this is not good for the town.)
				(I should go find someone with closest connection to C.S. )
				(hmm...)
				(I think I should go find merchant for now.)
				- ->->
 
			= Do_you_know_where_Cassandra_is
					~ Speaker("Narration")
				Sebastian went to the Witch's Nursery and asked Athena
					~ Speaker("Sebastian")
				Is Cassandra here? I want to ask her something.
					~ Speaker("Athena")
				I don't know where she is. But you can come inside. I think she will be back soon.
					~ Speaker("Narration")
				Sebastian and Athena went inside the Witch's Nursery.
					~ Speaker("Athena")
				You can sit back and relax here, I'll make some tea for you.
					~ Speaker("Narration")
				Athena walk away to the kitchen
				// The kitchen will be locked
					~ Speaker("Sebastian")
				(I should go and search this house while I can)
				~ ChangeLocation(Inside_Nursery)
				- ->->
	=== Location_Forest ===
		+ {ClueList !? The_Potion_is_just_Colored_Water}
			[Talk to Merchant] -> Talk_to_Merchant ->
		+ {ClueList ? The_Potion_is_just_Colored_Water}
			[Talk to Caravan] -> Talk_to_Caravan ->
		+ [Go to the Sanctuary]
			~ ChangeLocation(Sanctuary)
		- -> Investigate
	   
		== Talk_to_Merchant ==
			~ Conversation("Talk to Merchant")
				~ Speaker("Narration")
			Talking to Merchant.
			+ [What are you doing here?] -> What_are_you_doing_here ->
			+ { ClueList ? Merchant_Sells_the_Newspaper } {ClueList !? CS_Order_to_Forge_The_News}
				[I heard that you sells the Newspaper] -> I_heard_that_you_sells_the_Newspaper ->
			+ [(Nevermind)] {EndCon()} -> END #END
			- -> Talk_to_Merchant
			   
				= What_are_you_doing_here
						#speaker.Merchant
					Oh, I'm just continuing my business here.
					You see, my profit doesn't only come from selling my well earned and clean items.
					I also have a connection with this town leader too! So next time, don't try to interrupt my business.
					- ->->
				= I_heard_that_you_sells_the_Newspaper
						#speaker.Merchant
					Well yes, I sells the newspaper to this town.
						#speaker.Sebastian
					Then can we talk about how this headline "Movin out people" is fake?
						#speaker.Merchant
					Fake? Why would you think that?
						#speaker.Sebastian
					You see, the visitors do not checkout in the Meeting Hall.
						#speaker.Merchant
					Well, I don't care about your problem.
					It's just good money to make forging commission.
					//Merchant spilled the beans
						#speaker.Sebastian
					Forging, so you're the one who faked it?
						#speaker.Merchant
					Oops.
						#speaker.Sebastian
					I heard it you know. Don't just pretend to be innocent now.
						#speaker.Merchant
					Fine, I'm the one who forged it. 
					But now what are you going to do? Call the police? They aren't around here you know.
						#speaker.Sebastian
					There maybe no police but I could ask our good friend Alex to talk it out.
						#speaker.Merchant
					Oh... him... hahaha well what else do you want from me?
						#speaker.Sebastian
					I just want to know the real content. And also who commissioned you.
						#speaker.Merchant
					Who commissioned me? I don't know actually. I only communicated with them through the name of "C.S."
						#speaker.Sebastian
					C.S. ?
						#speaker.Merchant
					Yeah that's all I really know. And here's the real content that I got before I forged it.
						#speaker.Sebastian
					Ok, it's a good thing that you did what I asked.
					{ ClueList !? CS_Order_to_Forge_The_News:
						# clue.CS_Order_to_Forge_The_News
						~ GetClue(CS_Order_to_Forge_The_News)
					}
					{ ItemList !? Original_Newspaper:
						# item.Original_Newspaper
						~ GetItem(Original_Newspaper)
					}
					- ->->
 
		== Talk_to_Caravan ==
			~Conversation("Talk to Caravan")
				~ Speaker("Sebastian")
			What's going on here? I don't think you guys were staying here before.
				~ Speaker("Caravan")
			Oh hello young man, we just arrived here.
			We abandoned our hometown because of prophency about the plague. So we have to flee to a safe place here.
				~ Speaker("Sebastian")
			Plague? I didn't hear anything about it.
			Are you going anywhere else?
				~ Speaker("Caravan")
			Well, we don't have anywhere else to go.
			Miss Cassandra allowed us to stay just outside the town.
				~ Speaker("Sebastian")
			About the prophency... Where did you get it from?
				~ Speaker("Caravan")
			Miss Cassandra gave us a prophency that there would be plague in our town.
			Miss Cassandra is such a great leader. She's so kind to warn us and let us stay safe in her town.
			{ ClueList !? Cassandra_Prophesied_The_Plague:
					# clue.Cassandra_Prophesied_The_Plague
					~ GetClue(Cassandra_Prophesied_The_Plague)
			}
		- ->->
	=== Location_Inside_Nursery ===
		+ [Cassandra's Room] -> Cassandra_Room
		+ [The kitchen]
				~ Speaker("Narration")
			The kitchen is locked.
		- -> DONE
			// The kitchen is locked and you can hear Athena hummed along with some soft noise.
		
		= Cassandra_Room
				~ Speaker("Narration")
			Sebastian searching inside Cassandra's room.
				+ [Look on the table] -> Look_on_The_Table
		
		= Look_on_The_Table
				~ Speaker("Narration")
			Sebastian look at Cassandra's table.
				~ Speaker("Sebastian")
			(What is this letter?)
			(Change the headlines, Selling potion, Working with thiefs? Why is it here?)
				~ Speaker("Narration")
			The letter is signed as C.S.
				~ Speaker("Sebastian")
			(C.S. so... C.S. stands for Cassandra?)
				~ Speaker("Narration")
			Sebastian collected the letters.
			// ^ get clues and items functions
			{ ClueList !? CS_letters:
				# clue.CS_letters
				~ GetClue(CS_letters)
			}
			- -> Cutscene_Cassandra_Return
	//Chapter 3 stuff
	=== The_Reveal ===
		~ Speaker("Narration")
	Sebastian walked to the center of the plaza.
		~ Speaker("Sebastian")
	Attention everyone, I'm here to let everyone know that you have been living with lies and fake news all this time.

	\---------
	There's supposed to be some choices to choose again to answer the villagers
	If player get most of them correct, the villagers will believe that Sebastian is telling the truth.
	\---------
	- -> Exposed_Cassandra

	=== Exposed_Cassandra ===
	//part 1
		~ Speaker("Villagers")
	Cassandra! Why did you do this?!
		~ Speaker("Senile old man")
	You sold me a glass of water all this time?
		~ Speaker("Caravan")
	We have to abandoned our town just to let the thiefs takes everything!
		~ Speaker("Villagers")
	We don't need this fake leader in our town!
		~ Speaker("Narration")
	The villagers comes towards Cassandra with ill intents.
	//part 2
		~ Speaker("Cassandra")
	No, why are you listening to this child? I did everything for you people all this time.
		~ Speaker("Villagers")
	What you did is everything that makes our life worse! You just want to leech our work!
	    ~ Speaker("Narration")
	The villagers chase Cassandra out of the Sanctuary.
	- -> Confessed_Cassandra

	=== Confessed_Cassandra ===
	    ~ Speaker("Narration")
	Sebastian followed Cassandra outside the town.
		~ Speaker("Cassandra")
	I'll tell you, Sebastian. As a reward for being able to expose me.
		~ Speaker("Sebastian")
	Well, I already read it from your letters. You're also the one who captured my father.
	So, where is he now?
		~ Speaker("Cassandra")
	Alright... Listen closely, your father...
	Is already dead.
		~ Speaker("Sebastian")
	...
	You're not done with lying?
		~ Speaker("Cassandra")
	Ha, how funny, now you're the one who wants to hear some lies instead.
	If you don't believe me, head toward the forest to the east of my Nursery. 
	There's a hidden shack there that I use to do my ritual.
		~ Speaker("Sebastian")
	Ritual?
		~ Speaker("Cassandra")
	Yes Sebastian, ritual, the dark ones.
	I would love to tell you more but you must be dying to find your father now.
		~ Speaker("Sebastian")
	... You better not be scheming again.
-> END