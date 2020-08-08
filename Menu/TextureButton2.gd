extends TextureButton


func _ready():
	connect("pressed", self, "toggleSounds")


func toggleSounds():
	SoundManager.ToggleSound()