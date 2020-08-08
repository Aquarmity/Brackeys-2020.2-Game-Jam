extends Label


var seen_already = false

func _input(event):
	if (event.is_action_pressed("open_menu")):
		if (seen_already == false):
			seen_already = true
		else:
			queue_free()