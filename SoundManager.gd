extends AudioStreamPlayer

var menuSong = preload("res://Sounds/menu.wav")
var gameSong = preload("res://Sounds/game.wav")
var endSong = preload("res://Sounds/end.wav")
var loseSong = preload("res://Sounds/lose.wav")





func PlaySong(song):
	match song:
		"menu":
			stream = menuSong
			playing = false;
			playing = true;
		"game":
			stream = gameSong
			playing = false;
			playing = true;
		"end":
			stream = endSong
			playing = false;
			playing = true;
		"lose":
			stream = loseSong
			playing = false;
			playing = true;

func PlayAffect(affect):
	var newSoundNode = AudioStreamPlayer.new()
	newSoundNode.stream = load(affect)
	newSoundNode.bus = "SoundAffects"
	newSoundNode.autoplay = true
	add_child(newSoundNode)



var issound = true
func ToggleSound():
	if issound:
		issound = false
		AudioServer.set_bus_mute(0, true)
	else:
		issound = true
		AudioServer.set_bus_mute(0, false)
