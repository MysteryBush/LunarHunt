VAR current_chapter = -> Cutscene_Welcome_to_Sanctuary
VAR current_speaker = ""
VAR current_conversation = ""
VAR current_location = -> Cutscene_Welcome_to_Sanctuary
 
VAR CurrentItem = ()
VAR CountList = 0
VAR MaxList = 0
 
LIST ItemList = Original_Newspaper
 
LIST ClueTutorialList = Clue_01, Clue_02, Clue_03, Clue_04, Clue_05, Clue_06
LIST ClueList = News_about_Moving_Out_People, Visitors_do_not_Checkout, Merchant_Sells_the_Newspaper, CS_Order_to_Forge_The_News, CS_Sells_the_potion, This_Potion_Improve_Health, The_Potion_is_just_Colored_Water, Cassandra_Prophesied_The_Plague, No_Record_of_Recent_Plague, CS_letters
LIST EvidenceList = Merchant_took_the_axe, CS_Faked_The_News, CS_Sells_Fake_Potion, Cassandra_lied_about_the_plague
 
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
		// - Collected Clue: {clue}
	== function GetClueTutorial(clue) ==
		~ ClueTutorialList += clue
		// - Collected Clue: {clue}
	== function GetItem(item) ==
		~ ItemList += item
		// - Collected Item: {item}
	== function GetEvidence(evidence) ==
		~ EvidenceList += evidence
		// - Collected Evidence: {evi	dence}
	   
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
			#clue.News_about_Moving_Out_People
			- -> DONE
		= Start_Chapter_2
			<- After_Meeting_Hall
			~ ClueList += (Visitors_do_not_Checkout, Merchant_Sells_the_Newspaper, CS_Order_to_Forge_The_News)
			~ EvidenceList += CS_Faked_The_News
			#clue.Visitors_do_not_Checkout
			#clue.Merchant_Sells_the_Newspaper
			#clue.CS_Order_to_Forge_The_News
			#evidence.CS_Faked_The_News
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
				#speaker.Sebastian
			I think that's all the clues I can find for now.
			Let's see if I can make use of them
				#noSpeaker
			At this point, player must select the correct clues and merge them.
			This prototype can possibly implement it but it would take too long and won't be useful for the real prototype.
			Here's the evidence for now.
				#evidence.CS_Faked_The_News
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
			#noSpeaker
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
 
