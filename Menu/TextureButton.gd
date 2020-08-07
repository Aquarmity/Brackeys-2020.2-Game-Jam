extends TextureButton


onready var game_scene = preload("res://game.tscn")

func _on_TextureButton_pressed():
	# warning-ignore:return_value_discarded
	get_tree().change_scene_to(game_scene)