// --- Get Evidence Knots ---
	=== Evidence_Merchant_took_the_axe ===
		~ GetEvidence(Merchant_took_the_axe)
		-> go_tell_lumberjack
		= go_tell_lumberjack
			#speaker.Sebastian
		(So... 
		(This axe that the merchant selling is really belong to Alex)
		(I should go back and tell Alex about it)
			#noSpeaker
		Now it's time to go back and talk to the lumberjack
		#END
		-> END
	=== Evidence_CS_Faked_The_News ===
		~ GetEvidence(CS_Faked_The_News)
		-> who_is_CS
		= who_is_CS
			#speaker.Sebastian
		(Who is this C.S. and why are they making fake news?)
		(Even though I'm here to find my father this C.S. is only making it harder for me)
		(I should go back to town for more answers)
			#noSpeaker
		Go back to town to continue the search
		#timelineState.TimelineState_sanctuary_old_man
		#END
		-> END
	=== Evidence_CS_Sells_Fake_Potion ===
		~ GetEvidence(CS_Sells_Fake_Potion)

		#timelineState.TimelineState_caravan_arrive
		#END
		-> END
	=== Evidence_Cassandra_lied_about_the_plague ===
		~ GetEvidence(Cassandra_lied_about_the_plague)
		-> END
// --- Cutscenes list ---
	=== Cutscene_Welcome_to_Sanctuary ===
		//\--- Welcome to Sanctuary ---
		//play cutscene 
		// 	#noSpeaker
		// While entering the entrance of the Sanctuary town. Sebastian was wondering for himself.
		//resume
		= first
			#speaker.Sebastian
		(Now that I got here, I should ask if someone knows about missing people.)
		(That way, I should be able to find a lead about my father.)
		//Sebastian walk until in front of Athena
		#timeline.Athena
		#knot.Cutscene_Welcome_to_Sanctuary.Athena
		#END
		-> END
		// 	#noSpeaker
		// Athena, who was re-painting the town sign, noticed Sebastian.
		= Athena
			#speaker.Athena
		Hello there, you seem to be new here, do you need any help?
			#speaker.Sebastian
		Well, yes, I'm looking for a place to know more about something.
			#speaker.Athena
		What happened?
			#speaker.Sebastian
		I was traveling with my father but he got kidnapped by someone.
			#speaker.Athena
		Oh dear… I'm so sorry to hear that…
			#speaker.Sebastian
		No, don't be, father is a strong willed man. I believe he's still out there somewhere.
			#speaker.Athena
		Well, that's good to know.
		I'll help you however I can. I'll make sure you can find your father.
		If you're looking for leads, the town's meeting hall can help you. There are people who like to tell stories, town board, and also some rooms for visitors to rest.
			#speaker.Sebastian
		Thank you, I'll keep that in mind. I'll be sure to go ther-
			#speaker.Athena
		We can go there right now let's go!
			#speaker.Sebastian
		(She's so eager to help)
		(But I guess it's the first place to go anyway)
			#noSpeaker
		#timeline.To_meeting_hall
		#knot.Cutscene_Welcome_to_Sanctuary.To_meeting_hall
		// #END
		-> END
		//play cutscene
		= To_meeting_hall
			#noSpeaker
		Athena leads the way to the town meeting hall and Sebastian follows behind her.
		They arrived at the front of the town's meeting hall.
		#timeline.At_meeting_hall
		#knot.Cutscene_Welcome_to_Sanctuary.At_meeting_hall
		#END
		-> END

		= At_meeting_hall
			#speaker.Athena
		Here it is! The meeting hall!
			#speaker.Sebastian
		Thank you, I hope I can find my father soon.
			#speaker.Athena
		I'm sure you can!
			#noSpeaker
		#timeline.Go_in
		#knot.Cutscene_Welcome_to_Sanctuary.Go_in
		// #END
		- -> Go_in

		= Go_in
			#noSpeaker
		Athena walked away to the east.
		Sebastian walks up to the door and go inside the town's meeting hall.
		#spawn.3
		#scene.Meeting_Hall
		#END
		-> END 
		//-> Cutscene_Meeting_in_the_Meeting_Hall
	   
	=== Cutscene_Meeting_in_the_Meeting_Hall ===
		//\--- Meeting in the Meeting Hall ---
		// 	#noSpeaker
		// Sebastian walked in from the south door.
		= At_Meeting_Hall
			#speaker.Sebastian
		(So this is the town's meeting hall. It is well tended here)
			#speaker.Cassandra
		Here's the correct Headline
			#speaker.HallStaff
		Yes ma'am, it will be done.
		// #knot.Cutscene_Meeting_in_the_Meeting_Hall.Cassandra_approach
		#timeline.Cassandra_approach
		#knot.Cutscene_Meeting_in_the_Meeting_Hall.Cassandra_welcome
		// #timeline.Cassandra_welcome
		#noSpeaker
		-> END

		// = Cassandra_approach
		// 		#noSpeaker
		// 	Cassandra walks away from the staff to the door in the south.
		// 	Cassandra noticed Sebastian.
		// 	#knot.Cutscene_Meeting_in_the_Meeting_Hall.Cassandra_welcome
		// 	#timeline.Cassandra_welcome
		// 	#END
		
		= Cassandra_welcome
				#speaker.Cassandra
			Oh! Welcome! You look new here.
				#speaker.Sebastian
			Yes, I just got here. 
				#speaker.Cassandra
			You also looked young, are you traveling alone?
				#speaker.Sebastian
			No, I'm here to find my missing father.
			//This scene may need more dialogues or a simpler, shorter talk.
				#speaker.Cassandra
			Well, feel free to look around here. If you need a room you can ask the staff here.
				#speaker.Sebastian
			I'll keep that in mind
			
			#knot.Cutscene_Meeting_in_the_Meeting_Hall.Cassandra_leave
			#timeline.Cassandra_leave
			// #END
			#noSpeaker
			- -> END
		= Cassandra_leave
			#noSpeaker
		Cassandra exited the hall.
		#knot.Cutscene_Meeting_in_the_Meeting_Hall.look_around_meeting_hall
		#timeline.look_around_meeting_hall
		// #END
		#noSpeaker
		- -> END
		= look_around_meeting_hall
				#speaker.Sebastian
			(Alright, it's time to look around for some leads here)
			(I should ask the Staff here)
			//  -> Investigate_Meeting_Hall
			// ~ ChangeLocation(Meeting_Hall)
			#END
			// -> Investigate 
			-> END
  
	=== Cutscene_The_Witch_Nursery ===
	    //\--- Cutscene The Witch Nursery ---
		//part 1
			#speaker.Sebastian
		(I should keep going now)
			#speaker.Athena
		Oh! Hi again! Did you find something helpful?
			#speaker.Sebastian
		Oh, hi. I did find something useful. Thank you.
			#speaker.Athena
		I'm just glad to help.
		Oh, I don't think we know each other's names yet. I'm Athena. What's your name?
			#speaker.Sebastian
		Sebastian.
			#speaker.Athena
		That's a nice name!
			#speaker.Sebastian
		Thanks
			#speaker.Athena
		Alright! Sebastian, If you need any other help from me you can find me at the Witch's nursery.
			#speaker.Sebastian
		(Witch?)
			#speaker.Athena
		Don't worry. She's a good witch! She raised me when I was gravely ill.
		And she also runs this town too! Everyone trusts her and we all do our best to help her!
			#speaker.Sebastian
		I see, I'll be sure to visit the nursery later-
			#speaker.Athena
		Oh wait, I didn't show you the place yet. It's over here!
			#speaker.Sebastian
		Wait, it's fine I don't need to see it now- hey!
			#noSpeaker
		Athena dragged Sebastian to the Witch's Nursery.
		//part 2
			#speaker.Athena
		Here it is! The Witch's Nursery!
		We also call it the Nursery for short.
			#speaker.Sebastian
		There are children here, they seem to be enjoying themselves.
			#speaker.Athena
		Yeah, I helped her take care of them too.
		Just like you, their parents were also missing so we took them in.
			#speaker.Sebastian
		I see… Is it possible that I can ask them about it?
			#speaker.Athena
		Hmm… It's not an easy thing to ask. But I'll  ask them for you when they're ready.
			#speaker.Sebastian
		… Thanks, sorry if it's too much.
		I just wanted to find him.
			#speaker.Athena
		It's fine! I'm sure they'll understand.
			#speaker.Sebastian
		Anyway, thanks for showing me around, now I should get going and find my father.
			#speaker.Athena
		Oh! Right! Sorry if I bothered you too much.
		Now I should help Ms. Cassandra too. And I hope you can find your father soon!
			#speaker.Sebastian
		Thank you.
			#noSpeaker
		Athena walked into the Witch's Nursery
		//part 3
			#speaker.Sebastian
		(Now I can really start searching for clues)
		~ ChangeLocation(Sanctuary)
		#END
		// -> Investigate
		-> END
	   
		= skip
		~ ChangeLocation(Sanctuary)
		#END
		// - -> Investigate
		-> END
	 
	=== Cutscene_CS_Potion ===
		//\--- Cutscene C.S. Potion ---
		//part 1
		    #speaker.Sebastian
	    (Now I need to find this someone known as C.S.)
		(I should go back to the town again)
			#noSpeaker
		Sebastian went back inside the town and as he was walking by the shopping stalls...
		He saw a senile old man putting coins inside his mailbox.
		//part 2
			#speaker.Sebastian
		(Did he just put some coins inside his mailbox?)
		(Is this some kind of this town's culture custom? No, I think this might be a scheme.)
			#speaker.OldMan
		Oh, young lad, I just put some coins in there to buy some potion.
			#speaker.Sebastian
		Really? How did it all started?
			#speaker.OldMan
		Oh, one day, there was a mail delivered to me.
		It said that they know my health is poor and I could use some healthy potion.
		The seller name goes by C.S. and they had their workers to help check the mailbox and deliver potion to me.
			#speaker.Sebastian
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
			#speaker.OldMan
		Oh, Athena can help. There was a time when she brew a soothing potion for me. But she's so busy now. What a hardworking lass.
		I'm too old and it gets too tiring to walk even around my house. I did thought I would ask Athena to check my potion. 
		But when I heard she's so busy with work I thought I would test the potion myself.
			#speaker.Sebastian
		... And did it gets better?
			#speaker.OldMan
		Well, no. But my body could be getting worse too you know? Even so I can't be sure.
			#speaker.Sebastian
		Alright, I'll find someone to check this potion for you.
			#speaker.OldMan
		Not just anyone, I want it to be Athena or Cassandra. They know better when it comes to alchemy.
			#speaker.Sebastian
		Ok I will.
		(Well, I don't know who else to go for)
		(Now I should find Athena for help)
		#timelineState.TimelineState_Sanctuary_1
		#END
		~ ChangeLocation(Sanctuary)
		// -> Investigate
		-> END
	//This cutscene is unused for now
	=== Cutscene_Saleman ===
		//\--- Cutscene C.S. Potion ---
		    #speaker.Sebastian
	    (Now I need to find this someone known as C.S.)
		(I should go back to the town again)
			#noSpeaker
		Sebastian went back inside the town and as he was walking by the shopping stalls...
			#speaker.PotionSeller
		Come! Buy! Buy! Buy! This potion will solve all your problems!
			#speaker.Sebastian
		// (A potion seller? But isn't solving all problem a bit too farfecthed?)
			#speaker.PotionSeller
		This is the only BEST! and THE BEST! POTION! you can buy!
		Don't believe me? This potion is approved by our trustworthy scholar C.S. who has Ph.D. on potion alchemy!
			#speaker.Crowd
		We know! Please let us buy already!
			#speaker.PotionSeller
		Ate a weird mushroom?
			#speaker.Crowd
		Drink the potion!!!
			#speaker.PotionSeller
		Weird ghost lurking in your room?
			#speaker.Crowd
		Drink the potion!!!
			#speaker.PotionSeller
		Found a spider while you're gardening?
			#speaker.Crowd
		Drink the potion!!!
			#speaker.PotionSeller
		Marriage problem?
			#speaker.Crowd
		Drink the potion!!!
			#speaker.PotionSeller
		That's right everyone! We all agree this is the BEST! and the BEST! POTION!
		Normally we sell one potion for 10,000 coins
			#speaker.Sebastian 
		(... You can buy 10 houses with that.)
			#speaker.PotionSeller
		Now I will sell this potion in...
		DISCOUNTED PRICE OF 9,999 COINS
			#speaker.OneOfTheCrowd
		Wow! that saves me a meal!
			#speaker.PotionSeller
		So what are you waiting for? Get in line and GET YOUR POTION NOW!!!
			#speaker.Crowd
		YEAH!!!!!!!!!!
			#speaker.Sebastian
		(...)
		(I can't believe this is real)
		(But the seller mention the name C.S. so I should look more into it)
		// (He mentioned C.S. as a scholar. I wonder if that's also true.)
	    ~ ChangeLocation(Sanctuary)
		#END
	    // -> Investigate
		-> END
	=== Cutscene_Cassandra_Return ===
		/--- Cutscene Cassandra Return ---
			#noSpeaker
		The door opened and it was Cassandra who came into the room.
			#speaker.Cassandra
		What are you doing in my room?
			#speaker.Sebastian
		So you're the one who goes by the name C.S. ?
		Why are you doing this to your own people?
			#speaker.Cassandra
		Yes, I'm C.S. . Now that you know about it what will you do?
		They will only listen to me, as they always have been.
		Nobody in this town will believe a stranger like you.
			#speaker.Sebastian
		Well, yes I can. Because I got the evidences.
			#speaker.Cassandra
		Evidences? What evidences?
			#speaker.Sebastian
		If you really did nothing wrong then I challenge you to gather everyone at the town plaza
			#speaker.Cassandra
		Really? Fine. I'll let you do your little show performance. See if they will take it seriously.
		~ ChangeLocation(Sanctuary)
		- -> The_Reveal
// --- Location List ---
	=== Location_Meeting_Hall ===
		= Leave_the_meeting_hall
			{
			- ClueList ? News_about_Moving_Out_People && not Cutscene_The_Witch_Nursery:
				-> Cutscene_The_Witch_Nursery
			- ClueList ? News_about_Moving_Out_People:
				~ ChangeLocation(Sanctuary)
				-> Investigate
			}
				#noSpeaker
			Sebastian refuses to exit the building.
				#speaker.Sebastian
			I just got here, I should look around first.
			#END
		// - -> Investigate
		-> END
 
		== Talk_to_Staff ==
			//~ Conversation("Talk to Staff")
				#speaker.HallStaff
			Welcome, may I help you?
			+ [Do you know about missing people?] -> Do_you_know_about_missing_people ->
			+ { ClueList ? News_about_Moving_Out_People } {ClueList !? Visitors_do_not_Checkout}
				[I read the news but...] -> I_read_the_news_but ->
			+ { ClueList ? Cassandra_Prophesied_The_Plague } {ClueList !? No_Record_of_Recent_Plague}
				[Do you know about the plague refugee?] -> Do_you_know_about_the_plague_refugee ->
			+ [(Actually, I don't think I need help this time)] {EndCon()} -> END #END
			- -> Talk_to_Staff

			= Do_you_know_about_missing_people
					#speaker.HallStaff
				Oh, I think the newspaper talks about it. You should go take a look.
				The latest edition should be pinned at the town board over there.
				- ->->

			= I_read_the_news_but
					#speaker.Sebastian
				I read the news but they didn't talk about "missing people" just "moving out people".
					#speaker.HallStaff
				Really? Well, maybe they really do move out.
					#speaker.Sebastian
				But do you have visitors other than me resting here?
					#speaker.HallStaff
				Right now you're the only one but we used to have several other visitors before today.
					#speaker.Sebastian
				I see
					#speaker.HallStaff
				And like the news said, they didn't make a checkout so they won't get tracked!
					#speaker.Sebastian
				... sorry?
				Nevermind, thanks for your help.
					#speaker.HallStaff
				I'm just glad to be of service.
					#noSpeaker 
				{ ClueList !? Visitors_do_not_Checkout:
					#clue.Visitors_do_not_Checkout
					~ GetClue(Visitors_do_not_Checkout)
				}
					#speaker.Sebastian
				But If I may ask, where did you get these newspaper?
					#speaker.HallStaff
				Oh, we bought these newspaper from the merchant who comes here every morning.
				All I know is he's a human with tan colored skin and wear red clothes with turban.
					#speaker.Sebastian
				I see, thanks again.
					#noSpeaker 
				{ ClueList !? Merchant_Sells_the_Newspaper:
					#clue.Merchant_Sells_the_Newspaper
					~ GetClue(Merchant_Sells_the_Newspaper)
				}
				#END
				-> END

			= Do_you_know_about_the_plague_refugee
					#speaker.Sebastian
				Do you know about plague refugee outside the town?
					#speaker.HallStaff
				Hmm? There's a plague going on?
				//Oh dear god
					#speaker.Sebastian
				Yes, I heard from the caravan that Cassandra was the one who told them to flee and stay in the Sanctuary.
					#speaker.HallStaff
				Suddenly there's an unknown plague appeared out of nowhere? Isn't that unreasonable.
					#speaker.Sebastian
				Well, I'm curious about how Cassandra make her prophency.
				Is it possible that I could meet her?
					#speaker.HallStaff
				I'm sorry, I don't know where she is. But you can try to visit the Witch's Nursery.
					#noSpeaker 
				{ ClueList !? No_Record_of_Recent_Plague:
					#clue.No_Record_of_Recent_Plague
					~ GetClue(No_Record_of_Recent_Plague)
				}
				#END
				-> END
		== Town_Board ==
			//~ Conversation("Town Board")
			// 	#noSpeaker
			// Sebastian approach the Town Board
				#speaker.Sebastian
			(Let's see what the board got)
				+ [Newspaper] -> Newspaper ->
				+ [Leave the board] {EndCon()} -> END #END
			- -> Town_Board
 
		== Newspaper ==
			//~ Conversation("Newspaper")
				#speaker.Sebastian
			(There's a newspaper here)
			(There are some headlines I can read about it)
			-> Headlines
 
			= Headlines
				//~ Conversation("Headlines")
					#noSpeaker
				Pick a headline to read.
				+ [Moving Out People] -> Moving_Out_People
				+ [The Strongest Potion] -> The_Strongest_Potion
				// + [Fishy Fortune] -> Fishy_Fortune
				+ [The Most Beautiful of Them All] -> The_Most_Beautiful_of_Them_All
				+ [Stop Reading] -> Town_Board
 
			= Moving_Out_People
					#noSpeaker
				The content read "In the last few months, families have been searching for the location of their own relatives who went to the Sanctuary. 
				But those families didn't consider one of the reason for the travelers to travel is to get away from their own families.
				Therefore, it's normal to keep their location  hidden"
					#speaker.Sebastian
				(This could be the closest headline to help me so far)
				//(But what is this shrewd logic? "get away from their own families?" "normal to keep their location hidden?")
					#noSpeaker
				{ ClueList !? News_about_Moving_Out_People:
					// ~ GetClue(News_about_Moving_Out_People)
					#clue.News_about_Moving_Out_People
					// Clues collected: News_about_Moving_Out_People
					~ GetClue(News_about_Moving_Out_People)
				}
				#END
				-> END
			= The_Strongest_Potion
					#noSpeaker
				The content read "Do not ask for more potion. The potion seller only makes potions that can kills a dragon"
					//#speaker.Sebastian
				//(Doesn't that sounds... too much?)
				- ->->
			= Fishy_Fortune
					#noSpeaker
				The content read "This season is advised to catch only forest tuna. Other fish are not delicious and won't sell well"
					//#speaker.Sebastian
				//(I don't know much about fishing)
				- ->->
			= The_Most_Beautiful_of_Them_All
					#noSpeaker
				The content read "This year's winner of Miss Galaxy is rumored to be the one and only, Ms. Cassandra!"
					#speaker.Sebastian
				(... Really?)
				- ->->
 
	=== Location_The_Sanctuary ===
		-> END

		== Talk_to_Old_Man ==
				#speaker.OldMan
			Live your life while you still can young lad
			#END
			-> END
	   
	=== Location_Nursery ===
		- -> Investigate

		== Talk_to_Athena ==
			//~ Conversation("Talk to Athena")
				#speaker.Athena
			// Hello Sebastian! Are you lost? Do you need any help?
			Hello Sebastian! Do you need any help?
			+ { ClueList ? CS_Sells_the_potion } {ClueList !? The_Potion_is_just_Colored_Water}
				[Can you help me test this potion?] -> Can_you_help_me_test_this_potion ->
			+ { ClueList ? No_Record_of_Recent_Plague } {ClueList !? CS_letters}
				[Do you know where Cassandra is?] -> Do_you_know_where_Cassandra_is
			+ [(Nevermind)] {EndCon()} -> END #END
				- -> Talk_to_Athena
			
			= Can_you_help_me_test_this_potion
					#speaker.Sebastian
				So, you know about alchemy?
					#speaker.Athena
				Oh yes I do! I'm not as great as Cassandra but I can still help you with alchemy.
					#speaker.Sebastian
				Then can you help me with this potion? I want to make sure it's not poisonous.
					#speaker.Athena
				Sure, give me a moment.
				    #noSpeaker
				// Athena doing her alchemist thing. 
				She took the potion from Sebastian's hand, put it on the table nearby with some dust and mixer tools.
				Turns out the potion is just colored water.It does nothing good or bad.
				{ ClueList !? The_Potion_is_just_Colored_Water:
					#clue.The_Potion_is_just_Colored_Water
					~ GetClue(The_Potion_is_just_Colored_Water)
				}
					#speaker.Athena
				Wait this is a scam. Why would anyone do this?!
				Where did you get this potion from again?
					#speaker.Sebastian
				Well, this senile old man bought this potion from C.S.
				And the way they deliver the potion is suspicious.
					#speaker.Athena
				C.S. ? That name sounds familiar...
				I wish I know who they are. If you know something please tell me about it.
					#speaker.Sebastian
				Sure, I'm still working on finding C.S. anyway.
				(Why would C.S. do this... this is not good for the town.)
				(I should go find someone with closest connection to C.S. )
				(hmm...)
				(I think I should go find merchant for now.)
				#TimelineState_caravan_arrive
				#END
				-> END
 
			= Do_you_know_where_Cassandra_is
				// 	#noSpeaker
				// Sebastian went to the Witch's Nursery and asked Athena
					#speaker.Sebastian
				Is Cassandra here? I want to ask her something.
					#speaker.Athena
				I don't know where she is. But you can come inside. I think she will be back soon.
				// #knot.Talk_to_Athena.go_in_nursery
				// #knot.Talk_to_Athena.skip_to_cassandra_room
				// #timeline.go_in_nursery
				-> skip_to_cassandra_room
				// #END
				// -> END

				= go_in_nursery
					#noSpeaker
				Sebastian and Athena went inside the Witch's Nursery.
				//change scene to inside nursery
				// #knot.Talk_to_Athena.Inside_Nursery
				// #scene.Inside_Nursery
				// #spawn.4
				#knot.Talk_to_Athena.skip_to_cassandra_room
				#scene.Cassandra_Room
				#spawn.5
				#END
				-> END

			= Inside_Nursery
					#speaker.Athena
				You can sit back and relax here, I'll make some tea for you.
				//fade black

				#knot.Talk_to_Athena.athena_to_kitchen
				#timeline.athena_to_kitchen
				#END
				-> END

				= athena_to_kitchen
					#noSpeaker
				Athena walk away to the kitchen.
				// The kitchen will be locked
					#speaker.Sebastian
				(I should go and search this house while I can)
					#noSpeaker
				Sebastian investigate the house and went into a particular room.
				~ ChangeLocation(Inside_Nursery)

				= skip_to_cassandra_room
				#transition.open
					#noSpeaker
				Sebastian and Athena went inside the Witch's Nursery.	
				As Sebastian went inside the house. Athena went to the kitchen to get some tea for Sebastian.
				Sebastian took this chance to investigate further in the house.
				He found himself in a particular room.

				-> That_is_all
				// #scene.Cassandra_Room
				// #spawn.5
				// #END
				-> END
	=== Location_Forest ===
		{not Location_Forest} #cutscene.Wake_up
		-> END
		
	    == Talk_to_Lumberjack ==
			{ Clear_the_path:
				-> Thanks
			}
			{ About_the_axe:
				-> Clear_the_path
			}
			{ not Lost_axe: 
				-> Lost_axe
			}
			{ Lost_axe: 
				Hey there kid  #speaker.Lumberjack
			} 
			//Hey kid, have you found my axe?
				// + [When were you fainted?]-> When_were_you_fainted ->
				+ { Talk_to_Merchant } [Do you know about the merchant?]-> Do_you_know_about_the_merchant ->
				+ { EvidenceList ? Merchant_took_the_axe } [About the Axe...] -> About_the_axe ->
				+ [(Nevermind)] {EndCon()} -> END #END
				- -> Talk_to_Lumberjack
			//tutorial
				= Lost_axe
						#speaker.Lumberjack
					Hey, can you help me out?
					I got unconscious and when I woke up again. My axe is gone!
					I need the axe so I can clear this tree that blocks the way to our town.
					I also wrote my name, Alex, on my axe. 
						#noSpeaker
					{ ClueList !? Clue_01:
						#clue.Clue_01
						~ GetClue(Clue_01)
					}
					#END
					-> END
				= When_were_you_fainted
						#speaker.Lumberjack
					Not too long, I just got fainted this morning
						#noSpeaker
					{ ClueList !? Clue_03:
						#clue.Clue_03
						~ GetClue(Clue_03)
					}
					- -> Talk_to_Lumberjack
				= Do_you_know_about_the_merchant
						#speaker.Lumberjack
					That merchant? He has a habit of picking up someone's else belonging and sell them
						#noSpeaker
					{ ClueList !? Clue_02:
						#clue.Clue_02
						~ GetClue(Clue_02)
					}
					- -> Talk_to_Lumberjack
				= About_the_axe
						#speaker.Lumberjack
					It was with the merchant this whole time?
						#speaker.Lumberjack
					I'll go find him and take it back
					// run cutscene about Lumberjack taking back his axe

					#timeline.Lumberjack_go_to_the_Log
					#END 
					-> END
				= Clear_the_path
						#speaker.Lumberjack
					Alright, let's clear the path now
					#timeline.Lumberjack_clear_the_path
					#END 
					-> END
				= Thanks
						#speaker.Lumberjack
					Thanks for the help kid
					#END 
					-> END
			//chapter 1

		== Talk_to_Merchant ==
				#speaker.Merchant
			What do you want from me?
				//~ Conversation("Talk to Merchant")
				//Talking to Merchant.
				//Tutorial
				+ {Talk_to_Lumberjack} {not Talk_to_Lumberjack.About_the_axe} [Have you seen an axe?] -> Have_you_seen_an_axe ->
				// + {Talk_to_Lumberjack} {not Talk_to_Lumberjack.About_the_axe} [Were you there when the lumberjack fainted?] -> Were_you_there ->
				//chapter 1
				+ [What are you doing here?] -> What_are_you_doing_here ->
				+ { ClueList ? Merchant_Sells_the_Newspaper } {ClueList !? CS_Order_to_Forge_The_News}
					[I heard that you sells the Newspaper] -> I_heard_that_you_sells_the_Newspaper ->

				+ [(Nevermind)] {EndCon()} -> END #END
				- -> Talk_to_Merchant
			// Tutorial
				= Have_you_seen_an_axe
						#speaker.Merchant
					An axe? I found this one by the river far from here
						#noSpeaker
					{ ClueList !? Clue_04:
						#clue.Clue_04
						~ GetClue(Clue_04)
					}
					-> Talk_to_Merchant
				= Were_you_there
						#speaker.Merchant
					A lumberjack? Haven't see one today. I only got here this morning.
						#noSpeaker
					{ ClueList !? Clue_05:
						#clue.Clue_05
						~ GetClue(Clue_05)
					}
					-> Talk_to_Merchant
			// chapter 1
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
					#clue.CS_Order_to_Forge_The_News
					~ GetClue(CS_Order_to_Forge_The_News)
				}
					#noSpeaker
				{ ItemList !? Original_Newspaper:
					#item.Original_Newspaper
					~ GetItem(Original_Newspaper)
				}
				- ->->
 
		== Item_on_Sale ==
			{ Talk_to_Lumberjack.About_the_axe:
				-> no_axe
			}
			{ Talk_to_Lumberjack:
				-> Look_closer_at_the_axe
			}
				#speaker.Sebastian
			(Some items are put on sale here)
			// + { Talk_to_Lumberjack } [Look closer at the axe] -> Look_closer_at_the_axe
			#END
			-> END

			= Look_closer_at_the_axe
					#speaker.Sebastian
				(Wait, this axe has some letters carved on it)
				(Alex... so this is the axe to look for)
					#noSpeaker
				{ ClueList !? Clue_06:
					#clue.Clue_06
					~ GetClue(Clue_06)
				}
				#END
				-> END

			= no_axe
					#speaker.Sebastian
				(The axe is returned to the right owner now)
				#END
				-> END

		== Talk_to_Caravan ==
			// ~Conversation("Talk to Caravan")
				#speaker.Sebastian
			// What's going on here? I don't think you guys were staying here before.
			Hello, I didn't see you here before. What's going on?
				#speaker.Caravan
			// Oh hello young man, we just arrived here.
			Oh hello young man, I just traveled here together with my families
			We abandoned our hometown because of prophency about the plague. So we have to flee to a safe place here.
				#speaker.Sebastian
			Plague? I didn't hear anything about it.
			Are you going anywhere else?
				#speaker.Caravan
			Well, we don't have anywhere else to go.
			Miss Cassandra allowed us to stay just outside the town.
				#speaker.Sebastian
			About the prophency... Where did you get it from?
				#speaker.Caravan
			Miss Cassandra gave us a prophency that there would be plague in our town.
			Miss Cassandra is such a great leader. She's so kind to warn us and let us stay safe in her town.
			{ ClueList !? Cassandra_Prophesied_The_Plague:
					#clue.Cassandra_Prophesied_The_Plague
					~ GetClue(Cassandra_Prophesied_The_Plague)
			}
				#speaker.Sebastian
			(If I want to learn about the plague. I should find some records at the meeting hall)
			#END
			-> END
	=== Location_Inside_Nursery ===
		= The_kitchen
				#noSpeaker
			The kitchen is locked.
			- -> DONE
			// The kitchen is locked and you can hear Athena hummed along with some soft noise.
		
		= Cassandra_Room
				#noSpeaker
			Sebastian searching inside Cassandra's room.
				+ [Look on the table] -> Look_on_The_Table
		
		= Look_on_The_Table
				#noSpeaker
			Sebastian look at Cassandra's table.
				#speaker.Sebastian
			(What is this letter?)
			(Change the headlines, Selling potion, Working with thiefs? Why is it here?)
				#noSpeaker
			The letter is signed as C.S.
				#speaker.Sebastian
			(C.S. so... C.S. stands for Cassandra?)
				#noSpeaker
			Sebastian collected the letters.
			// ^ get clues and items functions
			{ ClueList !? CS_letters:
				#clue.CS_letters
				~ GetClue(CS_letters)
			}
			- -> Cutscene_Cassandra_Return
	//Chapter 3 stuff
		=== The_Reveal ===
				#noSpeaker
			Sebastian walked to the center of the plaza.
				#speaker.Sebastian
			Attention everyone, I'm here to let everyone know that you have been living with lies and fake news all this time.
			
			\---------
			There's supposed to be some choices to choose again to answer the villagers
			If player get most of them correct, the villagers will believe that Sebastian is telling the truth.
			\---------
			- -> Exposed_Cassandra

		=== Exposed_Cassandra ===
			//part 1
				#speaker.Villager
			Cassandra! Why did you do this?!
				#speaker.OldMan
			You sold me a glass of water all this time?
				#speaker.Caravan
			We have to abandoned our town just to let the thiefs takes everything!
				#speaker.Villager
			We don't need this fake leader in our town!
				#noSpeaker
			The villagers comes towards Cassandra with ill intents.
			//part 2
				#speaker.Cassandra
			No, why are you listening to this child? I did everything for you people all this time.
				#speaker.Villager
			What you did is everything that makes our life worse! You just want to leech our work!
				#noSpeaker
			The villagers chase Cassandra out of the Sanctuary.
			- -> Confessed_Cassandra

		=== Confessed_Cassandra ===
			#noSpeaker
		Sebastian followed Cassandra outside the town.
			#speaker.Cassandra
		I'll tell you, Sebastian. As a reward for being able to expose me.
			#speaker.Sebastian
		Well, I already read it from your letters. You're also the one who captured my father.
		So, where is he now?
			#speaker.Cassandra
		Alright... Listen closely, your father...
		Is already dead.
			#speaker.Sebastian
		...
		You're not done with lying?
			#speaker.Cassandra
		Ha, how funny, now you're the one who wants to hear some lies instead.
		If you don't believe me, head toward the forest to the east of my Nursery. 
		There's a hidden shack there that I use to do my ritual.
			#speaker.Sebastian
		Ritual?
			#speaker.Cassandra
		Yes Sebastian, ritual, the dark ones.
		I would love to tell you more but you must be dying to find your father now.
			#speaker.Sebastian
		... You better not be scheming again.

	//Sorry this is all I got for now
	=== That_is_all ===
			#transition.open
				#noSpeaker
			This is the end of the story... for now.
			If you wish to, you can still walk and look around the game.
			Thank you for taking your time trying out the game.
			#transition.close
			#END
			-> END
-> END